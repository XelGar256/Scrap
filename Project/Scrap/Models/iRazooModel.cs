using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrap.Classes;

namespace Scrap.Models
{
    class iRazooModel
    {
        public iRazooModel(string username, string password, BackgroundWorker bw)
        {
            string[] titles = { "PaidVerts" };
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("user-data-dir=" + App.Folder + "profileIR");

            IWebDriver driver = new ChromeDriver(service, options);

            driver.Navigate().GoToUrl("http://www.irazoo.com");

            Helpers.wait(500);

            Helpers.ByClass(driver, "BtnLogIn");

        }
    }
}