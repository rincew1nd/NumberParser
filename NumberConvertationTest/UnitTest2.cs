using System;
using System.Text;
using NumberConvertation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumberConvertationTest
{
    /// <summary>
    /// Summary description for UnitTest2
    /// </summary>
    [TestClass]
    public class UnitTest2
    {
        readonly Translator _tran = new Translator();
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(_tran.ConvertToOldRussianNumber(122) == "IВ", _tran.ConvertToOldRussianNumber(12));
        }
    }
}
