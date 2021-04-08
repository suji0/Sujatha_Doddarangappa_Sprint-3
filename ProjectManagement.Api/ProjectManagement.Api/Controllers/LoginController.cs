using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Shared;
using ProjectManagement.Entities;
using ProjectManagement.Data.Interfaces;
using System.Web.Http.Results;

namespace ProjectManagement.Api.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public PMContext<User> _context;
        IBaseRepository<User> _repository;
        public LoginController(PMContext<User> context, IBaseRepository<User> repository)
        {
            _context = context;
            _repository = repository;
        }
        [HttpGet]
        public bool LoginUser(int userId, string password)
        {
            var userController = new UserController(_context, _repository);
            var loginSuccessfull = false;
            var userDetails = (OkObjectResult)userController.Get(userId);
            var userPassword = ((User)userDetails.Value).Password;
            if (userPassword == password)
            {
                loginSuccessfull = true;
            }
            return loginSuccessfull;
        }
    }
}
