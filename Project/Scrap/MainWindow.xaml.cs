using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace Scrap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoginZoom_Click(object sender, RoutedEventArgs e)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(App.Folder);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("user-data-dir=" + App.Folder + "profile");

            
            IWebDriver driver = new ChromeDriver(service, options);

            driver.Navigate().GoToUrl("http://www.zoombucks.com/login.php");
     
            driver.FindElement(By.Name("username")).SendKeys(txtUsernameZoom.Text);
            driver.FindElement(By.Name("password")).SendKeys(txtPasswordZoom.Password);
            driver.FindElement(By.ClassName("signup_button")).Click();
            //jun videos
            wait(5000);
            driver.FindElement(By.ClassName("widgetcontent")).Click();

            wait(2000);
            Console.WriteLine(driver.Title.ToString());

            while (true)
            {
                try
                {
                    Console.WriteLine("Switching to Watch and Get Rewarded!");
                    switchToBrowserByString(driver, "Watch and");
                    Console.WriteLine("hey whats your title");
                    Console.WriteLine(driver.Title);
                    IList<IWebElement> iframes = driver.FindElements(By.TagName("iframe"));
                    Console.WriteLine(iframes.Count);

                    if (iframes.Count > 0)
                        switchToFrame(driver, iframes, iframes.Count);
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
                            wait(1000);
                            int rndDay = random.Next(1, 28);
                            clickThis = new SelectElement(dropDownDay);
                            clickThis.SelectByText(rndDay.ToString());
                            wait(1000);
                            int rndYear = random.Next(1974, 1994);
                            clickThis = new SelectElement(dropDownYear);
                            clickThis.SelectByText(rndYear.ToString());
                            wait(1000);
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
                        wait(5000);
                    }
                    catch
                    {
                        Console.WriteLine("Waiting 5 Seconds!!");
                        wait(5000);
                    }
                    finally { }

                    try
                    {
                        IWebElement nextPage = driver.FindElement(By.ClassName("webtraffic_next_button"));
                        nextPage.Click();
                        Console.WriteLine("Next Page");
                        wait(5000);
                    }
                    catch
                    {
                        Console.WriteLine("Waiting 5 Seconds!!");
                        wait(5000);
                    }
                    finally { }
                    try
                    {
                        IWebElement earn2Swag = driver.FindElement(By.Id("webtraffic_popup_start_button"));
                        earn2Swag.Click();
                        Console.WriteLine("earn2Swag found, wait 5 seconds");
                        wait(5000);
                    }
                    catch
                    {
                        Console.WriteLine("Waiting 5 Seconds!!");
                        wait(5000);
                    }
                    finally { }

                    try
                    {
                        IWebElement nextPage = driver.FindElement(By.Id("webtraffic_popup_next_button"));
                        nextPage.Click();
                        Console.WriteLine("Next Page");
                        wait(5000);
                    }
                    catch
                    {
                        Console.WriteLine("Waiting 5 Seconds!!");
                        wait(5000);
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
                            closeWindows(driver);
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
                                try
                                {
                                    driver.FindElement(By.XPath("//img[@alt='Claim your reward']")).Click();
                                    switchToBrowserByString(driver, "Watch and");
                                }
                                catch { }
                                finally { }
                                wait(5000);
                            }
                        }
                    }
                    catch { }
                    finally { }

                    try
                    {
                        Console.WriteLine(driver.FindElement(By.Id("compositor_placed_cta")).Displayed);
                        if (driver.FindElement(By.Id("compositor_placed_cta")).Displayed)
                        {
                            Console.WriteLine("Start Refresh");
                            driver.Navigate().Refresh();
                            Console.WriteLine("Stop Refresh");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Waiting 2 seconds!!");
                        wait(2000);
                    }
                    finally { }

                    try
                    {
                        IWebElement youTube = driver.FindElement(By.ClassName("ytp-play-button"));
                        youTube.Click();
                    }
                    catch { }
                    finally { }
                }
                catch { }
                finally { }
            }
        }

        static void wait(int delay)
        {
            System.Threading.Thread.Sleep(delay);
        }

        static void switchToBrowserFrameByString(IWebDriver driver, string targetBrowserString)
        {
            try
            {
                driver.SwitchTo().Frame(driver.FindElement(By.Id(targetBrowserString)));
            }
            catch { }
            finally { }
        }

        static void switchToFrame(IWebDriver driver, IList<IWebElement> iframes, int counts)
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

        static void switchToBrowserByString(IWebDriver driver, string targetBrowserString)
        {
            try
            {
                foreach (string defwindow in driver.WindowHandles)
                {
                    try
                    {
                        driver.SwitchTo().Window(defwindow);
                        Console.WriteLine(driver.Title.ToString());
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

        static void closeWindows(IWebDriver driver)
        {
            try
            {
                System.Collections.ObjectModel.ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                foreach (String window in windowHandles)
                {
                    IWebDriver popup = driver.SwitchTo().Window(window);
                    try
                    {
                        if (!popup.Title.Contains("Dashboard"))
                        {
                            if (!popup.Title.Contains("Watch and"))
                            {
                                driver.SwitchTo().Window(window);
                                driver.Close();
                            }
                        }
                    }
                    catch { }
                    finally { }
                }
            }
            catch { }
            finally { }
        }
    }
}
