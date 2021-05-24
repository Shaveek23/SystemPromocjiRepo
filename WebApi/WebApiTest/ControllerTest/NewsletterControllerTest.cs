using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Services;
using WebApi.Services.Services_Implementations;
using Xunit;

namespace WebApiTest.ControllerTest
{
    public class NewsletterControllerTest
    {
        private int UserId = 1;
        [Theory]
        [InlineData(1,true)]
        [InlineData(2,false)]
        public void SetSubscribeTest(int categoryId,bool isSubscribed)
        {
            NewsletterDTO newsletterDTO = new NewsletterDTO()
            {
                CategoryID = categoryId,
                isSubscribed = isSubscribed
            };

            var mockSerive = new Mock<INewsletterService>();
            var mockLogger = new Mock<ILogger<NewsletterController>>();
            var controller = new NewsletterController(mockSerive.Object, mockLogger.Object);
            mockSerive.Setup(x => x.SetSubscriptionAsync(newsletterDTO, UserId, isSubscribed)).Returns(Task.Run(() =>
            {
                return new ServiceResult<bool>(true);
            }));

            var actual = controller.SetSubscribe(UserId, newsletterDTO);
            var result= (bool)((ObjectResult)actual.Result).Value;
            Assert.True(result);
        }

    }
}
