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

            ByClass(driver, "ss-icon");

            try
            {
                driver.FindElement(By.Id("loginFormEmail")).SendKeys(username);
                driver.FindElement(By.ClassName("hero-form-first-password")).SendKeys(password);
            }
            catch { }

            ById(driver, "loginSubmit");

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
                try
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                    foreach (String window in windowHandles)
                    {
                        IWebDriver popup = driver.SwitchTo().Window(window);

                        try
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#displayWrap iframe")));
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

                        ById(driver, "webtraffic_popup_start_button");
                        ById(driver, "webtraffic_popup_next_button");
                        ByClass(driver, "webtraffic_start_button");
                        ByClass(driver, "webtraffic_next_button");

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
                            IWebElement rewardText = driver.FindElement(By.Id("ty_header"));
                            if (rewardText.Text == "You earned 1 Points!")
                            {
                                driver.Navigate().Refresh();
                                closeWindows(driver, titles);
                            }
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

                        try
                        {
                            switchToBrowserByString(driver, "Now Exploring great content!");
                            while (driver.Title.Contains("Now Exploring"))
                            {
                                switchToBrowserByString(driver, "Now Exploring great content!");
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
                                        switchToBrowserByString(driver, "Offer Walls");
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
                        /*
                        try
                        {
                            if (driver.FindElement(By.ClassName("reward_text")).Displayed)
                            {
                            */

                        try
                        {
                            if (driver.FindElement(By.Id("ty_header")).Text.Contains("Points"))
                            {
                                closeWindows(driver, titles);
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

                        try
                        {
                            if (driver.FindElement(By.Id("ty_header")).Text.Contains("Points"))
                            {
                                closeWindows(driver, titles);
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
                                closeWindows(driver, titles);
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
                    }
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
    }
}
