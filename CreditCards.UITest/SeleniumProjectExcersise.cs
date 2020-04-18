using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.Windows;
using System.IO;
using ApprovalTests;

namespace CreditCards.UITest
{
   public class SeleniumProjectExcersise
    {
       private const string HomeUrl = "https://ultimateqa.com/automation";
        private const string HomeTitle = "Automation Practice - Ultimate QA";
        private const string AboutUrl = "https://ultimateqa.com/blog/";
        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                
                driver.Navigate().GoToUrl(HomeUrl);

                DemoHelper.Pause();


                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

            }
        }

            [Fact]
            [Trait("Category", "Smoke")]

            public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                

                driver.Navigate().GoToUrl(HomeUrl);

                DemoHelper.Pause();

                driver.Navigate().Refresh();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

            }

        }

        [Fact]
        [Trait("Category", "Smoke")]

        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();
                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause();
                driver.Navigate().Back();
                DemoHelper.Pause();
                IWebElement searchButton = driver.FindElement(By.Id("searchsubmit"));


                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

            }
        }

        [Fact]
        [Trait("Category", "Smoke")]

        public void ReloadHomePageOnForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause();
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();
                driver.Navigate().Back();
                DemoHelper.Pause();

                driver.Navigate().Forward();
                DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

            }
        }

        [Fact]

        public void  FindingElements()
        {
            using (IWebDriver driver = new ChromeDriver()) 
            {
                driver.Navigate().GoToUrl(HomeUrl);
                driver.Manage().Window.Maximize();
                DemoHelper.Pause();
                IWebElement button = driver.FindElement(By.CssSelector("[name = 'jetpack_subscriptions_widget']"));
                button.Click();
                DemoHelper.Pause();

                IWebElement emailField = driver.FindElement(By.Name("email"));
                emailField.SendKeys("minacorovic");
                DemoHelper.Pause();

               
            }
        }
        [Fact]

        public void Slider()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://schule-frasnacht.ch/unsere-schule");
                driver.Manage().Window.Maximize();
                DemoHelper.Pause();
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollBy(0, 550)", "");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

                IWebElement nextPage = wait.Until((d) => d.FindElement(By.CssSelector("[class = 'slick-next slick-arrow']")));
                nextPage.Click();
               
                
                IWebElement prevPage =  wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[class= 'slick-prev slick-arrow']")));
                prevPage.Click();

            }
        }
        [Fact]

        public void interactionElements()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://ultimateqa.com/simple-html-elements-for-automation/");
                driver.Manage().Window.Maximize();
                DemoHelper.Pause();

                IJavaScriptExecutor js1 = (IJavaScriptExecutor)driver;
                js1.ExecuteScript("window.scrollBy(0, 550)", "");

               
                WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(4));

                IWebElement nameField = wait1.Until((d) => d.FindElement(By.Id("et_pb_contact_name_0")));
                nameField.SendKeys("Mina Ćorović");

                IWebElement emailField = driver.FindElement(By.Id("et_pb_contact_email_0"));
                emailField.SendKeys("minacorovic@gmail.com");

                IWebElement submitButton = driver.FindElement(By.Name("et_builder_submit_button"));
                submitButton.Click();
                DemoHelper.Pause();



                js1.ExecuteScript("window.scrollBy(0, 450)", "");
                DemoHelper.Pause();

                driver.FindElement(By.CssSelector("[value = 'female']")).Click();
                DemoHelper.Pause();

                driver.FindElement(By.CssSelector("[value = 'Bike']")).Click();
                driver.FindElement(By.CssSelector("[value = 'Car'")).Click();
                DemoHelper.Pause();


                IWebElement carType = driver.FindElement(By.CssSelector("div.et_pb_blurb_description > select"));
                SelectElement selectCar = new SelectElement(carType);

                selectCar.SelectByIndex(0);
                selectCar.SelectByValue("saab");
                selectCar.SelectByText("Opel");
                DemoHelper.Pause();

                IWebElement tab2 = driver.FindElement(By.XPath("/html/body/div[1]/div/div/article/div/div[1]/div/div/div/div[3]/div/div[1]/div[10]/ul/li[2]/a"));
                tab2.Click();
                DemoHelper.Pause();

                IWebElement content = driver.FindElement(By.XPath("/html/body/div[1]/div/div/article/div/div[1]/div/div/div/div[3]/div/div[1]/div[10]/div/div[2]/div"));

                Assert.Equal("Tab 2 content", content.Text);
                DemoHelper.Pause();

            }
        }

        [Fact]

        public void SwitchingTabs()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-switch-windows-2/");
                driver.Manage().Window.Maximize();

                IWebElement newTab = driver.FindElement(By.XPath("//*[@id=\"content\"]/div[2]/button[3]"));
                newTab.Click();
                DemoHelper.Pause(5000);

                var allTabs = driver.WindowHandles;
                string firstTab = allTabs[0];
                string secondTab = allTabs[1];

                driver.SwitchTo().Window(secondTab);
                DemoHelper.Pause();

                Assert.Equal("https://www.toolsqa.com/", driver.Url);


            }
        }

        [Fact]

        public void PopupWindow()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://www.toolsqa.com/handling-alerts-using-selenium-webdriver/");
                driver.Manage().Window.Maximize();
                DemoHelper.Pause();

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollBy(0, 350)", "");

                IWebElement popupButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/p[7]/button"));
                popupButton.Click();

                IAlert alert = driver.SwitchTo().Alert();

                Assert.Equal("Confirm pop up with OK and Cancel button", alert.Text);

                DemoHelper.Pause();

                alert.Dismiss();

                DemoHelper.Pause();



            }
        }

        [Fact]
        [UseReporter(typeof(BeyondCompare4Reporter))]

        public void ScreenShots()
        {
            using(IWebDriver driver = new ChromeDriver())

            {
                driver.Navigate().GoToUrl("http://doerig-bergsenn.ch/");
                driver.Manage().Window.Maximize();
                DemoHelper.Pause();

                IWebElement cookies = driver.FindElement(By.CssSelector("[title = 'Cookies Akzeptieren']"));
                cookies.Click();

                ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;

                Screenshot screenshot = screenshotDriver.GetScreenshot();

                screenshot.SaveAsFile("screenshotFile.jpeg", ScreenshotImageFormat.Jpeg);

                
                



            }
        }
        }


    }

