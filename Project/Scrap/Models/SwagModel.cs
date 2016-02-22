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
        string[] titles = { "Home | Swagbucks", "nCrave | Swagbucks", "www.swagbucks.com/?", "Entertainmentcrave.com", "nGage" };
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
            Console.WriteLine("I have logged In!!");

            try
            {
                driver.FindElement(By.Id("swagButtonModalExit")).Click();
            }
            catch { }
            finally { }
            Console.WriteLine("Please sir no swag button");

            dailys(driver, bw);

            Helpers.wait(500);
            while (!bw.CancellationPending && vids)
            {
                Videos(driver, bw);
            }
            Console.WriteLine("past the vids");

            while (true)
            {
                //nCrave(driver, bw);
                //swago(driver, bw);
                discoveryBreak(driver, bw);
                nGage(driver, bw);
                switchToBrowserByString(driver, "Home | Swagbucks");
                try
                {
                    driver.Navigate().Refresh();
                }
                catch { }
            }
            driver.Quit();
        }

        void swago(IWebDriver driver, BackgroundWorker bw)
        {
            ById(driver, "promoBannerIcon");
            switchToBrowserByString(driver, "Swagbucks.com");
            ById(driver, "swagoJoinNowCta");
        }

        void dailys(IWebDriver driver, BackgroundWorker bw)
        {
            try
            {
                driver.FindElement(By.LinkText("Daily Poll")).Click();
            }
            catch { }
            Helpers.wait(1000);
            try
            {
                IList<IWebElement> pollCheckboxes = driver.FindElements(By.ClassName("pollCheckbox"));
                Random random = new Random();
                int rndClick = random.Next(1, pollCheckboxes.Count);
                Console.WriteLine(rndClick);
                int counterClick = 1;
                foreach (IWebElement pollCheckbox in pollCheckboxes)
                {
                    Console.WriteLine(counterClick);
                    if (counterClick == rndClick)
                    {
                        pollCheckbox.Click();
                    }
                    counterClick++;
                }
            }
            catch { }
            Helpers.wait(1000);
            ByClass(driver, "todayPoll");
            Helpers.wait(1000);
            ByClass(driver, "logoTopbar");
            Helpers.wait(1000);
        }

        void nCrave(IWebDriver driver, BackgroundWorker bw)
        {
            bool craveMe = false;
            try
            {
                driver.FindElement(By.LinkText("Daily Crave")).Click();
                craveMe = true;
            }
            catch { }
            Helpers.wait(1000);
            try
            {
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[contains(@name, 'easyXDM')]")));
            }
            catch { }
            finally { }
            switchToBrowserFrameByString(driver, "parentiframe");
            try
            {
                /*
                IList<IWebElement> encraveTasks = driver.FindElements(By.ClassName("disableText"));
                int counter = 0;
                foreach (IWebElement encraveTask in encraveTasks)
                {
                    if (counter == encraveTasks.Count - 1)
                    {
                        encraveTask.Click();
                    }
                    counter++;
                }
                */
                driver.FindElement(By.ClassName("disableText")).Click();
                Helpers.wait(5000);
            }
            catch { }

            Helpers.wait(1000);
            switchToBrowserByString(driver, "Entertainmentcrave");
            //ById(driver, "crave_on");
            Helpers.wait(1000);
            while (craveMe)
            {
                bool someSwitch = false;
                closeWindows(driver, titles);

                try
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                    foreach (String window in windowHandles)
                    {
                        driver.SwitchTo().Window(window);
                        driver.SwitchTo().DefaultContent();

                        try
                        {
                            IList<IWebElement> urlLinks = driver.FindElements(By.ClassName("url-link"));
                            Console.WriteLine("Found url-link");
                            Console.WriteLine(urlLinks.Count);
                            Helpers.wait(1000);
                            foreach (IWebElement urlLink in urlLinks)
                            {
                                Console.WriteLine("Fuck Off Swagbucks");
                                urlLink.Click();
                                try
                                {
                                    if (driver.FindElement(By.ClassName("switch")).Displayed && !someSwitch)
                                    {
                                        ByClass(driver, "switch");
                                        someSwitch = true;
                                    }
                                }
                                catch { }
                                ById(driver, "link_down");
                                Helpers.wait(60000);
                                switchToBrowserFrameByString(driver, "contIframe");
                                ByClass(driver, "owl-next");
                                switchToBrowserByString(driver, "Entertainmentcrave");
                                driver.SwitchTo().DefaultContent();
                            }
                        }
                        catch { }
                        ById(driver, "link_down");
                        ByClass(driver, "keepCraving");

                        //switchToBrowserFrameByString(driver, "contIframe");
                        //driver.FindElement(By.ClassName("beforeswfanchor3")).Click();
                    }
                }
                catch { }
            }
            Helpers.wait(1000);
            ByClass(driver, "logoTopbar");
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
                while (discoBreak)
                {
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
                        /*
                        if (earned2Swag.Text == "You earned 2 Swag Bucks!")
                        {
                            Console.WriteLine("This is a test to find earned2Swag.Text");
                            Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                            using (var destination = File.AppendText("pointsLog.txt"))
                            {
                                destination.WriteLine(Regex.Match(earn2, @"\d+").Value);
                            }
                            IWebElement viewMoreContent = driver.FindElement(By.XPath("//*[@class=\"btn1 btn2\"]"));
                            viewMoreContent.Click();
                            Helpers.closeWindows(driver, titles);
                        }
                        */
                    }
                    catch
                    {
                        Console.WriteLine("Waiting 5 Seconds!!");
                        Helpers.wait(500);
                    }
                    finally { }
                    try
                    {

                        if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
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
                    catch { }

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
            Random random = new Random();
            bool nGageCards = false;

            try
            {
                IWebElement nGageCard = driver.FindElement(By.Id("cardContentImg-14-5"));
                nGageCard.Click();
                Console.WriteLine("NGAGE!!!!");
                nGageCards = true;
            }
            catch
            {
                try
                {
                    IWebElement nGageCard = driver.FindElement(By.Id("cardContentImg-14"));
                    nGageCard.Click();
                    nGageCards = true;
                }
                catch
                {
                    try
                    {
                        IWebElement nGageCard = driver.FindElement(By.Id("sbHomeCard-14-5"));
                        nGageCard.Click();
                        nGageCards = true;
                    }
                    catch
                    {
                        try
                        {
                            IWebElement nGageCard = driver.FindElement(By.Id("sbHomeCard54560-563"));
                            nGageCard.Click();
                            nGageCards = true;
                        }
                        catch
                        {
                            Console.WriteLine("No nGageCard found");
                        }
                    }
                }
            }
            Helpers.wait(5000);
            Helpers.switchToBrowserByString(driver, "nGage");
            try
            {
                while (nGageCards)
                {
                    switchToBrowserByString(driver, "nGage");
                    try
                    {
                        IWebElement startEarningBtn = driver.FindElement(By.XPath("//*[@class=\"success\"][@id=\"startEarning\"]"));
                        startEarningBtn.Click();
                        Console.WriteLine("startEarningBtn found,Helpers.wait 5 seconds");
                        Helpers.wait(2000);
                    }
                    catch
                    {
                        Console.WriteLine("stuff");
                        Helpers.wait(2000);
                    }

                    try
                    {
                        IWebElement discoverMoreBtn = driver.FindElement(By.XPath("//*[@class=\"success\"][@id=\"discoverMore\"]"));
                        discoverMoreBtn.Click();
                        closeWindows(driver, titles);
                        Helpers.wait(2000);
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
                                int rndClick = random.Next(2);

                                driver.FindElements(By.XPath("//div[@id='nextPage']/a"))[rndClick].Click();
                                break;
                            }
                            catch { }

                            try
                            {
                                driver.FindElement(By.Id("discoverMore")).Click();
                                closeWindows(driver, titles);
                                Helpers.wait(2000);
                                break;
                            }
                            catch { }
                        }
                    }
                    catch { }

                    switchToBrowserByString(driver, "nGage");

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

                    switchToBrowserByString(driver, "nGage");
                }
            }
            catch { }
        }

        void Videos(IWebDriver driver, BackgroundWorker bw)
        {
            string[] links = { "Editor's Pick", "Careers", "Comedy", "Entertainment", "Fashion", "Fitness", "Food", "Health", "Hobbies", "Home & Garden", "Music", "News & Politics", "Parenting", "Personal Finance", "Pets & Animals", "Shopping", "Sports", "Technology", "Travel", "Wedding" };
            int videoCount = 0;
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

            Helpers.wait(2000);

            // This is to click a link on left //
            IWebElement catLinks = driver.FindElement(By.LinkText(links[0].ToString()));
            if (catLinks.Displayed)
            {
                catLinks.Click();
            }
            else
            {
                Console.WriteLine("Couldn't click on link: " + links);
            }

            Helpers.wait(2000);
            driver.FindElement(By.ClassName("sbTrayListItemHeader")).Click();

            while (true)
            {
                IList<IWebElement> vidLinks = driver.FindElements(By.ClassName("sbPlaylistVideoImage"));
                videoCount = vidLinks.Count - 1;
                Console.WriteLine(vidLinks.Count);
                foreach (IWebElement vidLink in vidLinks)
                {
                    Helpers.wait(120000);
                    try
                    {
                        vidLink.Click();
                    }
                    catch { }
                    finally { }
                }
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

            /*
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
