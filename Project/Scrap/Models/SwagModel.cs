using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Scrap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Scrap.Models
{
    class SwagModel
    {
        string[] titles = { "Home | Swagbucks", "nCrave | Swagbucks", "www.swagbucks.com/?", "Entertainmentcrave.com" };
        public SwagModel(string username, string password, BackgroundWorker bw, bool vids)
        {

            ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("user-data-dir=" + App.Folder + "profileSB");


            IWebDriver driver = new ChromeDriver(service, options);

            driver.Navigate().GoToUrl("http://www.swagbucks.com/p/login");

            Helpers.wait(1000);
            //switchToBrowserByString(driver, "Login | Swagbucks");

            try
            {

                IWebElement userNames = driver.FindElement(By.Id("sbxJxRegEmail"));
                Helpers.wait(500);
                userNames.SendKeys(username);
                Helpers.wait(500);
                IWebElement userPassword = driver.FindElement(By.Id("sbxJxRegPswd"));
                Helpers.wait(500);
                userPassword.SendKeys(password);
                //xbxJxRegPswd
            }
            catch { }
            finally { }

            try
            {
                driver.FindElement(By.Id("loginBtn")).Click();
            }
            catch { }
            finally { }

            Helpers.wait(500);

            try
            {
                driver.FindElement(By.Id("swagButtonModalExit")).Click();
            }
            catch { }
            finally { }
            Helpers.wait(500);
            while (!bw.CancellationPending && vids)
            {
                Videos(driver, bw);
            }

            while (!bw.CancellationPending && !vids)
            {
                discoveryBreak(driver, bw);
                Helpers.switchToBrowserByString(driver, "Home | Swagbucks");
                Helpers.closeWindows(driver, titles);
                nGage(driver, bw);
            }

            driver.Quit();
        }

        void discoveryBreak(IWebDriver driver, BackgroundWorker bw)
        {
            bool discoBreak = false;
            try
            {
                IWebElement discBreak = driver.FindElement(By.Id("cardContentImg-1-5"));
                discBreak.Click();
                discoBreak = true;
            }
            catch
            {
                try
                {
                    IWebElement discBreak = driver.FindElement(By.Id("cardContentImg-1"));
                    discBreak.Click();
                    discoBreak = true;
                }
                catch
                {
                    try
                    {
                        IWebElement discBreak = driver.FindElement(By.Id("sbHomeCard-1-5"));
                        discBreak.Click();
                        discoBreak = true;
                    }
                    catch
                    {
                        try
                        {
                            IWebElement discBreak = driver.FindElement(By.Id("sbHomeCard54559-563"));
                            discBreak.Click();
                            discoBreak = true;
                        }
                        catch
                        {
                            Console.WriteLine("No Discovery Break Found!!");
                        }
                        finally { }
                    }
                }
            }

            Helpers.wait(5000);
            try
            {
                while (discoBreak && !bw.CancellationPending)
                {
                    Helpers.minimizeWindows(driver, titles);
                    try
                    {
                        driver.SwitchTo().Alert().Dismiss();
                    }
                    catch { }
                    finally { }
                    Console.WriteLine("In the discoBreak!!");
                    Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
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
                    finally { }

                    Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                    try
                    {
                        Helpers.switchToBrowserFrameByString(driver, "junFrame");
                        IWebElement earn2Swag = driver.FindElement(By.Id("webtraffic_popup_start_button"));
                        earn2Swag.Click();
                        Console.WriteLine("earn2Swag found,Helpers.wait 5 seconds");
                        Helpers.wait(500);
                    }
                    catch
                    {
                        Console.WriteLine("Waiting 5 Seconds!!");
                        Helpers.wait(500);
                    }

                    Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                    try
                    {
                        Helpers.switchToBrowserFrameByString(driver, "junFrame");
                        IWebElement nextPage = driver.FindElement(By.Id("webtraffic_popup_next_button"));
                        nextPage.Click();
                        Console.WriteLine("Next Page");
                        Helpers.wait(500);
                    }
                    catch
                    {
                        Console.WriteLine("Waiting 5 Seconds!!");
                        Helpers.wait(500);
                    }

                    Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                    try
                    {
                        Helpers.switchToBrowserFrameByString(driver, "junFrame");
                        IWebElement earned2Swag = driver.FindElement(By.Id("ty_header"));
                        string earn2;
                        earn2 = earned2Swag.Text;
                        if (earned2Swag.Text == "You earned 2 Swag Bucks!")
                        {
                            Console.WriteLine("This is a test to find earned2Swag.Text");
                            Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                            /*
                            using (var destination = File.AppendText("pointsLog.txt"))
                            {
                                destination.WriteLine(Regex.Match(earn2, @"\d+").Value);
                            }
                            */
                            IWebElement viewMoreContent = driver.FindElement(By.XPath("//*[@class=\"btn1 btn2\"]"));
                            viewMoreContent.Click();
                            Helpers.closeWindows(driver, titles);
                        }

                    }
                    catch
                    {
                        Console.WriteLine("Waiting 5 Seconds!!");
                        Helpers.wait(500);
                    }
                    finally { }

                    Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                    try
                    {
                        Helpers.switchToBrowserFrameByString(driver, "junFrame");
                        if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                        {
                            Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                            IWebElement viewMoreContent = driver.FindElement(By.XPath("//*[@class=\"btn1 btn2\"]"));
                            viewMoreContent.Click();
                            Helpers.closeWindows(driver, titles);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Waiting 5 seconds!!");
                        Helpers.wait(500);
                    }
                    finally { }

                    Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                    try
                    {
                        Helpers.switchToBrowserFrameByString(driver, "junFrame");
                        driver.FindElement(By.Id("expository_image")).Click();
                    }
                    catch { }
                    finally { }

                    Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                    try
                    {
                        Helpers.switchToBrowserFrameByString(driver, "junFrame");
                        driver.FindElement(By.Id("compositor_placed_innerclip_reuben")).Click();
                    }
                    catch { }
                    finally { }

                    Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                    try
                    {
                        Helpers.switchToBrowserFrameByString(driver, "junFrame");
                        if (driver.FindElement(By.Id("ty_body_text")).Displayed)
                        {
                            Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                            IWebElement viewMoreContent = driver.FindElement(By.XPath("//*[@class=\"btn1 btn2\"]"));
                            viewMoreContent.Click();
                            Helpers.closeWindows(driver, titles);
                        }
                    }
                    catch { }
                    finally { }

                    Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                    try
                    {
                        Helpers.switchToBrowserFrameByString(driver, "junFrame");
                        if (driver.FindElement(By.Id("countdown_control")).Text == "00:-1")
                        {
                            Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                            IWebElement viewMoreContent = driver.FindElement(By.XPath("//*[@class=\"btn1 btn2\"]"));
                            viewMoreContent.Click();
                            Helpers.closeWindows(driver, titles);
                        }
                    }
                    catch { }
                    finally { }

                    Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                    try
                    {
                        Helpers.switchToBrowserFrameByString(driver, "junFrame");
                        if (driver.FindElement(By.Id("offers_exhausted_message")).Displayed)
                        {
                            Console.WriteLine("Exiting discoBreak");
                            Helpers.switchToBrowserByString(driver, "Home | Swagbucks");
                            Helpers.closeWindows(driver, titles);
                            driver.Close();
                        }
                    }
                    catch { }
                    finally { }

                    Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                    try
                    {
                        Helpers.switchToBrowserFrameByString(driver, "junFrame");
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
                    }
                    catch { }
                    finally { }

                    Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                    try
                    {
                        Helpers.switchToBrowserFrameByString(driver, "junFrame");
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
                    }
                    catch { }
                    finally { }

                    Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                    System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                    int maxCount = windowHandles.Count;
                    int count = 0;

                    foreach (String window in windowHandles)
                    {
                        IWebDriver popup = driver.SwitchTo().Window(window);
                        try
                        {
                            if (popup.Title.Contains("www.swagbucks.com/?"))
                            {
                                break;
                            }
                        }
                        catch { }
                        finally { }
                        count++;
                    }
                    if (count == maxCount)
                        discoBreak = false;
                }
            }
            catch { }
            finally { }
        }


        void nGage(IWebDriver driver, BackgroundWorker bw)
        {
            try
            {
                IWebElement nGageCard = driver.FindElement(By.Id("cardContentImg-14-5"));
                nGageCard.Click();
                Console.WriteLine("NGAGE!!!!");
            }
            catch
            {
                try
                {
                    IWebElement nGageCard = driver.FindElement(By.Id("cardContentImg-14"));
                    nGageCard.Click();
                }
                catch
                {
                    try
                    {
                        IWebElement nGageCard = driver.FindElement(By.Id("sbHomeCard-14-5"));
                        nGageCard.Click();
                    }
                    catch
                    {
                        try
                        {
                            IWebElement nGageCard = driver.FindElement(By.Id("sbHomeCard54560-563"));
                            nGageCard.Click();
                        }
                        catch
                        {
                            Console.WriteLine("No nGageCard found");
                        }
                        finally { }
                    }
                }
            }
            Helpers.wait(5000);
            Helpers.switchToBrowserByString(driver, "nGage");
            try
            {
                while (driver.Title.Contains("nGage") && !bw.CancellationPending)
                {
                    Helpers.minimizeWindows(driver, titles);
                    try
                    {
                        driver.SwitchTo().Alert().Dismiss();
                        Helpers.closeWindows(driver, titles);
                    }
                    catch { }
                    finally { }

                    Helpers.switchToBrowserByString(driver, "nGage");
                    try
                    {
                        IWebElement startEarningBtn = driver.FindElement(By.XPath("//*[@class=\"success\"][@id=\"startEarning\"]"));
                        startEarningBtn.Click();
                        Console.WriteLine("startEarningBtn found,Helpers.wait 5 seconds");
                        Helpers.wait(500);
                    }
                    catch
                    {
                        Console.WriteLine("stuff");
                        Helpers.wait(500);
                    }

                    try
                    {
                        IWebElement discoverMoreBtn = driver.FindElement(By.XPath("//*[@class=\"success\"][@id=\"discoverMore\"]"));
                        discoverMoreBtn.Click();
                        Helpers.closeWindows(driver, titles);
                        Helpers.wait(500);
                    }
                    catch
                    {

                    }
                    try
                    {
                        System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                        foreach (String window in windowHandles)
                        {
                            IWebDriver popup = driver.SwitchTo().Window(window);
                            try
                            {
                                if (popup.Title.Contains("Not Available"))
                                {
                                    driver.SwitchTo().Window(window);
                                    driver.Close();
                                    break;
                                }
                            }
                            catch { }
                            finally { }
                        }
                    }
                    catch { }
                    finally { }
                    Helpers.switchToBrowserByString(driver, "nGage");
                }
            }
            catch { }
            finally { }
        }

        void Videos(IWebDriver driver, BackgroundWorker bw)
        {
            string links = "Wedding";
            Helpers.wait(1000);
            IWebElement findWatch = driver.FindElement(By.LinkText("Watch"));
            if (findWatch.Displayed)
            {
                findWatch.Click();
            }
            else
            {
                Console.WriteLine("Could not find Watch");
            }

            Helpers.wait(1000);

            IWebElement catLinks = driver.FindElement(By.LinkText("Personal Finance"));
            if (catLinks.Displayed)
            {
                catLinks.Click();
            }
            else
            {
                Console.WriteLine("Couldn't click on link: " + links);
            }

            Helpers.wait(1000);
            driver.FindElement(By.ClassName("sbTrayListItemHeader")).Click();

            while (true)
            {
                try
                {
                    if (driver.FindElement(By.ClassName("showPlaylists")).Displayed)
                    {
                        Helpers.wait(1000);
                        driver.FindElement(By.ClassName("playlistsShowImage")).Click();
                    }
                }
                catch { }
                finally { }
            }
        }
    }
}
