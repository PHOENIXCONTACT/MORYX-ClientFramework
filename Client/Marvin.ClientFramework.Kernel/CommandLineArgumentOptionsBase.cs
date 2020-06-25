using CommandLine;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Class for the command line parameters.
    /// </summary>
    public class CommandLineArgumentOptionsBase
    {
        /// <summary>
        /// Gets or sets the configuration folder.
        /// </summary>
        [Option('c', "configFolder", HelpText = "Sets the config target folder.", Default = KernelConstants.CONFIGS_DIR)]
        public string ConfigFolder { get; set; }
    }
}
