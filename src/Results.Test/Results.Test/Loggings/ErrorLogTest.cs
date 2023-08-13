using Microsoft.VisualStudio.TestTools.UnitTesting;
using Results.Loggings;
using Results.Test.Loggings.Mocks;

namespace Results.Test.Loggings
{
    [TestClass]
    public class ErrorLogTest
    {
        [TestMethod]
        public void Constructor_Run_AsExepected()
        {
            // Arrange
            const Severity severity = Severity.Error;
            const string message = "Test message";
            var additionalData = new AdditionalDataMock();
            var exception = new OutOfMemoryException("Test out of memory");

            // act
            var actual = new ErrorLog(severity, message, additionalData, exception);

            // Assert
            Assert.AreEqual(severity, actual.Severity);
            Assert.AreEqual(message, actual.Message);
            Assert.AreEqual(additionalData, actual.AddtinalData);
            Assert.AreEqual(exception, actual.Exception);
            Assert.IsTrue(actual.HasException);
        }

        [TestMethod]
        public void Constructor_NullException_AsExepebbcted()
        {
            // Arrange
            const Severity severity = Severity.Alert;

            // act
            var actual = new ErrorLog(severity, string.Empty);

            // Assert
            Assert.AreEqual(severity, actual.Severity);
            Assert.AreEqual(string.Empty, actual.Message);
            Assert.IsNull(actual.AddtinalData);
            Assert.IsNull(actual.Exception);
            Assert.IsFalse(actual.HasException);
        }
    }
}
