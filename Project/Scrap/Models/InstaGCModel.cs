using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Scrap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Scrap.Models
{
    class InstaGCModel
    {
        public InstaGCModel(string username, string password, BackgroundWorker bw)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("user-data-dir=" + App.Folder + "profileGC");

            IWebDriver driver = new ChromeDriver(service, options);
            driver.Navigate().GoToUrl("http://www.instagc.com/users/login");

            try
            {
                driver.FindElement(By.Name("username")).Clear();
                driver.FindElement(By.Name("password")).Clear();
                driver.FindElement(By.Name("username")).SendKeys(username);
                Helpers.wait(1000);
                driver.FindElement(By.Name("password")).SendKeys(password);
                driver.FindElement(By.ClassName("action")).Click();
            }
            catch { }
            finally { }

            Helpers.wait(5000);
            driver.Navigate().GoToUrl("http://www.instagc.com/earn/encrave");
            encrave(driver, bw);
        }

        public static void encrave(IWebDriver driver, BackgroundWorker bw)
        {
            bool clickVid = false;
            try
            {
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[contains(@name, 'easyXDM')]")));
            }
            catch { }

            switchToBrowserFrameByString(driver, "parentiframe");

            driver.FindElement(By.ClassName("disableText")).Click();

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

                        switchToBrowserByString(driver, "Entertainmentcrave");
                        switchToBrowserFrameByString(driver, "contIframe");

                        IList<IWebElement> tests = driver.FindElements(By.TagName("iframe"));
                        Console.WriteLine("/********************************/");
                        foreach (IWebElement test in tests)
                        {
                            Console.WriteLine(test.Displayed);
                            Console.WriteLine(test.GetCssValue("id"));
                            Console.WriteLine(test.GetCssValue("name"));
                            Console.WriteLine("***/***/***");
                        }
                        Console.WriteLine("/********************************/");

                        Helpers.wait(5000);

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
                            driver.FindElement(By.LinkText("Next")).Click();
                        }
                        catch { }

                        try
                        {
                            if (!clickVid)
                            {
                                driver.FindElement(By.Id("vidaolplay")).Click();
                                clickVid = true;
                            }
                        }
                        catch { }

                        switchFrameByNumber(driver, 0);

                        try
                        {
                            if (!clickVid)
                            {
                                Console.WriteLine("o2player_1");
                                driver.FindElement(By.Id("o2player_1")).Click();
                                clickVid = true;
                            }
                        }
                        catch { }

                        switchToBrowserByString(driver, "Entertainmentcrave");
                        try
                        {
                            driver.SwitchTo().DefaultContent();
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().ParentFrame();
                        }
                        catch { }

                        try
                        {
                            driver.FindElement(By.Id("link_down")).Click();
                            clickVid = false;
                        }
                        catch { }

                        try
                        {
                            driver.FindElement(By.ClassName("keepCraving")).Click();
                            clickVid = false;
                        }
                        catch { }
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
