using System;
using System.Collections.Generic;
using System.Windows.Markup;
using NumberConvertation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumberConvertationTest
{

    [TestClass]
    public class UnitTest1
    {
        readonly Translator _tran = new Translator();

        [TestMethod]
        public void EmptyString()
        {
            makeAssert(
                (new string[] { "" }),
                (new string[] { "" }),
                "error",
                "empty",
                "Не распозналась пустая строка",
                false
            );
        }

        [TestMethod]
        public void StringOfSpaces()
        {
            makeAssert(
                (new string[] { "           " }),
                (new string[] { "           " }),
                "error",
                "spaces",
                "Не распозналась строка с пробелами",
                false
            );
        }

        [TestMethod]
        public void BadSymbols()
        {
            makeAssert(
                (new string[] { "123 414", "one hundred and 12", "12 one hundred and", "щту hundred and" }),
                (new string[] { "123 414", "one hundred and 12", "12 one hundred and", "щту hundred and" }),
                "error",
                "undefinedsymbols",
                "Не распозналась строка с неверными символами",
                false
            );
        }

        [TestMethod]
        public void BadWords()
        {
            makeAssert(
                (new string[] { "twu one", "hundred and siven", "hundrid" }),
                (new string[] { "twu", "siven", "hundrid" }),
                "error",
                "notfinded",
                "В словаре не найдено значение.",
                true
            );
        }

        [TestMethod]
        public void Hundreds()
        {
            makeAssert(
                (new string[] { " one hundreds", "hundreds and siven", "hundreds" }),
                (new string[] { " one hundreds", "hundreds and siven", "hundreds" }),
                "error",
                "hundreds",
                "Не распозналась строка с hundreds.",
                false
            );
        }

        [TestMethod]
        public void WrongDash()
        {
            makeAssert(
                (new string[] { "twenty -hundred ", "hundred -twenty", "hundred- ten", " ten- hundred" }),
                (new string[] { "-hundred", "-twenty", "hundred-", "ten-" }),
                "error",
                "wrongdash",
                "Не распозналась строка с дэшем.",
                true
            );
        }

        [TestMethod]
        public void DashedWord()
        {
            makeAssert(
                (new string[] { "twenty-hundred", "hundred-twenty", "hundred-ten", "ten-hundred" }),
                (new string[] { "twenty-hundred", "hundred-twenty", "hundred-ten", "ten-hundred" }),
                "error",
                "dashedword",
                "Неверные слова соеденены дешем.",
                true
            );
        }

        [TestMethod]
        public void TooManyDashes()
        {
            makeAssert(
                (new string[] { "twenty-hundred-four", "four-hundred-twenty", "hundred-ten-" }),
                (new string[] { "twenty-hundred-four", "four-hundred-twenty", "hundred-ten-" }),
                "error",
                "toomanydashes",
                "Нераспознана строка с несколькими дешами.",
                true
            );
        }

        [TestMethod]
        public void WrongAnd()
        {
            makeAssert(
                (new string[] { "three and eight", "hundred and hundred", "eighty and five", "eighteen and four" }),
                (new string[] { "three and eight", "hundred and hundred", "eighty and five", "eighteen and four" }),
                "error",
                "wrongand",
                "Неверные слова между and.",
                true
            );
        }

        [TestMethod]
        public void AndLastFirst()
        {
            makeAssert(
                (new string[] { "and four", " and four" }),
                (new string[] { "and four", " and four" }),
                "error",
                "firstand",
                "Нераспознан and",
                false
            );
            makeAssert(
                (new string[] { "four and", "four and ", "hundred and" }),
                (new string[] { "four and", "four and ", "hundred and" }),
                "error",
                "lastand",
                "Нераспознан and",
                false
            );
        }

        [TestMethod]
        public void Zero()
        {
            makeAssert(
                (new string[] { "zero and four", "four zero" }),
                (new string[] { "three and eight", "hundred and hundred" }),
                "error",
                "zero",
                "Ноль в неверном месте",
                false
            );
        }

        [TestMethod]
        public void Hundred()
        {
            makeAssert(
                (new string[] { "hundred hundred" }),
                (new string[] { "hundred hundred" }),
                "error",
                "afterhundred",
                "После сотен только десятки и единицы",
                true
            );
        }

        [TestMethod]
        public void Dozens()
        {
            makeAssert(
                (new string[] { "sixty hundred", "sixty sixteen", "sixty sixty" }),
                (new string[] { "sixty hundred", "sixty sixteen", "sixty sixty" }),
                "error",
                "afterdozens",
                "После сотен только десятки и единицы",
                true
            );
        }

        [TestMethod]
        public void Dozen()
        {
            makeAssert(
                (new string[] { "sixteen one", "sixteen sixteen", "sixteen sixty" }),
                (new string[] { "sixteen one", "sixteen sixteen", "sixteen sixty" }),
                "error",
                "afterotherdozens",
                "После сотен только десятки и единицы",
                true
            );
        }

        [TestMethod]
        public void Units()
        {
            makeAssert(
                (new string[] { "one sixteen", "one sixteen", "one one" }),
                (new string[] { "one sixteen", "one sixteen", "one one" }),
                "error",
                "afterunits",
                "После сотен только десятки и единицы",
                true
            );
        }

        [TestMethod]
        public void Calculations()
        {
            makeAssert(
                (new string[] { "one", "sixteen", "sixty six", "six hundred", "hundred" }),
                (new string[] { "1", "16", "66", "600", "100" })
            );
            makeAssert(
                (new string[] { "two-hundred", "seventy-one", "sixty-six", "six-hundred" }),
                (new string[] { "200", "71", "66", "600" })
            );
            makeAssert(
                (new string[] { "three-hundred and one", "hundred and seventy-one", "five-hundred and sixty six", "one hundred and fifteen", "two-hundred and eighteen" }),
                (new string[] { "301", "171", "566", "115", "218" })
            );
            makeAssert(
                (new string[] { "three-hundred one", "hundred seventy-one", "five-hundred sixty six", "one hundred fifteen", "two-hundred eighteen" }),
                (new string[] { "301", "171", "566", "115", "218" })
            );
        }

        public void makeAssert(string[] inputStrArr, string[] expectedResult, string value, string errorName, string errorDescription, bool needValueAtEnd)
        {
            for (var i = 0; i < inputStrArr.Length; i++)
            {
                var input = inputStrArr[i];
                var expected = expectedResult[i];

                var result = _tran.TryParse(input);

                if (needValueAtEnd)
                    Assert.IsTrue(
                        result["result"] == value &&
                        result["error"] == _tran.ErrorDic[errorName] + expected,
                        errorDescription + "\r\n" + result["error"] + "\r\n" + _tran.ErrorDic[errorName] + expected
                        );
                else
                    Assert.IsTrue(
                        result["result"] == "error" &&
                        result["error"] == _tran.ErrorDic[errorName],
                        errorDescription + "\r\n" + result["error"] + "\r\n" + _tran.ErrorDic[errorName]
                        );
            }
        }
        public void makeAssert(string[] inputStrArr, string[] expectedResult)
        {
            for (var i = 0; i < inputStrArr.Length; i++)
            {
                var input = inputStrArr[i];
                var expected = expectedResult[i];

                var result = _tran.TryParse(input);
                
                Assert.IsTrue(
                    result["result"] == expected &&
                    result["error"] == "",
                    result["result"] + "!=" + expected + "  |  " + result["error"]
                );
            }
        }
    }
}
