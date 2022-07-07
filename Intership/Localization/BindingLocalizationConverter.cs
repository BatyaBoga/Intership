namespace Intership.Localization
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    /// <summary>
    /// Converter to get the value of a binding expression in localization.
    /// </summary>
    public class BindingLocalizationConverter : IMultiValueConverter
    {
        /// <summary>
        /// Convert.
        /// </summary>
        /// <param name="values">Values.</param>
        /// <param name="targetType">Type of target.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        /// <returns>Converted object.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
            {
                return null;
            }

            var key = System.Convert.ToString(values[1] ?? string.Empty);
            var value = LocalizationManager.Instance.Localize(key);
            if (value is string)
            {
                var args = (parameter as IEnumerable<object> ?? values.Skip(2)).ToArray();
                if (args.Length == 1 && !(args[0] is string) && args[0] is IEnumerable)
                {
                    args = ((IEnumerable)args[0]).Cast<object>().ToArray();
                }

                if (args.Any())
                {
                    return string.Format(value.ToString(), args);
                }
            }

            return value;
        }

        /// <summary>
        /// Not realized.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="targetTypes">Target types.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        /// <returns>Exception.</returns>
        /// <exception cref="NotSupportedException">NotSupportedException.</exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
