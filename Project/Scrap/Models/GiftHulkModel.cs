using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Scrap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Scrap.Models
{
    class GiftHulkModel
    {
        string[] titles = { "GiftHulk - They say nothing" };
        public GiftHulkModel(string username, string password, BackgroundWorker bw, bool openHulk, int cards)
        {
            int chips = 0;

            ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("user-data-dir=" + App.Folder + "profileGH");

            IWebDriver driver = new ChromeDriver(service, options);
            driver.Navigate().GoToUrl("http://www.gifthulk.com/");

            try
            {
                driver.FindElement(By.ClassName("signup-link")).Click();

                driver.FindElement(By.Name("log")).SendKeys(username);
                driver.FindElement(By.Name("pwd")).SendKeys(password);
                driver.FindElement(By.Name("pwd")).SendKeys(Keys.Enter);
            }
            catch { }
            finally { }
            Helpers.wait(5000);

            if (!openHulk)
            {
                while (!bw.CancellationPending)
                {
                    int.TryParse(driver.FindElement(By.Id("daily_chips")).Text, out chips);

                    if (chips > 0)
                    {
                        driver.Navigate().GoToUrl("http://www.gifthulk.com/guess-the-card/");
                        GuessCard(driver, cards);
                    }

                    try
                    {
                        driver.FindElement(By.Id("watch-video")).Click();
                        sideVideos(driver);
                    }
                    catch { }

                    Helpers.wait(5000);
                    videosWatch(driver);
                }
            }
        }

        void videosWatch(IWebDriver driver)
        {
            int videoCount = 0;
            bool videoGetting = true;
            while (videoGetting)
            {
                try
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                    foreach (String window in windowHandles)
                    {
                        try
                        {
                            IWebDriver popup = driver.SwitchTo().Window(window);
                        }
                        catch { }

                        try
                        {
                            driver.SwitchTo().DefaultContent();
                        }
                        catch { }

                        try
                        {
                            driver.FindElement(By.PartialLinkText("videos to watch")).Click();
                        }
                        catch { }

                        try
                        {
                            IList<IWebElement> videos = driver.FindElements(By.LinkText("Watch this video!"));
                            //MessageBox.Show(videos.Count.ToString());

                            try
                            {
                                if (DateTime.Now.Hour != 4)
                                {
                                    foreach (IWebElement video in videos)
                                    {
                                        video.Click();
                                        break;
                                    }
                                }
                                else
                                {
                                    videoGetting = false;
                                }
                            }
                            catch { }
                        }
                        catch { }

                        Helpers.switchToBrowserFrameByString(driver, "stick-video-popup-video");
                        Helpers.switchFrameByNumber(driver, 0);

                        Helpers.ById(driver, "webtraffic_popup_start_button");
                        Helpers.ById(driver, "webtraffic_popup_next_button");
                        Helpers.ById(driver, "expository_image");

                        try
                        {
                            if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                            {
                                driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                Helpers.closeWindows(driver, titles);
                            }
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.Id("ty_headline")).Displayed)
                            {
                                driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                Helpers.closeWindows(driver, titles);
                            }
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.Id("ty_body_text")).Displayed)
                            {
                                driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                Helpers.closeWindows(driver, titles);
                            }
                        }
                        catch { }

                        //MessageBox.Show(DateTime.Now.Hour.ToString());

                        try
                        {
                            if (driver.FindElement(By.ClassName("jw-text-duration")).Text == "00:07")
                            {
                                driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                            }
                        }
                        catch { }


                        Helpers.switchToBrowserFrameByString(driver, "vgPlayer");
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchToBrowserFrameByString(driver, "player");
                        Helpers.switchToBrowserFrameByString(driver, "player");

                        Helpers.ByClass(driver, "ytp-large-play-button");

                        try
                        {
                            driver.SwitchTo().DefaultContent();
                        }
                        catch { }

                        Helpers.switchToBrowserFrameByString(driver, "stick-video-popup-video");
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchToBrowserFrameByString(driver, "player");
                        Helpers.switchToBrowserFrameByString(driver, "player");

                        Helpers.ByClass(driver, "ytp-large-play-button");

                        try
                        {
                            driver.SwitchTo().DefaultContent();
                        }
                        catch { }

                        Helpers.switchToBrowserFrameByString(driver, "stick-video-popup-video");
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchToBrowserFrameByString(driver, "player");
                        Helpers.switchToBrowserFrameByString(driver, "player");

                        Helpers.ByClass(driver, "ytp-large-play-button");
                    }
                }
                catch { }
            }
        }

        void sideVideos(IWebDriver driver)
        {
            bool looping = true;
            while (looping)
            {
                try
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                    foreach (String window in windowHandles)
                    {
                        try
                        {
                            IWebDriver popup = driver.SwitchTo().Window(window);
                        }
                        catch { }

                        Helpers.switchToBrowserFrameByString(driver, "watch-video-popup-frame");

                        try
                        {
                            if (driver.FindElement(By.Id("offers_exhausted_message")).Displayed)
                            {
                                driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                Helpers.closeWindows(driver, titles);
                                looping = false;
                            }
                        }
                        catch { }

                        Helpers.ById(driver, "webtraffic_popup_start_button");
                        Helpers.ById(driver, "webtraffic_popup_next_button");
                        Helpers.ById(driver, "expository_image");

                        try
                        {
                            if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                            {
                                driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                try
                                {
                                    driver.FindElement(By.Id("watch-video")).Click();
                                }
                                catch { }
                                Helpers.closeWindows(driver, titles);
                            }
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.Id("ty_headline")).Displayed)
                            {
                                driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                try
                                {
                                    driver.FindElement(By.Id("watch-video")).Click();
                                }
                                catch { }
                                Helpers.closeWindows(driver, titles);
                            }
                        }
                        catch { }

                        try
                        {
                            if (driver.FindElement(By.Id("ty_body_text")).Displayed)
                            {
                                driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                try
                                {
                                    driver.FindElement(By.Id("watch-video")).Click();
                                }
                                catch { }
                                Helpers.closeWindows(driver, titles);
                            }
                        }
                        catch { }

                        if (driver.FindElement(By.TagName("div")).Text.Contains("No videos available right now."))
                        {
                            driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                            looping = false;
                        }

                        Helpers.switchToBrowserFrameByString(driver, "vgPlayer");
                        Helpers.switchFrameByNumber(driver, 0);
                        Helpers.switchToBrowserFrameByString(driver, "player");
                        Helpers.switchToBrowserFrameByString(driver, "player");

                        Helpers.ByClass(driver, "ytp-large-play-button");
                    }
                }
                catch { }
            }
        }

        void GuessCard(IWebDriver driver, int cards)
        {
            string code = "";
            int chips = 0, counting;
            int amount = 0, uses = 0, loopCount = 0;
            int.TryParse(driver.FindElement(By.Id("daily_chips")).Text, out chips);
            while (chips > 0)
            {
                counting = 0;
                if (driver.FindElement(By.Id("card_suits_block")).Displayed && driver.FindElement(By.Id("card_ranks_block")).Displayed)
                {
                    IList<IWebElement> tabLabels = driver.FindElements(By.ClassName("tab-label"));
                    foreach (IWebElement tabLabel in tabLabels)
                    {
                        if (tabLabel.Text.Contains("Win 50") && cards == 0)
                        {
                            tabLabel.Click();
                            Helpers.wait(5000);
                            break;
                        }
                        if (tabLabel.Text.Contains("Win 10") && cards == 1)
                        {
                            tabLabel.Click();
                            Helpers.wait(5000);
                            break;
                        }
                        if (tabLabel.Text.Contains("Win 4") && cards == 2)
                        {
                            tabLabel.Click();
                            Helpers.wait(5000);
                            break;
                        }
                    }
                }

                IList<IWebElement> chznSingles = driver.FindElements(By.ClassName("chzn-single"));
                //MessageBox.Show(chznSingles.Count.ToString());

                foreach (IWebElement chznSingle in chznSingles)
                {
                    if (cards == 0 || cards == 2 && counting == 0)
                    {
                        Random random = new Random();
                        int rndSuit = random.Next(0, 3);
                        //chznSingle.Click();
                        driver.FindElement(By.Id("card_suits_chzn")).Click();
                        if (rndSuit == 0)
                            driver.FindElement(By.Id("card_suits_chzn_o_1")).Click();
                        else if (rndSuit == 1)
                            driver.FindElement(By.Id("card_suits_chzn_o_2")).Click();
                        else if (rndSuit == 2)
                            driver.FindElement(By.Id("card_suits_chzn_o_3")).Click();
                        else if (rndSuit == 3)
                            driver.FindElement(By.Id("card_suits_chzn_o_4")).Click();
                    }

                    Helpers.wait(5000);

                    if (cards == 0 || cards == 1 && counting == 1)
                    {
                        Random random = new Random();
                        int rndSuit = random.Next(0, 12);
                        //chznSingle.Click();
                        driver.FindElement(By.Id("card_ranks_chzn")).Click();
                        if (rndSuit == 0)
                            driver.FindElement(By.Id("card_ranks_chzn_o_1")).Click();
                        else if (rndSuit == 1)
                            driver.FindElement(By.Id("card_ranks_chzn_o_2")).Click();
                        else if (rndSuit == 2)
                            driver.FindElement(By.Id("card_ranks_chzn_o_3")).Click();
                        else if (rndSuit == 3)
                            driver.FindElement(By.Id("card_ranks_chzn_o_4")).Click();
                        else if (rndSuit == 4)
                            driver.FindElement(By.Id("card_ranks_chzn_o_5")).Click();
                        else if (rndSuit == 5)
                            driver.FindElement(By.Id("card_ranks_chzn_o_6")).Click();
                        else if (rndSuit == 6)
                            driver.FindElement(By.Id("card_ranks_chzn_o_7")).Click();
                        else if (rndSuit == 7)
                            driver.FindElement(By.Id("card_ranks_chzn_o_8")).Click();
                        else if (rndSuit == 8)
                            driver.FindElement(By.Id("card_ranks_chzn_o_9")).Click();
                        else if (rndSuit == 9)
                            driver.FindElement(By.Id("card_ranks_chzn_o_10")).Click();
                        else if (rndSuit == 10)
                            driver.FindElement(By.Id("card_ranks_chzn_o_11")).Click();
                        else if (rndSuit == 11)
                            driver.FindElement(By.Id("card_ranks_chzn_o_12")).Click();
                        else if (rndSuit == 12)
                            driver.FindElement(By.Id("card_ranks_chzn_o_13")).Click();
                    }
                    counting++;
                }

                Helpers.wait(5000);

                try
                {
                    driver.FindElement(By.Id("game-lucky-button")).Click();
                }
                catch { }
                driver.FindElement(By.ClassName("game-lucky-button"));
                Console.WriteLine("Wait 5");
                Helpers.wait(5000);
                int.TryParse(driver.FindElement(By.Id("daily_chips")).Text, out chips);
                try
                {
                    if (driver.FindElement(By.ClassName("count")).Displayed)
                    {
                        IList<IWebElement> counts = driver.FindElements(By.ClassName("count"));
                        foreach (IWebElement count in counts)
                        {
                            if (loopCount == 0)
                                code = count.Text;
                            else if (loopCount == 1)
                                int.TryParse(count.Text, out amount);
                            else
                                int.TryParse(count.Text, out uses);
                        }
                    }
                }
                catch { }
                Helpers.wait(5000);
            }

            try
            {
                driver.FindElement(By.ClassName("logo")).Click();
            }
            catch { }
        }
    }
}