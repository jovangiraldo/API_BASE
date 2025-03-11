using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace API.Domain.Entities
{
    public class CreateTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string NameTask { get; set; }

        [Required]
        public string DescriptionTask { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TaskStatus Status { get; set; } = TaskStatus.Pendiente;
        
        // 🔹 Clave foránea para el usuario que recibe la tarea
        [Required]
        public int CreateAccountId { get; set; }

        [ForeignKey("CreateAccountId")]
        [JsonIgnore]
        public CreateAccount AssignedUser { get; set; }
    }

    public enum TaskStatus
    {
        Pendiente,
        EnProceso,
        Finalizada
    }
}
