using CounterCulture.Repositories;
using CounterCulture.Models;
using CounterCulture.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Tests
{
  [TestClass]
    public class SanityCheck
    {

        [TestMethod]
        public void DoIt(){
            Assert.IsTrue(true);
        }

    }
}
