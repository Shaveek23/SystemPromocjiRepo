using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace UIAutomatedTests
{
    public class AutomatedUITests : IDisposable
    {
        private readonly IWebDriver _driver;
        public AutomatedUITests()
        {
            _driver = new ChromeDriver("driver");
        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

       
        [Fact]
        public async void Create_WhenExecuted_ReturnsCreateView()
        {
            string postRandomContent = "Nowy post utworzony przez selenium: " + RandomString(20);

            _driver.Navigate()
                .GoToUrl("https://localhost:44399/getWall/1");
            var textBox = _driver.FindElement(By.Id("NewPost"));

            textBox.Click();

            textBox.Clear();

            textBox.SendKeys(postRandomContent);

            var postButton = _driver.FindElement(By.Id("PostBtnId"));

            postButton.Click();

            var postsContents = _driver.FindElements(By.ClassName("fb-user-status"));

            // Odnalezienie dodanego posta
            string id = "";
            string foundContent = "";
            foreach (var postContent in postsContents)
            {
                var currText = postContent.Text;
                if (currText == postRandomContent)
                {
                    id = postContent.FindElement(By.XPath("..")).GetProperty("id");
                    foundContent = currText;
                }
            }

            string testPostId = id.Split("_")[1];
            bool isDeleted;

            //Wysy³anie Request z delete
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://webapi20210317153051.azurewebsites.net/api/");
                client.DefaultRequestHeaders.Add("userID", "1");
                var response = await client.DeleteAsync($"post/{testPostId}");
                isDeleted = response.IsSuccessStatusCode;
            }

            Assert.Equal(foundContent, postRandomContent);
            Assert.True(isDeleted);

        }
    }
}
