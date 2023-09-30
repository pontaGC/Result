namespace SharedLibrary.Mvvm
{
    public class DelegateCommand : CommandBase
    {
        private readonly Func<bool> canExecuteFunction;
        private readonly Action executeAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="execute">The command action.</param>
        /// <param name="canExecute">The function to check whether or not the command can be executed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <c>null</c>.</exception>
        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            if (execute is null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            this.executeAction = execute;
            this.canExecuteFunction = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="execute">The command action.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute"/> is <c>null</c>.</exception>
        public DelegateCommand(Action execute)
            : this (execute, () => true)
        {
        }

        protected override bool CanExecute(object? parameter)
        {
            return this.canExecuteFunction?.Invoke() ?? true;
        }

        protected override void Execute(object? parameter)
        {
            this.executeAction.Invoke();
        }
    }
}
