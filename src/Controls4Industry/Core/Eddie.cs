// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace C4I
{
    /// <summary>
    /// Collection of attached properties and behaviors
    /// </summary>
    public class Eddie : DependencyObject
    {
        #region TextFormat

        /// <summary>
        /// Attached property to set the text format
        /// </summary>
        public static readonly DependencyProperty TextFormatProperty = DependencyProperty.RegisterAttached(
            "TextFormat", typeof(EddieTextFormat), typeof(Eddie), new PropertyMetadata(EddieTextFormat.Unset));

        /// <summary>
        /// Sets the text format attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTextFormat(DependencyObject element, EddieTextFormat value)
        {
            element.SetValue(TextFormatProperty, value);
        }

        /// <summary>
        /// Gets the text format attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static EddieTextFormat GetTextFormat(DependencyObject element)
        {
            return (EddieTextFormat)element.GetValue(TextFormatProperty);
        }

        #endregion

        #region Icon

        /// <summary>
        /// Attached property to set the icon
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached(
           "Icon", typeof(CommonShapeType), typeof(Eddie), new PropertyMetadata(default(CommonShapeType), IconChanged));

        /// <summary>
        /// Sets the icon attached property
        /// </summary>
        public static void SetIcon(DependencyObject element, CommonShapeType value)
        {
            element.SetValue(IconProperty, value);
        }

        /// <summary>
        /// Gets the icon attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static CommonShapeType GetIcon(DependencyObject element)
        {
            return (CommonShapeType)element.GetValue(IconProperty);
        }

        private static void IconChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var shape = CommonShapeFactory.GetShapeGeometry((CommonShapeType)args.NewValue);

            var path = d as Path;
            if (path == null)
                return;

            path.Data = shape;
            path.Stretch = Stretch.Uniform;
        }

        #endregion
    }
}
