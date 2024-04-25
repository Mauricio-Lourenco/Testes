using Interfaces.Repositories;
using Moq;
using WebApplication1;
using WebApplication1.Services;
using Xunit;

namespace Tests {
    public class UserServiceTests {

        private Mock<IUserRepository> mockUserRepository;
        private Mock<IJwtService> mockJwtService;
        private UserService userService;
        private User mockUser;
        private List<User> mockUsers;
      //private JwtService jwtService;

        public UserServiceTests() {

            mockUser = new User();
            mockUsers = new List<User> { new User(), new User() };
            mockUserRepository = new Mock<IUserRepository>();
            mockJwtService = new Mock<IJwtService>();
            userService = new UserService(mockUserRepository.Object, mockJwtService.Object);
     //     jwtService = new JwtService("3A5F9B2C4E7D81A053F6E248D92C0B17AAAA");

        }

        [Fact]
        public void CreateUserResponsible_Exception_UserAlreadyExists() {

            mockUserRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(mockUser);

            var exception = Assert.Throws<Exception>(() => userService.CreateUserResponsible(mockUser));

            Assert.Equal("User already exists", exception.Message);

        }

        [Fact]
        public void CreateUserEmployee_Exception_UserAlreadyExists() {

            mockUserRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(mockUser);

            var exception = Assert.Throws<Exception>(() => userService.CreateUserEmployee(mockUser));

            Assert.Equal("User already exists", exception.Message);

        }

        [Fact]
        public void ListAllUsers_Test() {

            mockUserRepository.Setup(x => x.GetAll()).Returns(mockUsers);

            var result = userService.ListAllUser();

            Assert.NotNull(result);
            Assert.Equal(mockUsers.Count, result.Count);

            for (int i = 0; i < mockUsers.Count; i++) {
                Assert.Equal(mockUsers[i].Id, result[i].Id);
                Assert.Equal(mockUsers[i].Email, result[i].Email);
                Assert.Equal(mockUsers[i].UserType, result[i].UserType);
            }
        }
        

        [Fact]
        public void Login_test() {

            User user = new User { Id = Guid.NewGuid(), Password = "123" };

            var result = userService.Login("teste@teste.com", "123");

            mockUserRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(user);

            //var jwt = jwtService.GenerateToken(user.Id.ToString(), user.Email);

            //Assert.Equal(jwt, result);


        }
       

        [Fact]
        public void DeleteUserById_test() {

            mockUserRepository.Setup(x => x.GetById(It.IsAny<Guid>()));

            var exception = Assert.Throws<ArgumentException>(() => userService.DeleteUserById(Guid.NewGuid()));

            Assert.Equal("User not found.", exception.Message);
        }

    }
}
