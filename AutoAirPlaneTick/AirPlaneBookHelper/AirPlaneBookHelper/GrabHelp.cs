using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAutomation;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace AirPlaneBookHelper
{
    class GrabHelp: FluentTest
    {
        protected string _targetUrl = "https://www.vanilla-air.com/tw/";
        protected string[] _palces = { "NRT", "OKA","CTS", "ASJ", "TPE", "KHH", "HKG" };
        protected int _placeFrom = 0;
        protected int _placeTo = 4;
        protected string _flightFile = "vanilla-air[*].png";
        protected int _waitDelay = 1000;

        public static IWebDriver webDriver = null;

        public GrabHelp()
        {
            webDriver = new FirefoxDriver();
            webDriver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 0, 2));
        }

        public void SearchCriteria()
        {

            #region Selenium

            webDriver.Navigate().GoToUrl(_targetUrl);
            var js = (IJavaScriptExecutor)webDriver;

            var popPanel = webDriver.FindElement(By.ClassName("tabChanger"));
            popPanel.Click();

            var startEle = webDriver.FindElement(By.ClassName("startPlacement"));
            js.ExecuteScript("arguments[0].setAttribute('data-value', arguments[1])", startEle, _palces[0]);
            js.ExecuteScript("arguments[0].setAttribute('value', arguments[1])", startEle, _palces[0]);


            var endEle = webDriver.FindElement(By.ClassName("endPlacement"));
            js.ExecuteScript("arguments[0].setAttribute('data-value', arguments[1])", endEle, _palces[4]);
            js.ExecuteScript("arguments[0].setAttribute('value', arguments[1])", endEle, _palces[4]);

            var searchDate1 = webDriver.FindElement(By.ClassName("startDay"));
            js.ExecuteScript("arguments[0].setAttribute('value', arguments[1])", searchDate1, "2016/01/11");

            var searchDate2 = webDriver.FindElement(By.ClassName("endDay"));
            js.ExecuteScript("arguments[0].setAttribute('value', arguments[1])", searchDate2, "2016/01/17");

            var searchBtn = webDriver.FindElement(By.Id("edit-submit-ticket"));
            searchBtn.Click();

            WebDriverWait driverWait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 3));
            
            // wait till the filght check box created
            driverWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//*[@class='vnl-flight-select flight1']//span[@class='vnl-radio-btn__add_class']")));
            driverWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//*[@class='vnl-flight-select flight2']//span[@class='vnl-radio-btn__add_class']")));


            Thread.Sleep(_waitDelay);// sleep for wait the UI showed
            TakeScreenShot(_flightFile);// take snap shot for the demo
            ReleaseWebDriver();


            #endregion

            #region FluentAutomation


            #endregion
        }

        public void ReleaseWebDriver()
        {
            webDriver.Quit();
        }

        public void TakeScreenShot(string fileName)
        {
            var folderLocation = Environment.CurrentDirectory + "\\ScreenShot\\";
            if (!Directory.Exists(folderLocation))
            {
                Directory.CreateDirectory(folderLocation);
            }

            var takeShot = ((ITakesScreenshot)webDriver).GetScreenshot();
            fileName = fileName.Replace("*", DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss"));
            var filename = new StringBuilder(folderLocation);
            filename.Append(fileName);
            takeShot.SaveAsFile(filename.ToString(), ImageFormat.Png);
        }
    }
}
