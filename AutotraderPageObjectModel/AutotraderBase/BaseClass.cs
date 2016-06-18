using AutotraderPageObjectModel.AutotraderBase;
using AutotraderPageObjectModel.AutotraderPages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutotraderPageObjectModel
{
    public class BaseClass
    {
        public static IWebDriver driver { get; set; }
        private static SelectElement select;
        public TestContext TestContext { get; set; }
        protected static ExtentReports extent;
        protected static ExtentTest test;



        static BaseClass()
        {
            driver = null;
            select = null;
        }

        public static void ScrollUp(IWebElement elem)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", elem);
        }

        public static void WaitForElementToExist(string element, int seconds)
        {
            var _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            _wait.Until(ExpectedConditions.ElementExists(By.CssSelector(element)));
        }

        public static void WaitForElementToBeVisible(string element, int seconds)
        {
            var _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(element)));
        }

        public static void WaitForElementToBeClickable(string element, int seconds)
        {
            var _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(element)));
        }

        public static void SelectByIndex(IWebElement element, int index)
        {
            select = new SelectElement(element);
            select.SelectByIndex(index);
        }

        public static void SelectByValue(IWebElement element, string value)
        {
            select = new SelectElement(element);
            select.SelectByValue(value); ;
        }

        public static void SelectByText(IWebElement element, string text)
        {
            select = new SelectElement(element);
            select.SelectByText(text);
        }


        public static void LaunchBrowser(string browser)
        {
            switch (browser)
            {
                case "Chrome":
                    driver = initialiseChrome();
                    break;
                case "Firefox":
                    driver = initialiseFirefox();
                    break;
                default:
                    Console.WriteLine(browser + " is not recognised. So Firefox is launched instead");
                    driver = initialiseFirefox();
                    break;
            }
            extent = ExtentManager.Instance;
            driver.Manage().Window.Maximize();
            test = extent.StartTest(TestContext.CurrentContext.Test.Name);
            test.Log(LogStatus.Pass, String.Format("{0} is up and running", TestContext.CurrentContext.Test.Name));
        }

        public static void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Status;
            //var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
            //        ? ""
            //        : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            LogStatus logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = LogStatus.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = LogStatus.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = LogStatus.Skip;
                    break;
                default:
                    logstatus = LogStatus.Pass;
                    break;
            }

            test.Log(logstatus, "Test ended with " + logstatus);

            extent.EndTest(test);
            extent.Flush();
        }

        private static IWebDriver initialiseChrome()
        {
            driver = new ChromeDriver();

            return driver;
        }

        private static IWebDriver initialiseFirefox()
        {
            driver = new FirefoxDriver();

            return driver;
        }

        public static void CloseBrowser()
        {
            TearDown();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Close();
            driver.Quit();
        }

        public static void LaunchUrl(String Url)
        {
            driver.Navigate().GoToUrl(Url);
        }

        public static Screenshot TakeScreenShot()
        {
            return ((ITakesScreenshot)driver).GetScreenshot();
        }

        public static void SaveScreenshot()
        {
            var testName = TestContext.CurrentContext.Test.Name;

            if (testName.Length > 100)
            {
                testName = testName.Substring(0, 100);
            }

            try
            {
                var dateNow = DateTime.Now.Date.ToString().Replace(@"/", "").Replace(@" ", "").Replace(@":", "");
                dateNow = dateNow.Substring(0, 8);

                var timeNow = DateTime.Now.TimeOfDay.ToString().Replace(@"/", "").Replace(@" ","").Replace(@":","").Replace(@".","");
                timeNow = timeNow.Substring(0, 6);

                var fileName = String.Format("C:\\Screenshots\\{0}_{1}_{2}.png", testName, dateNow, timeNow);

                var screenShot = TakeScreenShot();

                screenShot.SaveAsFile(fileName, System.Drawing.Imaging.ImageFormat.Png);

                test.Log(LogStatus.Info, "Snapshot below: " + test.AddScreenCapture(fileName));
            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format("Screesnhot cannot be written because of {0} ", e));
            }


           }

        private static IWebElement GetElement(By locator)
        {

            IWebElement element = null;
            int tryCount = 0;

            while (element == null)
            {
                try
                {
                    element = driver.FindElement(locator);
                }
                catch (Exception e)
                {
                    if (tryCount == 3)
                    {
                        SaveScreenshot();
                        throw e;
                    }
                }
                    var second = new TimeSpan(0, 0, 2);
                    Thread.Sleep(second);

                    tryCount++;
                
            }

            Console.WriteLine(element.ToString() + " is now retrieved");
            return element;

        }


        public static IWebElement GetElementById(String id)
        {
            By locator = By.Id(id);
            return GetElement(locator);
        }

        public static IWebElement GetElementByClassName(String className)
        {
            By locator = By.ClassName(className);
            return GetElement(locator);
        }

        public static IWebElement GetElementByName(String name)
        {
            By locator = By.Name(name);
            return GetElement(locator);
        }

        public static IWebElement GetElementByCssSelector(String cssSelector)
        {
            By locator = By.CssSelector(cssSelector);
            return GetElement(locator);
        }

        public static IWebElement GetElementByXpath(String xpath)
        {
            By locator = By.XPath(xpath);
            return GetElement(locator);
        }


        private static IList<IWebElement> GetElements(By locator)
        {

            IList<IWebElement> element = null;
            int tryCount = 0;

            while (element == null)
            {
                try
                {

                    element = driver.FindElements(locator);

                }
                catch (Exception e)
                {
                    if (tryCount == 3)
                    {
                        SaveScreenshot();
                        throw e;
                    }

                    var second = new TimeSpan(0, 0, 2);
                    Thread.Sleep(second);

                    tryCount++;
                }
            }

            Console.WriteLine(element.ToString() + " is now retrieved");
            return element;

        }

        public static IList<IWebElement> GetElementsById(String id)
        {
            By locator = By.Id(id);
            return GetElements(locator);
        }

        public static IList<IWebElement> GetElementsByClassName(String className)
        {
            By locator = By.ClassName(className);
            return GetElements(locator);
        }

        public static IList<IWebElement> GetElementsByName(String name)
        {
            By locator = By.Name(name);
            return GetElements(locator);
        }

        public static IList<IWebElement> GetElementsByCssSelector(String cssSelector)
        {
            By locator = By.CssSelector(cssSelector);
            return GetElements(locator);
        }

        public static IList<IWebElement> GetElementsByXpath(String xpath)
        {
            By locator = By.XPath(xpath);
            return GetElements(locator);
        }

        public static HomePage GivenINavigateAutotraderHomepage()
        {
            LaunchUrl("http://www.autotrader.co.uk/");
            return new HomePage();
        }

    }
}
