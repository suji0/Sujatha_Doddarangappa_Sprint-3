using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;
using ProjectManagement.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Project")]
    public class ProjectController : BaseController<Project>
    {
        private PMContext<Project> _context;
        public ProjectController(PMContext<Project> context, IBaseRepository<Project> repository) : base(repository)
        {
            _context = context;
            if (!_context.Table.Any())
            {
                _context.Table.Add(new Project
                {
                    ID = 001,
                    Name = "ABC",
                    CreatedOn = DateTime.Now,
                    Detail = "Abc Project"
                });
                _context.SaveChanges();
            }
        }
        [HttpGet()]
        public IActionResult GetAllProjects()
        {
            return base.Get();
        }

        [HttpGet]
        [Route("{projectId}")]
        public IActionResult GetProject(long projectId)
        {
            return base.Get(projectId);
        }


        [HttpPut]
        public IActionResult UpdateProject([FromBody] Project projectDetail)
        {
            return base.Put(projectDetail);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] Project projectDetail)
        {
            return base.Post(projectDetail);
        }

        [HttpDelete]
        public IActionResult DeleteUser(long id)
        {
            return base.Delete(id);
        }
    }
}
