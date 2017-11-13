using CommandLine;

namespace Marvin.ClientFramework.HeartOfLead
{
    /// <summary>
    /// Class for the command line parameters.
    /// In this class all custom command line parameters are defined.
    /// </summary>
    public class ArgumentOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether [start configurator].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [start configurator]; otherwise, <c>false</c>.
        /// </value>
        [Option('v', "configurator", HelpText = "Starts the configuration run mode.")]
        public bool StartConfigurator { get; set; }

        /// <summary>
        /// Gets or sets the configuration folder.
        /// </summary>
        [Option('c', "configFolder", HelpText = "Sets the config target folder.", Default = KernelConstants.CONFIGS_DIR)]
        public string ConfigFolder { get; set; }
    }
}