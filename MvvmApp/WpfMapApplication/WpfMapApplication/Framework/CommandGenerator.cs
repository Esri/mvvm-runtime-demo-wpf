using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Reflection;
using System.Windows.Markup;

namespace DevSummitDemo
{
  public class CommandGenerator : MarkupExtension, IValueConverter
  {
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return this;
    }

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (value == null)
        return null;
      if (!(value is ViewModel))
        throw new ArgumentException("Value is not a ViewModel class.");

      string methodName = parameter.ToString();
      if (string.IsNullOrEmpty(methodName))
        return null;

      var viewType = value.GetType();
      var methods = viewType.GetMethods();
      var properties = viewType.GetProperties();

      foreach (var method in methods)
      {
        if (methodName != method.Name)
          continue;

        var foundProperty = properties.FirstOrDefault(x => x.Name == "Can" + method.Name);

        return new ReflectiveCommand(value as ViewModel, method, foundProperty);
      }

      throw new InvalidOperationException("Target method not found '" + methodName + "'.");
    }

    //public object ConvertBack(object value, string typeName, object parameter, string language)
    public object ConvertBack(object value, Type typeName, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
