// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace Moryx.WpfToolkit
{
    /// <summary>
    /// Implementation of <see cref="ResourceDictionary"/> which will handle the assembly and sourcepath
    /// </summary>
    public class VersionResourceDictionary : ResourceDictionary, ISupportInitialize
    {
        private string _assemblyName;
        private int _initializingCount;
        private string _sourcePath;


        /// <summary>
        /// Initializes a new instance of the <see cref="VersionResourceDictionary"/> class.
        /// </summary>
        public VersionResourceDictionary()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionResourceDictionary"/> class.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="sourcePath">The source path.</param>
        public VersionResourceDictionary(string assemblyName, string sourcePath)
        {
            ((ISupportInitialize) this).BeginInit();
            AssemblyName = assemblyName;
            SourcePath = sourcePath;
            ((ISupportInitialize) this).EndInit();
        }

        /// <summary>
        /// The name of the assembly for the <see cref="SourcePath"/>
        /// </summary>
        public string AssemblyName
        {
            get { return _assemblyName; }
            set
            {
                EnsureInitialization();
                _assemblyName = value;
            }
        }

        /// <summary>
        /// The path of the source within the assembly
        /// </summary>
        public string SourcePath
        {
            get { return _sourcePath; }
            set
            {
                EnsureInitialization();
                _sourcePath = value;
            }
        }

        void ISupportInitialize.BeginInit()
        {
            base.BeginInit();
            _initializingCount++;
        }

        void ISupportInitialize.EndInit()
        {
            _initializingCount--;
            Debug.Assert(_initializingCount >= 0);

            if (_initializingCount <= 0)
            {
                if (Source != null)
                    throw new InvalidOperationException(
                        "Source property cannot be initialized on the VersionResourceDictionary");

                if (string.IsNullOrEmpty(AssemblyName) || string.IsNullOrEmpty(SourcePath))
                    throw new InvalidOperationException("AssemblyName and SourcePath must be set during initialization");

                //Using an absolute path is necessary in VS2015 for themes different than Windows 8.
                string uriStr = string.Format(@"pack://application:,,,/{0};component/{1}", AssemblyName, SourcePath);
                Source = new Uri(uriStr, UriKind.Absolute);
            }

            EndInit();
        }

        /// <summary>
        /// Ensures the initialization.
        /// </summary>
        private void EnsureInitialization()
        {
            if (_initializingCount <= 0)
                throw new InvalidOperationException(
                    "VersionResourceDictionary properties can only be set while initializing");
        }
    }
}
