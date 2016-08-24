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
        public RebelModel(string username, string password, BackgroundWorker bw, bool openRebel)
        {
            while (true)
            {
                ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
                service.HideCommandPromptWindow = true;

                ChromeOptions options = new ChromeOptions();
                options.AddArgument("start-maximized");
                options.AddArgument("user-data-dir=" + App.Folder + "profilePR");

                //if (openRebel)
                //options.AddAdditionalCapability("mobileEmulation", new Dictionary<string, string> { { "deviceName", "Google Nexus 5" } });

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

                if (!openRebel)
                {
                    driver.Navigate().GoToUrl("http://www.prizerebel.com/dailypoints.php");
                    dailyPoints(driver);
                    driver.Navigate().GoToUrl("http://www.prizerebel.com/offerwalls.php");
                    Helpers.wait(5000);

                    try
                    {
                        IList<IWebElement> virools = driver.FindElements(By.ClassName("filter-tab-btn"));
                        foreach (IWebElement virool in virools)
                        {
                            if (virool.Text == "Virool")
                            {
                                virool.Click();
                                break;
                            }
                        }
                        virool(driver);
                        videos(driver);
                    }
                    catch { }

                    driver.Navigate().GoToUrl("http://www.prizerebel.com/offerwalls.php");
                    Helpers.wait(5000);
                    try
                    {
                        IList<IWebElement> virools = driver.FindElements(By.ClassName("filter-tab-btn"));
                        foreach (IWebElement virool in virools)
                        {
                            if (virool.Text == "Encrave")
                            {
                                virool.Click();
                                break;
                            }
                        }
                        encrave(driver);
                    }
                    catch { }
                    driver.Quit();
                }
                else
                {
                    break;
                }
            }
        }

        void encrave(IWebDriver driver)
        {
            bool clickPlayer = false;
            bool nCraveLoop = true;
            bool checks = false;

            string[] titles = { "PrizeRebel.com | Earn", "Entertainmentcrave.com" };

            Helpers.wait(5000);
            Helpers.switchToBrowserFrameByStringClass(driver, "iframeOfferWall");
            Helpers.wait(5000);
            Helpers.switchFrameByNumber(driver, 0);

            try
            {
                Helpers.wait(5000);
                int textCounters = 0;
                IList<IWebElement> disableTexts = driver.FindElements(By.ClassName("disableText"));
                foreach (IWebElement disableText in disableTexts)
                {
                    Helpers.wait(5000);
                    if (textCounters == disableTexts.Count - 1)
                    {
                        disableText.Click();
                    }
                    textCounters++;
                }
            }
            catch { }

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
                        nCraveLoop = false;
                    }
                    //Start of Slides
                    Helpers.switchToBrowserByString(driver, "Entertainmentcrave");
                    Helpers.switchToBrowserFrameByString(driver, "contIframe");

                    Helpers.ByClass(driver, "flite-close-button");

                    Helpers.switchToBrowserByString(driver, "Entertainmentcrave");
                    Helpers.switchToBrowserFrameByString(driver, "contIframe");

                    try
                    {
                        driver.FindElement(By.ClassName("next")).FindElement(By.CssSelector("a[href*='nextPage']")).Click();
                    }
                    catch { }

                    Helpers.wait(1000);

                    Helpers.ByClass(driver, "_2");

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
                        driver.FindElement(By.ClassName("start-slideshow_btn")).Click();
                    }
                    catch { }

                    Helpers.wait(1000);

                    try
                    {
                        driver.FindElement(By.ClassName("next_btn")).Click();
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

        void trialPay(IWebDriver driver)
        {
            string[] titles = { "PrizeRebel.com | Earn" };
            int currentHour = DateTime.Now.Hour;
            bool loop = true;

            while (loop)
            {

                Helpers.wait(2000);
                Helpers.switchFrameByNumber(driver, 0);
                try
                {
                    driver.FindElement(By.LinkText("Videos")).Click();
                }
                catch { }

                try
                {
                    if (currentHour != DateTime.Now.Hour)
                    {
                        driver.Navigate().GoToUrl("http://www.prizerebel.com/offerwalls.php");
                        Helpers.closeWindows(driver, titles);
                        loop = false;
                    }
                }
                catch { }

                //ByClass(driver, "offer_row");
                try
                {
                    IList<IWebElement> offerRows = driver.FindElements(By.ClassName("largeOffer"));
                    foreach (IWebElement offerRow in offerRows)
                    {
                        Helpers.wait(1000);

                        if (offerRow.FindElement(By.ClassName("summary")).FindElement(By.ClassName("information")).FindElement(By.ClassName("requirements")).Text.Contains("video"))
                        {
                            offerRow.Click();
                            break;
                        }
                    }
                }
                catch { }

                Helpers.switchToBrowserByString(driver, "TrialPay");
                Helpers.wait(1000);
                Helpers.switchToBrowserFrameByString(driver, "tp-video-player");
                Helpers.ByClass(driver, "btn-text");

                Helpers.wait(5000);

                Helpers.switchToBrowserByString(driver, "Social Ingot");

                try
                {
                    driver.FindElement(By.TagName("img")).Click();
                }
                catch { }

                Helpers.wait(90000);

                Helpers.closeWindows(driver, titles);

                driver.Close();
                Helpers.switchToBrowserByString(driver, "Social Ingot");
                driver.Close();
                Helpers.switchToBrowserByString(driver, "TrialPay");
                driver.Close();
                Helpers.switchToBrowserByString(driver, "PrizeRebel.com");

                Helpers.closeWindows(driver, titles);
            }
        }

        public static void videos(IWebDriver driver)
        {
            bool looping = true;
            int currentHour = DateTime.Now.Hour;
            driver.Navigate().GoToUrl("http://www.prizerebel.com/ripply.php");

            while (looping)
            {
                System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                foreach (String window in windowHandles)
                {
                    IWebDriver popup = driver.SwitchTo().Window(window);

                    try
                    {
                        if (currentHour != DateTime.Now.Hour)
                        {
                            driver.Navigate().GoToUrl("http://www.prizerebel.com/members.php");
                            looping = false;
                        }
                    }
                    catch { }

                    IList<IWebElement> tests = driver.FindElements(By.ClassName("playHover"));
                    Console.WriteLine("playHover: " + tests.Count);
                    foreach (IWebElement test in tests)
                    {
                        try
                        {
                            test.Click();
                        }
                        catch { }
                        Helpers.wait(60000);
                    }

                    Helpers.wait(5000);

                    if (driver.FindElement(By.ClassName("small-9")).Text.Contains("Please try back later."))
                    {
                        looping = false;
                    }

                }

                driver.Navigate().Refresh();
            }

        }

        public static void virool(IWebDriver driver)
        {
            int points = 0, newpoints = 0;
            int currentHour = DateTime.Now.Hour;
            bool looping = true, looped = false;
            int counter = 0;
            while (looping)
            {
                try
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                    foreach (String window in windowHandles)
                    {
                        IWebDriver popup = driver.SwitchTo().Window(window);
                    }

                    try
                    {
                        driver.SwitchTo().DefaultContent();
                    }
                    catch { }

                    try
                    {
                        int.TryParse(driver.FindElement(By.ClassName("navigation-account-points-value")).Text, out points);
                    }
                    catch { }

                    try
                    {
                        if (driver.FindElement(By.Id("offerWallContent")).Text == "Please try back later.")
                        {
                            looping = false;
                        }
                    }
                    catch { }

                    Helpers.switchFrameByNumber(driver, 0);
                    counter = 0;

                    Helpers.wait(5000);

                    //ByClass(driver, "thumbnail");
                    IList<IWebElement> thumbnails = driver.FindElements(By.ClassName("thumbnail"));
                    Console.WriteLine("Thumbnails Count: " + thumbnails.Count);
                    if (thumbnails.Count == 0)
                    {
                        looping = false;
                    }
                    foreach (IWebElement thumbnail in thumbnails)
                    {
                        if (counter > thumbnails.Count - 2)
                        {
                            thumbnail.Click();
                            looped = true;
                        }

                        counter++;
                    }


                    while (looped)
                    {
                        //switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchToBrowserFrameByString(driver, "widgetPlayer");
                        try
                        {
                            IList<IWebElement> h1s = driver.FindElements(By.TagName("h1"));
                            foreach (IWebElement h1 in h1s)
                            {
                                if (h1.Text.Contains("504 Gateway"))
                                {
                                    driver.SwitchTo().DefaultContent();
                                    Helpers.switchFrameByNumber(driver, 0);
                                    Helpers.ByClass(driver, "close");
                                    looped = false;
                                    looping = false;
                                }
                            }
                        }
                        catch { }

                        try
                        {
                            IWebElement fbPageUp = driver.FindElement(By.ClassName("fb-share-btn"));
                            fbPageUp.SendKeys(Keys.PageUp);
                            Helpers.wait(1000);
                            fbPageUp.SendKeys(Keys.PageUp);
                            Helpers.wait(1000);
                            fbPageUp.SendKeys(Keys.PageUp);
                            Helpers.wait(1000);
                            fbPageUp.SendKeys(Keys.PageUp);
                            Helpers.wait(1000);
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.LinkText("Opt out")).Displayed)
                            {
                                driver.FindElement(By.LinkText("Opt out")).Click();
                                driver.SwitchTo().DefaultContent();
                                Helpers.switchFrameByNumber(driver, 0);
                                Helpers.ByClass(driver, "close");
                                looped = false;
                                looping = false;
                            }
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().DefaultContent();
                            Helpers.switchFrameByNumber(driver, 0);
                            IWebElement closing = driver.FindElement(By.ClassName("close"));
                            closing.SendKeys(Keys.PageUp);
                            Helpers.wait(1000);
                            closing.SendKeys(Keys.PageUp);
                            Helpers.wait(1000);
                            closing.SendKeys(Keys.PageUp);
                            Helpers.wait(1000);
                            closing.SendKeys(Keys.PageUp);
                            Helpers.wait(1000);
                        }
                        catch { }
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchToBrowserFrameByString(driver, "widgetPlayer");

                        Helpers.switchToBrowserFrameByString(driver, "player-container");

                        try
                        {
                            if (driver.FindElement(By.ClassName("videowall-still-info-bg")).Displayed)
                            {
                                driver.SwitchTo().DefaultContent();
                                Helpers.switchFrameByNumber(driver, 0);
                                Helpers.ByClass(driver, "close");
                                looped = false;
                            }
                        }
                        catch { }

                        Helpers.ByClass(driver, "ytp-large-play-button");

                        driver.SwitchTo().DefaultContent();
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);

                        try
                        {
                            if (driver.FindElement(By.LinkText("Opt out")).Displayed)
                            {
                                driver.SwitchTo().DefaultContent();
                                Helpers.switchFrameByNumber(driver, 0);
                                Helpers.ByClass(driver, "close");
                                looped = false;
                            }
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.ClassName("close")).Displayed)
                            {

                            }
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.Id("share-overlay-facebook")).Displayed)
                            {
                                //driver.FindElement(By.Id("share-overlay-facebook")).SendKeys(Keys.PageUp);
                                driver.SwitchTo().DefaultContent();
                                Helpers.switchFrameByNumber(driver, 0);
                                IWebElement closing = driver.FindElement(By.ClassName("close"));
                                closing.SendKeys(Keys.PageUp);
                                Helpers.wait(1000);
                                closing.SendKeys(Keys.PageUp);
                                Helpers.wait(1000);
                                closing.SendKeys(Keys.PageUp);
                                Helpers.wait(1000);
                                closing.SendKeys(Keys.PageUp);
                                Helpers.wait(1000);
                                Helpers.ByClass(driver, "close");
                                looped = false;
                            }
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.PartialLinkText("2 Gems")).Displayed)
                            {
                                driver.SwitchTo().DefaultContent();
                                Helpers.switchFrameByNumber(driver, 0);
                                Helpers.ByClass(driver, "close");
                                looped = false;
                                looping = false;
                            }
                        }
                        catch { }


                        try
                        {
                            if (driver.FindElement(By.Id("yt-subscribe-btn")).Displayed)
                            {
                                driver.SwitchTo().DefaultContent();
                                Helpers.switchFrameByNumber(driver, 0);
                                Helpers.ByClass(driver, "close");
                                looping = false;
                                looped = false;
                            }
                        }
                        catch { }

                        try
                        {
                            if (currentHour != DateTime.Now.Hour)
                            {
                                driver.SwitchTo().DefaultContent();
                                Helpers.switchFrameByNumber(driver, 0);
                                Helpers.ByClass(driver, "close");
                                driver.Navigate().GoToUrl("http://www.prizerebel.com/members.php");
                                looping = false;
                                looped = false;
                            }
                        }
                        catch { }
                    }

                    driver.SwitchTo().DefaultContent();
                    Helpers.switchFrameByNumber(driver, 0);
                    Helpers.switchFrameByNumber(driver, 0);

                    Helpers.ByClass(driver, "close");


                }
                catch { }

                try
                {
                    if (currentHour != DateTime.Now.Hour)
                    {
                        driver.SwitchTo().DefaultContent();
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.ByClass(driver, "close");
                        driver.Navigate().GoToUrl("http://www.prizerebel.com/members.php");
                        looping = false;
                        looped = false;
                    }
                }
                catch { }

            }

        }

        public static void dailyPoints(IWebDriver driver)
        {
            string[] titles = { "PrizeRebel.com | Earn" };
            bool loop = true;

            while (loop)
            {
                try
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                    foreach (String window in windowHandles)
                    {
                        try
                        {
                            IWebDriver popup = driver.SwitchTo().Window(window);
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.Id("noDisplay")).Displayed)
                            {
                                loop = false;
                            }
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#displayWrap iframe")));
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.Id("offers_exhausted_message")).Displayed)
                            {
                                loop = false;
                            }
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

                        try
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#displayWrap iframe")));
                        }
                        catch { }

                        Helpers.ById(driver, "webtraffic_popup_start_button");
                        Helpers.ById(driver, "webtraffic_popup_next_button");
                        Helpers.ByClass(driver, "webtraffic_start_button");
                        Helpers.ByClass(driver, "webtraffic_next_button");

                        // Chips Ad
                        Helpers.ById(driver, "compositor_placed_innerclip_cheddar");
                        Helpers.ById(driver, "compositor_placed_innerclip_gouda");
                        Helpers.ById(driver, "compositor_placed_innerclip_habanero");
                        Helpers.ById(driver, "compositor_placed_innerclip_flamin");
                        Helpers.ById(driver, "compositor_placed_innerclip_honeybbq");
                        Helpers.ById(driver, "compositor_placed_innerclip_korean");
                        Helpers.ById(driver, "compositor_placed_innerclip_oliveoil");
                        Helpers.ById(driver, "compositor_placed_innerclip_seasalt");
                        //

                        Helpers.ById(driver, "compositor_placed_innerclip_bajablast");

                        try
                        {
                            IWebElement rewardText = driver.FindElement(By.Id("ty_header"));
                            if (rewardText.Text == "You earned 1 Points!")
                            {
                                driver.Navigate().Refresh();
                                Helpers.closeWindows(driver, titles);
                            }
                        }
                        catch { }

                        try
                        {

                            if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
                            {
                                driver.Navigate().Refresh();
                                Helpers.closeWindows(driver, titles);
                            }
                        }

                        catch { }

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
                                    Helpers.wait(5000);
                                }
                            }
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#displayWrap iframe")));
                        }
                        catch { }

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
                                driver.Navigate().Refresh();
                                Console.WriteLine("Refresh Complete");
                            }
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.Id("vgPlayer")));
                        }
                        catch { }

                        Helpers.ByClass(driver, "closeBtn");

                        Helpers.switchFrameByNumber(driver, 0);

                        try
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.Id("player")));
                            driver.SwitchTo().Frame(driver.FindElement(By.Id("player")));
                        }
                        catch { }
                        //*/

                        try
                        {
                            driver.FindElement(By.ClassName("ytp-large-play-button")).Click();
                        }
                        catch { }

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
                                driver.Navigate().Refresh();
                                Console.WriteLine("Refresh Complete");
                            }
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.Id("ty_body_text")).Displayed)
                            {
                                Helpers.closeWindows(driver, titles);
                                Console.WriteLine("I'm Here!!");
                                driver.SwitchTo().ParentFrame();
                                Console.WriteLine("Attempting Refresh");
                                driver.Navigate().GoToUrl("http://www.prizerebel.com/ripply.php");
                                Helpers.wait(1000);
                                driver.Navigate().GoToUrl("http://www.prizerebel.com/dailypoints.php");
                                driver.Navigate().Refresh();
                                Console.WriteLine("Refresh Complete");
                            }
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#displayWrap iframe")));
                        }
                        catch { }
                        Helpers.wait(5000);

                        try
                        {
                            driver.SwitchTo().DefaultContent();
                        }
                        catch { }

                        try
                        {
                            IList<IWebElement> divs = driver.FindElements(By.TagName("div"));
                            Console.WriteLine(divs.Count);
                            foreach (IWebElement div in divs)
                            {
                                if (div.Text == "You have reached the limit for the day, please check back in 24 hrs.")
                                {
                                    loop = false;
                                }
                            }
                        }
                        catch { }


                        Helpers.switchFrameByNumber(driver, 0);
                        try
                        {
                            if (driver.FindElement(By.ClassName("jw-icon")).Displayed)
                            {
                                loop = false;
                            }
                        }
                        catch { }
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchToBrowserFrameByString(driver, "player");
                        Helpers.switchToBrowserFrameByString(driver, "player");
                        try
                        {
                            driver.FindElement(By.ClassName("ytp-large-play-button")).Click();
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().DefaultContent();
                        }
                        catch { }

                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);

                        try
                        {
                            if (driver.FindElement(By.ClassName("splash-divider")).Displayed)
                            {
                                driver.Navigate().Refresh();
                            }
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().DefaultContent();
                        }
                        catch { }

                        Helpers.switchFrameByNumber(driver, 0);
                        IList<IWebElement> bolds = driver.FindElements(By.TagName("b"));
                        foreach (IWebElement bold in bolds)
                        {
                            if (bold.Text.Contains("No videos"))
                            {
                                loop = false;
                            }
                        }

                        try
                        {
                            driver.SwitchTo().DefaultContent();
                        }
                        catch { }

                        Helpers.switchFrameByNumber(driver, 0);

                        Helpers.ById(driver, "webtraffic_popup_start_button");
                        Helpers.ById(driver, "webtraffic_popup_next_button");
                        Helpers.ByClass(driver, "webtraffic_start_button");
                        Helpers.ByClass(driver, "webtraffic_next_button");

                        Helpers.ById(driver, "expository_image");

                        try
                        {
                            if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                            {
                                driver.Navigate().Refresh();
                                Helpers.closeWindows(driver, titles);
                            }
                        }
                        catch { }

                        try
                        {
                            IWebElement rewardText = driver.FindElement(By.Id("ty_header"));
                            if (rewardText.Text == "You earned 1 Points!")
                            {
                                driver.Navigate().Refresh();
                                Helpers.closeWindows(driver, titles);
                            }
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.Id("ty_headline")).Text.Contains("Thanks for visiting"))
                            {
                                driver.Navigate().Refresh();
                                Helpers.closeWindows(driver, titles);
                            }
                        }
                        catch { }
                    }
                }
                catch { }
            }
        }


        /*
        public static void ById(IWebDriver driver, string targetID)
        {
            try
            {
                if (driver.FindElement(By.Id(targetID)).Displayed)
                {
                    Helpers.wait(2000);
                    driver.FindElement(By.Id(targetID)).Click();
                }
            }
            catch
            {
                Console.WriteLine("Trying to find ID " + targetID);
            }
            finally { }
        }

        public static void ByClass(IWebDriver driver, string targetClass)
        {
            try
            {
                if (driver.FindElement(By.ClassName(targetClass)).Displayed)
                {
                    Helpers.wait(2000);
                    driver.FindElement(By.ClassName(targetClass)).Click();
                }
            }
            catch
            {
                Console.WriteLine("Trying to find ClassName " + targetClass);
            }
            finally { }
        }

        public static void switchToBrowserFrameByString(IWebDriver driver, string targetBrowserString)
        {
            try
            {
                driver.SwitchTo().Frame(driver.FindElement(By.Id(targetBrowserString)));
            }
            catch { }
            finally { }
        }

        public static void switchToFrame(IWebDriver driver, IList<IWebElement> iframes, int counts)
        {
            try
            {
                foreach (IWebElement iframe in iframes)
                {
                    driver.SwitchTo().Frame(iframe);
                }
            }
            catch { }
            finally { }
        }

        public static void switchToBrowserByString(IWebDriver driver, string targetBrowserString)
        {
            try
            {
                foreach (string defwindow in driver.WindowHandles)
                {
                    Console.WriteLine(driver.Title.ToString());
                    try
                    {
                        driver.SwitchTo().Window(defwindow);
                    }
                    catch
                    {
                        Console.WriteLine("Hang Up");
                        break;
                    }
                    finally
                    {
                        Console.WriteLine("Couldn't Get Window");
                    }
                    try
                    {
                        if (driver.Title.Contains(targetBrowserString))
                        {
                            Console.WriteLine("You are now in " + driver.Title);
                            break;
                        }
                    }
                    catch
                    {
                        break;
                    }
                    finally { }
                }
            }
            catch { }
            finally { }
        }

        public static void closeWindows(IWebDriver driver, string[] titles)
        {
            try
            {
                System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                foreach (String window in windowHandles)
                {
                    IWebDriver popup = driver.SwitchTo().Window(window);
                    try
                    {
                        bool found = false;
                        foreach (string title in titles)
                        {
                            if (popup.Title.Contains(title))
                            {
                                found = true;
                                break;

                            }
                        }

                        if (!found)
                        {
                            driver.SwitchTo().Window(window);
                            driver.Close();
                        }
                    }
                    catch { }
                    finally { }
                }
            }
            catch { }
            finally { }
        }

        public static bool lookFor(IWebDriver driver, string title)
        {
            try
            {
                foreach (string defwindow in driver.WindowHandles)
                {
                    Console.WriteLine(driver.Title);
                    try
                    {
                        driver.SwitchTo().Window(defwindow);
                    }
                    catch
                    {
                        Console.WriteLine("Hang Up");
                        return true;
                    }
                    finally
                    {
                        Console.WriteLine("Couldn't Get Window");
                    }
                    try
                    {
                        if (driver.Title.Contains(title))
                        {
                            Console.WriteLine("There is " + title);
                            return true;
                        }
                    }
                    catch
                    {
                        return true;
                    }
                    finally { }
                }
                return false;
            }
            catch
            {
                return true;
            }
            finally { }
        }

        public static bool isID(IWebDriver driver, string element)
        {
            if (driver.FindElement(By.Id(element)).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool isClass(IWebDriver driver, string element)
        {
            if (driver.FindElement(By.Id(element)).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void switchFrameByNumber(IWebDriver driver, int frameIndex)
        {
            try
            {
                driver.SwitchTo().Frame(frameIndex);
            }
            catch { }
            finally { }
        }

        public static void switchToBrowserByElement(IWebDriver driver, IWebElement targetElement)
        {
            try
            {
                foreach (string defwindow in driver.WindowHandles)
                {
                    Console.WriteLine(driver.Title.ToString());
                    try
                    {
                        driver.SwitchTo().Window(defwindow);
                    }
                    catch
                    {
                        Console.WriteLine("Hang Up");
                        break;
                    }
                    finally
                    {
                        Console.WriteLine("Couldn't Get Window");
                    }
                    try
                    {
                        if (targetElement.Text.Contains("You're watching"))
                        {
                            break;
                        }
                    }
                    catch
                    {
                        break;
                    }
                    finally { }
                }
            }
            catch { }
            finally { }
        }
        */
    }
}
