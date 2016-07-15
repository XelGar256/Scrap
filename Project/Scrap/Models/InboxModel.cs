using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Scrap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Scrap.Models
{
    class InboxModel
    {
        string[] titles = { "Cash Videos" };

        public InboxModel(string username, string password, BackgroundWorker bw)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("user-data-dir=" + App.Folder + "profileIB");

            IWebDriver driver = new ChromeDriver(service, options);
            driver.Navigate().GoToUrl("http://www.inboxdollars.com");

            try
            {
                driver.FindElement(By.Id("loginname")).Clear();
                driver.FindElement(By.Id("pwd")).Clear();
                driver.FindElement(By.Id("loginname")).SendKeys(username);
                driver.FindElement(By.Id("pwd")).SendKeys(password);
                Helpers.wait(1000);
                driver.FindElement(By.ClassName("submit2")).Click();
            }
            catch { }

            try
            {
                driver.FindElement(By.ClassName("videos")).Click();
                videos(driver);
            }
            catch { }
        }

        void videos(IWebDriver driver)
        {
            bool clicked = false;
            while (true)
            {
                try
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<string> MorewindowHandles = driver.WindowHandles;

                    foreach (String window in MorewindowHandles)
                    {
                        try
                        {
                            IWebDriver popup = driver.SwitchTo().Window(window);
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().DefaultContent();

                            if (!clicked)
                            {
                                IList<IWebElement> offerButtons = driver.FindElements(By.ClassName("offerButton"));
                                foreach (IWebElement offerButton in offerButtons)
                                {
                                    if (offerButtons.Count < 1)
                                    {
                                        break;
                                    }
                                    offerButton.Click();
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            try
                            {
                                driver.FindElement(By.LinkText("Earn Now")).Click();
                            }
                            catch { }
                        }

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
                            driver.FindElement(By.Id("demosubmitimg")).Click();
                        }
                        catch { }

                        Helpers.ByClass(driver, "video-poster-play-icon");

                        try
                        {
                            /*
                            Actions builder = new Actions(driver);
                            Helpers.wait(1000);
                            IWebElement vidLink = driver.FindElement(By.ClassName("engagement"));
                            Helpers.wait(1000);
                            builder.MoveToElement(vidLink).Build().Perform();
                            //vidLink.Click();
                            */
                            if (driver.FindElement(By.ClassName("engagement")).Displayed)
                            {
                                driver.Quit();
                            }
                        }
                        catch { }

                        try
                        {
                            IList<IWebElement> choices = driver.FindElements(By.ClassName("button"));
                            foreach (IWebElement choice in choices)
                            {
                                choice.Click();
                            }
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.Id("adk_inter_cnt")).Text == "00:00")
                            {
                                driver.FindElement(By.Id("adk_inter_close")).Click();
                                Helpers.wait(5000);
                                Helpers.ByClass(driver, "videos");
                                Helpers.closeWindows(driver, titles);
                                clicked = false;
                            }
                        }
                        catch { }

                        try
                        {
                            Helpers.switchFrameByNumber(driver, 0);
                            driver.FindElement(By.ClassName("hd-container-header-action")).Click();
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().DefaultContent();
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#social_videos_content iframe")));
                            if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                            {
                                driver.SwitchTo().DefaultContent();
                                Helpers.ById(driver, "jungroupSubmit");
                            }
                        }
                        catch { }

                        try
                        {
                            Helpers.switchFrameByNumber(driver, 0);
                            Helpers.switchFrameByNumber(driver, 0);
                            Helpers.switchFrameByNumber(driver, 0);
                            Helpers.switchFrameByNumber(driver, 0);
                            driver.FindElement(By.ClassName("ytp-large-play-button")).Click();
                        }
                        catch { }

                        Helpers.wait(5000);
                    }

                    clicked = Helpers.lookFor(driver, "for E-Mail");

                    if (Helpers.lookFor(driver, "Entertainmentcrave.com"))
                    {
                        encrave(driver);
                    }
                }
                catch { }
            }
        }

        void encrave(IWebDriver driver)
        {
            bool clickPlayer = false;
            bool nCraveLoop = true;
            bool checks = false;

            string[] titles = { "Cash Videos - Earn", "Entertainmentcrave.com" };

            Helpers.switchToBrowserByString(driver, "Entertainmentcrave.com");

            int hr = DateTime.Now.Hour;

            while (nCraveLoop)
            {
                System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                foreach (String window in windowHandles)
                {
                    try
                    {
                        IWebDriver popup = driver.SwitchTo().Window(window);
                    }
                    catch { }

                    if (!Helpers.lookFor(driver, "Entertainmentcrave"))
                    {
                        nCraveLoop = false;
                    }

                    if (hr != DateTime.Now.Hour)
                    {
                        Helpers.switchToBrowserByString(driver, "Entertainmentcrave");
                        driver.Close();
                        nCraveLoop = false;
                    }
                    //Start of Slides
                    Helpers.switchToBrowserByString(driver, "Entertainmentcrave");
                    Helpers.switchToBrowserFrameByString(driver, "contIframe");

                    Helpers.ByClass(driver, "flite-close-button");

                    Helpers.switchToBrowserByString(driver, "Entertainmentcrave");
                    Helpers.switchToBrowserFrameByString(driver, "contIframe");

                    Helpers.switchFrameByNumber(driver, 0);
                    Helpers.ByClass(driver, "video-poster-play-icon");

                    try
                    {
                        driver.FindElement(By.ClassName("next")).FindElement(By.CssSelector("a[href*='nextPage']")).Click();
                    }
                    catch { }

                    Helpers.wait(1000);

                    try
                    {
                        driver.FindElement(By.ClassName("wp-post-navigation-next")).Click();
                    }
                    catch { }

                    Helpers.wait(1000);

                    try
                    {
                        driver.FindElement(By.ClassName("wp-post-navigation-next-1")).Click();
                    }
                    catch { }

                    Helpers.wait(1000);

                    try
                    {
                        driver.FindElement(By.ClassName("bx-next")).Click();
                    }
                    catch { }

                    Helpers.wait(1000);

                    try
                    {
                        driver.FindElement(By.ClassName("GalleryBig-nextArrow")).Click();
                    }
                    catch { }

                    Helpers.wait(1000);

                    try
                    {
                        driver.FindElement(By.ClassName("GalleryBig")).Click();
                    }
                    catch { }

                    Helpers.wait(1000);

                    try
                    {
                        Helpers.switchToBrowserByString(driver, "Entertainmentcrave");
                        Helpers.switchToBrowserFrameByString(driver, "contIframe");

                        driver.FindElement(By.ClassName("owl-buttons")).FindElement(By.ClassName("owl-next")).Click();
                    }
                    catch { }

                    try
                    {
                        Helpers.switchToBrowserByString(driver, "Entertainmentcrave");
                        Helpers.switchToBrowserFrameByString(driver, "contIframe");

                        driver.FindElement(By.ClassName("owl-buttons")).SendKeys(Keys.PageDown);
                    }
                    catch { }

                    try
                    {
                        Helpers.switchToBrowserByString(driver, "Entertainmentcrave");
                        Helpers.switchToBrowserFrameByString(driver, "contIframe");

                        driver.FindElement(By.ClassName("owl-next")).Click();
                    }
                    catch { }

                    Helpers.wait(1000);

                    try
                    {
                        Helpers.switchToBrowserByString(driver, "Entertainmentcrave");
                        Helpers.switchToBrowserFrameByString(driver, "contIframe");

                        driver.FindElement(By.ClassName("gallery-counters")).SendKeys(Keys.PageUp);
                        Helpers.wait(1000);
                        driver.FindElement(By.LinkText("Next")).Click();
                    }
                    catch { }

                    Helpers.wait(1000);

                    try
                    {
                        Helpers.switchToBrowserByString(driver, "Entertainmentcrave");
                        Helpers.switchToBrowserFrameByString(driver, "contIframe");

                        driver.FindElement(By.ClassName("gallery-counters")).SendKeys(Keys.PageUp);
                        Helpers.wait(1000);
                        driver.FindElement(By.ClassName("gallery-counters")).SendKeys(Keys.PageUp);
                        Helpers.wait(1000);
                        driver.FindElement(By.LinkText("Next")).Click();
                    }
                    catch { }

                    Helpers.wait(1000);

                    try
                    {
                        driver.FindElement(By.ClassName("btn-slideshow")).Click();
                    }
                    catch { }
                    //End of Slides

                    Helpers.wait(1000);

                    //Video
                    try
                    {
                        if (!clickPlayer)
                        {
                            IWebElement clickNext = driver.FindElement(By.ClassName("vdb_player"));
                            clickNext.Click();
                            clickPlayer = true;
                        }
                    }
                    catch { }
                    //End Video

                    Helpers.wait(1000);

                    //Check Marks
                    try
                    {
                        if (!driver.FindElement(By.ClassName("navPages")).Displayed)
                        {
                            checks = true;
                        }
                    }
                    catch { }

                    try
                    {
                        if (!checks)
                        {
                            IList<IWebElement> urlLinks = driver.FindElements(By.ClassName("url-link"));
                            if (urlLinks.Count > 0)
                            {
                                foreach (IWebElement urlLink in urlLinks)
                                {
                                    Random rnd = new Random();
                                    urlLink.Click();
                                    Helpers.wait(1000 * rnd.Next(60, 90));
                                    Helpers.switchToBrowserByString(driver, "Entertainmentcrave");
                                }

                                System.Collections.ObjectModel.ReadOnlyCollection<string> awindowHandles = driver.WindowHandles;

                                foreach (String awindow in awindowHandles)
                                {
                                    try
                                    {
                                        IWebDriver popup = driver.SwitchTo().Window(awindow);
                                    }
                                    catch { }

                                    if (driver.Title.Contains("Entertainmentcrave"))
                                    {
                                        Helpers.switchToBrowserByString(driver, driver.Title);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    catch { }

                    try
                    {
                        driver.SwitchTo().DefaultContent();
                    }
                    catch { }

                    //Link Up or Down
                    try
                    {
                        if (driver.FindElement(By.ClassName("active")).Displayed)
                        {

                            Random upDownRnd = new Random();
                            if (upDownRnd.Next(1, 2) < 2)
                            {
                                try
                                {
                                    IWebElement up = driver.FindElement(By.Id("link_up"));
                                    up.Click();
                                    clickPlayer = false;
                                }
                                catch { }
                            }
                            else
                            {
                                try
                                {

                                    IWebElement down = driver.FindElement(By.Id("link_down"));
                                    down.Click();
                                    clickPlayer = false;
                                }
                                catch { }
                            }
                        }
                    }
                    catch { }

                    //Keep Craving
                    try
                    {
                        driver.FindElement(By.ClassName("keepCraving")).Click();
                        Helpers.closeWindows(driver, titles);
                        clickPlayer = false;
                        checks = false;
                    }
                    catch { }
                }
            }
        }
    }
}