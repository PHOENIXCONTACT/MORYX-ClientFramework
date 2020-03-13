// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace C4I
{
    /// <summary>
    /// Adapts a DependencyObject to provide methods required for generate
    /// a Linq To Tree API
    /// </summary>
    public class VisualTreeAdapter : ILinqTree<DependencyObject>
    {
        private readonly DependencyObject _item;

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualTreeAdapter"/> class.
        /// </summary>
        public VisualTreeAdapter(DependencyObject item)
        {
            _item = item;
        }

        /// <summary>
        /// Returns all children of the given item
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DependencyObject> Children()
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(_item);
            for (var i = 0; i < childrenCount; i++)
            {
                yield return VisualTreeHelper.GetChild(_item, i);
            }
        }

        /// <summary>
        /// Returns the parent of the given item
        /// </summary>
        public DependencyObject Parent => VisualTreeHelper.GetParent(_item);
    }

    /// <summary>
    /// Defines an interface that must be implemented to generate the LinqToTree methods
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILinqTree<out T> where T : DependencyObject
    {
        /// <summary>
        /// Children of given T
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Children();

        /// <summary>
        /// Parent of given T
        /// </summary>
        T Parent { get; }
    }

    /// <summary>
    /// Extensions for the visual tree
    /// </summary>
    public static class VisualTreeExtensions
    {
        /// <summary>
        /// Returns a collection of descendant elements.
        /// </summary>
        public static IEnumerable<DependencyObject> Descendants(this DependencyObject item)
        {
            ILinqTree<DependencyObject> adapter = new VisualTreeAdapter(item);
            foreach (var child in adapter.Children())
            {
                yield return child;

                foreach (var grandChild in child.Descendants())
                {
                    yield return grandChild;
                }
            }
        }

        /// <summary>
        /// Returns a collection containing this element and all descendant elements.
        /// </summary>
        public static IEnumerable<DependencyObject> DescendantsAndSelf(this DependencyObject item)
        {
            yield return item;

            foreach (var child in item.Descendants())
            {
                yield return child;
            }
        }

        /// <summary>
        /// Returns a collection of ancestor elements.
        /// </summary>
        public static IEnumerable<DependencyObject> Ancestors(this DependencyObject item)
        {
            ILinqTree<DependencyObject> adapter = new VisualTreeAdapter(item);

            var parent = adapter.Parent;
            while (parent != null)
            {
                yield return parent;
                adapter = new VisualTreeAdapter(parent);
                parent = adapter.Parent;
            }
        }

        /// <summary>
        /// Returns a collection containing this element and all ancestor elements.
        /// </summary>
        public static IEnumerable<DependencyObject> AncestorsAndSelf(this DependencyObject item)
        {
            yield return item;

            foreach (var ancestor in item.Ancestors())
            {
                yield return ancestor;
            }
        }

        /// <summary>
        /// Returns a collection of child elements.
        /// </summary>
        public static IEnumerable<DependencyObject> Elements(this DependencyObject item)
        {
            ILinqTree<DependencyObject> adapter = new VisualTreeAdapter(item);
            foreach (var child in adapter.Children())
            {
                yield return child;
            }
        }

        /// <summary>
        /// Returns a collection of the sibling elements before this node, in document order.
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsBeforeSelf(this DependencyObject item)
        {
            if (item.Ancestors().FirstOrDefault() == null)
                yield break;
            foreach (var child in item.Ancestors().First().Elements())
            {
                if (child.Equals(item))
                    break;
                yield return child;
            }
        }

        /// <summary>
        /// Returns a collection of the after elements after this node, in document order.
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsAfterSelf(this DependencyObject item)
        {
            if (item.Ancestors().FirstOrDefault() == null)
                yield break;
            var afterSelf = false;
            foreach (var child in item.Ancestors().First().Elements())
            {
                if (afterSelf)
                    yield return child;

                if (child.Equals(item))
                    afterSelf = true;
            }
        }

        /// <summary>
        /// Returns a collection containing this element and all child elements.
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsAndSelf(this DependencyObject item)
        {
            yield return item;

            foreach (var child in item.Elements())
            {
                yield return child;
            }
        }

        /// <summary>
        /// Returns a collection of descendant elements which match the given type.
        /// </summary>
        public static IEnumerable<T> Descendants<T>(this DependencyObject item)
        {
            return item.Descendants().Where(i => i is T).Cast<T>();
        }

        /// <summary>
        /// Returns a collection of the sibling elements before this node, in document order
        /// which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsBeforeSelf<T>(this DependencyObject item)
        {
            return item.ElementsBeforeSelf().Where(i => i is T);
        }

        /// <summary>
        /// Returns a collection of the after elements after this node, in document order
        /// which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsAfterSelf<T>(this DependencyObject item)
        {
            return item.ElementsAfterSelf().Where(i => i is T);
        }

        /// <summary>
        /// Returns a collection containing this element and all descendant elements
        /// which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> DescendantsAndSelf<T>(this DependencyObject item)
        {
            return item.DescendantsAndSelf().Where(i => i is T);
        }

        /// <summary>
        /// Returns a collection of ancestor elements which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> Ancestors<T>(this DependencyObject item)
        {
            return item.Ancestors().Where(i => i is T);
        }

        /// <summary>
        /// Returns the next ancestor of type T
        /// </summary>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Ancestor<T>(this DependencyObject item) where T : DependencyObject
        {
            return item.Ancestors().FirstOrDefault(i => i is T) as T;
        }

        /// <summary>
        /// Returns a collection containing this element and all ancestor elements
        /// which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> AncestorsAndSelf<T>(this DependencyObject item)
        {
            return item.AncestorsAndSelf().Where(i => i is T);
        }

        /// <summary>
        /// Returns a collection of child elements which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> Elements<T>(this DependencyObject item)
        {
            return item.Elements().Where(i => i is T);
        }

        /// <summary>
        /// Returns a collection containing this element and all child elements.
        /// which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsAndSelf<T>(this DependencyObject item)
        {
            return item.ElementsAndSelf().Where(i => i is T);
        }
    }

    /// <summary>
    /// VisualTreeExtensions for enumerables
    /// </summary>
    public static class EnumerableVisualTreeExtensions
    {
        /// <summary>
        /// Applies the given function to each of the items in the supplied
        /// IEnumerable.
        /// </summary>
        private static IEnumerable<DependencyObject> DrillDown(this IEnumerable<DependencyObject> items,
            Func<DependencyObject, IEnumerable<DependencyObject>> function)
        {
            return items.SelectMany(function);
        }

        /// <summary>
        /// Applies the given function to each of the items in the supplied
        /// IEnumerable, which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> DrillDown<T>(this IEnumerable<DependencyObject> items,
            Func<DependencyObject, IEnumerable<DependencyObject>> function)
            where T : DependencyObject
        {
            return items.SelectMany(function).OfType<T>();
        }

        /// <summary>
        /// Returns a collection of descendant elements.
        /// </summary>
        public static IEnumerable<DependencyObject> Descendants(this IEnumerable<DependencyObject> items)
        {
            return items.DrillDown(i => i.Descendants());
        }

        /// <summary>
        /// Returns a collection containing this element and all descendant elements.
        /// </summary>
        public static IEnumerable<DependencyObject> DescendantsAndSelf(this IEnumerable<DependencyObject> items)
        {
            return items.DrillDown(i => i.DescendantsAndSelf());
        }

        /// <summary>
        /// Returns a collection of ancestor elements.
        /// </summary>
        public static IEnumerable<DependencyObject> Ancestors(this IEnumerable<DependencyObject> items)
        {
            return items.DrillDown(i => i.Ancestors());
        }

        /// <summary>
        /// Returns a collection containing this element and all ancestor elements.
        /// </summary>
        public static IEnumerable<DependencyObject> AncestorsAndSelf(this IEnumerable<DependencyObject> items)
        {
            return items.DrillDown(i => i.AncestorsAndSelf());
        }

        /// <summary>
        /// Returns a collection of child elements.
        /// </summary>
        public static IEnumerable<DependencyObject> Elements(this IEnumerable<DependencyObject> items)
        {
            return items.DrillDown(i => i.Elements());
        }

        /// <summary>
        /// Returns a collection containing this element and all child elements.
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsAndSelf(this IEnumerable<DependencyObject> items)
        {
            return items.DrillDown(i => i.ElementsAndSelf());
        }

        /// <summary>
        /// Returns a collection of descendant elements which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> Descendants<T>(this IEnumerable<DependencyObject> items)
            where T : DependencyObject
        {
            return items.DrillDown<T>(i => i.Descendants());
        }

        /// <summary>
        /// Returns a collection containing this element and all descendant elements.
        /// which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> DescendantsAndSelf<T>(this IEnumerable<DependencyObject> items)
            where T : DependencyObject
        {
            return items.DrillDown<T>(i => i.DescendantsAndSelf());
        }

        /// <summary>
        /// Returns a collection of ancestor elements which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> Ancestors<T>(this IEnumerable<DependencyObject> items)
            where T : DependencyObject
        {
            return items.DrillDown<T>(i => i.Ancestors());
        }

        /// <summary>
        /// Returns a collection containing this element and all ancestor elements.
        /// which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> AncestorsAndSelf<T>(this IEnumerable<DependencyObject> items)
            where T : DependencyObject
        {
            return items.DrillDown<T>(i => i.AncestorsAndSelf());
        }

        /// <summary>
        /// Returns a collection of child elements which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> Elements<T>(this IEnumerable<DependencyObject> items)
            where T : DependencyObject
        {
            return items.DrillDown<T>(i => i.Elements());
        }

        /// <summary>
        /// Returns a collection containing this element and all child elements.
        /// which match the given type.
        /// </summary>
        public static IEnumerable<DependencyObject> ElementsAndSelf<T>(this IEnumerable<DependencyObject> items)
            where T : DependencyObject
        {
            return items.DrillDown<T>(i => i.ElementsAndSelf());
        }
    }
}
