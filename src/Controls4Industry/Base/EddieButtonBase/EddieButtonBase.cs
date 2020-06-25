using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace C4I
{
    /// <summary>
    /// Bass class for the eddit buttons which adds some dependency properties to the 
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
            get { return (EddieButtonStyle)GetValue(EddieStyleProperty); }
            set { SetValue(EddieStyleProperty, value); }
        }

        /// <summary>
        /// DependencyProperty Icon type of <see cref="CommonShapeType"/> which sets the icon on the button
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(CommonShapeType), typeof(EddieButtonBase), new PropertyMetadata(CommonShapeType.Unset, IconChanged));

        private static void IconChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var control = d as EddieButtonBase;
            // ReSharper disable once PossibleNullReferenceException
            control.IconPath = ShapeFactory.GetShapeGeometry(control.Icon);
        }

        /// <summary>
        /// Icon type of <see cref="CommonShapeType"/> which sets the icon on the button
        /// </summary>
        public CommonShapeType Icon
        {
            get { return (CommonShapeType)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// DependencyProperty alternative to the property <see cref="Icon"/> which uses a geometry to draw the path on the button
        /// </summary>
        public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register(
            "IconPath", typeof(Geometry), typeof(EddieButtonBase), new PropertyMetadata(default(Geometry)));

        /// <summary>
        /// Alternative to the property <see cref="Icon"/> which uses a geometry to draw the path on the button
        /// </summary>
        public Geometry IconPath
        {
            get { return (Geometry)GetValue(IconPathProperty); }
            set { SetValue(IconPathProperty, value); }
        }
    }
}