using Sat.Recruitment.Api.Application.User;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Model.Request;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var userController = new UsersController();
            var request = new CreateUserRequest()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = "124"
            };
            var result = userController.CreateUser(request).Result;
            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Error);
        }

        [Fact]
        public void Test2()
        {
            var userController = new UsersController();
            var request = new CreateUserRequest()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = "124"
            };
            var result = userController.CreateUser(request).Result;
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Error);
        }


        [Fact]
        public void Test3()
        {
            IUserType userType = new UserNormal();
            var result = userType.GetMoney("5253");
            Assert.True(result > 0);
        }

        [Fact]
        public void Test4()
        {
            IUserType userType = new UserSuper();
            var result = userType.GetMoney("5253");
            Assert.True(result > 0);
        }


        [Fact]
        public void Test5()
        {
            IUserType userType = new UserNormal();
            var result = userType.GetMoney("5253");
            Assert.True(result > 0);
        }
    }
}
