using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sat.Recruitment.Api.Application.User;
using Sat.Recruitment.Api.CrossCore;
using Sat.Recruitment.Api.Model;
using Sat.Recruitment.Api.Model.Request;
using Sat.Recruitment.Api.Model.Response;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        [HttpPost]
        [Route("/create-user")]
        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            CreateUserResponse response = null;
            response = ModelIsValid(request);

            if (!response.IsSuccess)
                return response;

            UserType userType = new UserType(request.UserType);

            var newUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                Phone = request.Phone,
                UserType = request.UserType,
                Money = userType.GetMoney(request.Money)
            };

            var users = await Common.ReadUsersFromFile("Files/Users.txt");
            newUser.Email = Common.NormalizeEmail(newUser.Email);

            if (users.Any(x => (x.Email == newUser.Email || x.Phone == newUser.Phone) ||
                               (x.Name == newUser.Name && x.Address == newUser.Address)))
            {
                response = new CreateUserResponse()
                {
                    IsSuccess = false,
                    Error = "The user is duplicated"
                }; 
            }
            else
            {
                response = new CreateUserResponse()
                {
                    IsSuccess = true,
                    Error = "User Created"
                };
            }
            return response;
        }

        private CreateUserResponse ModelIsValid(CreateUserRequest request)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(request.Name))
                errors.Append("The Name is required. \n");

            if (string.IsNullOrEmpty(request.Email))
                errors.Append("The Email is required. \n");
            
            if (string.IsNullOrEmpty(request.Address))
                errors.Append("The Address is required. \n");
            
            if (string.IsNullOrEmpty(request.Phone))
                errors.Append("The Phone is required. \n");

            return new CreateUserResponse()
            {
                IsSuccess = errors.Length == 0,
                Error = errors.ToString()
            };
        }
    }
}
