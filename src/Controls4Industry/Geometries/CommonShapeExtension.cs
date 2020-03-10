using System;
using System.Windows.Markup;

namespace C4I
{
    /// <inheritdoc />
    /// <summary>
    /// MarkupExtension to support <see cref="T:C4I.CommonShapeType" /> within XAML
    /// </summary>
    public class CommonShapeExtension : MarkupExtension
    {
        /// <summary>
        /// Selected <see cref="CommonShapeType"/>
        /// </summary>
        [ConstructorArgument("shapeType")]
        public CommonShapeType ShapeType { get; set; }

        public CommonShapeExtension()
        {

        }

        public CommonShapeExtension(CommonShapeType shapeType)
        {
            ShapeType = shapeType;
        }

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CommonShapeFactory.GetShapeGeometry(ShapeType);
        }
    }
}