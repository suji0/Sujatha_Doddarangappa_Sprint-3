using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Controllers
{
    public class BaseController<T> : ControllerBase where T : BaseEntity
    {
        public IBaseRepository<T> _repository;
        public BaseController(IBaseRepository<T> repository)
        {
            _repository = repository;
        }
        [NonAction]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.Get());
            }
            catch (Exception ex)
            {
                var error = "Get failed due to " + ex;
                return Ok(error);
            }
        }

        [NonAction]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(_repository.Get(id));
            }
            catch (Exception ex)
            {
                var error = "Get failed due to " + ex;
                return Ok(error);
            }
        }

        [NonAction]
        public IActionResult Post(T entity)
        {
            try
            {
                return Ok(_repository.Add(entity));
            }
            catch (Exception ex)
            {
                var error = "Creation failed due to " + ex;
                return Ok(error);
            }
        }

        [NonAction]
        public IActionResult Put(T entity)
        {
            try
            {
                return Ok(_repository.Update(entity));
            }
            catch (Exception ex)
            {
                var error = "Update failed due to " + ex;
                return Ok(error);
            }
        }

        [NonAction]
        public IActionResult Delete(long id)
        {
            try
            {
                return Ok(_repository.Delete(id));
            }
            catch (Exception ex)
            {
                var error = "Delete failed due to " + ex;
                return Ok(error);
            }
        }

    }
}
