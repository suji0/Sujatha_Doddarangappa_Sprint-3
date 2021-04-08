using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManagement.Shared;

namespace ProjectManagement.Data.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public PMContext<T> _context;
        public BaseRepository(PMContext<T> context)
        {
            _context = context;
        }
        public int Add(T entity)
        {
            _context.Add(entity);
            return _context.SaveChanges();
        }

        public int Delete(long id)
        {
            _context.Table.Remove(_context.Table.FirstOrDefault(item => item.ID == id));
            return _context.SaveChanges();
        }

        public IQueryable<T> Get()
        {
            return _context.Table;
        }

        public T Get(long id)
        {
            return _context.Table.FirstOrDefault(item => item.ID == id);
        }

        public int Update(T entity)
        {
            _context.Update(entity);
            return _context.SaveChanges();
        }
    }
}
