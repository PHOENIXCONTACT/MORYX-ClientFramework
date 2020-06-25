using System;
using System.Windows;
using System.Windows.Data;

namespace C4I
{
    public class GenericEnumConverter : IValueConverter
    {
        public bool Reverse { get; set; }

        /// <summary>
        /// Converts an enum value to a boolean or a Visibility value. 
        /// </summary>
        /// <param name="value">An enum value.</param>
        /// <param name="targetType">The target type of the conversion. Must be Boolean or Visibility.</param>
        /// <param name="parameter">The parameter, that will be used for comparison with the value.</param>
        /// <param name="culture">This parameter is ignored.</param>
        /// <returns>
        /// If the target type is boolean, true is returned if the parameter equals the value and false otherwise.
        /// If the target type is Visibility, Visible is returned if the parameter equals the value and Collapsed otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if value is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown if value is not an enum.</exception>
        /// <exception cref="ArgumentException">Thrown if target type is not of type bool or visibility.</exception>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (!(value is Enum))
            {
                throw new ArgumentException("Value must be an enum", "value");
            }

            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            bool result = false;

            if (parameter is Enum)
            {
                result = Enum.Equals(value, parameter);
            }

            if (parameter is string)
            {
                result = Enum.Equals(value, Enum.Parse(value.GetType(), parameter.ToString().Trim(), true));
            }

            if (parameter.GetType() == Enum.GetUnderlyingType(value.GetType()))
            {
                object lhs = System.Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()), null);
                result = lhs.Equals(parameter);
            }

            if (Reverse)
            {
                result = !result;
            }

            if (targetType == typeof(bool) || targetType == typeof(bool?))
            {
                return result;
            }
            else if (targetType == typeof(Visibility))
            {
                if (result)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else
            {
                throw new ArgumentException("Expected target type bool or visibiltiy", "targetType");
            }
        }

        /// <summary>
        /// Converts a boolean or a Visibility value to an enum value. 
        /// </summary>
        /// <param name="value">A boolean or visibility value.</param>
        /// <param name="targetType">The target type of the conversion. Must be an enum.</param>
        /// <param name="parameter">The parameter, that will be used for comparison with the value.</param>
        /// <param name="culture">This parameter is ignored.</param>
        /// <returns>
        /// If value is false, or Collapsed, then DependencyProperty.UnsetValue is returned. Otherwise
        /// the enum value matching the parameter is returned.</returns>
        /// <exception cref="ArgumentNullException">Thrown if value is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown if value is not of type bool or visibility.</exception>
        /// <exception cref="ArgumentException">Thrown if target type is not an enum.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if ((value is bool) || (value is bool?))
            {
                if ((bool?)value == false || (bool?)value == null)
                {
                    return DependencyProperty.UnsetValue;
                }
            }
            else if (value is Visibility)
            {
                if ((Visibility)value == Visibility.Collapsed)
                {
                    return DependencyProperty.UnsetValue;
                }
            }
            else
            {
                throw new ArgumentException("Value must be bool, bool? or Visibility", "value");
            }

            if (!targetType.IsSubclassOf(typeof(Enum)))
            {
                throw new ArgumentException("Epected target type Enum", "targetType");
            }

            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            if (parameter is Enum)
            {
                return parameter;
            }
            else if (parameter is string || parameter.GetType() == Enum.GetUnderlyingType(targetType))
            {
                return Enum.Parse(targetType, parameter.ToString().Trim(), false);
            }

            throw new ArgumentException("Unknown paramater type.");
        }
    }
}
