using System.Windows.Media;
using C4I;
using Marvin.Container;

namespace Marvin.ClientFramework.Tests.Playground
{
    [Plugin(LifeCycle.Singleton, typeof(IModuleIcon))]
    public class ModuleIcon : IModuleIcon
    {
        public Geometry Icon
        {
            get { return ShapeFactory.GetShapeGeometry(CommonShapeType.Cloud); }
        }
    }
}