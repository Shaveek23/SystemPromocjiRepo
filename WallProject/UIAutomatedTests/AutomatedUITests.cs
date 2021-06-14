using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace UIAutomatedTests
{
    public class AutomatedUITests : IDisposable
    {
        private readonly string projectName = "WallProject";
        private static  readonly string currentUser = "135";
        private static readonly string localHostUrl = $"https://localhost:44399/getWall/{currentUser}";
        private static readonly string deployedWallAppUrl = $"https://wallproject.azurewebsites.net/getWall/{currentUser}";

        private readonly string targetURL = $"{localHostUrl}";

        private readonly string APIurl = "https://systempromocji.azurewebsites.net/";
        private string getDriverPath()
        {
            return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
        }

        private string getApplicationPath()
        {
            return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\")) + projectName;
        }

        private readonly IWebDriver _driver;
        public AutomatedUITests()
        {
            var driverPath = getDriverPath();


            _driver = new ChromeDriver(driverPath + "driver");
            
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
                .GoToUrl(targetURL);
        

            // Dodanie nowego posta:
            var textBox = _driver.FindElement(By.Id("NewPost"));

            textBox.Click();

            textBox.Clear();

            textBox.SendKeys(postRandomContent);

            var postButton = _driver.FindElement(By.Id("PostBtnId"));

            postButton.Click();

            _driver.Navigate()
               .GoToUrl(targetURL);


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
                client.BaseAddress = new Uri(APIurl);
                client.DefaultRequestHeaders.Add("userID", currentUser);
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
                .GoToUrl(targetURL);
            var textBox = _driver.FindElement(By.CssSelector("textarea[id ^= 'NewComment_']"));

            textBox.Click();

            textBox.Clear();
            textBox.SendKeys(commentRandomContent);
            var commentButton = _driver.FindElement(By.Id("AddNewComment"));

            commentButton.Click();

            _driver.Navigate()
               .GoToUrl(targetURL);


            var commentsContents = _driver.FindElements(By.ClassName("fb-user-status-comment"));
            // Odnalezienie dodanego komentarza
            string id = "";
            string foundContent = "";
            foreach (var commentContent in commentsContents)
            {
                var curr = commentContent.FindElements(By.XPath("//*[@type='text']"));
                foreach(var Text in curr)
                {
                    var currText = Text.GetAttribute("value");
                    if (currText == commentRandomContent)
                    {
                        id = Text.GetProperty("id");
                        foundContent = currText;
                        break;
                    }
                }
              
            }

            
         
            bool isDeleted;
            id = id.Substring(12);
            //Wysy³anie Request z delete
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIurl);
                client.DefaultRequestHeaders.Add("userID", currentUser);
                var response = await client.DeleteAsync($"comment/{id}");
                isDeleted = response.IsSuccessStatusCode;
            }

            Assert.Equal(foundContent, commentRandomContent);
            Assert.True(isDeleted);

        }
        [Fact]
        public void FiltrationTest()
        {
            _driver.Navigate()
                   .GoToUrl(targetURL);
           
            var categories = _driver.FindElements(By.ClassName("icheck"));
            var postsContents = _driver.FindElements(By.ClassName("displayedCategories"));
            var firstCategory = categories[0].FindElement(By.XPath("..")).Text;
           
         
            categories[0].Click();
            var postsContentsAfter = _driver.FindElements(By.ClassName("displayedCategories"));
            var containsAfter = false;
            foreach (var postContent in postsContentsAfter)
            {
                var currText = postContent.Text;
                if (currText == firstCategory)
                {
                    containsAfter = true;
                    break;
                }


            }
           
            Assert.False(containsAfter);


        }
        [Fact]
        public async void Like_Post_Test()
        {
            _driver.Navigate()
              .GoToUrl(targetURL);
            var posts = _driver.FindElements(By.ClassName("fb-user-status"));
            string likeName= "Like_"+ posts[0].FindElement(By.XPath("..")).GetProperty("id");


            var like = _driver.FindElement(By.Id(likeName));
            var likeContent1 = like.Text;
            like.Click();

            like = _driver.FindElement(By.Id(likeName));
            var likeContent2 = like.Text;
            like.Click();

            like = _driver.FindElement(By.Id(likeName));
            var likeContent3 = like.Text;

            Assert.Equal(likeContent1, likeContent3);
            Assert.NotEqual(likeContent1, likeContent2);

        }
        [Fact]
        public async void AdddPostWithCategory()
        {
            string postRandomContent = "Nowy post utworzony przez selenium: " + RandomString(20);

            _driver.Navigate()
                .GoToUrl(targetURL);


            // Dodanie nowego posta:
            var textBox = _driver.FindElement(By.Id("NewPost"));

            textBox.Click();

            textBox.Clear();

            textBox.SendKeys(postRandomContent);
           
            var selectElement = new SelectElement(_driver.FindElement(By.Id("CategorySelection")));
            selectElement.SelectByValue("2");
            string categoryText = selectElement.Options[1].Text;
        

            var postButton = _driver.FindElement(By.Id("PostBtnId"));

            postButton.Click();

            _driver.Navigate()
               .GoToUrl(targetURL);


            var postsContents = _driver.FindElements(By.ClassName("fb-user-status"));

            // Odnalezienie dodanego posta
            string id = "";
            string foundContent = "";
            var categoryId = "";
            foreach (var postContent in postsContents)
            {
                var currText = postContent.Text;
                if (currText == postRandomContent)
                {
                    id = postContent.FindElement(By.XPath("..")).GetProperty("id");
                     categoryId = postContent.FindElement(By.XPath("..")).FindElement(By.ClassName("fb-user-details")).FindElement(By.ClassName("post_category")).Text;
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
                client.BaseAddress = new Uri(APIurl);
                client.DefaultRequestHeaders.Add("userID", currentUser);
                var response = await client.DeleteAsync($"post/{testPostId}");
                isDeleted = response.IsSuccessStatusCode;
            }
            Assert.Equal(categoryText, categoryId);
            Assert.Equal(foundContent, postRandomContent);
            Assert.True(isDeleted);
        }
        [Fact]
        public async void AdddPostWithTitley()
        {
            string postRandomContent = "Nowy post utworzony przez selenium: " + RandomString(20);
            string titleRandomContent =  RandomString(20);

            _driver.Navigate()
                .GoToUrl(targetURL);

          


            // Dodanie nowego posta:
            var textBox = _driver.FindElement(By.Id("NewPost"));

            textBox.Click();

            textBox.Clear();

            textBox.SendKeys(postRandomContent);
            var titleBox = _driver.FindElement(By.Id("Title"));

            titleBox.Click();
            titleBox.Clear();

            titleBox.SendKeys(titleRandomContent);

            var postButton = _driver.FindElement(By.Id("PostBtnId"));

            postButton.Click();

            _driver.Navigate()
               .GoToUrl(targetURL);
          
        


            var postsContents = _driver.FindElements(By.ClassName("fb-user-status"));

            // Odnalezienie dodanego posta
            string id = "";
            string foundContent = "";
            var title = "";
            foreach (var postContent in postsContents)
            {
                var currText = postContent.Text;
                if (currText == postRandomContent)
                {
                    id = postContent.FindElement(By.XPath("..")).GetProperty("id");
                    title = postContent.FindElement(By.XPath("..")).FindElement(By.ClassName("post_title")).Text;
                    foundContent = currText;
                }
            }
            // Odnalezienie dodanego posta
           

            string testPostId = "";
            if (id != "")
                testPostId = id.Split("_")[1];
            bool isDeleted;



            //Wysy³anie Request z delete


            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIurl);
                client.DefaultRequestHeaders.Add("userID", "1");
                var response = await client.DeleteAsync($"post/{testPostId}");
                isDeleted = response.IsSuccessStatusCode;
            }
           
            Assert.Equal(foundContent, postRandomContent);
            Assert.Equal(title, titleRandomContent);
            Assert.True(isDeleted);
        }

        [Fact]
        public void SeeMyPostsTest()
        {
            _driver.Navigate()
                  .GoToUrl(targetURL);
          
            var postButton = _driver.FindElements(By.CssSelector("button[class='btn btn-outline-primary btn-block']"));

            postButton[1].Click();

            var postsContents = _driver.FindElements(By.ClassName("fb-user-details"));
            string user;
           
            bool isOk = true;
            if ( postsContents.Count>0)
            {
                user = postsContents[0].FindElement(By.CssSelector("a")).Text;
                for (int i=1;i<postsContents.Count;i++)
                {
                    if(user != postsContents[i].FindElement(By.CssSelector("a")).Text)
                    {
                        isOk = false;
                        break;
                    }
                }
            }
            Assert.True(isOk);
        }
        [Fact]
        public void SeeMyCommentsTest()
        {
            _driver.Navigate()
                  .GoToUrl(targetURL);

            var commentButton = _driver.FindElements(By.CssSelector("button[class='btn btn-outline-primary btn-block']"));

           commentButton[2].Click();

            var commentsContents = _driver.FindElements(By.ClassName("cmt-details"));
            string user;

            bool isOk = true;
            if (commentsContents.Count > 0)
            {
                user = commentsContents[0].FindElement(By.CssSelector("h5")).Text;
                for (int i = 1; i < commentsContents.Count; i++)
                {
                    if (user != commentsContents[i].FindElement(By.CssSelector("h5")).Text)
                    {
                        isOk = false;
                        break;
                    }
                }
            }
            Assert.True(isOk);
        }

        [Fact]
        public async void EditPost()
        {
           

            _driver.Navigate()
                .GoToUrl(targetURL);



            var postButton = _driver.FindElement(By.Id("PostBtnId"));

            postButton.Click();

            _driver.Navigate()
               .GoToUrl(targetURL);

          
            var selfPostButton = _driver.FindElements(By.CssSelector("button[class='btn btn-outline-primary btn-block']"));

            selfPostButton[1].Click();
            // znalezienie postu
            var textTitleBox = _driver.FindElement(By.ClassName("post_title"));
            //przejscie do postu
            textTitleBox.Click();

            var editButton = _driver.FindElement(By.CssSelector("button[class='btn btn-outline-warning']"));
            editButton.Click();
            //pobranie obecnej tresci i podmiana na nowa
            var post = _driver.FindElement(By.CssSelector("input[id ^= 'PostContentInput_']"));
       
            var postText = post.GetProperty("value");
            post.Click();
            post.Clear();
           string  postRandomContent = "Nowy post utworzony przez selenium: " + RandomString(20);
            post.SendKeys(postRandomContent);

            //zapisanie
            var saveButton = _driver.FindElement(By.CssSelector("button[class='btn btn-outline-primary']"));
            saveButton.Click();
            //edycja na pierwotny tekst
            editButton = _driver.FindElement(By.CssSelector("button[class='btn btn-outline-warning']"));
            var editedPost = _driver.FindElement(By.CssSelector("input[id ^= 'PostContentInput_']"));
            var editedText = editedPost.GetProperty("value");
            editButton.Click();
            editedPost.Click();
            editedPost.Clear();
            editedPost.SendKeys(postText);
            saveButton = _driver.FindElement(By.CssSelector("button[class='btn btn-outline-primary']"));
            saveButton.Click();
            var currentText = _driver.FindElement(By.CssSelector("input[id ^= 'PostContentInput_']")).GetProperty("value");
            Assert.Equal(postRandomContent, currentText);
            Assert.Equal(postText, editedText );



        }
    }
}
