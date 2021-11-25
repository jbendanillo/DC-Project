using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Jeniza_DC_Project
{
    class Base
    {
        public static IWebDriver webDriver { get; set; }

        [SetUp]
        public void SetUp()
        {
            //launch the browser
            webDriver = new ChromeDriver();
            webDriver.Navigate().GoToUrl("https://www.demo.bnz.co.nz/client/");
            Thread.Sleep(3000);
        }

        [TearDown]
        public void TearDown()
        {
            //quit the browser
            webDriver.Quit();
        }
    }
}
