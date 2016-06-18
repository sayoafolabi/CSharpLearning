using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutotraderPageObjectModel.AutotraderPages
{
    public class SearchResultPage : BaseClass
    {
        private IList<IWebElement> searchResult;


        public void ThenIAmOnSearchResultPage()
        {
            searchResult = GetElementsByClassName("search-result__title");
            Assert.True(searchResult.Count > 0, "Search result list not displayed");
        }
    }
}
