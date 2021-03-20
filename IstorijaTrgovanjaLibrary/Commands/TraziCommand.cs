using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace IstorijaTrgovanjaLibrary.Commands
{
    public class TraziCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action _execute;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public TraziCommand(Action execute)
        {
            this._execute = execute;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}
