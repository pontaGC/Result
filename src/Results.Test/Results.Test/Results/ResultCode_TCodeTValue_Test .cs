using Microsoft.VisualStudio.TestTools.UnitTesting;
using Results.Test.Results.Mocks;

namespace Results.Test.Results
{
    [TestClass]
    public class RsultCodeTest_TCodeTvalue_Test
    {
        #region Succeeded

        [TestMethod]
        public void Succeeded_AsExpected()
        {
            // Arrange
            var mockValue = new MockResultValue();

            // Act
            var actual = ResultCodes<MockResultCode, MockResultValue>.Succeeded(mockValue);

            // Assert
            Assert.IsTrue(actual.IsSuccess);
            Assert.IsFalse(actual.IsFailed);
            Assert.IsFalse(actual.HasException);
            Assert.AreEqual(0, actual.Codes.Count());
            Assert.AreEqual(0, actual.AdditionalData.Count());
        }

        #endregion

        #region Failed

        [TestMethod]
        public void Failed_AsExpected()
        {
            // Act
            var actual = ResultCodes<MockResultCode>.Failed();

            // Assert
            Assert.IsFalse(actual.IsSuccess);
            Assert.IsTrue(actual.IsFailed);
            Assert.IsFalse(actual.HasException);
            Assert.AreEqual(0, actual.Codes.Count());
            Assert.AreEqual(0, actual.AdditionalData.Count());
        }

        #endregion

        #region AddCode

        [TestMethod]
        public void AddCode_NullCode_NotAdded()
        {
            // Arrange
            var mockValue = new MockResultValue();

            var result1 = ResultCodes<Version, MockResultValue>.Succeeded(mockValue);
            var result2 = ResultCodes<Version, MockResultValue>.Failed();

            // Act
            var actual1 = result1.AddCode(null as IEnumerable<Version>);
            var actual2 = result2.AddCode(null as Version);

            // Assert
            Assert.AreSame(result1, actual1);
            Assert.AreSame(result2, actual2);
            Assert.AreSame(mockValue, result1.Value);
            Assert.AreEqual(0, actual1.Codes.Count());
            Assert.AreEqual(0, actual2.Codes.Count());
        }

        [TestMethod]
        public void AddCode_OneCode_AddedCode()
        {
            // Arrange
            var result = ResultCodes<MockResultCode, MockResultValue>.Failed();

            // Act
            var actual = result.AddCode(MockResultCode.NoError);

            // Assert
            Assert.AreSame(result, actual);
            Assert.AreEqual(1, actual.Codes.Count());
        }

        [TestMethod]
        public void AddCode_Codes_AddedCodes()
        {
            // Arrange
            var mockValue = new MockResultValue();
            var result = ResultCodes<MockResultCode, MockResultValue>.Succeeded(mockValue);
            var codes = new[] {
                MockResultCode.NoError,
                MockResultCode.Warning,
                MockResultCode.Error, };

            // Act
            var actual = result.AddCode(codes);

            // Assert
            Assert.AreSame(result, actual);
            Assert.AreEqual(3, actual.Codes.Count());
            Assert.AreEqual(MockResultCode.NoError, actual.Codes.First());
            Assert.AreEqual(MockResultCode.Warning, actual.Codes.Skip(1).First());
            Assert.AreEqual(MockResultCode.Error, actual.Codes.Skip(2).First());
        }

        #endregion

        #region AddAdditionalData

        [TestMethod]
        public void AddAdditionalData_NullKey_NotAddded()
        {
            // Arrange
            var mockValue = new MockResultValue();
            const string Name = "TEST NAME";
            const int Number = 100;
            var data = new MockAdditionalData
            {
                Name = Name,
                Number = Number,
            };

            var result = ResultCodes<MockResultCode, MockResultValue>.Succeeded(mockValue);

            // Act
            var actual = result.AddAdditionalData(null, data);

            // Assert
            Assert.AreSame(result, actual);
            Assert.AreEqual(0, result.AdditionalData.Keys.Count());
        }

        [TestMethod]
        public void AddAdditionalData_AddNewData_AdddedNewData()
        {
            // Arrange
            var mockValue = new MockResultValue();
            const string Key = "Key";
            const string Name = "TEST NAME";
            const int Number = 100;
            var data = new MockAdditionalData
            {
                Name = Name,
                Number = Number,
            };

            var result = ResultCodes<MockResultCode, MockResultValue>.Succeeded(mockValue);

            // Act
            var actual = result.AddAdditionalData(Key, data)
                               .AdditionalData[Key];

            // Assert
            Assert.AreSame(data, actual);
        }

        [TestMethod]
        public void AddAdditionalData_AddDataByRegisteredKey_UpdateNewData()
        {
            // Arrange
            const string Key = "Key";
            var oldData = new MockAdditionalData
            {
                Name = "Old TEST NAME",
                Number = 100,
            };

            const string NewName = "New NAME";
            const int NewNumber = 9999;
            var newData = new MockAdditionalData
            {
                Name = NewName,
                Number = NewNumber,
            };

            var result = ResultCodes<MockResultCode, MockResultValue>.Failed()
                                                    .AddAdditionalData(Key, oldData);

            // Act
            var actual = result.AddAdditionalData(Key, newData)
                               .AdditionalData[Key];

            // Assert
            Assert.AreSame(newData, actual);
        }

        #endregion

        #region WithException

        [TestMethod]
        public void WithException_AsExpected()
        {
            // Arrange
            var exception = new Exception("Test exception");
            var result = ResultCodes<MockResultCode, MockResultValue>.Failed();

            // Act
            var actual = result.WithException(exception);

            // Arrange
            Assert.AreSame(result, actual);
            Assert.IsTrue(actual.HasException);
            Assert.AreSame(exception, actual.Exception);
        }

        #endregion
    }
}
