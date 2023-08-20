using Microsoft.VisualStudio.TestTools.UnitTesting;
using Results.Loggings;
using Results.Test.Loggings.Mocks;

namespace Results.Test.Results.TestHelpers
{
    internal static class ResultTestHelper
    {
        internal const string ErrorLogMessage = "ErrorLog: Error";
        internal const string AlertErrorLogMessage = "ErrorLog: Alert";
        internal const string FatalErrorLogMessage = "ErrorLog: Fatal";
        internal const string WarningLogMessage = "Log: Warning";
        internal const string InformationLogMessage = "Log: Information";
        internal const string TestExceptionMessage = "ErrorException: Test";
        internal static readonly Exception TestException = new Exception(TestExceptionMessage);

        [Ignore]
        public static ErrorLog CreateErrorLog()
        {
            return new ErrorLog(
                Severity.Error,
                ErrorLogMessage,
                new AdditionalDataMock(),
                TestException);
        }

        [Ignore]
        public static ErrorLog CreateAlertErrorLog()
        {
            return new ErrorLog(
                Severity.Alert,
                AlertErrorLogMessage,
                new AdditionalDataMock(),
                TestException);
        }

        [Ignore]
        public static ErrorLog CreateFatalErrorLog()
        {
            return new ErrorLog(
                Severity.Fatal,
                FatalErrorLogMessage,
                new AdditionalDataMock(),
                TestException);
        }

        [Ignore]
        public static Log CreateWarningLog()
        {            
            return new Log(Severity.Warning, WarningLogMessage, new AdditionalDataMock());
        }

        [Ignore]
        public static Log CreateInformationLog()
        {
            return new Log(Severity.Information, InformationLogMessage, new AdditionalDataMock());
        }
    }
}
