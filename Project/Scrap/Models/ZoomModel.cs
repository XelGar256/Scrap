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
            bool looping = true;
            while (looping)
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
                    ////Helpers.wait(5000);
                    driver.FindElement(By.Id("password")).SendKeys(password);
                    driver.FindElement(By.ClassName("btn-block")).Click();
                }
                catch { }

                Helpers.wait(10000);
                if (!justZoom)
                {

                    try
                    {
                        int counter = 0;
                        IList<IWebElement> turnOffNotifcations = driver.FindElements(By.ClassName("btn-block"));
                        if (!Helpers.findText(driver, "Milestones"))
                        {
                            foreach (IWebElement turnOffNotication in turnOffNotifcations)
                            {
                                if (counter == turnOffNotifcations.Count - 1)
                                {
                                    turnOffNotication.Click();
                                }
                                counter++;
                            }
                        }
                        else
                        {
                            foreach (IWebElement turnOffNotication in turnOffNotifcations)
                            {
                                if (counter == 1)
                                {
                                    turnOffNotication.Click();
                                }
                                counter++;
                            }
                        }
                    }
                    catch { }

                    int hr = DateTime.Now.Hour;

                    try
                    {
                        driver.Navigate().GoToUrl("http://members.grabpoints.com/#/offers/watch_videos");
                    }
                    catch { }

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
                                    if (driver.Title.Contains("Facebook"))
                                    {
                                        driver.Close();
                                    }
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

                    hr = DateTime.Now.Hour;

                    while (DateTime.Now.Hour == hr)
                    { }
                    junVideos = false;
                    volume = false;
                    viroolBool = false;
                }
                else
                {
                    looping = false;
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
                    driver.SwitchTo().DefaultContent();
                    Helpers.switchFrameByNumber(driver, 0);

                    Helpers.switchToBrowserFrameByString(driver, "widgetPlayer");
                    Helpers.switchToBrowserFrameByString(driver, "player-container");

                    Helpers.ByClass(driver, "ytp-large-play-button");

                    try
                    {
                        if (driver.FindElement(By.ClassName("clearfix")).Displayed)
                        { }

                    }
                    catch { }
                }
            }

            /*
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
                    if (driver.Title.Contains("Facebook"))
                    {
                        driver.Close();
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

                try
                {
                    if (driver.FindElement(By.ClassName("wicon-youtube")).Displayed)
                    {
                        driver.SwitchTo().DefaultContent();
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.ByClass(driver, "close");
                        looped = false;
                    }
                }
                catch { }

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
                    if (driver.FindElement(By.ClassName("ytp-videowall-still-info-content")).Displayed)
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

                Helpers.switchFrameByNumber(driver, 0);
                Helpers.switchFrameByNumber(driver, 0);

                try
                {
                    if (driver.FindElement(By.ClassName("ytp-videowall-still-info-content")).Displayed)
                    {
                        driver.SwitchTo().DefaultContent();
                        Helpers.ByClass(driver, "close");
                        looped = false;
                    }
                }
                catch { }

                try
                {
                    if (driver.FindElement(By.ClassName("ytp-endscreen-content")).Displayed)
                    {
                        driver.SwitchTo().DefaultContent();
                        Helpers.ByClass(driver, "close");
                        looped = false;
                    }
                }
                catch { }
            }*/
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

                        try
                        {
                            if (driver.Title.Contains("Facebook"))
                            {
                                driver.Close();
                            }
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().DefaultContent();
                        }
                        catch { }

                        Helpers.switchFrameByNumber(driver, 0);

                        try
                        {
                            IWebElement dropDownMonth = driver.FindElement(By.ClassName("b_month"));
                            IWebElement dropDownDay = driver.FindElement(By.ClassName("b_day"));
                            IWebElement dropDownYear = driver.FindElement(By.ClassName("b_year"));
                            IWebElement dropDownGender = driver.FindElement(By.ClassName("gender"));
                            string[] months = { "January", "Febuary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                            string[] gender = { "Male", "Female" };
                            Random random = new Random();
                            int rndMonth = random.Next(0, 11);
                            Console.WriteLine(rndMonth);
                            SelectElement clickThis = new SelectElement(dropDownMonth);
                            clickThis.SelectByText(months[rndMonth]);
                            Helpers.wait(1000);
                            int rndGender = random.Next(0, 1);
                            Console.WriteLine(rndMonth);
                            clickThis = new SelectElement(dropDownGender);
                            clickThis.SelectByText(gender[rndMonth]);
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
                            driver.FindElement(By.ClassName("submit")).Click();
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().DefaultContent();
                        }
                        catch { }

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

                        driver.SwitchTo().DefaultContent();

                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchToBrowserFrameByString(driver, "player");
                        Helpers.switchToBrowserFrameByString(driver, "player");

                        Helpers.ByClass(driver, "ytp-large-play-button");

                        driver.SwitchTo().DefaultContent();
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);

                        try
                        {
                            if (driver.FindElement(By.ClassName("ytp-error-content-wrap")).Displayed)
                            {
                                driver.SwitchTo().DefaultContent();
                                driver.FindElement(By.Id("next_btn")).Click();
                            }
                        }
                        catch { }

                        Helpers.switchToBrowserFrameByString(driver, "player");

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

            while (Helpers.lookFor(driver, "hyprmx.com") || Helpers.lookFor(driver, "No Offers") || Helpers.lookFor(driver, "Now exploring..."))
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
                            if (driver.Title.Contains("Facebook"))
                            {
                                driver.Close();
                            }
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