using System.Windows.Input;

namespace SharedLibrary.Mvvm
{
    public abstract class CommandBase : ICommand
    {
        /// <inheritdoc />
        public event EventHandler? CanExecuteChanged;

        /// <inheritdoc />
        bool ICommand.CanExecute(object? parameter)
        {
            return this.CanExecute(parameter);
        }

        /// <inheritdoc />
        void ICommand.Execute(object? parameter)
        {
            this.Execute(parameter);
        }

        protected virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        protected abstract void Execute(object? parameter);
        
        /// <summary>
        /// Raises <c>CanExecutedChanged</c> event of this command.
        /// </summary>
        public void RaiseCanExecuted()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
