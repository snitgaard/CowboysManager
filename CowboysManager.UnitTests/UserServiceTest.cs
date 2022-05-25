using CowboysManager.Core.Entities;
using CowboysManager.Core.Interfaces;
using CowboysManager.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowboysManager.UnitTests
{
    public class UserServiceTest
    {
        private UserService userService;
        private Mock<IUserRepository<User>> repoMock;

        public UserServiceTest()
        {
            var users = new List<User>
            {
                new User { Id = 1, Username="Username1", Password = "Password1"},
                new User { Id = 2, Username="Username2", Password = "Password2"},
            };

            repoMock = new Mock<IUserRepository<User>>();
            repoMock.Setup(x => x.GetAllUsers()).Returns(users);
            userService = new UserService(repoMock.Object);
        }

        [Fact]
        public void CreateUserExistingUserExpectInvalidOperationException()
        {
            User u = new User()
            {
                Id = 1,
                Username = "Username1",
                Password = "Password1"
            };
            var ex = Assert.Throws<InvalidOperationException>(() => userService.CreateUser(u));
            Assert.Equal("This User already exists", ex.Message);
            repoMock.Verify(repo => repo.CreateUser(It.Is<User>(us => us == u)), Times.Never);
        }

        [Fact]
        public void CreateUserIsNull()
        {
            UserService service = new UserService(repoMock.Object);

            var ex = Assert.Throws<ArgumentException>(() => service.CreateUser(null));

            Assert.Equal("User is missing", ex.Message);
            repoMock.Verify(repo => repo.CreateUser(It.Is<User>(u => u == null)), Times.Never);
        }

        [Fact]
        public void CreateUserValid()
        {
            IUserRepository<User> repo = repoMock.Object;
            UserService service = new UserService(repo);

            User u = new User()
            {
                Username = "Fimmer",
                Password = "password"
            };

            service.CreateUser(u);

            repoMock.Verify(repo => repo.CreateUser(It.Is<User>((us => us == u))), Times.Once);
        }

        [Theory]
        [InlineData(1, null, "Password")] //Testing for missing username
        [InlineData(1, "", "Password")] //Testing for empty username
        [InlineData(1, "UsernameIsTooLong", "Password")] //Testing for username length above 16
        public void CreateUserInvalid(int id, string username, string password)
        {
            UserService service = new UserService(repoMock.Object);

            User u = new User()
            {
                Id = id,
                Username = username,
                Password = password,
            };

            var ex = Assert.Throws<ArgumentException>(() => service.CreateUser(u));

            Assert.Equal("Invalid user property", ex.Message);
            repoMock.Verify(repo => repo.CreateUser((It.Is<User>(us => us == u))), Times.Never);
        }
    }
}
