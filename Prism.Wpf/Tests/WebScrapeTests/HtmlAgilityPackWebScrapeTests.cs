//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace WebScrape.Tests
{
    using HtmlAgilityPack;
    using NUnit.Framework;

    [TestFixture]
    public class HtmlAgilityPackWebScrapeTests
    {
        //METHODNAME_CONDITION_EXPECTATION
        [Test]
        public void LoadPage_LoadValidWebPage_PageFound()
        {
            //Arrange
            var web = new HtmlWeb();

            //Act
            var document = web.Load("http://www.worcestercityfc.org/news/");

            //Assert
            Assert.IsTrue(!document.ParsedText.Contains("Page not found (404)"));
        }

        [Test]
        public void LoadPage_LoadInValidWebPage_PageNotFound()
        {
            // Arrange
            var web = new HtmlWeb();

            // Act
            var document = web.Load("http://www.worcestercityfc.org/fakenews/");

            // Assert
            Assert.IsTrue(document.ParsedText.Contains("Page not found (404)"));
        }

        [Test]
        public void SelectNode_FindHtmlNodesWithTheChosenClass_NodesAreFound()
        {
            // Arrange
            var web = new HtmlWeb();
            var document = web.Load("http://www.worcestercityfc.org/news/");

            // Act
            var nodes = document.DocumentNode.SelectNodes("//a[@class='heading__title u-gamma']");

            // Assert
            Assert.IsNotNull(nodes);
        }

        [Test]
        public void SelectNode_FindHtmlNodesWithTheChosenClass_NodesAreNull()
        {
            // Arrange
            var web = new HtmlWeb();
            var document = web.Load("http://www.worcestercityfc.org/news/");

            // Act
            var nodes = document.DocumentNode.SelectNodes("//a[@class='heading__title false']");

            // Assert
            Assert.IsNull(nodes);
        }
    }
}
// WebScrape.Tests namespace 