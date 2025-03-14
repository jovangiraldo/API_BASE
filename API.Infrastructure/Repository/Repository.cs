using API.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context) { 
        
            _context = context;
            _dbSet = _context.Set<T>();
        }
       
        public void Add(T entity)
        {
            _context.Add(entity);

            Save();

        }

        public void Delete(T id)
        {
            var obj = _context.Set<T>().Find(id);

            if (obj != null)
            {
                _context.Set<T>().Remove(obj);            

                Save();
            }
        }

        public IEnumerable<T> GetAll()
        {
           return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "La entidad no puede ser null.");
            }

            _context.Set<T>().Update(entity);
            return _context.SaveChanges() > 0; // Retorna true si se actualizó correctamente
        }

    }
}
