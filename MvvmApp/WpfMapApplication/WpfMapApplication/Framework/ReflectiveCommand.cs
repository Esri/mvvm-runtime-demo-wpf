using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace DevSummitDemo
{
  public class ReflectiveCommand : ICommand
  {
    readonly PropertyInfo _canExecute;
    readonly MethodInfo _execute;
    readonly object _targetObject;

    /// <summary>
    /// Creates a new ReflectiveCommand for the targetObject using the execute method
    /// and canExecute property. The canExecute property can be omitted, in which case
    /// ICommand.CanExecute will return true.
    /// </summary>
    public ReflectiveCommand(object targetObject, MethodInfo execute, PropertyInfo canExecute)
    {
      _targetObject = targetObject;
      _execute = execute;
      _canExecute = canExecute;

      // if the targetObject implements INotifyPropertyChanged and a canExecute
      // property has been specified, then wire into the PropertyChanged event
      // so that we can call CanExecuteChanged when the property changes
      //
      var notifier = _targetObject as INotifyPropertyChanged;
      if (notifier != null && _canExecute != null)
      {
        notifier.PropertyChanged += (s, e) =>
        {
          if (e.PropertyName == _canExecute.Name)
            CanExecuteChanged(this, EventArgs.Empty);
        };
      }
    }

    public bool CanExecute(object parameter)
    {
      if (_canExecute != null)
        return (bool)_canExecute.GetValue(_targetObject, null);
      return true;
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
      if (parameter == null)
        _execute.Invoke(_targetObject, null);
      else
        _execute.Invoke(_targetObject, new object[] { parameter });
    }
  }
}
