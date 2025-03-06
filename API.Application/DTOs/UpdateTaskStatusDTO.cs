using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Application.DTOs
{
    public class UpdateTaskStatusDTO
    {
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TaskUpdate Status { get; set; }

        public enum TaskUpdate
        {
            Pendiente,
            EnProceso,
            Finalizada
        }
    }
}
