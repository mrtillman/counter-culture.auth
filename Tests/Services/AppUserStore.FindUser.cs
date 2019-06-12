using CounterCulture.Repositories;
using CounterCulture.Models;
using CounterCulture.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
  [TestClass]
    public class AppUserStore_FindUser
    {
        private AppUserStore appUserStore;

        public AppUserStore_FindUser() {
            var mockUserRepo = new Mock<IAppUserRepository>(MockBehavior.Strict);
            mockUserRepo.Setup(repo => repo.FindByEmail(It.IsAny<string>()))
                        .Returns(new AppUser());
            appUserStore = new AppUserStore(mockUserRepo.Object);
        }

        [TestMethod]
        public async Task ShouldFindUserByEmail(){
            var foundUser = await appUserStore.FindByEmailAsync("user@example.com", CancellationToken.None);
            Assert.IsNotNull(foundUser);
        }

    }
}
