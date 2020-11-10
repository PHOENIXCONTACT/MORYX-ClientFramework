// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Moryx.WpfToolkit
{
    /// <summary>
    /// EddieTextBox with watermark, icon and lock icon feature
    /// </summary>
    public class EddieTextBox : TextBox
    {
        static EddieTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieTextBox), new FrameworkPropertyMetadata(typeof(EddieTextBox)));
        }

        /// <summary>
        /// Watermark to be shown
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(
            nameof(Watermark), typeof (string), typeof (EddieTextBox), new PropertyMetadata(default(string)));

        /// <summary>
        /// Sets or gets the watermark to be shown
        /// </summary>
        public string Watermark
        {
            get => (string) GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }

        /// <summary>
        /// DependencyProperty Icon type of <see cref="Geometry"/> which sets the icon on the text box
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon), typeof(Geometry), typeof(EddieTextBox), new FrameworkPropertyMetadata( CommonShapeFactory.GetShapeGeometry(CommonShapeType.Pencil),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender), null);

        /// <summary>
        /// Visible icon geometry of the text box
        /// </summary>
        public Geometry Icon
        {
            get => (Geometry) GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// DependencyProperty Icon type of <see cref="Geometry"/> which sets the lock icon on the text box
        /// </summary>
        public static readonly DependencyProperty LockIconProperty = DependencyProperty.Register(
            nameof(LockIcon), typeof(Geometry), typeof(EddieTextBox), new FrameworkPropertyMetadata(CommonShapeFactory.GetShapeGeometry(CommonShapeType.Lock),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender), null);

        /// <summary>
        /// Visible lock icon geometry of the text box
        /// </summary>
        public Geometry LockIcon
        {
            get => (Geometry)GetValue(LockIconProperty);
            set => SetValue(LockIconProperty, value);
        }
    }
}
