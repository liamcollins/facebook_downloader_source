using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IWebDriver b;

            try
            {
                var chromeDriverService = ChromeDriverService.CreateDefaultService("chromedriver87");
                chromeDriverService.HideCommandPromptWindow = true;
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--disable-notifications"); // to disable notification

                b = new ChromeDriver(chromeDriverService, options);
            } catch
            {
                var chromeDriverService = ChromeDriverService.CreateDefaultService("chromedriver88");
                chromeDriverService.HideCommandPromptWindow = true;
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--disable-notifications"); // to disable notification

                b = new ChromeDriver(chromeDriverService, options);
            }

            b.Manage().Window.Maximize();

            b.Navigate().GoToUrl("https://facebook.com");

            b.FindElement(By.Id("email")).SendKeys(username.Text);
            b.FindElement(By.Id("pass")).SendKeys(password.Text);
            b.FindElement(By.Name("login")).Click();

            ExplicitWait(b, "//*[@id=\"mount_0_0\"]/div/div[1]/div[1]/div[3]/div/div/div[1]/div[1]/div/div[1]/div/div/div[1]/div/div/div[1]/ul/li/div/a/div[1]/div[2]/div/div/div/div/span/span").Click();

            String base_url = b.Url;

            b.Url = base_url + "/photos";
            ExplicitWait(b, "//*[@id=\"mount_0_0\"]/div/div[1]/div[1]/div[3]/div/div/div[1]/div[1]/div/div/div[4]/div/div/div/div/div/div/div/div[3]/div[1]/div[1]/div/div/a/img").Click();

            WebClient download_client = new WebClient();

            string previous_src = "";
            string src = "";

            System.IO.Directory.CreateDirectory("Photos of you!");

            for (int i = 0; i < 10000; i++)
            {
                // detect video
                if (b.Url.Contains("/videos/"))
                {
                    string path = "video-urls.txt";
                    if (!File.Exists(path))
                    {
                        // Create a file to write to.
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            sw.WriteLine(b.Url);
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(b.Url);
                        }
                    }
                    i--;
                    b.FindElement(By.TagName("body")).SendKeys(OpenQA.Selenium.Keys.Right);
                    continue;
                }


                for (int j = 0; j < 20; j++)
                {
                    try
                    {
                        src = ExplicitWait(b, "//*[@id=\"mount_0_0\"]/div/div[1]/div[1]/div[4]/div/div/div[1]/div/div[3]/div[2]/div/div[2]/div/div[1]/div/div[2]/div/img").GetAttribute("src");
                    }
                    catch (OpenQA.Selenium.StaleElementReferenceException stalerefexception)
                    {
                        System.Threading.Thread.Sleep(200);
                    }
                }

                if (src == previous_src)
                {
                    break;
                }

                string title = "Photos of you!/photo" + i + ".png";

                download_client.DownloadFile(src, title);

                previous_src = src;

                b.FindElement(By.TagName("body")).SendKeys(OpenQA.Selenium.Keys.Right);

            }

            b.Quit();
            label11.Visible = true;
        }


        public static IWebElement ExplicitWait(IWebDriver b, string xpath)
        {
            return new WebDriverWait(b, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementToBeClickable(By.XPath(xpath)));
        }

    }


}
