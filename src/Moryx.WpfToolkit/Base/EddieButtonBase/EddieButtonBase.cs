// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Moryx.WpfToolkit
{
    /// <summary>
    /// Base class for the eddie buttons which adds some dependency properties to the
    /// basic <see cref="Button"/>
    /// </summary>
    public abstract class EddieButtonBase : Button
    {
        /// <summary>
        /// Dependency property for the selected style of the button
        /// </summary>
        public static readonly DependencyProperty EddieStyleProperty = DependencyProperty.Register(
            "EddieStyle", typeof(EddieButtonStyle), typeof(EddieButtonBase), new PropertyMetadata(EddieButtonStyle.Green));

        /// <summary>
        /// Property for the selected style of the button
        /// </summary>
        public EddieButtonStyle EddieStyle
        {
            get => (EddieButtonStyle)GetValue(EddieStyleProperty);
            set => SetValue(EddieStyleProperty, value);
        }

        /// <summary>
        /// DependencyProperty Icon type of <see cref="Geometry"/> which sets the icon on the button
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon),
            typeof(Geometry), typeof(EddieButtonBase),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender), null);

        /// <summary>
        /// Geometry which sets the icon on the button
        /// </summary>
        public Geometry Icon
        {
            get => (Geometry)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
    }
}
