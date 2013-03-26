using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DevSummitDemo
{
  /// <summary>
  /// Represents an object which can be observed via INotifyPropertyChanged. 
  /// </summary>
  public abstract class ViewModel : INotifyPropertyChanged
  {
    /// <summary>
    /// Helper method to set a property value, typically used in implementing a setter.
    /// Returns true if the property actually changed.
    /// </summary>
    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
    {
      if (object.Equals(storage, value)) return false;

      storage = value;
      NotifyPropertyChanged<T>(propertyName);
      return true;
    }

    /// <summary>
    /// Raises the PropertyChanged event for the specified property.
    /// </summary>
    public void NotifyPropertyChanged<T>([CallerMemberName] string propertyName = null)
    {
      PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Raises the PropertyChanged event for all properties of the object.
    /// </summary>
    public void NotifyPropertyChanged()
    {
      PropertyChanged(this, new PropertyChangedEventArgs(null));
    }
    
    /// <summary>
    /// Raises the PropertyChanged event for the specified property.
    /// </summary>
    public void NotifyPropertyChanged<T>(Expression<Func<T>> property)
    {
      var lambda = (LambdaExpression)property;

      MemberExpression memberExpression;
      if (lambda.Body is UnaryExpression)
      {
        var unaryExpression = (UnaryExpression)lambda.Body;
        memberExpression = (MemberExpression)unaryExpression.Operand;
      }
      else memberExpression = (MemberExpression)lambda.Body;

      NotifyPropertyChanged<T>(memberExpression.Member.Name);
    }

    /// <summary>
    /// Clears the registered event listeners for PropertyChanged.
    /// </summary>
    protected void ClearPropertyChangedListeners()
    {
      PropertyChanged = delegate { };
    }

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged = delegate { };

    #endregion
  }
}
