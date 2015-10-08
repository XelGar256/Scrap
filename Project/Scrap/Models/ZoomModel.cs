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
        public ZoomModel(string username, string password, BackgroundWorker bw)
        {
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
            Helpers.wait(5000);

            try
            {
                driver.FindElement(By.LinkText("Jun Videos")).Click();
                Console.WriteLine("Jun Videos Clicked!!");
                Helpers.switchToBrowserByString(driver, "Watch Videos");
                IList<IWebElement> oLinks = driver.FindElements(By.ClassName("go-btns"));
                int count = 0;
                foreach (IWebElement oLink in oLinks)
                {
                    if (count == 1)
                    {
                        oLink.FindElement(By.TagName("a")).Click();
                        break;
                    }
                    count++;
                }
                Helpers.wait(2000);
                if (driver.Title.Contains("Offer"))
                {
                    driver.FindElement(By.ClassName("brand")).Click();
                    Helpers.wait(2000);
                    driver.FindElement(By.ClassName("widgetcontent")).Click();
                }
            }
            catch
            {
                driver.FindElement(By.ClassName("widgetcontent")).Click();
            }

            Helpers.wait(2000);
            Console.WriteLine(driver.Title.ToString());

            while (!bw.CancellationPending)
            {
                try
                {
                    Console.WriteLine("Switching to Watch and Get Rewarded!");
                    Helpers.switchToBrowserByString(driver, "Watch and");
                    Console.WriteLine("hey whats your title");
                    Console.WriteLine(driver.Title);
                    IList<IWebElement> iframes = driver.FindElements(By.TagName("iframe"));
                    Console.WriteLine(iframes.Count);

                    if (!driver.Title.Contains("Watch and"))
                    {
                        try
                        {
                            driver.FindElement(By.ClassName("widgetcontent")).Click();
                        }
                        catch { }
                        finally { }
                    }

                    if (iframes.Count > 0)
                        Helpers.switchToFrame(driver, iframes, iframes.Count);
                    else
                        driver.SwitchTo().DefaultContent();


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
                        driver.FindElement(By.Id("demosubmitimg")).Click();
                    }
                    catch { }
                    finally { }

                    try
                    {
                        IWebElement earn2Swag = driver.FindElement(By.ClassName("webtraffic_start_button"));
                        earn2Swag.Click();
                        Console.WriteLine("earn2Swag found, wait 5 seconds");
                        Helpers.wait(5000);
                    }
                    catch
                    {
                        Console.WriteLine("Waiting 5 Seconds!!");
                        Helpers.wait(5000);
                    }
                    finally { }

                    try
                    {
                        IWebElement nextPage = driver.FindElement(By.ClassName("webtraffic_next_button"));
                        nextPage.Click();
                        Console.WriteLine("Next Page");
                        Helpers.wait(5000);
                    }
                    catch
                    {
                        Console.WriteLine("Waiting 5 Seconds!!");
                        Helpers.wait(5000);
                    }
                    finally { }
                    try
                    {
                        IWebElement earn2Swag = driver.FindElement(By.Id("webtraffic_popup_start_button"));
                        earn2Swag.Click();
                        Console.WriteLine("earn2Swag found, wait 5 seconds");
                        Helpers.wait(5000);
                    }
                    catch
                    {
                        Console.WriteLine("Waiting 5 Seconds!!");
                        Helpers.wait(5000);
                    }
                    finally { }

                    try
                    {
                        IWebElement nextPage = driver.FindElement(By.Id("webtraffic_popup_next_button"));
                        nextPage.Click();
                        Console.WriteLine("Next Page");
                        Helpers.wait(5000);
                    }
                    catch
                    {
                        Console.WriteLine("Waiting 5 Seconds!!");
                        Helpers.wait(5000);
                    }
                    finally { }

                    try
                    {
                        IWebElement rewardText = driver.FindElement(By.Id("ty_header"));
                        if (rewardText.Text == "You earned 2 ZBs!")
                        {
                            Console.WriteLine("Start Refresh");
                            driver.Navigate().Refresh();
                            Console.WriteLine("Stop Refresh");
                            Helpers.closeWindows(driver, new string[] { "Dashboard", "Watch and" });
                        }
                        else if (rewardText.Text == "You've earned 2 ZBs!")
                        {
                            Console.WriteLine("Start Refresh");
                            driver.Navigate().Refresh();
                            Console.WriteLine("Stop Refresh");
                            Helpers.closeWindows(driver, new string[] { "Dashboard", "Watch and" });
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
                                try
                                {
                                    driver.FindElement(By.XPath("//img[@alt='Claim your reward']")).Click();
                                    Helpers.switchToBrowserByString(driver, "Watch and");
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
                        Console.WriteLine(driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed);
                        if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                        {
                            Console.WriteLine("Start Refresh");
                            driver.Navigate().Refresh();
                            Console.WriteLine("Stop Refresh");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Waiting 2 seconds!!");
                        Helpers.wait(2000);
                    }
                    finally { }

                    //Helpers.switchToBrowserFrameByString(driver, "player");
                    try
                    {
                        driver.FindElement(By.Id("player")).Click();
                    }
                    catch { }
                    finally { }
                    try
                    {
                        IWebElement youTube = driver.FindElement(By.ClassName("ytp-play-button"));
                        youTube.Click();
                    }
                    catch
                    {
                        IWebElement youTube = driver.FindElement(By.ClassName("ytp-large-play-button"));
                        youTube.Click();
                    }
                    finally { }

                    try
                    {
                        if (driver.FindElement(By.ClassName("naked")).Displayed)
                        {
                            Console.WriteLine("Start Refresh");
                            driver.Navigate().Refresh();
                            Console.WriteLine("Stop Refresh");
                        }
                    }
                    catch { }
                    finally { }

                    try
                    {
                        if (driver.FindElement(By.TagName("body")).Text == "No Videos available.")
                            driver.Close();
                    }
                    catch { }
                    finally { }
                }
                catch { }
                finally { }
            }

            try
            {
                driver.FindElement(By.LinkText("Jun Videos")).Click();
                Console.WriteLine("Jun Videos Clicked!!");
                Helpers.switchToBrowserByString(driver, "Watch Videos");
                IList<IWebElement> oLinks = driver.FindElements(By.ClassName("go-btns"));
                int count = 0;
                foreach (IWebElement oLink in oLinks)
                {
                    if (count == 0)
                    {
                        oLink.FindElement(By.TagName("a")).Click();
                        break;
                    }
                    count++;
                }
                Helpers.wait(2000);
                if (driver.Title.Contains("Offer"))
                {
                    driver.FindElement(By.ClassName("brand")).Click();
                    Helpers.wait(2000);
                    driver.FindElement(By.ClassName("widgetcontent")).Click();
                }
            }
            catch { }
            finally { }

            try
            {
                driver.Quit();
            }
            catch { }
        }
    }
}
