using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    /// <summary>
    /// Behavior attached propertis for the <see cref="TreeView"/> or <see cref="EddieTreeView"/>
    /// </summary>
    public class TreeViewSelectedItemBehaviour
    {
        /// <summary>
        /// Attached property for the view models selected tree item
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.RegisterAttached("SelectedItem", typeof(object),
                typeof(TreeViewSelectedItemBehaviour), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TreeViewSelectedItemChanged));

        /// <summary>
        /// Gets the selected item.
        /// </summary>
        public static object GetSelectedItem(DependencyObject dependencyObject)
        {
            return dependencyObject.GetValue(SelectedItemProperty);
        }

        /// <summary>
        /// Sets the selected item.
        /// </summary>
        public static void SetSelectedItem(DependencyObject dependencyObject, object value)
        {
            dependencyObject.SetValue(SelectedItemProperty, value);
        }

        /// <summary>
        /// This is the handler for when our new property's value changes
        /// When our property is set to a non null value we need to add an event handler for the TreeView's SelectedItemChanged event
        /// </summary>
        private static void TreeViewSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tv = d as TreeView;

            if (e.NewValue == null && e.OldValue != null)
            {
                tv.SelectedItemChanged -=
                    OnSelectedItemChanged;
            }
            else if (e.NewValue != null && e.OldValue == null)
            {
                tv.SelectedItemChanged +=
                    OnSelectedItemChanged;
            }
        }

        /// <summary>
        /// When TreeView.SelectedItemChanged fires, set our new property to the value
        /// </summary>
        private static void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SetSelectedItem((DependencyObject)sender, e.NewValue);
        }
    }
}