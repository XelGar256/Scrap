using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Scrap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Scrap.Models
{
    class LootPalaceModel
    {
        bool hyprMX = false;
        string[] titles = { "Earn Free Gift" };

        public LootPalaceModel(string username, string password, BackgroundWorker bw)
        {

            ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("user-data-dir=" + App.Folder + "profileLP");

            IWebDriver driver = new ChromeDriver(service, options);
            driver.Navigate().GoToUrl("http://lootpalace.com/wp-login.php");

            try
            {
                driver.FindElement(By.Name("log")).Clear();
                driver.FindElement(By.Name("pwd")).Clear();
                driver.FindElement(By.Name("log")).SendKeys(username);
                Helpers.wait(1000);
                driver.FindElement(By.Name("pwd")).SendKeys(password);
                driver.FindElement(By.Id("rememberme")).Click();
                driver.FindElement(By.ClassName("button")).Click();
            }
            catch { }
            finally { }

            Helpers.wait(5000);

            try
            {
                driver.FindElement(By.ClassName("checkin")).Click();
            }
            catch { }
            finally { }

            IList<IWebElement> offerBoxes = driver.FindElements(By.ClassName("offerbox"));

            while (true)
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
                        //driver.FindElement(By.CssSelector("a[src='http://lootpalace.com/wp-content/uploads/2013/07/offers-hypermx.jpg']")).Click();
                        if (!hyprMX)
                        {
                            driver.FindElement(By.XPath("//a[contains(@href, 'hyprmx')]/img")).Click();
                            hyperMX(driver, bw);
                        }
                    }
                    catch { }
                    try
                    {
                        //driver.FindElement(By.CssSelector("a[src='http://lootpalace.com/wp-content/uploads/2013/07/offers-hypermx.jpg']")).Click();
                        if (hyprMX)
                        {
                            driver.FindElement(By.XPath("//a[contains(@href, 'virool')]/img")).Click();
                            virool(driver, bw);
                        }
                    }
                    catch { }
                    try
                    {
                        //driver.FindElement(By.CssSelector("a[src='http://lootpalace.com/wp-content/uploads/2013/07/offers-hypermx.jpg']")).Click();
                        if (hyprMX)
                        {
                            driver.FindElement(By.XPath("//a[contains(@href, 'superrewards')]/img")).Click();
                            superRewards(driver, bw);
                        }
                    }
                    catch { }
                }
            }
        }

        void virool(IWebDriver driver, BackgroundWorker bw)
        {
            driver.FindElement(By.XPath("//a[contains(@href, 'thumbnail')]/img")).Click();
        }

        void superRewards(IWebDriver driver, BackgroundWorker bw)
        {
            bool loop = true;

            try
            {
                driver.SwitchTo().Frame(driver.FindElement(By.ClassName("fancybox-iframe")));
            }
            catch { }

            Helpers.wait(5000);

            try
            {
                driver.FindElement(By.LinkText("Video")).Click();
            }
            catch { }

            Helpers.wait(5000);

            if (driver.FindElement(By.ClassName("no-offers")).Displayed)
            {
                driver.Quit();
            }

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
            while (loop)
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
                            driver.SwitchTo().Frame(driver.FindElement(By.ClassName("fancybox-iframe")));
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#view7-frame iframe")));
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#video_main iframe")));
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
                            if (driver.FindElement(By.Id("ty_headline")).Displayed)
                            {
                                driver.Navigate().GoToUrl("http://lootpalace.com/");
                                loop = false;
                                closeWindows(driver, titles);
                            }
                        }
                        catch { }
                    }
                }
                catch { }
            }
        }

        void hyperMX(IWebDriver driver, BackgroundWorker bw)
        {
            bool loop = true;

            while (loop)
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
                        driver.SwitchTo().Frame(driver.FindElement(By.ClassName("fancybox-iframe")));
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
                        if (driver.FindElement(By.Id("thank_you_content")).Displayed)
                        {
                            Console.WriteLine("Rewarded");
                            switchToBrowserByString(driver, "Earn Free Gift");
                            driver.Navigate().GoToUrl("http://www.lootpalace.com");
                            closeWindows(driver, titles);
                            loop = false;
                        }
                    }
                    catch { }

                    try
                    {
                        if (driver.FindElement(By.Id("offers_exhausted_message")).Displayed)
                        {
                            driver.Navigate().GoToUrl("http://lootpalace.com/");
                            hyprMX = true;
                            Helpers.wait(5000);
                            loop = false;
                        }
                    }
                    catch { }

                    switchToBrowserFrameByString(driver, "vgPlayer");

                    switchFrameByNumber(driver, 0);
                    switchFrameByNumber(driver, 0);

                    switchToBrowserFrameByString(driver, "player");
                    try
                    {
                        driver.FindElement(By.ClassName("ytp-large-play-button")).Click();
                    }
                    catch { }


                    try
                    {
                        driver.FindElement(By.ClassName("nextstepimg")).Click();
                    }
                    catch { }
                    try
                    {
                        driver.FindElement(By.XPath("//img[@alt='Claim your reward']")).Click();
                    }
                    catch { }

                    //switchToBrowserByString(driver, "Now Exploring");
                    try
                    {
                        bool exploring = false;
                        if (driver.Title.Contains("Now Exploring"))
                        {
                            exploring = true;
                        }
                        while (exploring)
                        {
                            System.Collections.ObjectModel.ReadOnlyCollection<string> aMorewindowHandles = driver.WindowHandles;

                            foreach (String windower in aMorewindowHandles)
                            {
                                try
                                {
                                    IWebDriver popup = driver.SwitchTo().Window(windower);
                                }
                                catch { }

                                //switchToBrowserByString(driver, "Now Exploring great content!");
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
                                    driver.FindElement(By.XPath("//a[contains(@href, 'hyprmx')]/img")).Click();
                                }
                                catch { }
                                try
                                {
                                    driver.FindElement(By.XPath("//img[@alt='Next Page']")).Click();
                                }
                                catch { }
                                try
                                {
                                    driver.FindElement(By.XPath("//img[@alt='Claim your reward']")).Click();
                                    exploring = false;
                                }
                                catch { }
                                Helpers.wait(5000);
                            }
                        }
                    }
                    catch { }
                }
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
