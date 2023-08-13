using Microsoft.VisualStudio.TestTools.UnitTesting;
using Results.Loggings;
using Results.Test.Loggings.Mocks;

namespace Results.Test.Loggings
{
    [TestClass]
    public class LogTest
    {
        [TestMethod]
        public void Constructor_Run_AsExepected()
        {
            // Arrange
            const Severity severity = Severity.Information;
            const string message = "Test message";
            var additionalData = new AdditionalDataMock();

            // act
            var actual = new Log(severity, message, additionalData);

            // Assert
            Assert.AreEqual(severity, actual.Severity);
            Assert.AreEqual(message, actual.Message);
            Assert.AreEqual(additionalData, actual.AddtinalData);
            Assert.IsTrue(actual.HasMessage);
        }

        [TestMethod]
        public void Constructor_NullAddtionalData_AsExepebbcted()
        {
            // Arrange
            const Severity severity = Severity.Warning;

            // act
            var actual = new Log(severity, string.Empty);

            // Assert
            Assert.AreEqual(severity, actual.Severity);
            Assert.AreEqual(string.Empty, actual.Message);
            Assert.IsNull(actual.AddtinalData);
            Assert.IsFalse(actual.HasMessage);
        }
    }
}
