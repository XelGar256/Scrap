using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Scrap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Scrap.Models
{
    class ZoomModel
    {
        string[] titles = { "Dashboard", "Watch and", "http://www.zoombucks.com/hourly_offer_contest.php?check_in", "RadioLoyalty" };

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
            try
            {
                driver.FindElement(By.XPath("//a[contains(@href, 'hourly_offer_contest')]")).Click();
                //MessageBox.Show(driver.Title);
                hour = DateTime.Now.Hour;
            }
            catch
            {
                Debug.WriteLine("Couldn't Click Contest");
            }
            finally { }
            Helpers.wait(2000);

            while (!bw.CancellationPending)
            {

                try
                {
                    driver.FindElement(By.LinkText("Jun Videos")).Click();
                    Debug.WriteLine("Jun Videos Clicked!!");
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
                    driver.FindElement(By.ClassName("brand")).Click();
                    Helpers.wait(2000);
                }
                catch
                {
                    try
                    {
                        driver.FindElement(By.ClassName("widgetcontent")).Click();
                    }
                    catch
                    {
                        Helpers.wait(2000);
                        try
                        {
                            driver.FindElement(By.ClassName("brand")).Click();
                        }
                        catch { }
                        finally {}
                    }
                    finally { }
                }

                Helpers.wait(2000);
                Debug.WriteLine(driver.Title.ToString());

                try
                {
                    Debug.WriteLine("Switching to Watch and Get Rewarded!");
                    Helpers.switchToBrowserByString(driver, "Watch and");
                    Debug.WriteLine("hey whats your title");
                    Debug.WriteLine(driver.Title);
                    IList<IWebElement> iframes = driver.FindElements(By.TagName("iframe"));
                    Debug.WriteLine(iframes.Count);

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
                }
                catch { }
                finally { }

                while (driver.Title.Contains("Watch and"))
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
                            Debug.WriteLine("Couldn't Click Contest");
                        }
                        finally { }
                    }

                    Debug.WriteLine("Last Hour: " + hour);
                    Debug.WriteLine("Current Hour: " + DateTime.Now.Hour);
                    try
                    {
                        Debug.WriteLine("Switching to Watch and Get Rewarded!");
                        Helpers.switchToBrowserByString(driver, "Watch and");
                        Debug.WriteLine("hey whats your title");
                        Debug.WriteLine(driver.Title);
                        IList<IWebElement> iframes = driver.FindElements(By.TagName("iframe"));
                        Debug.WriteLine(iframes.Count);

                        try
                        {
                            IWebElement dropDownMonth = driver.FindElement(By.Id("dob_month"));
                            IWebElement dropDownDay = driver.FindElement(By.Id("dob_day"));
                            IWebElement dropDownYear = driver.FindElement(By.Id("dob_year"));
                            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                            Random random = new Random();
                            int rndMonth = random.Next(0, 11);
                            Debug.WriteLine(rndMonth);
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
                            Debug.WriteLine(rndClick);
                            int counterClick = 1;
                            foreach (IWebElement oLink in oLinks)
                            {
                                Debug.WriteLine(counterClick);
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
                            Debug.WriteLine("Zoooommmm!! found, wait 1 seconds");
                            Helpers.wait(1000);
                        }
                        catch
                        {
                            Debug.WriteLine("Waiting 1 Seconds!!");
                            Helpers.wait(1000);
                        }
                        finally { }

                        try
                        {
                            IWebElement nextPage = driver.FindElement(By.ClassName("webtraffic_next_button"));
                            nextPage.Click();
                            Debug.WriteLine("Next Page");
                            Helpers.wait(1000);
                        }
                        catch
                        {
                            Debug.WriteLine("Waiting 1 Seconds!!");
                            Helpers.wait(1000);
                        }
                        finally { }
                        try
                        {
                            IWebElement earn2Swag = driver.FindElement(By.Id("webtraffic_popup_start_button"));
                            earn2Swag.Click();
                            Debug.WriteLine("earn2Swag found, wait 1 seconds");
                            Helpers.wait(1000);
                        }
                        catch
                        {
                            Debug.WriteLine("Waiting 1 Seconds!!");
                            Helpers.wait(1000);
                        }
                        finally { }

                        try
                        {
                            IWebElement nextPage = driver.FindElement(By.Id("webtraffic_popup_next_button"));
                            nextPage.Click();
                            Debug.WriteLine("Next Page");
                            Helpers.wait(1000);
                        }
                        catch
                        {
                            Debug.WriteLine("Waiting 5 Seconds!!");
                            Helpers.wait(1000);
                        }
                        finally { }

                        try
                        {
                            IWebElement rewardText = driver.FindElement(By.Id("ty_header"));
                            if (rewardText.Text == "You earned 2 ZBs!")
                            {
                                Debug.WriteLine("Start Refresh");
                                driver.Navigate().Refresh();
                                Debug.WriteLine("Stop Refresh");
                                Helpers.closeWindows(driver, titles);
                            }
                            else if (rewardText.Text == "You've earned 2 ZBs!")
                            {
                                Debug.WriteLine("Start Refresh");
                                driver.Navigate().Refresh();
                                Debug.WriteLine("Stop Refresh");
                                Helpers.closeWindows(driver, titles);
                            }
                            else if (rewardText.Text == "You earned 1 ZBs!")
                            {
                                Debug.WriteLine("Start Refresh");
                                driver.Navigate().Refresh();
                                Debug.WriteLine("Stop Refresh");
                                Helpers.closeWindows(driver, titles);
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
                                    Debug.WriteLine("Waiting to finish");
                                    try
                                    {
                                        driver.FindElement(By.XPath("//img[@alt='Claim your reward']")).Click();
                                        Helpers.switchToBrowserByString(driver, "Watch and");
                                    }
                                    catch { }
                                    finally { }
                                    Helpers.wait(1000);
                                }
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
                                        Debug.WriteLine("Couldn't Click Contest");
                                    }
                                    finally { }
                                }
                                Debug.WriteLine("Last Hour: " + hour);
                                Debug.WriteLine("Current Hour: " + DateTime.Now.Hour);
                            }
                        }
                        catch { }
                        finally { }

                        /*
                        IList<IWebElement> oframes = driver.FindElements(By.TagName("iframe"));
                        Debug.WriteLine(oframes.Count);

                        if (oframes.Count > 0)
                            Helpers.switchToFrame(driver, iframes, iframes.Count);
                        else
                            driver.SwitchTo().DefaultContent();
                        */

                        /*
                        try
                        {
                            IWebElement youTube = driver.FindElement(By.CssSelector("div.ytp-play-button"));
                            youTube.Click();
                        }
                        catch
                        {
                            IWebElement youTube = driver.FindElement(By.CssSelector("div.ytp-large-play-button"));
                            youTube.Click();
                        }
                        finally { }
                        */

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        Helpers.switchToBrowserByString(driver, "Watch and");
                        try
                        {
                            driver.FindElement(By.Id("app_downloader_button")).Click();

                        }
                        catch
                        {
                            driver.FindElement(By.Id("webtraffic_wrapper")).Click();
                        }
                        finally { }

                        try
                        {
                            driver.FindElement(By.Id("cta_sharing_image")).Click();
                        }
                        catch
                        {
                            driver.FindElement(By.Id("player_wrapper")).Click();
                        }
                        finally { }

                        try
                        {
                            IWebElement twitter = driver.FindElement(By.Id("twitter_sharing"));
                            if (twitter.Displayed)
                            {
                                Helpers.closeWindows(driver, titles);
                                Debug.WriteLine("Start Refresh");
                                driver.Navigate().Refresh();
                                Debug.WriteLine("Stop Refresh");
                            }
                        }
                        catch { }
                        finally { }
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }
                    catch { }
                    finally { }

                    try
                    {
                        IList<IWebElement> eframes = driver.FindElements(By.TagName("iframe"));
                        Debug.WriteLine(eframes.Count);

                        if (eframes.Count > 0)
                            Helpers.switchToFrame(driver, eframes, eframes.Count);
                        else
                            driver.SwitchTo().DefaultContent();

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



                    Debug.WriteLine("Switching to Watch and Get Rewarded!");
                    Helpers.switchToBrowserByString(driver, "Watch and");
                }

                Helpers.wait(10000);
                Debug.WriteLine("Start Refresh");
                try
                {
                    driver.Navigate().Refresh();
                }
                catch { }
                finally { }
                Debug.WriteLine("Stop Refresh");
            }


            //<img src="odometer/2.png" id="odometer">
            //driver.FindElement(By.XPath("//img[@src,'/2014/images/check_in.png']")).Click();
            /*
            driver.Navigate().GoToUrl("http://www.zoombucks.com/video/");
            bool videos = true;
            Helpers.wait(2000);
            IList<IWebElement> vidLinks = driver.FindElements(By.ClassName("watch_video_box_thumb"));
            foreach (IWebElement vidLink in vidLinks)
            {
                vidLink.Click();
                break;
            }
            IWebElement odometer = driver.FindElement(By.Id("odometer"));
            string oldOdometer = odometer.GetAttribute("src");
            while (videos)
            {
                if (!driver.Title.Contains("video"))
                {
                    try
                    {
                        Debug.WriteLine(odometer.GetAttribute("src"));
                    }
                    catch
                    {
                        odometer = driver.FindElement(By.Id("odometer"));
                        Debug.WriteLine(odometer.GetAttribute("src"));
                    }
                    finally { }
                }
                else


                if (oldOdometer != odometer.GetAttribute("src"))
                {
                    oldOdometer = odometer.GetAttribute("src");
                    vidLinks = driver.FindElements(By.ClassName("watch_video_box_thumb"));
                    foreach (IWebElement vidLink in vidLinks)
                    {
                        vidLink.Click();
                        break;
                    }
                }


                Helpers.wait(10000);
            }
             * */


            //ty_header You earned 1 reward!

            //virool
            //
            /*
            try
            {
                Debug.WriteLine("Switching to Watch and Get Rewarded!");
                Helpers.switchToBrowserByString(driver, "Watch and");
                Debug.WriteLine("hey whats your title");
                Debug.WriteLine(driver.Title);
                IList<IWebElement> iframes = driver.FindElements(By.TagName("iframe"));
                Debug.WriteLine(iframes.Count);

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
            }
            catch { }
            finally { }

            //play vid
            try
            {
                driver.FindElement(By.ClassName("ytp-large-play-button")).Click();
            }
            catch { }
            finally { }

            if (driver.FindElement(By.ClassName("btn-inner-text")).Displayed)
                driver.FindElement(By.ClassName("close")).Click();
            */

            try
            {
                driver.Quit();
            }
            catch { }
        }
    }
}
