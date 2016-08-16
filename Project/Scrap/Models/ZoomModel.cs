using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Scrap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Scrap.Models
{
    class ZoomModel
    {
        string[] titles = { "Grab Points" };
        bool junVideos = false, volume = false, viroolBool = false;
        public ZoomModel(string username, string password, BackgroundWorker bw, bool justZoom)
        {
            while (true)
            {
                ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
                service.HideCommandPromptWindow = true;

                ChromeOptions options = new ChromeOptions();
                options.AddArgument("start-maximized");
                options.AddArgument("user-data-dir=" + App.Folder + "profileZB");

                IWebDriver driver = new ChromeDriver(service, options);
                driver.Navigate().GoToUrl("http://members.grabpoints.com/#/login?email=" + username);

                try
                {
                    //driver.FindElement(By.Name("email")).SendKeys(username);
                    //driver.FindElement(By.Name("email")).SendKeys(Keys.Enter);
                    Helpers.wait(5000);
                    driver.FindElement(By.Id("password")).SendKeys(password);
                    driver.FindElement(By.ClassName("btn-block")).Click();
                }
                catch { }

                Helpers.wait(2000);

                try
                {
                    int counter = 0;
                    IList<IWebElement> turnOffNotifcations = driver.FindElements(By.ClassName("btn-block"));
                    foreach (IWebElement turnOffNotication in turnOffNotifcations)
                    {
                        if (counter == turnOffNotifcations.Count - 1)
                        {
                            turnOffNotication.Click();
                        }
                        counter++;
                    }
                }
                catch { }

                int hr = DateTime.Now.Hour;

                if (!justZoom)
                {
                    while (!viroolBool)
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
                                    IList<IWebElement> surveys = driver.FindElements(By.ClassName("btn-block"));
                                    if (surveys.Count > 2)
                                    {
                                        driver.FindElement(By.ClassName("btn-block")).Click();
                                    }
                                }
                                catch { }

                                if (!junVideos)
                                    junVids(driver);
                                if (!volume)
                                    volume11(driver);
                                if (volume && junVideos && !viroolBool)
                                    virool(driver);
                                Helpers.wait(5000);
                            }
                        }
                        catch { }
                    }

                    Helpers.closeWindows(driver, titles);
                    driver.Close();
                    driver.Quit();

                    while (DateTime.Now.Hour == hr)
                    { }
                }
            }
        }

        void grabPointsTV(IWebDriver driver)
        {
            Helpers.wait(5000);

            try
            {
                driver.FindElement(By.PartialLinkText("GrabPoints TV")).Click();
            }
            catch { }

            Helpers.wait(5000);

            try
            {
                driver.FindElement(By.ClassName("list-item-img")).Click();
            }
            catch { }

            Helpers.wait(5000);

            while (true)
            {
                Helpers.wait(2000);
                Random rnd = new Random();
                Helpers.wait(1000 * rnd.Next(60, 120));
                try
                {
                    driver.FindElement(By.PartialLinkText("Next")).Click();
                }
                catch { }
            }
        }
        void virool(IWebDriver driver)
        {
            bool looped = false;

            try
            {
                driver.FindElement(By.LinkText("Watch Videos")).Click();
            }
            catch { }

            try
            {
                driver.FindElement(By.LinkText("Virool")).Click();
            }
            catch { }

            while (Helpers.lookFor(driver, "Virool"))
            {
                int counter = 0;
                IList<IWebElement> thumbnails = driver.FindElements(By.ClassName("thumbnail"));
                Console.WriteLine("Thumbnails Count: " + thumbnails.Count);
                if (thumbnails.Count == 0)
                {
                    Helpers.closeWindows(driver, titles);
                    viroolBool = true;
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

                IList<IWebElement> bolds = driver.FindElements(By.TagName("b"));
                foreach (IWebElement bold in bolds)
                {
                    if (bold.Text.Contains("No videos"))
                    {
                        looped = false;
                        viroolBool = true;
                    }
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
                            looped = false;
                        }
                    }
                    catch { }

                    driver.SwitchTo().DefaultContent();
                    Helpers.switchFrameByNumber(driver, 0);

                    try
                    {
                        if (driver.FindElement(By.ClassName("twitter-share-btn")).Displayed)
                        {
                            driver.SwitchTo().DefaultContent();
                            Helpers.switchFrameByNumber(driver, 0);
                            Helpers.ByClass(driver, "close");
                            looped = false;
                        }
                    }
                    catch { }
                }
            }
        }

        void volume11(IWebDriver driver)
        {
            try
            {
                driver.FindElement(By.PartialLinkText("Volume")).Click();
            }
            catch { }

            while (Helpers.lookFor(driver, "b.v11media.com"))
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

                        //driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#video_body iframe")));
                        //driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);

                        IList<IWebElement> bolds = driver.FindElements(By.TagName("b"));
                        foreach (IWebElement bold in bolds)
                        {
                            if (bold.Text.Contains("No videos"))
                            {
                                Helpers.closeWindows(driver, titles);
                                volume = true;
                            }
                        }

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
                            if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                            {
                                driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Click();
                                Helpers.closeWindows(driver, titles);
                            }
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

                            if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
                            {
                                Helpers.closeWindows(driver, titles);
                            }
                        }
                        catch { }

                        try
                        {
                            Helpers.switchToBrowserFrameByString(driver, "junFrame");
                            driver.FindElement(By.Id("expository_image")).Click();
                        }
                        catch { }

                        Helpers.wait(1000);

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
                                        Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                                    }
                                    catch { }
                                    finally { }
                                    Helpers.wait(5000);
                                }
                            }
                        }
                        catch { }

                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);

                        try
                        {
                            if (driver.FindElement(By.Id("offers_exhausted_message")).Displayed)
                            {
                                driver.Close();
                                Helpers.closeWindows(driver, titles);
                                volume = true;
                            }
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.Id("ty_body_text")).Displayed)
                            {
                                driver.Close();
                                Helpers.closeWindows(driver, titles);
                                volume = true;
                            }
                        }
                        catch { }

                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);

                        Helpers.ByClass(driver, "ytp-large-play-button");
                    }
                }
                catch { }
            }
        }

        void junVids(IWebDriver driver)
        {
            try
            {
                driver.FindElement(By.LinkText("Jun Videos")).Click();
            }
            catch { }

            while (Helpers.lookFor(driver, "hyprmx.com") || Helpers.lookFor(driver, "No Offers"))
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

                        IList<IWebElement> bolds = driver.FindElements(By.TagName("b"));
                        foreach (IWebElement bold in bolds)
                        {
                            if (bold.Text.Contains("No videos"))
                            {
                                driver.Close();
                                junVideos = true;
                            }
                        }

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
                            if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                            {
                                driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Click();
                                Helpers.closeWindows(driver, titles);
                            }
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

                            if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
                            {
                                Helpers.closeWindows(driver, titles);
                            }
                        }
                        catch { }

                        try
                        {
                            Helpers.switchToBrowserFrameByString(driver, "junFrame");
                            driver.FindElement(By.Id("expository_image")).Click();
                        }
                        catch { }

                        Helpers.wait(1000);

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
                                        Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                                    }
                                    catch { }
                                    finally { }
                                    Helpers.wait(5000);
                                }
                            }
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.Id("offers_exhausted_message")).Displayed)
                            {
                                driver.Close();
                                Helpers.closeWindows(driver, titles);
                                junVideos = true;
                            }
                        }
                        catch { }

                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);

                        Helpers.ByClass(driver, "ytp-large-play-button");
                    }
                }
                catch { }
            }
        }
    }
}
/*
                //jun videos
                Helpers.wait(2000);

                Helpers.switchToBrowserByString(driver, "Dashboard");

                if (!justZoom)
                {
                    try
                    {
                        driver.FindElement(By.XPath("//a[contains(@href, 'hourly_offer_contest')]")).Click();
                        hour = DateTime.Now.Hour;
                    }
                    catch
                    {
                        Console.WriteLine("Couldn't Click Contest");
                    }
                    finally { }

                    Helpers.ByClass(driver, "brand");


                    Helpers.wait(1000);
                    Helpers.ByClass(driver, "widgetcontent");
                    Helpers.wait(1000);

                    while (Helpers.lookFor(driver, "Watch and")) //testing
                    {
                        if (hour != DateTime.Now.Hour)
                        {
                            try
                            {
                                Helpers.switchToBrowserByString(driver, "Dashboard");
                                driver.FindElement(By.XPath("//a[contains(@href, 'hourly_offer_contest')]")).Click();
                                hour = DateTime.Now.Hour;
                            }
                            catch
                            {
                                Console.WriteLine("Couldn't Click Contest");
                            }
                            finally { }

                            Helpers.ByClass(driver, "brand");
                        }

                        try
                        {
                            System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                            foreach (String window in windowHandles)
                            {
                                IWebDriver popup = driver.SwitchTo().Window(window);

                                Helpers.wait(1000);
                                Helpers.switchFrameByNumber(driver, 0);

                                //

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

                                //
                                Helpers.ById(driver, "webtraffic_popup_start_button");
                                Helpers.ById(driver, "webtraffic_popup_next_button");
                                Helpers.ByClass(driver, "webtraffic_start_button");
                                Helpers.ByClass(driver, "webtraffic_next_button");
                                Helpers.ByClass(driver, "webtraffic_button");
                                Helpers.ById(driver, "expository_image");

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
                                    if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
                                    {
                                        driver.Navigate().Refresh();
                                        Helpers.closeWindows(driver, titles);
                                    }
                                }
                                catch { }

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
                                    if (driver.FindElement(By.Id("compositor_placed_innerclip_youtube")).Displayed)
                                    {
                                        driver.Navigate().Refresh();
                                        Helpers.closeWindows(driver, titles);
                                    }
                                }
                                catch { }

                                try
                                {
                                    IWebElement rewardText = driver.FindElement(By.Id("ty_header"));
                                    if (rewardText.Text == "You earned 2 ZBs!")
                                    {

                                        driver.Navigate().Refresh();

                                        Helpers.closeWindows(driver, titles);
                                    }
                                    else if (rewardText.Text == "You've earned 2 ZBs!")
                                    {

                                        driver.Navigate().Refresh();

                                        Helpers.closeWindows(driver, titles);
                                    }
                                    else if (rewardText.Text == "You earned 1 ZBs!")
                                    {

                                        driver.Navigate().Refresh();

                                        Helpers.closeWindows(driver, titles);
                                    }
                                    else if (rewardText.Text.Contains("2 ZBs"))
                                    {
                                        driver.Navigate().Refresh();

                                        Helpers.closeWindows(driver, titles);
                                    }
                                }
                                catch { }



                            }
                            Helpers.switchToBrowserByString(driver, "Now Exploring great content!");
                            try
                            {
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
                                    }
                                    try
                                    {
                                        driver.FindElement(By.XPath("//img[@alt='Claim your reward']")).Click();
                                        Helpers.switchToBrowserByString(driver, "Watch and Get");
                                    }
                                    catch { }
                                    Helpers.wait(5000);
                                }
                            }
                            catch { }

                            Helpers.switchFrameByNumber(driver, 0);

                            try
                            {
                                if (driver.FindElement(By.Id("compositor_placed_innerclip_youtube")).Displayed)
                                {
                                    driver.Navigate().Refresh();
                                    Helpers.closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            IList<IWebElement> testIframes = driver.FindElements(By.TagName("iframe"));
                            Console.WriteLine("How many iFrames are avaible = " + testIframes.Count);
                            Console.WriteLine("*********************************************");
                            foreach (IWebElement testIframe in testIframes)
                            {
                                Console.WriteLine(testIframe.Displayed);
                                Console.WriteLine(testIframe.Text);
                                Console.WriteLine(testIframe.GetAttribute("id"));
                            }
                            Console.WriteLine("*********************************************");

                            try
                            {
                                driver.SwitchTo().Frame(driver.FindElement(By.Id("vgPlayer")));
                            }
                            catch { }
                            testIframes = driver.FindElements(By.TagName("iframe"));
                            Console.WriteLine("Number of iFrames after vgPlayer = " + testIframes.Count);
                            foreach (IWebElement testIframe in testIframes)
                            {
                                Console.WriteLine(testIframe.Displayed);
                                Console.WriteLine(testIframe.Text);
                                Console.WriteLine(testIframe.GetAttribute("id"));
                            }

                            try
                            {
                                Helpers.switchFrameByNumber(driver, 0);
                                testIframes = driver.FindElements(By.TagName("iframe"));
                                Console.WriteLine("Number of iFrames after vgPlayer = " + testIframes.Count);
                                foreach (IWebElement testIframe in testIframes)
                                {
                                    Console.WriteLine(testIframe.Displayed);
                                    Console.WriteLine(testIframe.Text);
                                    Console.WriteLine(testIframe.GetAttribute("id"));
                                }
                            }
                            catch { }
                            try
                            {
                                driver.SwitchTo().Frame(driver.FindElement(By.Id("player")));
                                driver.SwitchTo().Frame(driver.FindElement(By.Id("player")));
                            }
                            catch { }
                            //

                            try
                            {
                                driver.FindElement(By.ClassName("ytp-large-play-button")).Click();
                            }
                            catch { }

                            Console.WriteLine("Switching to Browser");
                            Helpers.switchToBrowserByString(driver, "Offer Walls");
                            try
                            {
                                Console.WriteLine("Switching to Frame");
                                driver.SwitchTo().Frame(1);

                                if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
                                {
                                    driver.Navigate().Refresh();
                                    Helpers.closeWindows(driver, titles);
                                }
                            }

                            catch { }

                            Helpers.switchToBrowserByString(driver, "Offer Walls");
                            try
                            {
                                driver.SwitchTo().DefaultContent();
                                Helpers.switchFrameByNumber(driver, 0);
                            }
                            catch { }

                            try
                            {
                                if (driver.FindElement(By.Id("offers_exhausted_message")).Displayed)
                                {
                                    driver.Close();
                                    Helpers.wait(5000);
                                }
                            }
                            catch { }


                            Helpers.switchToBrowserByString(driver, "Watch and");
                            IList<IWebElement> stackedFrames = driver.FindElements(By.TagName("iframe"));
                            Console.WriteLine("Current Number Of IFrames " + stackedFrames.Count);
                            Helpers.switchFrameByNumber(driver, stackedFrames.Count);
                            if (driver.FindElement(By.TagName("error")).Text.Contains("AccessDeniedAccess"))
                            {
                                Console.WriteLine("HOLY SHIT DUDE");
                                driver.Close();
                            }
                        }
                        catch { }

                        Helpers.switchToBrowserByString(driver, "Watch and");
                        Helpers.switchFrameByNumber(driver, 0);

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
                            driver.SwitchTo().DefaultContent();
                        }
                        catch { }
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchToBrowserFrameByString(driver, "vgPlayer");
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchToBrowserFrameByString(driver, "player");
                        Helpers.ByClass(driver, "ytp-large-play-button");

                        try
                        {
                            driver.SwitchTo().DefaultContent();
                        }
                        catch { }
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchToBrowserFrameByString(driver, "player");
                        Helpers.switchToBrowserFrameByString(driver, "player");
                        Helpers.ByClass(driver, "ytp-large-play-button");
                    }

                    Helpers.switchToBrowserByString(driver, "Dashboard");
                    Console.WriteLine(driver.Title);


                    driver.Navigate().GoToUrl("http://www.zoombucks.com/offer_walls.php?t=1444653478#volume-11");

                    Helpers.switchToBrowserByString(driver, "Offer Walls");
                    bool offerWall = true;

                    do
                    {
                        try
                        {
                            System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                            foreach (String window in windowHandles)
                            {
                                IWebDriver popup = driver.SwitchTo().Window(window);

                                try
                                {
                                    driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#volume-11 iframe")));
                                }
                                catch { }
                                try
                                {
                                    if (driver.FindElement(By.ClassName("video_title")).Text != "Watch this video!")
                                    {
                                        try
                                        {
                                            Console.WriteLine("Looking for Videos");
                                            Helpers.ById(driver, "next_btn");
                                        }
                                        catch { }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Videos Found");
                                        Helpers.wait(500);
                                        driver.SwitchTo().Frame(0);
                                        driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));
                                        Console.WriteLine("Lets Get Started");
                                        //

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

                                        //
                                        Helpers.ById(driver, "expository_image");
                                        Helpers.ById(driver, "webtraffic_popup_start_button");
                                        Helpers.ById(driver, "webtraffic_popup_next_button");
                                        Helpers.ByClass(driver, "webtraffic_start_button");
                                        Helpers.ByClass(driver, "webtraffic_next_button");
                                    }
                                }
                                catch { }

                                try
                                {
                                    if (driver.FindElement(By.Id("offers_exhausted_message")).Displayed)
                                    {
                                        driver.Close();
                                        Helpers.wait(5000);
                                    }
                                }
                                catch { }

                                try
                                {
                                    if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                                    {
                                        driver.Navigate().Refresh();
                                        Helpers.closeWindows(driver, titles);
                                    }
                                }
                                catch { }


                                Helpers.wait(5000);

                                try
                                {
                                    Console.WriteLine("Switching to Browser");
                                    Console.WriteLine("Switching to Frame");
                                    driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#volume-11 iframe")));
                                    Console.WriteLine("Switching to Frame");
                                    driver.SwitchTo().Frame(0);
                                    Console.WriteLine("Switching to Frame");
                                    driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));

                                }
                                catch { }
                            }

                            try
                            {
                                Console.WriteLine("Switching to Browser");
                                Helpers.switchToBrowserByString(driver, "Offer Walls");
                                Console.WriteLine("Switching to Frame");
                                driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#volume-11 iframe")));
                                driver.SwitchTo().Frame(0);
                                driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));

                                try
                                {
                                    if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
                                    {
                                        driver.Navigate().Refresh();
                                        Helpers.closeWindows(driver, titles);
                                    }
                                }
                                catch { }

                                IWebElement volume11reward = driver.FindElement(By.Id("ty_header"));
                                if (volume11reward.Text == "You earned 1 reward!")
                                {
                                    Console.WriteLine("refreshing....");
                                    driver.Navigate().Refresh();
                                    Helpers.closeWindows(driver, titles);
                                }
                                else if (volume11reward.Text.Contains("2 ZBs"))
                                {
                                    Console.WriteLine("refreshing....");
                                    driver.Navigate().Refresh();
                                    Helpers.closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            try
                            {
                                Console.WriteLine("Switching to Browser");
                                Helpers.switchToBrowserByString(driver, "Offer Walls");
                                Console.WriteLine("Switching to Frame");
                                driver.SwitchTo().Frame(0);

                                if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
                                {
                                    driver.Navigate().Refresh();
                                    Helpers.closeWindows(driver, titles);
                                }
                            }

                            catch { }


                            Helpers.switchToBrowserByString(driver, "Now Exploring great content!");
                            try
                            {
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
                                    }
                                    try
                                    {
                                        driver.FindElement(By.XPath("//img[@alt='Claim your reward']")).Click();
                                        Helpers.switchToBrowserByString(driver, "Watch and Get");
                                    }
                                    catch { }
                                    Helpers.wait(5000);
                                }
                            }
                            catch { }

                            Console.WriteLine("Switching to Frame");
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#volume-11 iframe")));
                            Console.WriteLine("Switching to Frame");
                            driver.SwitchTo().Frame(0);
                            Console.WriteLine("Switching to Frame");
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));

                            if (driver.FindElement(By.Id("offers_exhausted_message")).Displayed)
                            {
                                offerWall = false;
                            }

                            try
                            {
                                driver.SwitchTo().DefaultContent();
                                Helpers.switchFrameByNumber(driver, 0);
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

                        }
                        catch { }

                        try
                        {
                            IList<IWebElement> testIframes = driver.FindElements(By.TagName("iframe"));
                            Console.WriteLine("How many iFrames are avaible = " + testIframes.Count);
                            Console.WriteLine("*********************************************");
                            foreach (IWebElement testIframe in testIframes)
                            {
                                Console.WriteLine(testIframe.Displayed);
                                Console.WriteLine(testIframe.Text);
                                Console.WriteLine(testIframe.GetAttribute("id"));
                            }
                            Console.WriteLine("*********************************************");
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().DefaultContent();
                        }
                        catch { }
                        try
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#volume-11 iframe")));
                            //driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#video_body iframe")));
                            Helpers.switchFrameByNumber(driver, 0);
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));
                        }
                        catch { }

                        Helpers.switchToBrowserFrameByString(driver, "vgPlayer");

                        try
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#player iframe")));
                        }
                        catch { }

                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);

                        try
                        {
                            driver.FindElement(By.ClassName("ytp-large-play-button")).Click();
                        }
                        catch { }

                        Helpers.switchFrameByNumber(driver, 0);
                        try
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#video_body iframe")));
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));
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

                    } while (offerWall);

                    try
                    {
                        driver.Quit();
                    }
                    catch { }
                }

                bool zoomLooping = true;
                int currrentHour = DateTime.Now.Hour;

                while (zoomLooping)
                {
                    if (currrentHour != DateTime.Now.Hour)
                    {
                        zoomLooping = false;
                    }
                }
            }
        }
    }
}
    */
