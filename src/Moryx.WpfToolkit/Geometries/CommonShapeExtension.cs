// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Windows.Markup;

namespace Moryx.WpfToolkit
{
    /// <inheritdoc />
    /// <summary>
    /// MarkupExtension to support <see cref="T:Moryx.WpfToolkit.CommonShapeType" /> within XAML
    /// </summary>
    public class CommonShapeExtension : MarkupExtension
    {
        /// <summary>
        /// Selected <see cref="CommonShapeType"/>
        /// </summary>
        [ConstructorArgument("shapeType")]
        public CommonShapeType ShapeType { get; set; }

        /// <summary>
        /// Default constructor to create the shape extension
        /// </summary>
        public CommonShapeExtension()
        {
        }

        /// <summary>
        /// Constructor to create the shape extension with an initial shape type
        /// </summary>
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
