using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
        

            // Dodanie nowego posta:
            var textBox = _driver.FindElement(By.Id("NewPost"));

            textBox.Click();

            textBox.Clear();

            textBox.SendKeys(postRandomContent);

            var postButton = _driver.FindElement(By.Id("PostBtnId"));

            postButton.Click();

            _driver.Navigate()
               .GoToUrl("https://localhost:44399/getWall/1");


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

            string testPostId = "";
            if (id != "")
                testPostId = id.Split("_")[1];
            bool isDeleted;

            //Wysy³anie Request z delete
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://webapi20210317153051.azurewebsites.net/");
                client.DefaultRequestHeaders.Add("userID", "1");
                var response = await client.DeleteAsync($"post/{testPostId}");
                isDeleted = response.IsSuccessStatusCode;
            }

            Assert.Equal(foundContent, postRandomContent);
            Assert.True(isDeleted);

        }
        [Fact]
        public async void Create_Comment()
        {
            string commentRandomContent = "Nowy komentarz utworzony przez selenium: " + RandomString(20);

            _driver.Navigate()
                .GoToUrl("https://localhost:44399/getWall/1");
            var textBox = _driver.FindElement(By.CssSelector("textarea[id ^= 'NewComment_']"));

            textBox.Click();

            textBox.Clear();
            textBox.SendKeys(commentRandomContent);
            var commentButton = _driver.FindElement(By.Id("AddNewComment"));

            commentButton.Click();

            _driver.Navigate()
               .GoToUrl("https://localhost:44399/getWall/1");


            var commentsContents = _driver.FindElements(By.ClassName("fb-user-status"));
            // Odnalezienie dodanego komentarza
            string id = "";
            string foundContent = "";
            foreach (var commentContent in commentsContents)
            {
                var currText = commentContent.Text;
                if (currText == commentRandomContent)
                {
                    id = commentContent.GetProperty("id");
                    foundContent = currText;
                }
            }

            
         
            bool isDeleted;
            //Wysy³anie Request z delete
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://webapi20210317153051.azurewebsites.net/");
                client.DefaultRequestHeaders.Add("userID", "1");
                var response = await client.DeleteAsync($"comment/{id}");
                isDeleted = response.IsSuccessStatusCode;
            }

            Assert.Equal(foundContent, commentRandomContent);
            Assert.True(isDeleted);

        }
    }
}
