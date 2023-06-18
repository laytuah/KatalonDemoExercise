using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatalonDemoExercise.Hooks
{
    public class Context
    {
        public IWebDriver driver;
        //string baseUrl = "https://cms.demo.katalon.com/";
        string baseUrl = EnvironmentData.baseUrl;

        public void LoadKatalonDemoApplication()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Navigate().GoToUrl(baseUrl);
            driver.Manage().Window.FullScreen();
        }

        public void CloseKatalonDemoApplication()
        {
            driver.Quit();
        }
    }
}
