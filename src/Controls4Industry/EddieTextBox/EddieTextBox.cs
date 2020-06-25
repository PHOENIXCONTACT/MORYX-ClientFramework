using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace C4I
{
    public class EddieTextBox : TextBox
    {
        static EddieTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieTextBox), new FrameworkPropertyMetadata(typeof(EddieTextBox)));
        }

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(
            "Watermark", typeof (string), typeof (EddieTextBox), new PropertyMetadata(default(string)));

        public string Watermark
        {
            get { return (string) GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        /// <summary>
        /// DependencyProperty Icon type of <see cref="CommonShapeType"/> which sets the icon on the button
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(CommonShapeType), typeof(EddieTextBox), new PropertyMetadata(CommonShapeType.Pencil, IconChanged));

        private static void IconChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            SetDefaultPath(dependencyObject as EddieTextBox);
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
            "IconPath", typeof(Geometry), typeof(EddieTextBox), new PropertyMetadata(default(Geometry), IconPathChanged));

        private static void IconPathChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            SetDefaultPath(dependencyObject as EddieTextBox);
        }

        /// <summary>
        /// Alternative to the property <see cref="Icon"/> which uses a geometry to draw the path on the button
        /// </summary>
        public Geometry IconPath
        {
            get { return (Geometry)GetValue(IconPathProperty); }
            set { SetValue(IconPathProperty, value); }
        }

        ///
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            SetDefaultPath(this);
        }

        private static void SetDefaultPath(EddieTextBox box)
        {
            if (box.IconPath == null)
            {
                box.IconPath = ShapeFactory.GetShapeGeometry(box.Icon);
            }
            else
            {
                box.IconPath = box.IconPath;
            }
        }
    }
}