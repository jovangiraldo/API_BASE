using API.Application.DTOs;
using API.Application.Interfaces;
using API.Domain.Entities;
using API.Domain.Interfaces;
using API.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;

using System.Threading;

namespace WebAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IRepository<CreateTask> _repository;
        private readonly IRepository<CreateAccount> _account;
        private readonly ICreateTask<CreateTask> _create;
        public TaskController(IRepository<CreateTask> repository, IRepository<CreateAccount> account, ICreateTask<CreateTask> create)
        {

            _repository = repository;
            _account = account;
            _create = create;

        }

        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public ActionResult AssignTask([FromBody] CreateTaskDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _account.GetById(request.CreateAccountId);
            if (user == null)
            {
                return NotFound(new { error = "Usuario no encontrado." });
            }

            // Creando la entidad basada en el DTO
            var task = new CreateTask
            {
                NameTask = request.NameTask,
                DescriptionTask = request.DescriptionTask,
                CreateAccountId = request.CreateAccountId,
                Status = API.Domain.Entities.TaskStatus.Pendiente

            };

            _repository.Add(task);
            _repository.Save();

            return Ok(new { message = "✅ Tarea asignada con éxito", task });
        }

        [HttpGet("user-task/{userId}")]
        public ActionResult<IEnumerable<CreateTask>> GetUserTask(int userId)
        {
            if (userId <= 0)
                return BadRequest(new { error = "El ID del usuario no es válido." });

            var tasks = _create.GetTaskByUserId(userId);

            if (tasks == null || !tasks.Any())
                return NotFound(new { error = "No hay tareas asignadas para este usuario." });

            return Ok(tasks);
        }


        [HttpPatch("Update-State/{taskid}")]
        public ActionResult UpdateStateTask(int taskid, [FromBody] UpdateTaskStatusDTO request)
        {        
            if (taskid <= 0)
            {
                return BadRequest(new { error = "El ID de la tarea no es válido." });
            }

            if (request.Status == null)
            {
                return BadRequest(new { error = "El estado de la tarea es obligatorio." });
            }

           
            var task = _repository.GetById(taskid);

            if (task == null)
            {
                return NotFound(new { error = "La tarea no existe." });
            }

            task.Status = (API.Domain.Entities.TaskStatus)request.Status;

            _repository.Update(task);
            _repository.Save();

            return Ok(new { message = "Estado de la tarea actualizado correctamente." });
        }




        [HttpPatch("{taskId}")]
        public ActionResult UpdateTaskEditComponent(int taskId, [FromBody] UpdateTaskDTO resquest) {

            try 
            {
                if (resquest == null)
                {
                    return BadRequest(new { error = "El objeto no puede ser nulo" });
                }

                var task = _create.GetTaskById(taskId);

                if (task == null)
                {

                    return NotFound(new { error = "La tarea no existe" });
                }

                task.NameTask = resquest.NameTask;
                task.DescriptionTask = resquest.DescriptionTask;

                _repository.Update(task);

                return Ok(new { message = "Tarea actualizada correctamente" });

            } catch (Exception ex) {

                return StatusCode(500, new { error = "Error interno del servidor", details = ex.Message });
            }      
        }

        [HttpDelete]
        public ActionResult DeleteTask(int taskId)
        {          

            if (taskId < 0 )
            {
                return BadRequest(new {error = "El ID de la tarea no es válido." });
            }

            var tasks = _create.GetTaskById(taskId);

            if (tasks == null)
            {
                return NotFound(new {error = "La tarea no existe." });
            }

            _create.DeleteTask(tasks.Id);


            return Ok(new { message = "Tarea eliminada correctamente" });
        }
    }
}
