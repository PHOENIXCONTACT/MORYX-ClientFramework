using Marvin.Container;

namespace Marvin.ClientFramework.Tests.HistoryWriter
{
    [PluginFactory]
    public interface IHistoryWorkspaceFactory
    {
        IModuleWorkspace CreateWorkspace(int level);

        void Destroy(IModuleWorkspace workspace);
    }
}