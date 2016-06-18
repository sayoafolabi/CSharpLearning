using AutotraderPageObjectModel.AutotraderPages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutotraderPageObjectModel.AutotraderTests
{
    
    public class SearchForACar
    {
        private HomePage homepage;
        private SearchResultPage searchResultPage;

        [SetUp]
        public void Initialise()
        {
            BaseClass.LaunchBrowser("Chrome");
            homepage = new HomePage();
            searchResultPage = new SearchResultPage();
        }

        [TearDown]
        public void TearDown()
        {
            BaseClass.CloseBrowser();
        }

        [Test]
        public void SearchForACarTest()
        {
            homepage = BaseClass.GivenINavigateAutotraderHomepage();
            homepage.AndIAmOnAutotraderHomepage();
            homepage.WhenIEnterValidPostcode();
            homepage.AndISelectDistanceToPostcode();
            homepage.AndISelectACarMake();
            searchResultPage = homepage.AndIClickOnSearchCarButton();
            searchResultPage.ThenIAmOnSearchResultPage();

        }

        [Test]
        public void SearchForACar2Test()
        {
            homepage = BaseClass.GivenINavigateAutotraderHomepage();
            homepage.AndIAmOnAutotraderHomepage();
            homepage.WhenIEnterValidPostcode();
            homepage.AndISelectDistanceToPostcode();
            homepage.AndISelectACarMake();
            searchResultPage = homepage.AndIClickOnSearchCarButton();
            searchResultPage.ThenIAmOnSearchResultPage();

        }
    }
}
