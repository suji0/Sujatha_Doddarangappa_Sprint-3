using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;
using ProjectManagement.Shared;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Task")]
    public class TaskController : BaseController<Task>
    {

        public PMContext<Task> _taskContext;

        public TaskController(PMContext<Task> taskContext, IBaseRepository<Task> repository) : base(repository)
        {
            _taskContext = taskContext;

            if (!_taskContext.Table.Any())
            {
                _taskContext.Table.Add(new Task
                {
                    ID = 001,
                    AssignedToUserID = 001,
                    CreatedOn = DateTime.Now,
                    ProjectID = 001,
                    Status = Entities.Enums.TaskStatus.New,
                    Detail = "Test Task"
                });
            }
            _taskContext.SaveChanges();
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            return base.Get();

        }

        [HttpGet]
        [Route("{taskId}")]
        public IActionResult GetTask(long taskId)
        {
            return base.Get(taskId);
        }

        [HttpPut]
        public IActionResult UpdateTask([FromBody] Task taskDetail)
        {
            return base.Put(taskDetail);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] Task taskDetail)
        {
            return base.Post(taskDetail);
        }

        [HttpDelete]
        public IActionResult DeleteTask(long id)
        {
            return base.Delete(id);
        }
    }
}