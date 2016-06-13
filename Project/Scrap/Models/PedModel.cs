using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Scrap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Scrap.Models
{
    class PedModel
    {
        string[] titles = { "nothing is" };

        public PedModel(string username, string password, BackgroundWorker bw, bool openPed)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("user-data-dir=" + App.Folder + "profilePed");

            IWebDriver driver = new ChromeDriver(service, options);
            driver.Navigate().GoToUrl("http://www.pedtoclick.com/index.php?view=login");

            try
            {
                driver.FindElement(By.Name("username")).SendKeys(username);
                driver.FindElement(By.Name("password")).SendKeys(password);
                driver.FindElement(By.Name("password")).SendKeys(Keys.Enter);
            }
            catch { }
            finally { }
            Helpers.ByClass(driver, "login");

            Helpers.wait(5000);
            if (!openPed)
            {
                driver.Navigate().GoToUrl("http://www.pedtoclick.com/superRewards.php");

                Helpers.wait(10000);

                IList<IWebElement> iframes = driver.FindElements(By.TagName("iframe"));
                foreach (IWebElement iframe in iframes)
                {
                    Debug.WriteLine(iframe.Text);
                }

                Helpers.switchToBrowserByString(driver, "PEDtoClick");
                Helpers.switchFrameByNumber(driver, 0);
                try
                {
                    IList<IWebElement> lis = driver.FindElements(By.TagName("li"));
                    Debug.WriteLine(lis.Count);
                    IList<IWebElement> navOptions = driver.FindElements(By.ClassName("nav-option"));
                    Debug.WriteLine(navOptions.Count);
                }
                catch { }

                Helpers.switchToBrowserByString(driver, "PEDtoClick");
                Helpers.switchFrameByNumber(driver, 1);
                try
                {
                    IList<IWebElement> lis = driver.FindElements(By.TagName("li"));
                    Debug.WriteLine(lis.Count);
                    IList<IWebElement> navOptions = driver.FindElements(By.ClassName("nav-option"));
                    Debug.WriteLine(navOptions.Count);
                }
                catch { }

                Helpers.switchToBrowserByString(driver, "PEDtoClick");
                Helpers.switchFrameByNumber(driver, 2);
                try
                {
                    IList<IWebElement> lis = driver.FindElements(By.TagName("li"));
                    Debug.WriteLine(lis.Count);
                    IList<IWebElement> navOptions = driver.FindElements(By.ClassName("nav-option"));
                    Debug.WriteLine(navOptions.Count);
                    driver.FindElement(By.LinkText("Video")).Click();

                    Helpers.wait(5000);

                    try
                    {
                        IList<IWebElement> h2s = driver.FindElements(By.TagName("h2"));
                        foreach (IWebElement h2 in h2s)
                        {
                            if (h2.Text.Contains("HyprMX"))
                            {
                                h2.Click();
                                superRewards(driver);
                            }
                        }
                    }
                    catch { }
                }
                catch { }

            }
        }

        void superRewards(IWebDriver driver)
        {
            string[] titles = { "PEDtoClick" };

            while (true)
            {
                System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                foreach (String window in windowHandles)
                {
                    try
                    {
                        IWebDriver popup = driver.SwitchTo().Window(window);
                    }
                    catch { }

                    Helpers.switchToBrowserByString(driver, "PEDtoClick");
                    Helpers.switchFrameByNumber(driver, 2);
                    try
                    {
                        IList<IWebElement> lis = driver.FindElements(By.TagName("li"));
                        Debug.WriteLine(lis.Count);
                        IList<IWebElement> navOptions = driver.FindElements(By.ClassName("nav-option"));
                        Debug.WriteLine(navOptions.Count);
                        driver.FindElement(By.LinkText("Video")).Click();

                        Helpers.wait(5000);

                        try
                        {
                            IList<IWebElement> h2s = driver.FindElements(By.TagName("h2"));
                            foreach (IWebElement h2 in h2s)
                            {
                                if (h2.Text.Contains("HyprMX"))
                                {
                                    h2.Click();
                                    superRewards(driver);
                                }
                            }
                        }
                        catch { }
                    }
                    catch { }

                    Helpers.switchToBrowserByString(driver, "PEDtoClick");
                    Helpers.switchFrameByNumber(driver, 2);

                    Helpers.switchFrameByNumber(driver, 0);
                    Helpers.switchFrameByNumber(driver, 0);

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

                    try
                    {
                        driver.FindElement(By.Id("demosubmitimg")).Click();
                    }
                    catch { }

                    Helpers.ById(driver, "webtraffic_popup_start_button");
                    Helpers.ById(driver, "webtraffic_popup_next_button");
                    Helpers.ByClass(driver, "webtraffic_start_button");
                    Helpers.ByClass(driver, "webtraffic_next_button");

                    try
                    {
                        if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                        {
                            Helpers.closeWindows(driver, titles);
                            driver.Navigate().GoToUrl("http://www.pedtoclick.com/index.php?view=login");
                            driver.Navigate().GoToUrl("http://www.pedtoclick.com/superRewards.php");
                        }
                    }
                    catch { }

                    Helpers.switchToBrowserByString(driver, "PEDtoClick");
                    Helpers.switchFrameByNumber(driver, 2);
                    Helpers.switchFrameByNumber(driver, 0);
                    Helpers.switchFrameByNumber(driver, 0);

                    try
                    {
                        if (driver.FindElement(By.Id("ty_headline")).Displayed)
                        {
                            Helpers.closeWindows(driver, titles);
                            driver.Navigate().GoToUrl("http://www.pedtoclick.com/index.php?view=login");
                            driver.Navigate().GoToUrl("http://www.pedtoclick.com/superRewards.php");
                            Helpers.switchToBrowserByString(driver, "PEDtoClick");
                            Helpers.switchFrameByNumber(driver, 2);
                            try
                            {
                                IList<IWebElement> lis = driver.FindElements(By.TagName("li"));
                                Debug.WriteLine(lis.Count);
                                IList<IWebElement> navOptions = driver.FindElements(By.ClassName("nav-option"));
                                Debug.WriteLine(navOptions.Count);
                                driver.FindElement(By.LinkText("Video")).Click();

                                Helpers.wait(5000);

                                try
                                {
                                    IList<IWebElement> h2s = driver.FindElements(By.TagName("h2"));
                                    foreach (IWebElement h2 in h2s)
                                    {
                                        if (h2.Text.Contains("HyprMX"))
                                        {
                                            h2.Click();
                                        }
                                    }
                                }
                                catch { }
                            }
                            catch { }
                        }
                    }
                    catch { }
                }
            }
        }
    }
}
