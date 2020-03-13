// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Interface for a loader view
    /// </summary>
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
        string AppName { get; set; }

        /// <summary>
        /// Indicates an error error.
        /// </summary>
        void IndicateError();
    }
}
