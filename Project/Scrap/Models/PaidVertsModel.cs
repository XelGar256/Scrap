using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrap.Classes;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace Scrap.Models
{
    class PaidVertsModel
    {
        public PaidVertsModel(string username, string password, BackgroundWorker bw)
        {
            string[] titles = { "PaidVerts" };
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("user-data-dir=" + App.Folder + "profilePV");


            IWebDriver driver = new ChromeDriver(service, options);

            driver.Navigate().GoToUrl("https://www.paidverts.com/");

            while (driver.Title.Contains("Home") && !driver.FindElement(By.LinkText("HOME")).Displayed)
            {
                Helpers.ByClass(driver, "login_account");
            }

            Helpers.wait(1000);
            try {
                driver.FindElement(By.Id("email")).SendKeys(username);
                driver.FindElement(By.Id("password")).SendKeys(password);
            }
            catch { }

            while (driver.Title.Contains("login"))
            { 

            }

            Helpers.wait(5000);
            driver.FindElement(By.LinkText("HOME")).Click();
            Helpers.wait(5000);

            Helpers.ByClass(driver, "menu_link_offers");

            IList<IWebElement> bigGreen = driver.FindElements(By.ClassName("button_green_big"));
            int counter = 0;
            foreach (IWebElement button in bigGreen)
            {
                if(counter == bigGreen.Count - 1)
                {
                    button.Click();
                }
                counter++;
            }

            bool cashOffers = true;
            bool looping = true;

            while (cashOffers)
            {
                Helpers.wait(5000);
                Helpers.switchToBrowserByString(driver, "Cash Offers");

                Helpers.switchFrameByNumber(driver, 0);
                try
                {
                    driver.FindElement(By.LinkText("Video")).Click();
                }
                catch { }
                finally { }

                Helpers.wait(5000);

                IList<IWebElement> offers = driver.FindElements(By.ClassName("offer_text"));
                counter = 0;

                foreach (IWebElement offer in offers)
                {
                    if (offer.Text.Contains("HyprMX"))
                    {
                        offer.Click();
                        break;
                    }
                    else
                    {
                        if (counter == offers.Count - 1)
                        {
                            driver.SwitchTo().DefaultContent();
                            driver.FindElement(By.LinkText("HOME")).Click();
                            cashOffers = false;
                            looping = false;
                        }
                    }
                    counter++;
                }

                Helpers.wait(5000);

                try
                {
                    driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#view7-frame iframe")));
                    driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#video_main iframe")));
                }
                catch { }
                finally { }

                try
                {
                    IWebElement dropDownMonth = driver.FindElement(By.Id("dob_month"));
                    IWebElement dropDownDay = driver.FindElement(By.Id("dob_day"));
                    IWebElement dropDownYear = driver.FindElement(By.Id("dob_year"));
                    string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                    Random random = new Random();
                    int rndMonth = random.Next(0, 11);
                    Console.WriteLine(rndMonth);
                    SelectElement clickThis = new SelectElement(dropDownMonth);
                    clickThis.SelectByText(months[rndMonth]);
                    Helpers.wait(3000);
                    int rndDay = random.Next(1, 28);
                    clickThis = new SelectElement(dropDownDay);
                    clickThis.SelectByText(rndDay.ToString());
                    Helpers.wait(3000);
                    int rndYear = random.Next(1974, 1994);
                    clickThis = new SelectElement(dropDownYear);
                    clickThis.SelectByText(rndYear.ToString());
                    Helpers.wait(3000);
                }
                catch { }
                finally { }

                try
                {
                    IList<IWebElement> oLinks = driver.FindElements(By.ClassName("singleselectset_radio"));
                    Random random = new Random();
                    int rndClick = random.Next(1, oLinks.Count);
                    Console.WriteLine(rndClick);
                    int counterClick = 1;
                    foreach (IWebElement oLink in oLinks)
                    {
                        Console.WriteLine(counterClick);
                        if (counterClick == rndClick)
                        {
                            oLink.Click();
                        }
                        counterClick++;
                    }
                }
                catch { }
                finally { }

                try
                {
                    driver.FindElement(By.Id("demosubmitimg")).Click();
                }
                catch { }
                finally { }

                while (looping)
                {
                    Helpers.ById(driver, "expository_image");
                    try
                    {
                        if (driver.FindElement(By.Id("ty_body_text")).Displayed)
                            looping = false;
                                }
                    catch { }
                    finally { }

                    Helpers.ById(driver, "webtraffic_popup_start_button");
                    Helpers.ById(driver, "webtraffic_popup_next_button");
                    Helpers.ByClass(driver, "webtraffic_start_button");
                    Helpers.ByClass(driver, "webtraffic_next_button");

                    try
                    {
                        if(driver.FindElement(By.Id("ty_header")).Displayed)
                            looping = false;
                    }
                    catch { }
                    finally { }

                }

                driver.Navigate().Refresh();

                Helpers.closeWindows(driver, titles);
            }

            cashGrid(driver);
            earnBAP(driver);
            /*
            */
        }

        public static void cashGrid(IWebDriver driver)
        {
            //class = playGameLink
            Helpers.ByClass(driver, "menu_cgrid");
            Console.WriteLine(driver.FindElement(By.ClassName("chances")).Text);
            Console.WriteLine(Regex.Match(driver.FindElement(By.ClassName("chances")).Text, @"\d+").Value);
            int chance;
            int.TryParse(Regex.Match(driver.FindElement(By.ClassName("chances")).Text, @"\d+").Value, out chance);

            while (chance != 0)
            {
                try
                {
                    driver.FindElement(By.ClassName("playGameLink")).Click();
                }
                catch { }
                finally { }
                Helpers.wait(5000);

                try
                {
                    driver.FindElement(By.Id("text-1")).SendKeys(driver.FindElement(By.Id("t-1")).Text);
                }
                catch { }
                finally { }
                Helpers.ById(driver, "view_ad");

                try
                {


                while (driver.Title.Contains("Paid Ad"))
                {

                }
                }
                catch { }
                finally { }
            }
        }
        public static void earnBAP(IWebDriver driver)
        {
            while (true)
            {
                Helpers.ByClass(driver, "menu_link_paidad");

                Helpers.ById(driver, "view-1");

                try {
                    driver.FindElement(By.Id("text-1")).SendKeys(driver.FindElement(By.Id("t-1")).Text);
                    driver.FindElement(By.Id("text-2")).SendKeys(driver.FindElement(By.Id("t-2")).Text);
                    driver.FindElement(By.Id("text-3")).SendKeys(driver.FindElement(By.Id("t-3")).Text);
                }
                catch { }
                finally { }

                Helpers.ById(driver, "view_ad");

                try {
                    while (driver.Title.Contains("Paid Ad"))
                    {

                    }
                }
                catch { }
                finally { }
            }
        }

    }
}
