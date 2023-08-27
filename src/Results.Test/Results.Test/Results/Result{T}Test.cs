using Microsoft.VisualStudio.TestTools.UnitTesting;
using Results.Loggings;
using Results.Test.Results.Mocks;
using Results.Test.Results.TestHelpers;

namespace Results.Test.Results
{
    [TestClass]
    public class ResultTest_T
    {
        #region Succeeded

        [TestMethod]
        public void Succeeded_AsExpected()
        {
            // Arrange
            var mockValue = new MockResultValue();

            // Act
            var actual = Result<MockResultValue>.Succeeded(mockValue);

            // Assert
            Assert.IsTrue(actual.IsSuccess);
            Assert.IsFalse(actual.IsFailed);
            Assert.AreEqual(mockValue, actual.Value);
            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(0, actual.AllLogs.Count());
        }

        #endregion

        #region Failed

        [TestMethod]
        public void Failed_AsExpected()
        {
            // Act
            var actual = Result<MockResultValue>.Failed();
            
            // Assert
            Assert.IsFalse(actual.IsSuccess);
            Assert.IsTrue(actual.IsFailed);
            Assert.IsNull(actual.Value);
            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(0, actual.AllLogs.Count());
        }

        [TestMethod]
        public void Failed_SetValue_AsExpected()
        {
            // Arrange
            var mockValue = new MockResultValue();

            // Act
            var actual = Result<MockResultValue>.Failed(mockValue);

            // Assert
            Assert.IsFalse(actual.IsSuccess);
            Assert.IsTrue(actual.IsFailed);
            Assert.AreEqual(mockValue, actual.Value);
            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(0, actual.AllLogs.Count());
        }

        #endregion

        #region AddErrors

        [TestMethod]
        public void AddErrors_ArgumentIsNullCollection_NotAddedError()
        {
            // Arrange
            var result = Result<MockResultValue>.Failed();

            // Act
            var actual = result.AddErrors(null as IEnumerable<IErrorLog>);

            // Assert
            Assert.IsTrue(ReferenceEquals(result, actual));
            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(0, actual.AllLogs.Count());
        }

        [TestMethod]
        public void AddErrors_ErrorCollection_AsExpected()
        {
            // Arrange
            var errorLog = new ErrorLog(Severity.Error, "Added Error", new Exception("ErrorException"));
            var alertLog = ResultTestHelper.CreateAlertErrorLog();
            var fatalLog = ResultTestHelper.CreateFatalErrorLog();
            var errors = new[] { errorLog, alertLog, fatalLog, };
            var baseResult = Result<MockResultValue>.Failed();

            // Act
            var actual = baseResult.AddErrors(errors);

            // Assert
            Assert.IsTrue(ReferenceEquals(baseResult, actual));

            Assert.AreEqual(errors.Length, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(errors.Length, actual.AllLogs.Count());

            Assert.AreEqual(errors[0], actual.Errors.Skip(0).FirstOrDefault());
            Assert.AreEqual(errors[1], actual.Errors.Skip(1).FirstOrDefault());
            Assert.AreEqual(errors[2], actual.Errors.Skip(2).FirstOrDefault());
            Assert.AreEqual(Severity.Error, actual.Errors.Skip(0).First().Severity);
            Assert.AreEqual("Added Error", actual.Errors.Skip(0).First().Message);
            Assert.IsTrue(actual.Errors.Skip(0).First().HasException);
            Assert.AreEqual("ErrorException", actual.Errors.Skip(0).First().Exception.Message);
        }

        [TestMethod]
        public void AddErrors_AddErrorCollectionToNotEmptyErrorResult_AsExpected()
        {
            // Arrange
            var addingErrors = new[]
            {
                ResultTestHelper.CreateAlertErrorLog(),
                ResultTestHelper.CreateFatalErrorLog(),
            };

            var baseErrorLog = ResultTestHelper.CreateErrorLog();
            var baseWarningLog = ResultTestHelper.CreateWarningLog();
            var baseInforamtionLog = ResultTestHelper.CreateInformationLog();
            var baseResult = Result<MockResultValue>.Failed()
                                   .AddErrors(baseErrorLog)
                                   .AddLogs(baseWarningLog)
                                   .AddLogs(baseInforamtionLog);

            // Act
            var actual = baseResult.AddErrors(addingErrors);

            // Assert
            Assert.IsTrue(ReferenceEquals(baseResult, actual));

            //// Log count check
            Assert.AreEqual(3, actual.Errors.Count());
            Assert.AreEqual(1, actual.Warnings.Count());
            Assert.AreEqual(1, actual.Informations.Count());
            Assert.AreEqual(5, actual.AllLogs.Count());

            //// Original error log check
            Assert.AreEqual(baseErrorLog, actual.Errors.Skip(0).FirstOrDefault());

            //// Added errors check
            Assert.AreEqual(addingErrors[0], actual.Errors.Skip(1).FirstOrDefault());
            Assert.AreEqual(addingErrors[1], actual.Errors.Skip(2).FirstOrDefault());
        }

        [TestMethod]
        public void AddErrors_ArgumentIsNull_NotAddedError()
        {
            // Arrange
            var result = Result<MockResultValue>.Failed();

            // Act
            var actual = result.AddErrors(null as IErrorLog);

            // Assert
            Assert.IsTrue(ReferenceEquals(result, actual));
            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(0, actual.AllLogs.Count());
        }

        [TestMethod]
        public void AddErrors_OneError_AsExpected()
        {
            // Arrange
            var errorLog = new ErrorLog(Severity.Alert, "Added Error", new Exception("ErrorException"));
            var baseResult = Result<MockResultValue>.Failed();

            // Act
            var actual = baseResult.AddErrors(errorLog);

            // Assert
            Assert.IsTrue(ReferenceEquals(baseResult, actual));

            Assert.AreEqual(1, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(1, actual.AllLogs.Count());

            Assert.AreEqual(errorLog, actual.Errors.Skip(0).FirstOrDefault());
            Assert.AreEqual("Added Error", actual.Errors.Skip(0).First().Message);
            Assert.AreEqual("ErrorException", actual.Errors.Skip(0).First().Exception.Message);
        }

        [TestMethod]
        public void AddErrors_OneErrorToNotEmptyErrorResult_AsExpected()
        {
            // Arrange
            var addingError = new ErrorLog(Severity.Alert, "Added Error", new Exception("ErrorException"));

            var baseErrorLog = ResultTestHelper.CreateErrorLog();
            var baseWarningLog = ResultTestHelper.CreateWarningLog();
            var baseInforamtionLog = ResultTestHelper.CreateInformationLog();
            var baseResult = Result<MockResultValue>.Failed()
                                   .AddErrors(baseErrorLog)
                                   .AddLogs(baseWarningLog)
                                   .AddLogs(baseInforamtionLog);

            // Act
            var actual = baseResult.AddErrors(addingError);

            // Assert
            Assert.IsTrue(ReferenceEquals(baseResult, actual));

            Assert.AreEqual(2, actual.Errors.Count());
            Assert.AreEqual(1, actual.Warnings.Count());
            Assert.AreEqual(1, actual.Informations.Count());
            Assert.AreEqual(4, actual.AllLogs.Count());

            Assert.AreEqual(baseErrorLog, actual.Errors.Skip(0).FirstOrDefault());
            Assert.AreEqual(addingError, actual.Errors.Skip(1).FirstOrDefault());
        }

        [TestMethod]
        public void AddErrors_ArgumentMessageAndExceptionAreNull_NotAddedError()
        {
            // Arrange
            var result = Result<MockResultValue>.Failed();

            // Act
            var actual = result.AddErrors(null, Severity.Error, null);

            // Assert
            Assert.IsTrue(ReferenceEquals(result, actual));

            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(0, actual.AllLogs.Count());
        }

        [TestMethod]
        public void AddErrors_AddMessageByDefault_AddedError()
        {
            // Arrange
            const string Message = "Error Message";
            var result = Result<MockResultValue>.Failed();

            // Act
            var actual = result.AddErrors(Message);

            // Assert
            Assert.IsTrue(ReferenceEquals(result, actual));

            Assert.AreEqual(1, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(1, actual.AllLogs.Count());

            Assert.AreEqual(Severity.Error, actual.Errors.Skip(0).First().Severity);
            Assert.AreEqual(Message, actual.Errors.Skip(0).First().Message);
            Assert.IsNull(actual.Errors.Skip(0).First().Exception);
            Assert.IsNull(actual.Errors.Skip(0).First().AddtinalData);
        }

        [TestMethod]
        public void AddErrors_AddMessage_AddedError()
        {
            // Arrange
            const string Message = "Error Message";
            const Severity severity = Severity.Alert;
            var exception = new Exception("ErrorException");

            var result = Result<MockResultValue>.Failed();

            // Act
            var actual = result.AddErrors(Message, severity, exception);

            // Assert
            Assert.IsTrue(ReferenceEquals(result, actual));

            Assert.AreEqual(1, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(1, actual.AllLogs.Count());

            Assert.AreEqual(severity, actual.Errors.Skip(0).First().Severity);
            Assert.AreEqual(Message, actual.Errors.Skip(0).First().Message);
            Assert.AreEqual(exception, actual.Errors.Skip(0).First().Exception);
            Assert.IsNull(actual.Errors.Skip(0).First().AddtinalData);
        }

        #endregion

        #region AddWarnings

        [TestMethod]
        public void AddWarnings_ArgumentIsNull_NotAddedWarning()
        {
            // Arrange
            var result = Result<MockResultValue>.Succeeded();
            var result2 = Result<MockResultValue>.Failed();

            // Act
            var actual = result.AddWarnings(null as IEnumerable<string>);
            var actual2 = result2.AddWarnings(null as string);

            // Assert
            Assert.IsTrue(ReferenceEquals(result, actual));
            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(0, actual.AllLogs.Count());

            Assert.IsTrue(ReferenceEquals(result2, actual2));
            Assert.AreEqual(0, actual2.Errors.Count());
            Assert.AreEqual(0, actual2.Warnings.Count());
            Assert.AreEqual(0, actual2.Informations.Count());
            Assert.AreEqual(0, actual2.AllLogs.Count());
        }

        [TestMethod]
        public void AddWarnings_OneWarning_AsExpected()
        {
            // Arrange
            const string WarningMessage = "Warning message";
            var baseResult = Result<MockResultValue>.Succeeded();

            // Act
            var actual = baseResult.AddWarnings(new[] { WarningMessage });

            // Assert
            Assert.IsTrue(ReferenceEquals(baseResult, actual));

            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(1, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(1, actual.AllLogs.Count());

            Assert.AreEqual(Severity.Warning, actual.Warnings.First().Severity);
            Assert.AreEqual(WarningMessage, actual.Warnings.First().Message);
        }

        [TestMethod]
        public void AddWarnings_MultipleMessages_AsExpected()
        {
            // Arrange
            const string Message1 = "Warning Message1";
            const string Message2 = "Warning Message2";
            const string Message3 = "Warning Message2";
            var messages = new[] { Message1, Message2, Message3, };

            var result = Result<MockResultValue>.Succeeded();

            // Act
            var actual = result.AddWarnings(messages);

            // Assert
            Assert.IsTrue(ReferenceEquals(result, actual));

            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(3, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(3, actual.AllLogs.Count());
        }

        [TestMethod]
        public void AddWarnings_OneWarningToNotEmptyResult_AsExpected()
        {
            // Arrange
            const string WarningMessage = "Warning message";

            var baseErrorLog = ResultTestHelper.CreateErrorLog();
            var baseWarningLog = ResultTestHelper.CreateWarningLog();
            var baseInforamtionLog = ResultTestHelper.CreateInformationLog();
            var baseResult = Result<MockResultValue>.Failed()
                                   .AddErrors(baseErrorLog)
                                   .AddLogs(baseWarningLog)
                                   .AddLogs(baseInforamtionLog);

            // Act
            var actual = baseResult.AddWarnings(new[] { WarningMessage, });

            // Assert
            Assert.IsTrue(ReferenceEquals(baseResult, actual));

            Assert.AreEqual(1, actual.Errors.Count());
            Assert.AreEqual(2, actual.Warnings.Count());
            Assert.AreEqual(1, actual.Informations.Count());
            Assert.AreEqual(4, actual.AllLogs.Count());

            Assert.AreEqual(baseWarningLog, actual.Warnings.Skip(0).FirstOrDefault());
            Assert.AreEqual(WarningMessage, actual.Warnings.Skip(1).FirstOrDefault().Message);
        }

        [TestMethod]
        public void AddWarnings_AddMessage_AddedWarning()
        {
            // Arrange
            const string Message = "Warning Message";
            var result = Result<MockResultValue>.Succeeded();

            // Act
            var actual = result.AddWarnings(Message);

            // Assert
            Assert.IsTrue(ReferenceEquals(result, actual));

            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(1, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(1, actual.AllLogs.Count());

            Assert.AreEqual(Severity.Warning, actual.Warnings.First().Severity);
            Assert.AreEqual(Message, actual.Warnings.First().Message);
        }

        #endregion

        #region AddInformations

        [TestMethod]
        public void AddInformations_ArgumentIsNull_NotAddedInformation()
        {
            // Arrange
            var result = Result<MockResultValue>.Succeeded();
            var result2 = Result<MockResultValue>.Failed();

            // Act
            var actual = result.AddInformations(null as IEnumerable<string>);
            var actual2 = result2.AddInformations(null as string);

            // Assert
            Assert.IsTrue(ReferenceEquals(result, actual));
            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(0, actual.AllLogs.Count());

            Assert.IsTrue(ReferenceEquals(result2, actual2));
            Assert.AreEqual(0, actual2.Errors.Count());
            Assert.AreEqual(0, actual2.Warnings.Count());
            Assert.AreEqual(0, actual2.Informations.Count());
            Assert.AreEqual(0, actual2.AllLogs.Count());
        }

        [TestMethod]
        public void AddInformations_OneInformation_AsExpected()
        {
            // Arrange
            const string InformationMessage = "Information message";
            var baseResult = Result<MockResultValue>.Succeeded();

            // Act
            var actual = baseResult.AddInformations(new[] { InformationMessage });

            // Assert
            Assert.IsTrue(ReferenceEquals(baseResult, actual));

            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(1, actual.Informations.Count());
            Assert.AreEqual(1, actual.AllLogs.Count());

            Assert.AreEqual(Severity.Information, actual.Informations.First().Severity);
            Assert.AreEqual(InformationMessage, actual.Informations.First().Message);
        }

        [TestMethod]
        public void AddInformations_MultipleMessages_AsExpected()
        {
            // Arrange
            const string Message1 = "Information Message1";
            const string Message2 = "Information Message2";
            const string Message3 = "Information Message2";
            var messages = new[] { Message1, Message2, Message3, };

            var result = Result<MockResultValue>.Succeeded();

            // Act
            var actual = result.AddInformations(messages);

            // Assert
            Assert.IsTrue(ReferenceEquals(result, actual));

            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(3, actual.Informations.Count());
            Assert.AreEqual(3, actual.AllLogs.Count());
        }

        [TestMethod]
        public void AddInformations_OneInformationToNotEmptyResult_AsExpected()
        {
            // Arrange
            const string InformationMessage = "Information message";

            var baseErrorLog = ResultTestHelper.CreateErrorLog();
            var baseWarningLog = ResultTestHelper.CreateWarningLog();
            var baseInforamtionLog = ResultTestHelper.CreateInformationLog();
            var baseResult = Result<MockResultValue>.Failed()
                                   .AddErrors(baseErrorLog)
                                   .AddLogs(baseWarningLog)
                                   .AddLogs(baseInforamtionLog);

            // Act
            var actual = baseResult.AddInformations(new[] { InformationMessage, });

            // Assert
            Assert.IsTrue(ReferenceEquals(baseResult, actual));

            Assert.AreEqual(1, actual.Errors.Count());
            Assert.AreEqual(1, actual.Warnings.Count());
            Assert.AreEqual(2, actual.Informations.Count());
            Assert.AreEqual(4, actual.AllLogs.Count());

            Assert.AreEqual(baseInforamtionLog, actual.Informations.Skip(0).FirstOrDefault());
            Assert.AreEqual(InformationMessage, actual.Informations.Skip(1).FirstOrDefault().Message);
        }

        [TestMethod]
        public void AddInformations_AddMessage_AddedInformation()
        {
            // Arrange
            const string Message = "Information Message";
            var result = Result<MockResultValue>.Succeeded();

            // Act
            var actual = result.AddInformations(Message);

            // Assert
            Assert.IsTrue(ReferenceEquals(result, actual));

            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(1, actual.Informations.Count());
            Assert.AreEqual(1, actual.AllLogs.Count());

            Assert.AreEqual(Severity.Information, actual.Informations.First().Severity);
            Assert.AreEqual(Message, actual.Informations.First().Message);
        }

        #endregion

        #region AddLogs

        [TestMethod]
        public void AddLogs_ArgumentIsNull_NotAddedLogs()
        {
            // Arrange
            var result = Result<MockResultValue>.Succeeded();
            var result2 = Result<MockResultValue>.Failed();

            // Act
            var actual = result.AddLogs(null as IEnumerable<ILog>);
            var actual2 = result2.AddLogs(null as ILog);

            // Assert
            Assert.IsTrue(ReferenceEquals(result, actual));
            Assert.AreEqual(0, actual.Errors.Count());
            Assert.AreEqual(0, actual.Warnings.Count());
            Assert.AreEqual(0, actual.Informations.Count());
            Assert.AreEqual(0, actual.AllLogs.Count());

            Assert.IsTrue(ReferenceEquals(result2, actual2));
            Assert.AreEqual(0, actual2.Errors.Count());
            Assert.AreEqual(0, actual2.Warnings.Count());
            Assert.AreEqual(0, actual2.Informations.Count());
            Assert.AreEqual(0, actual2.AllLogs.Count());
        }

        #endregion
    }
}
