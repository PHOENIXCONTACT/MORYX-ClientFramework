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

        public static readonly DependencyProperty TextFormatProperty = DependencyProperty.RegisterAttached(
            "TextFormat", typeof(EddieTextFormat), typeof(Eddie), new PropertyMetadata(EddieTextFormat.Unset));

        public static void SetTextFormat(DependencyObject element, EddieTextFormat value)
        {
            element.SetValue(TextFormatProperty, value);
        }

        public static EddieTextFormat GetTextFormat(DependencyObject element)
        {
            return (EddieTextFormat)element.GetValue(TextFormatProperty);
        }

        #endregion

        #region Icon

        public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached(
           "Icon", typeof(CommonShapeType), typeof(Eddie), new PropertyMetadata(default(CommonShapeType), IconChanged));

        public static void SetIcon(DependencyObject element, CommonShapeType value)
        {
            element.SetValue(IconProperty, value);
        }

        public static CommonShapeType GetIcon(DependencyObject element)
        {
            return (CommonShapeType)element.GetValue(IconProperty);
        }

        private static void IconChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var shape = ShapeFactory.GetShapeGeometry((CommonShapeType)args.NewValue);

            var path = d as Path;
            if (path != null)
            {
                path.Data = shape;
                path.Stretch = Stretch.Uniform;
            }
        }

        #endregion
    }
}