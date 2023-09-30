using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Results.Loggings;
using ResultSample.VisualStudioImages;

namespace ResultSample
{
    [ValueConversion(typeof(Severity), typeof(Uri))]
    internal class LogSeverityImageUriConverter : IValueConverter
    {
        #region IValueConverter

        /// <inheritdoc />
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var severity = (Severity)value;
            return ConvertServerityToIconImage(severity) ?? DependencyProperty.UnsetValue;
        }

        /// <inheritdoc />
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        #endregion

        internal Uri ConvertServerityToIconImage(Severity severity)
        {
            switch (severity)
            {
                case Severity.Fatal:
                case Severity.Alert:
                case Severity.Error:
                    return VSImageUris.StatusError;

                case Severity.Warning:
                    return VSImageUris.StatusWarning;

                case Severity.Information:
                    return VSImageUris.StatusInformation;

                default:
                    return null;
            }
        }
    }
}
