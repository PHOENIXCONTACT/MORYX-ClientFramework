using System;
using System.Windows.Markup;

namespace C4I
{
    /// <inheritdoc />
    /// <summary>
    /// MarkupExtension to support <see cref="T:C4I.FaShapeType" /> within XAML
    /// </summary>
    public class FaShapeExtension : MarkupExtension
    {
        /// <summary>
        /// Selected <see cref="FaType"/>
        /// </summary>
        public FaShapeType ShapeType { get; set; }

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return FaShapeFactory.GetShapeGeometry(ShapeType);
        }
    }
}