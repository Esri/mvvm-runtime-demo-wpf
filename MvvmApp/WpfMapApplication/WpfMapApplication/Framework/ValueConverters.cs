using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace DevSummitDemo
{
  /// <summary>
  /// Returns Visibility.Visible if value is true.
  /// </summary>
  public class BoolToVisibilityConverter : MarkupExtension, IValueConverter
  {
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return this;
    }

    #region IValueConverter Members

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (parameter == null)
        return (bool)value ? Visibility.Visible : Visibility.Collapsed;
      return (bool)value ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type typeName, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
    #endregion
  }

  /// <summary>
  /// Returns Visibility.Visible if string value is null or empty.
  /// </summary>
  public class IsNullOrEmptyToVisibilityConverter : MarkupExtension, IValueConverter
  {
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return this;
    }

    #region IValueConverter Members

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (parameter == null)
        return value is string && !string.IsNullOrEmpty(value.ToString()) ? Visibility.Collapsed : Visibility.Visible;

      return value is string && !string.IsNullOrEmpty((string)value) ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    #endregion
  }

  /// <summary>
  /// Converts a string value to an integer value.
  /// </summary>
  public class StringToIntegerConverter : MarkupExtension, IValueConverter
  {
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return this;
    }

    #region IValueConverter Members

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      int result = 0;
      if (Int32.TryParse((string)value, out result))
        return result;

      return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    #endregion
  }

}
