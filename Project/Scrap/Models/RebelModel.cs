using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Scrap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Scrap.Models
{
    class RebelModel
    {
        string[] titles = { "Home | Swagbucks", "nCrave | Swagbucks", "www.swagbucks.com/?", "Entertainmentcrave.com" };
        public RebelModel(string username, string password, BackgroundWorker bw)
        {

            ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("user-data-dir=" + App.Folder + "profilePR");


            IWebDriver driver = new ChromeDriver(service, options);

            driver.Navigate().GoToUrl("http://www.prizerebel.com");

            Helpers.ByClass(driver, "ss-icon");

            try
            {
                driver.FindElement(By.Id("loginFormEmail")).SendKeys(username);
                driver.FindElement(By.ClassName("hero-form-first-password")).SendKeys(password);
            }
            catch { }

            Helpers.ById(driver, "loginSubmit");

            Helpers.wait(1000);

            Helpers.ById(driver, "earn-tour-step-3");
        }
    }
}
