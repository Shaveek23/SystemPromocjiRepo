using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallProject.Controllers;
using WallProject.Services;
using WallProject.Services.Services_Interfaces;
using Xunit;

namespace WallProjectTest.ControllersTest
{
    public class UserInterfaceControllerTest
    {
        [Theory]
        [InlineData(1,"cokolwiek","jakikolwiek@cokolwiek.wp.pl")]
        [InlineData(13,"jakas osoba","randomowy@adres.pl")]
        [InlineData(420,"najlepsze trawniki","najlepsze@trawniki.warszawa.pl")]
        public void EditUserTest(int userId, string userName, string userEmail)
        {
            var mockUserService = new Mock<IUserService>();
            var mockLogger = new Mock<ILogger<UserInterfaceController>>();
            var controller = new UserInterfaceController(mockLogger.Object, mockUserService.Object);
            mockUserService.Setup(x => x.EditUser(userId, userName,userEmail)).Returns(Task.Run(() => new ServiceResult<bool>(true)));
            var result = (controller.EditUser(userId, userName, userEmail).Result) as ViewResult;
            Assert.NotNull(result);
            Assert.Null(result.ViewData.Model);
        }
    }
}
