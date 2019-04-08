using System;
using System.Collections.Generic;
using C4I;
using Caliburn.Micro;

namespace Marvin.Controls.Demo.ViewModels
{
    public class IconsViewModel : Screen
    {
        public override string DisplayName => "Icons";

        public ICollection<string> ShapeTypes { get; }

        public IconsViewModel()
        {
            ShapeTypes = new List<string>(Enum.GetNames(typeof(CommonShapeType)));
        }
    }
}
