using CowboysManager.Core.Entities;
using CowboysManager.Core.Interfaces;
using CowboysManager.Core.Services;
using Moq;

namespace CowboysManager.Specflow.StepDefinitions
{
    [Binding]
    public class UserSteps
    {
        private UserService userService;
        private Mock<IUserRepository<User>> repoMock;
        string _username;
        User userResult;
        private Exception _actualException;
        public UserSteps()
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
        [Given(@"I enter the username '(.*)' that already exists")]
        public void GivenIEnterTheUsernameThatAlreadyExists(string username)
        {  
            _username = username;
        }

        [When(@"I create the account")]
        public void WhenICreateTheAccount() 
        {
            var user = new User()
            {
                Id = 1,
                Username = _username
            };

            try
            {
                userResult = userService.CreateUser(user);

            }
            catch (Exception ex)
            {
                _actualException = ex;
            }
        }

        [Then(@"The user is presented with the error message '(.*)'")]
        public void ThenTheUserIsPresentedWithAnErrorMessage(string expectedError)
        {
            Assert.Equal(expectedError, _actualException.Message);
        }

        [Given(@"I enter the username '([^']*)' that doesnt exist")]
        public void GivenIEnterTheUsernameThatDoesntExist(string username)
        {
            _username = username;
        }

        [Then(@"The user is not presented with an error message")]
        public void ThenTheUserIsNotPresentedWithAnErrorMessage()
        {
            Assert.Null(userResult);  
        } 
        [Given(@"I enter the an empty username '(.*)'")]
        public void GivenIEnterTheAnEmptyUsername(string username)
        {
            _username = username;
        }
    }
}