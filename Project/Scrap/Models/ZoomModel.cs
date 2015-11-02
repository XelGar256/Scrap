using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Scrap.Classes;
using System;
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
            Helpers.switchToBrowserByString(driver, "Dashboard");
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

            Helpers.ByClass(driver, "brand");


            Helpers.wait(1000);
            Helpers.ByClass(driver, "widgetcontent");
            Helpers.wait(1000);

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
                        closeWindows = true;
                    }
                }
                catch { }
                finally { }

                //Helpers.closeWindows(driver, titles);

                Helpers.switchToBrowserByString(driver, "Watch and");

                if (closeWindows)
                {
                    Helpers.closeWindows(driver, titles);
                    closeWindows = false;
                }

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

            Helpers.switchToBrowserByString(driver, "Dashboard");
            Console.WriteLine(driver.Title);


            driver.Navigate().GoToUrl("http://www.zoombucks.com/offer_walls.php?t=1444653478#volume-11");

            Helpers.switchToBrowserByString(driver, "Offer Walls");
            bool offerWall = true;

            do
            {
                Helpers.switchToBrowserByString(driver, "Offer Walls");
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
                            Helpers.ById(driver, "next_btn");
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
                        Helpers.ById(driver, "expository_image");
                        Helpers.ById(driver, "webtraffic_popup_start_button");
                        Helpers.ById(driver, "webtraffic_popup_next_button");
                        Helpers.ByClass(driver, "webtraffic_start_button");
                        Helpers.ByClass(driver, "webtraffic_next_button");
                    }
                }
                catch { }
                finally { }

                try
                {
                    Console.WriteLine(driver.FindElement(By.Id("ty_header")).Text);

                    if (driver.FindElement(By.Id("ty_header")).Displayed)
                    {
                        Helpers.switchToBrowserByString(driver, "Offer");
                        Console.WriteLine(driver.Title);
                        Console.WriteLine("REWARD!!");
                        //Helpers.closeWindows(driver, titles);
                        Helpers.wait(500);
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
                    }
                }
                catch { }
                finally { }

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

                Helpers.switchToBrowserByString(driver, "Offer Walls");

                try
                {
                    Console.WriteLine("Switching to Browser");
                    Helpers.switchToBrowserByString(driver, "Offer Walls");
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
    }
}
