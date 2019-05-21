using Moq;
using System;
using CounterCulture.Services;
using CounterCulture.Repositories;
using Microsoft.Extensions.Options;
using CounterCulture.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UserService_Find
    {
        private UserService service;

        public UserService_Find() {
          var repoMock = new Mock<IUserRepository>(MockBehavior.Strict);
          repoMock.Setup(mock => mock.Find(It.IsAny<String>(), It.IsAny<String>()))
                  .Returns(new User());
          service = new UserService(repoMock.Object);
        }

        [TestMethod]
        public void ShouldGetUser()
        {
            String username = "waldo";
            String password = "waldo's password";
            User user = service.Find(username, password);
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void ShouldReturnNull_WhenCredentialsNullOrEmpty()
        {
            User user = service.Find("", "");
            Assert.IsNull(user);
        }
    }
}
