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
    public class PlatformServiceTest
    {
        private Mock<IPlatformRepository<Platform>> repoMock;
        private PlatformService platformService;

        public PlatformServiceTest()
        {

            var platforms = new List<Platform>
            {
                new Platform {Id = 1, Username = "Username1", EncryptedPassword="JwATE5LBQlER7zp52OusptWaVUaQC5oq1GLE15bn7kg", Name = "Youtube", UserId = 1},
                new Platform {Id = 2, Username = "Username2", EncryptedPassword="JwATE5LBQlER7zp52OusptWaVUaQC5oq1GLE15bn7kg", Name = "Facebook", UserId = 1}
            };
            repoMock = new Mock<IPlatformRepository<Platform>>();

            repoMock.Setup(x => x.GetAllPlatforms()).Returns(platforms);
            platformService = new PlatformService(repoMock.Object);
        }

        [Fact]
        public void CreatePlatformIsNull()
        {

            var ex = Assert.Throws<ArgumentException>(() => platformService.CreatePlatform(null));

            Assert.Equal("Platform is missing", ex.Message);
            repoMock.Verify(repo => repo.CreatePlatform(It.Is<Platform>(u => u == null)), Times.Never);
        }

        [Fact]
        public void CreatePlatformValid()
        {
            Platform p = new Platform()
            {
                Name = "Fimmer",
                Username = "FimFi",
                UserId = 1
            };

            platformService.CreatePlatform(p);

            repoMock.Verify(repo => repo.CreatePlatform(It.Is<Platform>((pl => pl == p))), Times.Once);
        }

        [Theory]
        [InlineData(1, null, "Test", "JwATE5LBQlER7zp52OusptWaVUaQC5oq1GLE15bn7kg", 2)] //Testing for missing username
        [InlineData(1, "", "Test", "JwATE5LBQlER7zp52OusptWaVUaQC5oq1GLE15bn7kg", 2)] //Testing for empty username
        public void CreatePlatformInvalid(long id, string name, string username, string encryptedPassword, long userId)
        {

            Platform p = new Platform()
            {
                Id = id,
                Name = name,
                Username = username,
                EncryptedPassword = encryptedPassword,
                UserId = userId
            };

            var ex = Assert.Throws<InvalidOperationException>(() => platformService.CreatePlatform(p));

            Assert.Equal("Invalid platform property", ex.Message);
            repoMock.Verify(repo => repo.CreatePlatform((It.Is<Platform>(pl => pl == p))), Times.Never);
        }

    }
}
