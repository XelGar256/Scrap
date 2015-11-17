﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Scrap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Scrap.Models
{
    class ZoomModel
    {
        string[] titles = { "Dashboard", "Offer Walls", "Watch and" };
        public ZoomModel(string username, string password, BackgroundWorker bw)
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
                Helpers.wait(5000);
                switchFrameByNumber(driver, 0);

                ById(driver, "webtraffic_popup_start_button");
                ById(driver, "webtraffic_popup_next_button");
                ByClass(driver, "webtraffic_start_button");
                ByClass(driver, "webtraffic_next_button");
                ById(driver, "expository_image");

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
                                switchToBrowserByString(driver, "Dashboard");
                            }
                            catch { }
                            finally { }
                            Helpers.wait(5000);
                        }
                    }
                }
                catch { }
                finally { }

                try
                {
                    switchToBrowserByString(driver, "Watch and");
                    switchFrameByNumber(driver, 0);
                    if (driver.FindElement(By.Id("ty_header")).Text.Contains("ZBs"))
                    {
                        Console.WriteLine("I'm Here!!");
                        Helpers.wait(1000);
                        driver.Navigate().Refresh();
                        closeWindows(driver, titles);
                        Helpers.wait(5000);
                    }
                }
                catch { }
                finally { }

                try
                {
                    if (driver.FindElement(By.TagName("body")).Text.Contains("No Videos available."))
                    {
                        driver.Close();
                    }
                }
                catch { }
                finally { }
            }

            /*
            bool watchAnd = true;
            bool closeWindows = false;

            do
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

                //string mainHandle = driver.CurrentWindowHandle;
                Helpers.wait(1000);
                Helpers.switchToBrowserByString(driver, "Watch and");
                Helpers.wait(1000);
                Helpers.switchFrameByNumber(driver, 0);
                Helpers.ById(driver, "webtraffic_popup_start_button");
                Helpers.switchToBrowserByString(driver, "Watch and");
                Helpers.switchFrameByNumber(driver, 0);
                Helpers.ById(driver, "webtraffic_popup_next_button");
                Helpers.switchToBrowserByString(driver, "Watch and");
                Helpers.switchFrameByNumber(driver, 0);
                Helpers.ByClass(driver, "webtraffic_start_button");
                Helpers.switchToBrowserByString(driver, "Watch and");
                Helpers.switchFrameByNumber(driver, 0);
                Helpers.ByClass(driver, "webtraffic_next_button");

                Helpers.switchToBrowserByString(driver, "Watch and");
                Helpers.switchFrameByNumber(driver, 0);
                Helpers.ById(driver, "expository_image");

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
                                Helpers.switchToBrowserByString(driver, "Dashboard");
                            }
                            catch { }
                            finally { }
                            Helpers.wait(5000);
                        }
                    }
                }
                catch { }
                finally { }

                //Console.WriteLine(driver.FindElement(By.ClassName("reward_text ")).ToString());
                //if (Helpers.isID(driver, "ty_header"));
                Helpers.switchToBrowserByString(driver, "Watch and");
                Helpers.switchFrameByNumber(driver, 0);

                try
                {
                    if (driver.FindElement(By.Id("ty_header")).Text.Contains("ZBs"))
                    {
                        Console.WriteLine("I'm Here!!");
                        //driver.Close();
                        Helpers.wait(1000);
                        Helpers.switchToBrowserByString(driver, "Watch and");
                        driver.Navigate().Refresh();
                        Helpers.closeWindows(driver, titles);
                    }
                }
                catch { }
                finally { }

                //Helpers.closeWindows(driver, titles);

                Helpers.switchToBrowserByString(driver, "Watch and");

                Helpers.switchToBrowserByString(driver, "Watch and");
                try
                {
                    if (driver.FindElement(By.TagName("body")).Text.Contains("No Videos available."))
                    {
                        watchAnd = false;
                    }
                }
                catch { }
                finally { }

                watchAnd = Helpers.lookFor(driver, "Watch and");

            } while (watchAnd);
            */
            switchToBrowserByString(driver, "Dashboard");
            Console.WriteLine(driver.Title);


            driver.Navigate().GoToUrl("http://www.zoombucks.com/offer_walls.php?t=1444653478#volume-11");

            switchToBrowserByString(driver, "Offer Walls");
            bool offerWall = true;

            do
            {
                switchToBrowserByString(driver, "Offer Walls");
                try
                {
                    driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#volume-11 iframe")));
                }
                catch { }
                finally { }
                try
                {
                    if (driver.FindElement(By.ClassName("video_title")).Text != "Watch this video!")
                    {
                        try
                        {
                            Console.WriteLine("Looking for Videos");
                            closeWindows(driver, titles);
                            ById(driver, "next_btn");
                        }
                        catch { }
                        finally { }
                    }
                    else
                    {
                        Console.WriteLine("Videos Found");
                        //if (driver.FindElement(By.Id("ty_header")).Text == "You earned 1 reward!")
                        Helpers.wait(500);
                        driver.SwitchTo().Frame(0);
                        driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));
                        Console.WriteLine("Lets Get Started");
                        ById(driver, "expository_image");
                        ById(driver, "webtraffic_popup_start_button");
                        ById(driver, "webtraffic_popup_next_button");
                        ByClass(driver, "webtraffic_start_button");
                        ByClass(driver, "webtraffic_next_button");
                    }
                }
                catch { }
                finally { }

                try
                {
                    Console.WriteLine(driver.FindElement(By.Id("ty_header")).Text);

                    if (driver.FindElement(By.Id("ty_header")).Displayed)
                    {
                        switchToBrowserByString(driver, "Offer");
                        Console.WriteLine(driver.Title);
                        Console.WriteLine("REWARD!!");
                        //Helpers.closeWindows(driver, titles);
                        Helpers.wait(5000);
                        //driver.Navigate().Refresh();
                        //driver.Navigate().GoToUrl("http://www.zoombucks.com/offer_walls.php?t=1444653478#volume-11");
                        //driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#volume-11 iframe")));
                        //Helpers.ById(driver, "next_btn");
                    }
                }
                catch { }
                finally { }

                try
                {
                    if (driver.FindElement(By.Id("offers_exhausted_message")).Displayed)
                    {
                        driver.Navigate().Refresh();
                        Helpers.wait(5000);
                    }
                }
                catch { }
                finally { }

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
                        }
                        try
                        {
                            driver.FindElement(By.XPath("//img[@alt='Claim your reward']")).Click();
                            break;
                        }
                        catch { }
                        finally { }
                        Helpers.wait(5000);
                    }
                }
                catch { }
                finally { }

                Helpers.wait(5000);
                switchToBrowserByString(driver, "Offer Walls");

                try
                {
                    Console.WriteLine("Switching to Browser");
                    switchToBrowserByString(driver, "Offer Walls");
                    Console.WriteLine("Switching to Frame");
                    driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#volume-11 iframe")));
                    Console.WriteLine("Switching to Frame");
                    driver.SwitchTo().Frame(0);
                    Console.WriteLine("Switching to Frame");
                    driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));

                    if (driver.FindElement(By.Id("ty_header")).Text == "You earned 1 reward!")
                    {
                        Console.WriteLine("refreshing....");
                        driver.Navigate().Refresh();
                    }
                }
                catch { }
                finally { }



            } while (offerWall);

            /*
            try
            {
                driver.Quit();
            }
            catch { }
            */
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
