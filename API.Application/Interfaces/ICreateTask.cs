using API.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Application.Interfaces
{
    public interface ICreateTask<T> where T : class
    {
         List<CreateTask> GetTaskByUserId(int userId);

        CreateTask GetTaskById(int taskId);

        void DeleteTask(int taskId);

    }
}
