using System;
using System.Windows.Markup;

namespace C4I
{
    /// <inheritdoc />
    /// <summary>
    /// MarkupExtension to support <see cref="T:C4I.MdiShapeType" /> within XAML
    /// </summary>
    public class MdiShapeExtension : MarkupExtension
    {
        /// <summary>
        /// Selected <see cref="MdiShapeType"/>
        /// </summary>
        public MdiShapeType ShapeType { get; set; }

        /// <summary>
        /// Default constructor to create the shape extension
        /// </summary>
        public MdiShapeExtension()
        {
        }

        /// <summary>
        /// Constructor to create the shape extension with an initial shape type
        /// </summary>
        public MdiShapeExtension(MdiShapeType shapeType)
        {
            ShapeType = shapeType;
        }

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return MdiShapeFactory.GetShapeGeometry(ShapeType);
        }
    }
}