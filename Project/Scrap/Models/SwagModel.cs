using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Scrap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Scrap.Models
{
    class SwagModel
    {
        string[] titles = { "Home | Swagbucks", "nCrave | Swagbucks", "www.swagbucks.com/?", "Entertainmentcrave.com", "nGage" };
        public SwagModel(string username, string password, BackgroundWorker bw, bool vids, bool openBucks)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("user-data-dir=" + App.Folder + "profileSB");

            IWebDriver driver = new ChromeDriver(service, options);

            driver.Navigate().GoToUrl("http://www.swagbucks.com/p/login");

            Helpers.wait(1000);
            //Helpers.switchToBrowserByString(driver, "Login | Swagbucks");

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

            try
            {
                driver.FindElement(By.Id("loginBtn")).Click();
            }
            catch { }

            Helpers.wait(500);
            Console.WriteLine("I have logged In!!");

            try
            {
                driver.FindElement(By.Id("swagButtonModalExit")).Click();
            }
            catch { }

            try
            {
                driver.FindElement(By.Id("goldSurveyModalExit")).Click();
            }
            catch { }

            Console.WriteLine("Please sir no swag button");

            if (!openBucks)
            {
                dailys(driver, bw);

                Helpers.wait(500);
                while (!bw.CancellationPending && vids)
                {
                    video(driver, bw);
                }
                Console.WriteLine("past the vids");

                while (true)
                {
                    discoveryBreak(driver, bw);
                    Helpers.switchToBrowserByString(driver, "Swagbucks");
                    driver.Navigate().Refresh();
                    nGage(driver, bw);
                    Helpers.switchToBrowserByString(driver, "Swagbucks");
                    driver.Navigate().Refresh();
                    Helpers.switchToBrowserByString(driver, "Swagbucks");
                    video(driver, bw);

                    driver.Navigate().GoToUrl("http://www.swagbucks.com/");
                }
            }
            else
            {
                while (true)
                    if (bw.CancellationPending)
                    {
                        driver.Quit();
                    }
            }
        }

        void dailys(IWebDriver driver, BackgroundWorker bw)
        {
            try
            {
                driver.FindElement(By.PartialLinkText("Daily Poll")).Click();
            }
            catch { }

            Helpers.wait(1000);
            Helpers.switchToBrowserByString(driver, "Daily Poll");
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
            Helpers.ByClass(driver, "todayPoll");
            Helpers.wait(1000);
            Helpers.switchToBrowserByString(driver, "Home | Swagbucks");
        }

        void nCrave(IWebDriver driver, BackgroundWorker bw)
        {
            bool craveMe;
            try
            {
                driver.FindElement(By.LinkText("Daily Crave")).Click();
            }
            catch { }
            Helpers.wait(1000);
            try
            {
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[contains(@name, 'easyXDM')]")));
            }
            catch { }
            finally { }
            Helpers.switchToBrowserFrameByString(driver, "parentiframe");
            try
            {
                driver.FindElement(By.ClassName("disableText")).Click();
                Helpers.wait(5000);
            }
            catch { }

            Helpers.wait(1000);
            Helpers.switchToBrowserByString(driver, "Entertainmentcrave");

            Helpers.wait(1000);

            craveMe = Helpers.lookFor(driver, "nGage");
            while (craveMe)
            {
                if (bw.CancellationPending)
                {
                    driver.Quit();
                }
                bool someSwitch = false;
                Helpers.closeWindows(driver, titles);

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
                                        Helpers.ByClass(driver, "switch");
                                        someSwitch = true;
                                    }
                                }
                                catch { }
                                Helpers.ById(driver, "link_down");
                                Helpers.wait(60000);
                                Helpers.switchToBrowserFrameByString(driver, "contIframe");
                                Helpers.ByClass(driver, "owl-next");
                                Helpers.switchToBrowserByString(driver, "Entertainmentcrave");
                                driver.SwitchTo().DefaultContent();
                            }
                        }
                        catch { }

                        Helpers.ById(driver, "link_down");
                        Helpers.ByClass(driver, "keepCraving");
                    }
                }
                catch { }
            }
            Helpers.wait(1000);
            Helpers.ByClass(driver, "logoTopbar");
        }

        void discoveryBreak(IWebDriver driver, BackgroundWorker bw)
        {
            bool discoBreak;
            try
            {
                IWebElement discBreak = driver.FindElement(By.Id("cardContentImg-1-5"));
                discBreak.Click();
                Helpers.wait(5000);
            }
            catch
            {
                try
                {
                    IWebElement discBreak = driver.FindElement(By.Id("cardContentImg-1"));
                    discBreak.Click();
                    Helpers.wait(5000);
                }
                catch
                {
                    try
                    {
                        IWebElement discBreak = driver.FindElement(By.Id("sbHomeCard-1-5"));
                        discBreak.Click();
                        Helpers.wait(5000);
                    }
                    catch
                    {
                        try
                        {
                            IWebElement discBreak = driver.FindElement(By.Id("sbHomeCard54559-563"));
                            discBreak.Click();
                            Helpers.wait(5000);
                        }
                        catch
                        {
                            Console.WriteLine("No Discovery Break Found!!");
                        }
                    }
                }
            }

            discoBreak = Helpers.lookFor(driver, "www.swagbucks.com/?");

            Helpers.wait(5000);
            while (discoBreak)
            {
                if (bw.CancellationPending)
                {
                    driver.Quit();
                }

                Console.WriteLine("In the discoBreak!!");
                Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                try
                {
                    Helpers.switchToBrowserByString(driver, "Now Exploring great content!");
                    while (Helpers.lookFor(driver, "static.jungroup.com/"))
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
                            Helpers.wait(5000);
                        }
                    }
                }
                catch { }

                /*
                try
                {
                    if (driver.Title.Contains("Facebook"))
                    {
                        driver.Close();
                    }
                }
                catch { }
                */

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
                    //if (driver.FindElement(By.ClassName("jw-text-elapsed")).Text == "00:00")
                    if (driver.FindElement(By.ClassName("jw-reset")).Displayed)
                    {
                        Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
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
                }
                catch
                {
                    Console.WriteLine("Waiting 5 Seconds!!");
                    Helpers.wait(500);
                }

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

                try
                {

                    if (driver.FindElement(By.Id("ty_headline")).Displayed)
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

                // Chips Ad
                Helpers.ById(driver, "compositor_placed_innerclip_cheddar");
                Helpers.ById(driver, "compositor_placed_innerclip_gouda");
                Helpers.ById(driver, "compositor_placed_innerclip_habanero");
                Helpers.ById(driver, "compositor_placed_innerclip_flamin");
                Helpers.ById(driver, "compositor_placed_innerclip_honeybbq");
                Helpers.ById(driver, "compositor_placed_innerclip_korean");
                Helpers.ById(driver, "compositor_placed_innerclip_oliveoil");
                Helpers.ById(driver, "compositor_placed_innerclip_seasalt");
                Helpers.ById(driver, "compositor_placed_innerclip_seasalt");
                //
                Helpers.ById(driver, "compositor_placed_innerclip_bajablast");


                Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                try
                {
                    Helpers.switchToBrowserFrameByString(driver, "junFrame");
                    driver.FindElement(By.Id("expository_image")).Click();
                }
                catch { }

                Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");
                try
                {
                    Helpers.switchToBrowserFrameByString(driver, "junFrame");
                    driver.FindElement(By.Id("compositor_placed_innerclip_reuben")).Click();
                }
                catch { }

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

                    try
                    {
                        driver.FindElement(By.Id("demosubmitimg")).Click();
                    }
                    catch { }
                }
                catch { }

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

                    try
                    {
                        driver.FindElement(By.Id("demosubmitimg")).Click();
                    }
                    catch { }
                }
                catch { }

                Helpers.switchToBrowserByString(driver, "www.swagbucks.com/?cmd");

                Helpers.switchToBrowserFrameByString(driver, "junFrame");
                Helpers.switchToBrowserFrameByString(driver, "vgPlayer");
                Helpers.switchFrameByNumber(driver, 0);
                Helpers.switchFrameByNumber(driver, 0);
                Helpers.switchToBrowserFrameByString(driver, "player");

                Helpers.ById(driver, "ytp-large-play-button");

                discoBreak = Helpers.lookFor(driver, "www.swagbucks.com/?");
            }
        }


        void nGage(IWebDriver driver, BackgroundWorker bw)
        {
            Random random = new Random();
            bool nGageCards;

            try
            {
                IWebElement nGageCard = driver.FindElement(By.Id("cardContentImg-14-5"));
                nGageCard.Click();
                Console.WriteLine("NGAGE!!!!");
                Helpers.wait(5000);
            }
            catch
            {
                try
                {
                    IWebElement nGageCard = driver.FindElement(By.Id("cardContentImg-14"));
                    nGageCard.Click();
                    Helpers.wait(5000);
                }
                catch
                {
                    try
                    {
                        IWebElement nGageCard = driver.FindElement(By.Id("sbHomeCard-14-5"));
                        nGageCard.Click();
                        Helpers.wait(5000);
                    }
                    catch
                    {
                        try
                        {
                            IWebElement nGageCard = driver.FindElement(By.Id("sbHomeCard54560-563"));
                            nGageCard.Click();
                            Helpers.wait(5000);
                        }
                        catch
                        {
                            Console.WriteLine("No nGageCard found");
                            nGageCards = false;
                        }
                    }
                }
            }
            Helpers.wait(5000);
            Helpers.switchToBrowserByString(driver, "nGage");

            nGageCards = Helpers.lookFor(driver, "player.ngage-media.com");
            while (nGageCards)
            {
                if (bw.CancellationPending)
                {
                    driver.Quit();
                }

                Helpers.switchToBrowserByString(driver, "nGage");
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
                    Helpers.closeWindows(driver, titles);
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
                            if (driver.Title.Contains("Facebook"))
                            {
                                driver.Close();
                            }
                        }
                        catch { }

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
                            Helpers.closeWindows(driver, titles);
                            Helpers.wait(2000);
                            break;
                        }
                        catch { }
                    }
                }
                catch { }

                Helpers.switchToBrowserByString(driver, "nGage");

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
                nGageCards = Helpers.lookFor(driver, "player.ngage-media.com");

            }
        }

        void video(IWebDriver driver, BackgroundWorker bw)
        {
            int currentHour = DateTime.Now.Hour;
            bool setComplete = false;
            int linkArray = 0, videoGroupWatched = 0, vidCount = 0, currentVideo = 0, totalVid = 0;
            string[] linkText = { "Editor's Pick", "Careers", "Comedy", "Entertainment", "Fashion", "Fitness", "Food", "Health", "Hobbies", "Home & Garden", "Music", "News & Politics", "Parenting", "Pets & Animals", "Shopping", "Sports", "Technology", "Travel", "Wedding" };
            string[] holdMe = new string[100];

            driver.FindElement(By.LinkText("Watch")).Click();
            Helpers.wait(1000);
            while (!setComplete)
            {
                driver.FindElement(By.LinkText(linkText[linkArray])).Click();
                Actions builder = new Actions(driver);
                Helpers.wait(1000);
                IList<IWebElement> videoLinks = driver.FindElements(By.ClassName("watchCard"));
                Helpers.wait(1000);
                foreach (IWebElement vidLink in videoLinks)
                {
                    try
                    {
                        builder.MoveToElement(vidLink).Build().Perform();
                        vidCount++;
                        Helpers.wait(1000);
                        builder.MoveToElement(driver.FindElement(By.LinkText("nCrave"))).Build().Perform();
                        Helpers.wait(1000);
                    }
                    catch { }
                    finally { }
                }
                Helpers.wait(5000);
                Console.WriteLine(vidCount);
                Console.WriteLine(currentVideo);
                Console.WriteLine(videoGroupWatched);
                while (videoGroupWatched <= vidCount)
                {
                    try
                    {
                        Console.WriteLine("Video Group Watched: " + videoGroupWatched);
                        IList<IWebElement> vidLinks = driver.FindElements(By.ClassName("watchCard"));
                        Helpers.wait(1000);
                        foreach (IWebElement vidLink in vidLinks)
                        {
                            Console.WriteLine("Searching Group of Videos: " + currentVideo);
                            if (vidCount - videoGroupWatched - 1 == currentVideo)
                            {
                                try
                                {
                                    builder = new Actions(driver);
                                    builder.MoveToElement(vidLink).Click().Build().Perform();
                                    Helpers.wait(1000);
                                    driver.FindElement(By.ClassName("iconWatch")).Click();
                                    Helpers.wait(1000);
                                }
                                catch { }
                                finally { }
                                int watchingVideos = 0;
                                int videosEarn = 0;
                                int.TryParse(Regex.Match(driver.FindElement(By.Id("watchVideosEarn")).Text, @"\d+").Value, out videosEarn);
                                Console.WriteLine("watchingVideos " + watchingVideos);
                                Console.WriteLine("videosEarn" + videosEarn);
                                Console.WriteLine("watchingVideos " + watchingVideos);
                                Console.WriteLine("videosEarn" + videosEarn);
                                IList<IWebElement> oLinks = driver.FindElements(By.ClassName("sbPlaylistVideoNumber"));
                                foreach (IWebElement oLink in oLinks)
                                {
                                    if (bw.CancellationPending)
                                    {
                                        driver.Quit();
                                    }

                                    try
                                    {
                                        Helpers.wait(2000);
                                        Random rnd = new Random();
                                        oLink.Click();
                                        Console.WriteLine("Link Clicked");
                                        watchingVideos++;
                                        Helpers.wait(1000 * rnd.Next(40, 60));
                                    }
                                    catch { }
                                    finally { }
                                }
                                Helpers.wait(1000);
                                driver.FindElement(By.LinkText(linkText[linkArray])).Click();
                            }
                            Helpers.wait(1000);
                            currentVideo++;
                            if (DateTime.UtcNow.Hour >= 8 && DateTime.UtcNow.Hour <= 9)
                            {
                                videoGroupWatched = 5000;
                                setComplete = true;
                                break;
                            }
                        }
                        Helpers.wait(1000);
                        currentVideo = 0;
                        videoGroupWatched++;
                    }
                    catch { }
                    finally { }
                }
                currentVideo = 0;
                linkArray += 1;
                totalVid++;
                if (linkArray == 19)
                    setComplete = true;

                if (currentHour != DateTime.Now.Hour)
                {
                    setComplete = true;
                }
            }
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
        }
    }
}
