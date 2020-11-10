using System.Collections;
using System.Windows.Data;

namespace Moryx.WpfToolkit
{
    /// <summary>
    /// Helper extensions for <see cref="IValueConverter"/>
    /// </summary>
    public static class ConverterExtension
    {
        /// <summary>
        /// Tries to convert the input value to a boolean typed value
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AsBool(this IValueConverter converter, object value)
        {
            if (value is bool)
            {
                return (bool) value;
            }

            var s = value as string;
            if(s != null)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    if (s.ToLower() == "true")
                    {
                        return true;
                    }
                    
                    if (s.ToLower() == "false")
                    {
                        return false;
                    }
                    
                    return true;
                }

                return false;
            }

            var collection = value as ICollection;
            if (collection != null)
            {
                return collection.Count > 0;
            }

            if (value == null)
            {
                return false;
            }

            return true;
        }
    }
}
