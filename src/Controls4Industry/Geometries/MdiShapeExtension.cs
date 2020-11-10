// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Windows.Markup;

namespace Moryx.WpfToolkit
{
    /// <inheritdoc />
    /// <summary>
    /// MarkupExtension to support <see cref="T:Moryx.WpfToolkit.MdiShapeType" /> within XAML
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
