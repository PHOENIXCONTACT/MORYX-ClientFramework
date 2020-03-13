// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// DTO for a failed download from an <see cref="Uri"/>
    /// </summary>
    public class FailedDownload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FailedDownload"/> class.
        /// </summary>
        /// <param name="uri">The URI of the file to download</param>
        /// <param name="exception">The thrown exception while downloading</param>
        public FailedDownload(Uri uri, Exception exception)
        {
            Uri = uri;
            Error = exception;
        }

        /// <summary>
        /// The URI of the file to download
        /// </summary>
        public Uri Uri { get; set; }
        
        /// <summary>
        /// The thrown exception while downloading
        /// </summary>
        public Exception Error { get; set; }
    }
}
