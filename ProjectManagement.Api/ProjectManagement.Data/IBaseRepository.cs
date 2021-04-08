using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManagement.Data.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IQueryable<T> Get();

        T Get(long id);

        int Add(T entity);

        int Update(T entity);

        int Delete(long id);

    }
}
