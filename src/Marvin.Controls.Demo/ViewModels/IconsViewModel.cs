using System;
using System.Collections.Generic;
using System.Windows.Media;
using C4I;
using Caliburn.Micro;

namespace Marvin.Controls.Demo.ViewModels
{
    public class IconWrapper
    {
        public string Name { get; set; }

        public Geometry Geometry { get; set; }
    }

    public class IconsViewModel : Screen
    {
        public override string DisplayName => "Icons";

        public ICollection<IconWrapper> CommonShapes { get; } = new List<IconWrapper>();

        public ICollection<IconWrapper> MdiShapes { get; } = new List<IconWrapper>();

        public IconsViewModel()
        {
            foreach (CommonShapeType value in Enum.GetValues(typeof(CommonShapeType)))
            {
                CommonShapes.Add(new IconWrapper
                {
                    Name = value.ToString(),
                    Geometry = CommonShapeFactory.GetShapeGeometry(value)
                });
            }

            foreach (MdiShapeType value in Enum.GetValues(typeof(MdiShapeType)))
            {
                MdiShapes.Add(new IconWrapper
                {
                    Name = value.ToString(),
                    Geometry = MdiShapeFactory.GetShapeGeometry(value)
                });
            }
        }
    }
}
