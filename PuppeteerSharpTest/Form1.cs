using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PuppeteerSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuppeteerSharpTest
{
    public partial class Form1 : Form
    {
        public String GetMethod(String url)
        {
            var json = "";

            using (var client = new HttpClient())
            {
                var endpoint = new Uri(url);
                var result = client.GetAsync(endpoint).Result;
                json = result.Content.ReadAsStringAsync().Result;
            }
            return json;
        }

        private String openprofile(String id)
        {
            var mo = GetMethod($"http://localhost:55555/profiles/{id}/start-automation");
            var jsonResult = (JObject)JsonConvert.DeserializeObject(mo);
            return jsonResult["data"]["wsEndpoint"].ToString(); 


        }

        private String getRandomProfileId()
        {
            var mo = GetMethod($"http://localhost:55555/profiles");
            var jsonResult = (JObject)JsonConvert.DeserializeObject(mo);
            var profileIds = jsonResult["data"]["lst_profile"];
            foreach (var profileId in profileIds)
            {
                return profileId["id"].ToString();
            }
            return "";
        }

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnCapture_Click(object sender, EventArgs e)
        {

            var wsEnpoint = openprofile(textBox1.Text);
            textBox2.Text = wsEnpoint;
            var options = new ConnectOptions()
            {
                BrowserWSEndpoint = wsEnpoint
            };

            var url = "https://www.google.com/";

            var browser = await PuppeteerSharp.Puppeteer.ConnectAsync(options);

            // Create a new page and go to Bing Maps
            Page page = null;

            page = browser.NewPageAsync().Result;
            await page.GoToAsync("https://google.com");


            await page.WaitForSelectorAsync("[name='q']");
            await page.FocusAsync("[name='q']");
            await page.Keyboard.TypeAsync("Hello wworld");
            await page.Keyboard.PressAsync("Enter");
           // await page.ClickAsync(".searchbox-searchbutton");
           // await page.WaitForNavigationAsync();

           // string content = await page.GetContentAsync();
           // richTextBox1.Text = content;


           // await page.SetViewportAsync(new ViewPortOptions
           // {
           //     Width = 1125,
           //     Height = 2436
           // });
           // Thread.Sleep(3000);
           // await page.ScreenshotAsync("E:\\ai\\screenshot.png");

           // Close the browser
           //await browser.CloseAsync();
           // pictureBox1.Image = Image.FromFile("E:\\ai\\screenshot.png");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var id = getRandomProfileId();
            textBox1.Text = id;
        }
    }
}
