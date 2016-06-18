using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutotraderPageObjectModel.AutotraderPages
{
    public class HomePage : BaseClass
    {
        private IWebElement autoTraderLogo;
        private IWebElement postcode;
        private IWebElement distance;
        private IWebElement make;
        private IWebElement submitButton;


        public void AndIAmOnAutotraderHomepage()
        {
            autoTraderLogo = GetElementByClassName("site-header__logo");
            Assert.True(autoTraderLogo.Displayed, "Autotrader homepage is not displayed");
        }

        public void WhenIEnterValidPostcode()
        {
            postcode = GetElementById("postcode");
            postcode.Clear();
            postcode.SendKeys("OL9 8LE");
        }

        public void AndISelectDistanceToPostcode()
        {
            distance = GetElementById("radius");
            SelectByValue(distance, "50");

        }

        public void AndISelectACarMake()
        {
            make = GetElementById("searchVehiclesMake");
            SelectByValue(make, "audi");
        }

        public SearchResultPage AndIClickOnSearchCarButton()
        {
            submitButton = GetElementById("search");
            submitButton.Click();

            return new SearchResultPage();
        }

    }
}
