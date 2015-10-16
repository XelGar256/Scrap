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

            //Helpers.ByClass(driver, "earn-tour-step-2");
            driver.Navigate().GoToUrl("http://www.prizerebel.com/dailypoints.php");
            dailyPoints(driver);
        }

        public static void dailyPoints(IWebDriver driver)
        {
            string[] titles = { "PrizeRebel.com | Earn" };

            while (true)
            {
                Helpers.switchToBrowserByString(driver, "PrizeRebel.com | Earn");
                try
                {
                    try
                    {
                        driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#displayWrap iframe")));
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
                        IWebElement dropDownMonth = driver.FindElement(By.Id("dob_month"));
                        IWebElement dropDownDay = driver.FindElement(By.Id("dob_day"));
                        IWebElement dropDownYear = driver.FindElement(By.Id("dob_year"));
                        string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                        Random random = new Random();
                        int rndMonth = random.Next(0, 11);
                        Console.WriteLine(rndMonth);
                        SelectElement clickThis = new SelectElement(dropDownMonth);
                        clickThis.SelectByText(months[rndMonth]);
                        Helpers.wait(1000);
                        int rndDay = random.Next(1, 28);
                        clickThis = new SelectElement(dropDownDay);
                        clickThis.SelectByText(rndDay.ToString());
                        Helpers.wait(1000);
                        int rndYear = random.Next(1974, 1994);
                        clickThis = new SelectElement(dropDownYear);
                        clickThis.SelectByText(rndYear.ToString());
                        Helpers.wait(1000);
                    }
                    catch { }
                    finally { }

                    try
                    {
                        driver.FindElement(By.Id("demosubmitimg")).Click();
                    }
                    catch { }
                    finally { }
                }
                catch { }
                finally { }

                Helpers.switchToBrowserByString(driver, "PrizeRebel.com | Earn");
                try
                {
                    driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#displayWrap iframe")));
                }
                catch { }
                finally { }

                Helpers.ById(driver, "webtraffic_popup_start_button");
                Helpers.ById(driver, "webtraffic_popup_next_button");
                Helpers.ByClass(driver, "webtraffic_start_button");
                Helpers.ByClass(driver, "webtraffic_next_button");

                try
                {
                    Helpers.switchToBrowserByString(driver, "Now Exploring great content!");
                    while (driver.Title.Contains("Now Exploring"))
                    {
                        Helpers.switchToBrowserByString(driver, "Now Exploring great content!");
                        try
                        {
                            IWebElement greatContent = driver.FindElement(By.ClassName("nextstepimg"));
                            greatContent.Click();
                        }
                        catch
                        {
                            Console.WriteLine("Waiting to finish");
                            try
                            {
                                driver.FindElement(By.XPath("//img[@alt='Claim your reward']")).Click();
                                Helpers.switchToBrowserByString(driver, "Offer Walls");
                            }
                            catch { }
                            finally { }
                            Helpers.wait(5000);
                        }
                    }
                }
                catch { }
                finally { }

                Helpers.switchToBrowserByString(driver, "PrizeRebel.com | Earn");

                try
                {
                    driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#displayWrap iframe")));
                }
                catch { }
                finally { }

                try
                {
                    if (driver.FindElement(By.Id("ty_header")).Text.Contains("Points"))
                    {
                        Helpers.closeWindows(driver, titles);
                        Console.WriteLine("I'm Here!!");
                        driver.SwitchTo().ParentFrame();
                        Console.WriteLine("Attempting Refresh");
                        driver.Navigate().GoToUrl("http://www.prizerebel.com/ripply.php");
                        Helpers.wait(1000);
                        driver.Navigate().GoToUrl("http://www.prizerebel.com/dailypoints.php");
                        //driver.Navigate().Refresh();
                        Console.WriteLine("Refresh Complete");
                    }
                }
                catch { }
                finally { }
                Helpers.switchToBrowserByString(driver, "PrizeRebel.com | Earn");
                try
                {
                    driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#displayWrap iframe")));
                }
                catch { }
                finally { }
                Helpers.wait(5000);
            }
        }
    }
}
