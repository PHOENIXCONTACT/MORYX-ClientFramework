using System;
using System.Windows.Input;

namespace Marvin.ClientFramework.HeartOfLead
{
    /// <summary>
    /// Main kernel class to load the overall client framework. 
    /// Will instanciate the container and will start the whole lifecycle.
    /// </summary>
    public class Program : Kernel.HeartOfLead<HeartOfLeadArgumentOptions>
    {
        #region Main and ctor

        /// <summary>
        /// Main start point of this application
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            new Program(args).Start();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        public Program(string[] args) : base(args)
        {
        }

        #endregion

        /// <inheritdoc />
        protected override void OnApplicationConfigured()
        {
            base.OnApplicationConfigured();

            //Start configurator, if wanted
            if (CommandLineOptions.StartConfigurator || AppConfig.OpenConfigWithControl && (Keyboard.Modifiers & ModifierKeys.Control) > 0)
                AppConfig.RunMode = KernelConstants.CONFIG_RUNMODE;
        }
    }
}