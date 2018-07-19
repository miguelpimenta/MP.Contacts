using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MP.Contacts.Support
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> action;

        public RelayCommand(Action<object> action)
        {
            this.action = action;
        }

        #region ICommand

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            action(parameter);
        }

        #endregion ICommand
    }
}