using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Header_Reader;
using Header_Reader.Interfaces;

namespace HeaderReaderTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValidateBadUrl()
        {
            var url = "www.mechatrobot.pl";
            try
            {
                var analyzer = new Header_Reader.HtmlAnalyzer(url);
                Assert.Fail("no exception thrown");
            }
            catch (UriFormatException ex)
            {
                Assert.IsTrue(ex is UriFormatException);
            }
        }
        [TestMethod]
        public void ValidateKeyWords()
        {
            var url = "http://www.mechatrobot.pl";
            var analyzer = new Header_Reader.HtmlAnalyzer(url);
            analyzer.SetKeywords();
            Assert.IsNotNull(analyzer.keyWordsArray);
        }
        [TestMethod]
        public void EmptyKeyWords()
        {
            var url = "http://www.google.pl";
            try
            {
                var analyzer = new Header_Reader.HtmlAnalyzer(url);
                analyzer.SetKeywords();
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is Exception);
            }
        }
    }
}
