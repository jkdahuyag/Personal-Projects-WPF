using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Dahuyag_AS2.Tests
{
    public class GiantIntTests
    {
        [TestCase("23,007,999,000", "6234241", 1)]
        [TestCase("23,007,999,000", "23007999000", 0)]
        [TestCase("60,3,4,241", "23,007,999,000", 1)]
        [TestCase("60,3,4,241", "23,007,999,000,123", -1)]
        public void CompareTo_GivenGiantInt_CorrectResult(string giantInt1, string giantInt2, int expectedResult)
        {
            //Arrange
            var n1 = new GiantInt(giantInt1);
            var n2 = new GiantInt(giantInt2);

            //Act
            int result = Comparer<GiantInt>.Default.Compare(n1, n2);
            //Assert
            result.Should().Be(expectedResult);
        }

        #region StringArrayCtorTests

        [Test]
        public void ConstructorStringArray_GivenDataWithCommaNadOrSpacing_EqualDataProperty()
        {
            //Arrange
            List<string[]> testStrings = new List<string[]>();
            List<string> expectedResult = new List<string>();

            testStrings.Add(new string[] { "23", "007", "999", "000" });
            testStrings.Add(new string[] { "-23", "007", "999", "000" });
            testStrings.Add(new string[] { "007", "000", "7  ", "  7", " 7 ", " 7", "7 ", "7" });
            testStrings.Add(new string[] { "000", "00", "99", "000" });

            expectedResult.Add("23007999000");
            expectedResult.Add("-23007999000");
            expectedResult.Add("7000007007007007007007");
            expectedResult.Add("99000");

            int expectedCompareResult = 0;
            for (var index = 0; index < testStrings.Count; index++)
            {
                var expectedGiantInt = new GiantInt(expectedResult[index]);

                //Act
                var n1 = new GiantInt(testStrings[index]);
                //Assert
                n1.CompareTo(expectedGiantInt).Should().Be(expectedCompareResult);
                Console.WriteLine($"{n1} : {expectedGiantInt} || Successfully compared index {index}");
            }
        }
        [Test]
        public void ConstructorStringArray_GivenInvalidData_EqualDataProperty()
        {
            //Arrange
            List<string[]> testStrings = new List<string[]>();

            testStrings.Add(new string[] { "23", "-007", "999", "000" });
            testStrings.Add(new string[] { "-23", "007", "09h9", "000" });
            testStrings.Add(new string[] { "007", "000", "7  0 0 0", "  7", " 7 ", " 7", "7 ", "7" });
            testStrings.Add(new string[] { "00 0", "00", "99", "000" });

            int expectedCompareResult = 0;
            for (var index = 0; index < testStrings.Count; index++)
            {
                //Act
                var act = () =>
                {
                    var n1 = new GiantInt(testStrings[index]);
                };

                //Assert
                act.Should().Throw<Exception>();
                Console.WriteLine($"Successfully tested index {index}");
            }
        }


        #endregion

        #region StringCtorTests

        [TestCase("23, 007, 999, 000", "23007999000")]
        [TestCase("00000", "0")]
        [TestCase("007,999,0", "7999000")]
        [TestCase("-2,2", "-2002")]
        public void ConstructorString_GivenDataWithCommaNadOrSpacing_EqualDataProperty(string given, string expectedResult)
        {
            //Arrange
            var expectedGiantInt = new GiantInt(expectedResult);
            int expectedCompareResult = 0;
            //Act
            var n1 = new GiantInt(given);
            //Assert
            n1.CompareTo(expectedGiantInt).Should().Be(expectedCompareResult);
        }
        [TestCase("23, 007 999, 000")]
        [TestCase("23007999, 000")]
        [TestCase("23, 007999000")]
        [TestCase("23,007h999,000")]
        [TestCase("-2,-2")]
        public void ConstructorString_GivenDataWithInvalidData_ThrowError(string given)
        {
            //Arrange
            //Act
            var act = () =>
            {
                var n1 = new GiantInt(given);
            };
            //Assert

            act.Should().Throw<InvalidOperationException>();
        }
        [TestCase("23,007,h,999,000")]
        [TestCase("23,h,999,000")]
        [TestCase("h,999,000")]
        public void ConstructorString_GivenDataWithInvalidFormat_ThrowError(string given)
        {
            //Arrange
            //Act
            var act = () =>
            {
                var n1 = new GiantInt(given);
            };
            //Assert

            act.Should().Throw<FormatException>();
        }


        #endregion

        [TestCase("29,000", "1000", "30000")]
        [TestCase("239,000", "20", "239020")]
        [TestCase("239,000,253", "120", "239000373")]
        [TestCase("239,980", "1,20", "241000")]
        public void Add_GivenGiantInt_CorrectResult(string giantInt1, string giantInt2, string result)
        {
            //Arrange
            var n1 = new GiantInt(giantInt1);
            var n2 = new GiantInt(giantInt2);
            var expectedN1 = n1;
            var expectedN2 = n2;
            var expectedGiantInt = new GiantInt(result);
            //Act
            var Result = n1.Add(n2);
            //Assert
            Result.CompareTo(expectedGiantInt).Should().Be(0);
            n1.CompareTo(expectedN1).Should().Be(0);
            n2.CompareTo(expectedN2).Should().Be(0);
        }
        [TestCase("29,000", "1000", "28000")]
        [TestCase("239,000", "20", "238980")]


        public void Subtract_GivenGiantInt_CorrectResult(string giantInt1, string giantInt2, string result)
        {
            //Arrange
            var n1 = new GiantInt(giantInt1);
            var n2 = new GiantInt(giantInt2);
            var expectedN1 = n1;
            var expectedN2 = n2;
            var expectedGiantInt = new GiantInt(result);
            //Act
            var Result = n1.Subtract(n2);
            //Assert
            Result.CompareTo(expectedGiantInt).Should().Be(0);
            n1.CompareTo(expectedN1).Should().Be(0);
            n2.CompareTo(expectedN2).Should().Be(0);
        }
    }
}

