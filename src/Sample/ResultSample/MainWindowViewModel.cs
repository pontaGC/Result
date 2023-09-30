using Results;
using Results.Loggings;
using SharedLibrary.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ResultSample
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            this.ExecuteCommand = new DelegateCommand(this.Execute);
            this.ClearLogsCommand = new DelegateCommand(this.ClearLogs);
        }

        /// <summary>
        /// Gets a collectoin of the log.
        /// </summary>
        public ObservableCollection<ILog> Logs { get; } = new ObservableCollection<ILog>();

        public ICommand ExecuteCommand { get; }

        public ICommand ClearLogsCommand { get; }

        private void Execute()
        {
            var result = DoDummyOperation();

            foreach(var log in result.AllLogs)
            {
                this.Logs.Add(log);
            }
        }

        private void ClearLogs()
        {
            this.Logs.Clear();
        }

        private Result DoDummyOperation()
        {
            var informations = new[]
            {
                new Log(Severity.Information, "Information 1"),
                new Log(Severity.Information, "Information 2")
            };

            var errors = new[]
            {
                new Log(Severity.Error, "Error 1"),
                new Log(Severity.Error, "Error 2"),
            };

            return Result.Failed()
                         .AddLogs(errors)
                         .AddWarnings("Warnig 1.")
                         .AddLogs(informations);
        }
    }
}
