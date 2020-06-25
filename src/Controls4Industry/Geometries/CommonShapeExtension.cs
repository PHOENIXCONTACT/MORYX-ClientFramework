using System;
using System.Windows.Markup;

namespace C4I
{
    public class CommonShapeExtension : MarkupExtension
    {
        public CommonShapeType ShapeType { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ShapeFactory.GetShapeGeometry(ShapeType);
        }
    }
}