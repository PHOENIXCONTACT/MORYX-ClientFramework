namespace Marvin.ClientFramework.Kernel
{
    public interface ILoaderView
    {
        /// <summary>
        /// Gets or sets the maximum progress.
        /// </summary>
        int Maximum { get; set; }

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        int Value { get; set; }

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        string StatusMessage { get; set; }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        string AppnName { get; set; }

        /// <summary>
        /// Indicates an error error.
        /// </summary>
        void IndicateError();
    }
}