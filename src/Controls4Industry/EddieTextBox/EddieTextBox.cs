using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace C4I
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
            "Watermark", typeof (string), typeof (EddieTextBox), new PropertyMetadata(default(string)));

        /// <summary>
        /// Sets or gets the watermark to be shown
        /// </summary>
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

        /// <summary>
        /// DependencyProperty LockIcon type of <see cref="CommonShapeType"/> which sets the lock icon on the button
        /// </summary>
        public static readonly DependencyProperty LockIconProperty = DependencyProperty.Register(
            "LockIcon", typeof(CommonShapeType), typeof(EddieTextBox), new PropertyMetadata(CommonShapeType.Lock, LockIconChanged));

        private static void LockIconChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            SetLockPath(dependencyObject as EddieTextBox);
        }

        /// <summary>
        /// LockIcon type of <see cref="CommonShapeType"/> which sets the lock icon on the button
        /// </summary>
        public CommonShapeType LockIcon
        {
            get { return (CommonShapeType)GetValue(LockIconProperty); }
            set { SetValue(LockIconProperty, value); }
        }

        /// <summary>
        /// DependencyProperty alternative to the property <see cref="LockIcon"/> which uses a geometry to draw the path on the box
        /// </summary>
        public static readonly DependencyProperty LockIconPathProperty = DependencyProperty.Register(
            "LockIconPath", typeof(Geometry), typeof(EddieTextBox), new PropertyMetadata(default(Geometry), LockIconPathChanged));

        private static void LockIconPathChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            SetLockPath(dependencyObject as EddieTextBox);
        }

        /// <summary>
        /// Alternative to the property <see cref="LockIcon"/> which uses a geometry to draw the path on the box
        /// </summary>
        public Geometry LockIconPath
        {
            get { return (Geometry)GetValue(LockIconPathProperty); }
            set { SetValue(LockIconPathProperty, value); }
        }

        ///
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            SetDefaultPath(this);
            SetLockPath(this);
        }

        private static void SetDefaultPath(EddieTextBox box)
        {
            if (box.IconPath == null)
            {
                box.IconPath = CommonShapeFactory.GetShapeGeometry(box.Icon);
            }
        }

        private static void SetLockPath(EddieTextBox box)
        {
            if (box.LockIconPath == null)
            {
                box.LockIconPath = CommonShapeFactory.GetShapeGeometry(box.LockIcon);
            }
        }
    }
}