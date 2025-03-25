using API.Application.Interfaces;
using API.Domain.Entities;
using API.Infrastructure.Context;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Application.Services
{
    public class CreateTaskServices<T> : ICreateTask<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public CreateTaskServices(ApplicationDbContext context) { 
        
            _context = context;
        }

        public void DeleteTask(int taskId)
        {
           
            if (taskId <= 0)
            {
                throw new ArgumentException("El ID de la tarea no es válido.");
            }

           
            var obj = _context.Set<CreateTask>().FirstOrDefault(t => t.Id == taskId);

            if (obj == null)
            {
                throw new KeyNotFoundException("La tarea no existe o ya fue eliminada.");
            }

            _context.Remove(obj);
            _context.SaveChanges();
        }

      

        public CreateTask GetTaskById(int taskId)
        {
           return _context.CreateTask.FirstOrDefault(t => t.Id == taskId);
        }

        public List<CreateTask>GetTaskByUserId(int userId)
        {
            return  _context.CreateTask
                    .Where(t => t.CreateAccountId == userId)
                    .ToList();           
        }
    }
}
