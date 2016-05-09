using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Scrap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Scrap.Models
{
    class GiftHulkModel
    {
        string[] titles = { "nothing is" };

        public GiftHulkModel(string username, string password, BackgroundWorker bw, bool openHulk)
        {
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
                //ByClass(driver, "orange-button");
                Helpers.switchToBrowserByString(driver, "nothing is");
                IList<IWebElement> iframes = driver.FindElements(By.TagName("iframe"));
                Console.WriteLine(iframes.Count);
                //Find Videos Link in My Reward Box

                int chips = 0;
                int.TryParse(driver.FindElement(By.ClassName("count")).Text, out chips);
                if (chips > 0)
                {
                    IList<IWebElement> asideButtons = driver.FindElements(By.ClassName("aside-button"));
                    foreach (IWebElement asideButton in asideButtons)
                    {
                        if (asideButton.Text.Contains("Guess to"))
                        {
                            asideButton.SendKeys(Keys.PageDown);
                            asideButton.Click();
                            break;
                        }
                    }
                    //ByClass(driver, "aside-button");
                    IList<IWebElement> tabLabels = driver.FindElements(By.ClassName("tab-label"));
                    foreach (IWebElement tabLabel in tabLabels)
                    {
                        if (tabLabel.Text == "Win 4 Hulk Coins!")
                        {
                            tabLabel.Click();
                            break;
                        }
                    }
                    //for (int counter = 0; counter == chips; counter++)
                    string code = "";
                    int amount = 0, uses = 0, loopCount = 0;
                    while (chips > 0)
                    {
                        string[] suit = { "Heart", "Diamond", "Club", "Spade" };
                        Random random = new Random();
                        int rndSuit = random.Next(0, 3);
                        //SelectElement clickThis = new SelectElement(driver.FindElement(By.Id("card_suits_chzn")));
                        //clickThis.SelectByText(suit[rndSuit]);
                        driver.FindElement(By.ClassName("chzn-single")).Click();
                        if (rndSuit == 0)
                            driver.FindElement(By.Id("card_suits_chzn_o_1")).Click();
                        else if (rndSuit == 1)
                            driver.FindElement(By.Id("card_suits_chzn_o_2")).Click();
                        else if (rndSuit == 2)
                            driver.FindElement(By.Id("card_suits_chzn_o_3")).Click();
                        else if (rndSuit == 3)
                            driver.FindElement(By.Id("card_suits_chzn_o_4")).Click();
                        driver.FindElement(By.Id("game-lucky-button")).Click();
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
                    }

                    Helpers.ByClass(driver, "logo");
                }

                //
                bool videoWatching = false;
                videoWatching = true;
                while (videoWatching)
                {
                    Helpers.switchToBrowserByString(driver, "nothing is");
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

                            try
                            {
                                driver.FindElement(By.PartialLinkText("videos to watch")).Click();
                            }
                            catch { }
                            //                    }
                            //                }
                            //                catch { }

                            Helpers.wait(5000);
                            try
                            {
                                driver.FindElement(By.LinkText("Watch this video!")).Click();
                            }
                            catch { }
                            Helpers.ByClass(driver, "nextstepimg");
                            try
                            {
                                driver.FindElement(By.XPath("//img[@alt='Claim your reward']")).Click();
                            }
                            catch { }
                            Helpers.wait(500);
                            try
                            {
                                IList<IWebElement> testIframes = driver.FindElements(By.TagName("iframe"));
                                Console.WriteLine("Number of iFrames = " + testIframes.Count);
                                driver.SwitchTo().Frame(driver.FindElement(By.Id("stick-video-popup-video")));
                                testIframes = driver.FindElements(By.TagName("iframe"));
                                Console.WriteLine("Number of iFrames = " + testIframes.Count);
                                driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));
                                testIframes = driver.FindElements(By.TagName("iframe"));
                                Console.WriteLine("Number of iFrames = " + testIframes.Count);
                            }
                            catch { }
                            //

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

                            //
                            try
                            {
                                if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }

                            }
                            catch { }

                            Console.WriteLine("Lets Get Started");
                            Helpers.ById(driver, "expository_image");
                            Helpers.ById(driver, "webtraffic_popup_start_button");
                            Helpers.ById(driver, "webtraffic_popup_next_button");
                            Helpers.ByClass(driver, "webtraffic_start_button");
                            Helpers.ByClass(driver, "webtraffic_next_button");

                            try
                            {
                                if (driver.FindElement(By.TagName("b")).Text.Contains("Learn More"))
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            try
                            {
                                if (driver.FindElement(By.Id("thank_you_content")).Displayed)
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            Helpers.wait(500);
                            try
                            {
                                driver.SwitchTo().Frame(driver.FindElement(By.Id("stick-video-popup-video")));
                                driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));
                            }
                            catch { }

                            try
                            {
                                if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            Helpers.switchToBrowserByString(driver, "nothing is");

                            Helpers.wait(500);
                            try
                            {
                                driver.SwitchTo().Frame(driver.FindElement(By.Id("stick-video-popup-video")));
                                driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));
                            }
                            catch { }

                            Helpers.ById(driver, "compositor_placed_innerclip_cta");

                            // Chips Ad
                            Helpers.ById(driver, "compositor_placed_innerclip_cheddar");
                            Helpers.ById(driver, "compositor_placed_innerclip_gouda");
                            Helpers.ById(driver, "compositor_placed_innerclip_habanero");
                            Helpers.ById(driver, "compositor_placed_innerclip_flamin");
                            Helpers.ById(driver, "compositor_placed_innerclip_honeybbq");
                            Helpers.ById(driver, "compositor_placed_innerclip_korean");
                            Helpers.ById(driver, "compositor_placed_innerclip_oliveoil");
                            Helpers.ById(driver, "compositor_placed_innerclip_seasalt");
                            //


                            try
                            {
                                IWebElement rewardText = driver.FindElement(By.Id("ty_header"));
                                if (rewardText.Text == "You earned 1 reward!")
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                                else if (rewardText.Text == "You've 1 reward!")
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                                else if (rewardText.Text == "You earned 1 ZBs!")
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                                else if (rewardText.Text.Contains("1 reward!"))
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            try
                            {
                                if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            try
                            {
                                driver.FindElement(By.ClassName("ytp-large-play-button")).Click();
                            }
                            catch { }

                            Helpers.switchToBrowserByString(driver, "Now Exploring great content!");
                            try
                            {
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
                                        Helpers.switchToBrowserByString(driver, "Watch and Get");
                                    }
                                    catch { }
                                    Helpers.wait(5000);
                                }
                            }
                            catch { }

                            try
                            {
                                if (driver.FindElement(By.ClassName("reward_text")).Displayed)
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            try
                            {
                                driver.SwitchTo().Frame(driver.FindElement(By.Id("stick-video-popup-video")));
                                driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));
                            }
                            catch { }
                            try
                            {
                                driver.SwitchTo().Frame(driver.FindElement(By.Id("vgPlayer")));
                            }
                            catch { }
                            try
                            {
                                IList<IWebElement> testIframes = driver.FindElements(By.TagName("iframe"));
                                Console.WriteLine("Number of iFrames = " + testIframes.Count);
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
                                IList<IWebElement> testIframes = driver.FindElements(By.TagName("iframe"));
                                Console.WriteLine("Number of iFrames = " + testIframes.Count);
                                Helpers.switchFrameByNumber(driver, 0);
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

                            try
                            {
                                driver.FindElement(By.ClassName("ytp-large-play-button")).Click();
                            }
                            catch { }

                            try
                            {
                                driver.SwitchTo().Frame(driver.FindElement(By.Id("stick-video-popup-video")));
                                driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));
                            }
                            catch { }

                            try
                            {
                                if (driver.FindElement(By.Id("offers_exhausted_message")).Displayed)
                                {
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.wait(5000);
                                    videoWatching = false;
                                }
                            }
                            catch { }

                            driver.SwitchTo().DefaultContent();
                            try
                            {
                                Helpers.switchToBrowserFrameByString(driver, "stick-video-popup-video");
                                Helpers.switchFrameByNumber(driver, 0);
                            }
                            catch { }

                            //                            }
                            try
                            {
                                if (driver.FindElement(By.Id("ty_headline")).Displayed)
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }

                            }
                            catch { }
                        }
                    }
                    catch { }
                }

                bool someLoop = true;

                while (someLoop) /// New Stuff
                {

                    Helpers.switchToBrowserByString(driver, "nothing is");
                    try
                    {
                        System.Collections.ObjectModel.ReadOnlyCollection<string> aMorewindowHandles = driver.WindowHandles;

                        foreach (String awindow in aMorewindowHandles)
                        {
                            try
                            {
                                IWebDriver popup = driver.SwitchTo().Window(awindow);
                            }
                            catch { }

                            try
                            {
                                driver.FindElement(By.PartialLinkText("videos to watch")).Click();
                            }
                            catch { }

                            try
                            {
                                driver.FindElement(By.LinkText("Launch Targeted Videos")).Click();
                            }
                            catch { }

                            try
                            {
                                driver.SwitchTo().Frame(driver.FindElement(By.Id("stick-video-popup-video")));
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

                            //
                            try
                            {
                                if (driver.FindElement(By.Id("ty_headline")).Text == "Thanks for visiting great content!")
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }

                            }
                            catch { }

                            try
                            {
                                if (driver.FindElement(By.Id("ty_headline")).Displayed)
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }

                            }
                            catch { }

                            Console.WriteLine("Lets Get Started");
                            Helpers.ById(driver, "expository_image");
                            Helpers.ById(driver, "webtraffic_popup_start_button");
                            Helpers.ById(driver, "webtraffic_popup_next_button");
                            Helpers.ByClass(driver, "webtraffic_start_button");
                            Helpers.ByClass(driver, "webtraffic_next_button");

                            try
                            {
                                if (driver.FindElement(By.TagName("b")).Text.Contains("Learn More"))
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            try
                            {
                                if (driver.FindElement(By.Id("thank_you_content")).Displayed)
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            Helpers.wait(500);
                            try
                            {
                                driver.SwitchTo().Frame(driver.FindElement(By.Id("stick-video-popup-video")));
                            }
                            catch { }

                            try
                            {
                                if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            Helpers.switchToBrowserByString(driver, "nothing is");

                            Helpers.wait(500);
                            try
                            {
                                driver.SwitchTo().Frame(driver.FindElement(By.Id("stick-video-popup-video")));
                                driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));
                            }
                            catch { }

                            Helpers.ById(driver, "compositor_placed_innerclip_cta");

                            // Chips Ad
                            Helpers.ById(driver, "compositor_placed_innerclip_cheddar");
                            Helpers.ById(driver, "compositor_placed_innerclip_gouda");
                            Helpers.ById(driver, "compositor_placed_innerclip_habanero");
                            Helpers.ById(driver, "compositor_placed_innerclip_flamin");
                            Helpers.ById(driver, "compositor_placed_innerclip_honeybbq");
                            Helpers.ById(driver, "compositor_placed_innerclip_korean");
                            Helpers.ById(driver, "compositor_placed_innerclip_oliveoil");
                            Helpers.ById(driver, "compositor_placed_innerclip_seasalt");
                            //


                            try
                            {
                                IWebElement rewardText = driver.FindElement(By.Id("ty_header"));
                                if (rewardText.Text == "You earned 1 reward!")
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                                else if (rewardText.Text == "You've 1 reward!")
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                                else if (rewardText.Text == "You earned 1 ZBs!")
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                                else if (rewardText.Text.Contains("1 reward!"))
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            try
                            {
                                if (driver.FindElement(By.Id("compositor_placed_innerclip_cta")).Displayed)
                                {
                                    Console.WriteLine("Rewarded");
                                    Helpers.switchToBrowserByString(driver, "nothing is");
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.closeWindows(driver, titles);
                                }
                            }
                            catch { }

                            try
                            {
                                driver.SwitchTo().DefaultContent();
                                Helpers.switchToBrowserFrameByString(driver, "stick-video-popup-video");
                                Helpers.switchToBrowserFrameByString(driver, "vgPlayer");
                                Helpers.switchFrameByNumber(driver, 0);
                                Helpers.switchToBrowserFrameByString(driver, "player");
                                Helpers.switchToBrowserFrameByString(driver, "player");
                            }
                            catch { }

                            try
                            {
                                driver.FindElement(By.ClassName("ytp-large-play-button")).Click();
                            }
                            catch { }

                            try
                            {
                                driver.SwitchTo().Frame(driver.FindElement(By.Id("stick-video-popup-video")));
                                driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("div#html_wrapper iframe")));
                            }
                            catch { }

                            try
                            {
                                driver.FindElement(By.ClassName("ytp-large-play-button")).Click();
                            }
                            catch { }

                            try
                            {
                                driver.SwitchTo().Frame(driver.FindElement(By.Id("stick-video-popup-video")));
                            }
                            catch { }

                            try
                            {
                                driver.SwitchTo().DefaultContent();
                                Helpers.switchToBrowserFrameByString(driver, "stick-video-popup-video");
                            }
                            catch { }

                            try
                            {
                                if (driver.FindElement(By.Id("offers_exhausted_message")).Displayed)
                                {
                                    driver.Navigate().GoToUrl("http://www.gifthulk.com/");
                                    Helpers.wait(5000);
                                    videoWatching = false;
                                    someLoop = false;
                                }
                            }
                            catch { }
                        }

                        Helpers.switchToBrowserByString(driver, "Now Exploring");

                        try
                        {
                            System.Collections.ObjectModel.ReadOnlyCollection<string> bMorewindowHandles = driver.WindowHandles;

                            foreach (String windower in bMorewindowHandles)
                            {
                                try
                                {
                                    IWebDriver popup = driver.SwitchTo().Window(windower);
                                }
                                catch { }

                                bool exploring = false;
                                if (driver.Title.Contains("Now Exploring"))
                                {
                                    exploring = true;
                                }
                                while (exploring)
                                {

                                    //switchToBrowserByString(driver, "Now Exploring great content!");
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
                                        driver.FindElement(By.XPath("//a[contains(@href, 'hyprmx')]/img")).Click();
                                    }
                                    catch { }
                                    try
                                    {
                                        driver.FindElement(By.XPath("//img[@alt='Next Page']")).Click();
                                    }
                                    catch { }
                                    try
                                    {
                                        driver.FindElement(By.XPath("//img[@alt='Claim your reward']")).Click();
                                        exploring = false;
                                    }
                                    catch { }
                                    Helpers.wait(5000);

                                }
                            }
                        }
                        catch { }
                    }
                    catch { }
                }
                driver.Quit();
            }
        }

        /*
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
            catch { }
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
        */
    }
}
