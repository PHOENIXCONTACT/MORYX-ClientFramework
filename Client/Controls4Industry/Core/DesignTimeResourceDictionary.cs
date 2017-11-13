using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace C4I
{
    /// <summary>
    /// Represents a resource dictionary which is used only in design time
    /// </summary>
    public class DesignTimeResourceDictionary : ResourceDictionary
    {
        /// <summary>
        /// GIndicating whether this instance is in design mode or not.
        /// </summary>
        public bool IsInDesignMode
        {
            get
            {
                return (bool)DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty,
                            typeof(DependencyObject)).Metadata.DefaultValue;
            }
        }

        /// <summary>
        /// Gets or sets the uniform resource identifier (URI) to load resources from.
        /// </summary>
        public new Uri Source
        {
            get { return base.Source; }
            set
            {
                if (!IsInDesignMode)
                    return;

                Debug.WriteLine("Setting Source = " + value);
                base.Source = value;
            }
        }
    }
}