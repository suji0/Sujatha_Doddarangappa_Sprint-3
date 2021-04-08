using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Entities;
using System.Collections.Generic;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Shared;
using System.Linq;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : BaseController<User>
    {
        public PMContext<User> _userContext;
        public UserController(PMContext<User> userContext, IBaseRepository<User> repository) : base(repository)
        {
            _userContext = userContext;
            if (!_userContext.Table.Any())
            {
                _userContext.Table.Add(new User
                {
                    ID = 001,
                    FirstName = "Sujatha",
                    LastName = "D",
                    Email = "sujatha8050@gmail.com",
                    Password = "Suji8050"
                });
            }
            _userContext.SaveChanges();
        }
        [HttpGet]
        public IActionResult Get()
        {
            return base.Get();
        }

        [HttpGet]
        [Route("{userId}")]
        public IActionResult Get(long userId)
        {
            return base.Get(userId);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User userDetail)
        {
            return base.Put(userDetail);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User userDetail)
        {
            return base.Post(userDetail);
        }

        [HttpDelete]
        public IActionResult DeleteUser(long Id)
        {
            return base.Delete(Id);
        }

    }
}
