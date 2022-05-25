using CowboysManager.Core.Entities;
using CowboysManager.Core.Interfaces;
using CowboysManager.Core.Services;
using Moq;
using System;
using TechTalk.SpecFlow;

namespace CowboysManager.Specflow.StepDefinitions
{
    [Binding]
    public class PlatformSteps
    {
        private Mock<IPlatformRepository<Platform>> repoMock;
        private PlatformService platformService;
        string _platformName;
        Platform platformResult;
        private Exception _actualException;

        public PlatformSteps()
        {
            var user = new User()
            {
                Id = 1,
                Username = "Username1"
            };

            var platforms = new List<Platform>
            {
                new Platform {Id = 1, Username = "Username1", EncryptedPassword="JwATE5LBQlER7zp52OusptWaVUaQC5oq1GLE15bn7kg", Name = "YouTube", UserId = user.Id},
                new Platform {Id = 2, Username = "Username2", EncryptedPassword="JwATE5LBQlER7zp52OusptWaVUaQC5oq1GLE15bn7kg", Name = "Facebook", UserId = user.Id}
            };
            repoMock = new Mock<IPlatformRepository<Platform>>();

            repoMock.Setup(x => x.GetAllPlatformsByUserId(user.Id)).Returns(platforms);
            platformService = new PlatformService(repoMock.Object);
        }


        [Given(@"I enter the platform name '([^']*)' that already exists")]
        public void GivenIEnterThePlatformNameThatAlreadyExists(string platformName)
        {
            _platformName = platformName;
        }

        [When(@"I create the platform")]
        public void WhenICreateThePlatform()
        {
            var platform = new Platform()
            {
                Name = _platformName,
                Username = "Username1",
                EncryptedPassword = "JwATE5LBQlER7zp52OusptWaVUaQC5oq1GLE15bn7kg",
                UserId = 1
            };

            try
            {
                platformService.CreatePlatform(platform);

            }
            catch (Exception ex)
            {

                _actualException = ex;
            }
        }

        [Then(@"I am presented with the error message '(.*)'")]
        public void ThenIAmPresentedWithTheErrorMessage(string expectedError)
        {
            Assert.Equal(expectedError, _actualException.Message);
        }

        [Given(@"I enter the platform name '(.*)' that doesnt exist")]
        public void GivenIEnterThePlatformNameThatDoesntExist(string platformName)
        {
            _platformName = platformName;
        }

        [Then(@"I am not presented with an error message")]
        public void ThenIAmNotPresentedWithAnErrorMessage()
        {
            Assert.Null(platformResult);
        }

        [Given(@"I enter the an empty platform name '([^']*)'")]
        public void GivenIEnterTheAnEmptyPlatformName(string platformName)
        {
            _platformName = platformName;
        }
    }
}
