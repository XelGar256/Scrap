using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Scrap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Scrap.Models
{
    class GrindaModel
    {
        public GrindaModel(string username, string password, BackgroundWorker bw, bool openGrinda)
        {
            string numbers = "";

            ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("user-data-dir=" + App.Folder + "profileGB");

            IWebDriver driver = new ChromeDriver(service, options);
            driver.Navigate().GoToUrl("http://www.grindabuck.com/login");

            try
            {
                driver.FindElement(By.Id("login_username")).SendKeys(username);
                driver.FindElement(By.Id("pwd")).SendKeys(password);

                driver.FindElement(By.ClassName("btn-lg")).Click();
            }
            catch { }

            IList<IWebElement> smalls = driver.FindElements(By.TagName("small"));
            foreach (IWebElement small in smalls)
            {
                if (small.Text.Contains("Last Checkin"))
                {
                    numbers = small.Text;
                    break;
                }
            }

            if (DateTime.Parse(numbers.Split(' ')[2]).DayOfYear != DateTime.Now.DayOfYear)
            {
                checkIn(driver);
            }


        }

        void checkIn(IWebDriver driver)
        {
            try
            {
                //driver.FindElement(By.ClassName("btn-primary")).Click();
                driver.FindElement(By.LinkText("CHECKED IN")).Click();
                driver.FindElement(By.ClassName("bg-success")).Click();
            }
            catch { }
        }
    }
}
