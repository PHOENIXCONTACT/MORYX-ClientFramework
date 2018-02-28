using System;
using Marvin.ClientFramework.Kernel;

namespace Marvin.ClientFramework.Start
{
    /// <summary>
    /// Main kernel class to load the overall client framework. 
    /// Will instanciate the container and will start the whole lifecycle.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main start point of this application
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            var hol = new HeartOfLead(args);
            hol.Initialize();

            // Add style extension
            hol.OverrideDefaultStyle(new Uri("pack://application:,,,/HeartOfLead;component/CustomTheme.xaml",
                UriKind.RelativeOrAbsolute));

            // Start Application
            hol.Start();
        }
    }
}