using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models.DTOs;
using ReimbursementTrackerApp.Models;
using ReimbursementTrackerApp.Repositories;
using ReimbursementTrackerApp.Services;
using ReimbursementTrackerApp.Contexts;
using System.Text;

namespace ReimbursementTrackerTest
{
    public class UserServiceTests
    {
        IRepository<string, User> repository;
        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<RTAppContext>()
                                .UseInMemoryDatabase("dbTestCustomer")//a database that gets created temp for testing purpose
                                .Options;
            RTAppContext context = new RTAppContext(dbOptions);
            repository = new UserRepository(context);

        }

        [Test]
        [TestCase("praveena.vallela2002@gmail.com", "praveena")]
        public void LoginTest(string un, string pass)
        {

            //Arrange
            var appSettings = @"{""SecretKey"": ""Anything will work here this is just for testing""}";
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            var tokenService = new TokenService(configurationBuilder.Build());
            IUserService userService = new UserService(repository, tokenService);
            userService.Register(new UserDTO
            {
                Username = un,
                Password = pass,
                Role = "Admin"
            });
            //Action
            var resulut = userService.Login(new UserDTO { Username = "praveena.vallela2002@gmail.com", Password = "praveena", Role = "Admin" });
            //Assert
            Assert.AreEqual("praveena.vallela2002@gmail.com", resulut.Username);
        }

    }
}

