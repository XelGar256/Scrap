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
        string[] titles = { "Dashboard", "Offer Walls", "Watch and" };
        public ZoomModel(string username, string password, BackgroundWorker bw, bool justZoom)
        {
            int hour = -1;
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("user-data-dir=" + App.Folder + "profileZB");

            IWebDriver driver = new ChromeDriver(service, options);
            driver.Navigate().GoToUrl("http://www.zoombucks.com/");

            try
            {
                if (driver.FindElement(By.ClassName("signup_form")).Displayed)
                    driver.Navigate().GoToUrl("http://www.zoombucks.com/login.php");

                driver.FindElement(By.Name("username")).SendKeys(username);
                driver.FindElement(By.Name("password")).SendKeys(password);
                driver.FindElement(By.ClassName("signup_button")).Click();
            }
            catch { }
            finally { }
            //jun videos
            Helpers.wait(2000);
            switchToBrowserByString(driver, "Dashboard");

            if (!justZoom)
            {
                try
                {
                    driver.FindElement(By.XPath("//a[contains(@href, 'hourly_offer_contest')]")).Click();
                    //MessageBox.Show(driver.Title);
                    hour = DateTime.Now.Hour;
                }
                catch
                {
                    Console.WriteLine("Couldn't Click Contest");
                }
                finally { }

                ByClass(driver, "brand");


                Helpers.wait(1000);
                ByClass(driver, "widgetcontent");
                Helpers.wait(1000);

                while (lookFor(driver, "Watch and")) //testing
                {
                    if (hour != DateTime.Now.Hour)
                    {
                        try
                        {
                            Helpers.switchToBrowserByString(driver, "Dashboard");
                            driver.FindElement(By.XPath("//a[contains(@href, 'hourly_offer_contest')]")).Click();
                            //MessageBox.Show(driver.Title);
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
                            switchFrameByNumber(driver, 0);

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
                            ById(driver, "webtraffic_popup_start_button");
                            ById(driver, "webtraffic_popup_next_button");
                            ByClass(driver, "webtraffic_start_button");
                            ByClass(driver, "webtraffic_next_button");
                            ByClass(driver, "webtraffic_button");
                            ById(driver, "expository_image");

                            // Chips Ad
                            ById(driver, "compositor_placed_innerclip_cheddar");
                            ById(driver, "compositor_placed_innerclip_gouda");
                            ById(driver, "compositor_placed_innerclip_habanero");
                            ById(driver, "compositor_placed_innerclip_flamin");
                            ById(driver, "compositor_placed_innerclip_honeybbq");
                            ById(driver, "compositor_placed_innerclip_korean");
                            ById(driver, "compositor_placed_innerclip_oliveoil");
                            ById(driver, "compositor_placed_innerclip_seasalt");
                            //

                            try
                            {
                                if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
                                {
                                    driver.Navigate().Refresh();
                                    closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            try
                            {
                                if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                                {
                                    driver.Navigate().Refresh();
                                    closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            try
                            {
                                if (driver.FindElement(By.Id("compositor_placed_innerclip_youtube")).Displayed)
                                {
                                    driver.Navigate().Refresh();
                                    closeWindows(driver, titles);
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

                        switchFrameByNumber(driver, 0);

                        try
                        {
                            if (driver.FindElement(By.Id("compositor_placed_innerclip_youtube")).Displayed)
                            {
                                driver.Navigate().Refresh();
                                closeWindows(driver, titles);
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
                        //* *************** Remove Me ***************************
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
                            switchFrameByNumber(driver, 0);
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
                        //*/

                        try
                        {
                            driver.FindElement(By.ClassName("ytp-large-play-button")).Click();
                        }
                        catch { }

                        /*
                        Helpers.switchToBrowserByString(driver, "Watch and");
                        driver.SwitchTo().DefaultContent();
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
                        }
                        catch { }
                        */

                        Console.WriteLine("Switching to Browser");
                        switchToBrowserByString(driver, "Offer Walls");
                        try
                        {
                            Console.WriteLine("Switching to Frame");
                            driver.SwitchTo().Frame(1);

                            if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
                            {
                                driver.Navigate().Refresh();
                                closeWindows(driver, titles);
                            }
                        }

                        catch { }

                        switchToBrowserByString(driver, "Offer Walls");
                        try
                        {
                            driver.SwitchTo().DefaultContent();
                            switchFrameByNumber(driver, 0);
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
                        switchFrameByNumber(driver, stackedFrames.Count);
                        if (driver.FindElement(By.TagName("error")).Text.Contains("AccessDeniedAccess"))
                        {
                            Console.WriteLine("HOLY SHIT DUDE");
                            driver.Close();
                        }
                    }
                    catch { }

                    switchFrameByNumber(driver, 0);

                    try
                    {
                        if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
                        {
                            driver.Navigate().Refresh();
                            closeWindows(driver, titles);
                        }
                    }
                    catch { }
                }

                switchToBrowserByString(driver, "Dashboard");
                Console.WriteLine(driver.Title);


                driver.Navigate().GoToUrl("http://www.zoombucks.com/offer_walls.php?t=1444653478#volume-11");

                switchToBrowserByString(driver, "Offer Walls");
                bool offerWall = true;

                do
                {
                    try
                    {
                        System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                        foreach (String window in windowHandles)
                        {
                            IWebDriver popup = driver.SwitchTo().Window(window);

                            //switchToBrowserByString(driver, "Offer Walls");
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
                                        //closeWindows(driver, titles);
                                        ById(driver, "next_btn");
                                    }
                                    catch { }
                                }
                                else
                                {
                                    Console.WriteLine("Videos Found");
                                    //if (driver.FindElement(By.Id("ty_header")).Text == "You earned 1 reward!")
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
                                    ById(driver, "expository_image");
                                    ById(driver, "webtraffic_popup_start_button");
                                    ById(driver, "webtraffic_popup_next_button");
                                    ByClass(driver, "webtraffic_start_button");
                                    ByClass(driver, "webtraffic_next_button");
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
                                    closeWindows(driver, titles);
                                }
                            }
                            catch { }


                            Helpers.wait(5000);
                            //switchToBrowserByString(driver, "Offer Walls");

                            try
                            {
                                Console.WriteLine("Switching to Browser");
                                //switchToBrowserByString(driver, "Offer Walls");
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
                            switchToBrowserByString(driver, "Offer Walls");
                            Console.WriteLine("Switching to Frame");
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#volume-11 iframe")));
                            driver.SwitchTo().Frame(0);
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));

                            try
                            {
                                if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
                                {
                                    driver.Navigate().Refresh();
                                    closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            IWebElement volume11reward = driver.FindElement(By.Id("ty_header"));
                            if (volume11reward.Text == "You earned 1 reward!")
                            {
                                Console.WriteLine("refreshing....");
                                driver.Navigate().Refresh();
                                closeWindows(driver, titles);
                            }
                            else if (volume11reward.Text.Contains("2 ZBs"))
                            {
                                Console.WriteLine("refreshing....");
                                driver.Navigate().Refresh();
                                closeWindows(driver, titles);
                            }
                        }
                        catch { }

                        try
                        {
                            Console.WriteLine("Switching to Browser");
                            switchToBrowserByString(driver, "Offer Walls");
                            Console.WriteLine("Switching to Frame");
                            driver.SwitchTo().Frame(0);

                            if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
                            {
                                driver.Navigate().Refresh();
                                closeWindows(driver, titles);
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
                            switchFrameByNumber(driver, 0);
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
                    /* *************** Remove Me ***************************
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
                        switchFrameByNumber(driver, 0);
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
                    */

                    try
                    {
                        driver.FindElement(By.ClassName("ytp-large-play-button")).Click();
                    }
                    catch { }

                    switchFrameByNumber(driver, 0);
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
                            closeWindows(driver, titles);
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
        }


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
            System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

            try
            {
                foreach (string defwindow in windowHandles)
                {
                    Console.WriteLine(driver.Title.ToString());
                    try
                    {
                        if (defwindow != null)
                        {
                            driver.SwitchTo().Window(defwindow);
                            break;
                        }
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
    }
}
