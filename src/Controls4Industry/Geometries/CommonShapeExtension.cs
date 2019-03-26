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
        public CommonShapeType ShapeType { get; set; }

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ShapeFactory.GetShapeGeometry(ShapeType);
        }
    }
}