using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace IstorijaTrgovanjaLibrary.Commands
{
    public class SearchCancelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action _execute;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public SearchCancelCommand(Action execute)
        {
            this._execute = execute;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}
