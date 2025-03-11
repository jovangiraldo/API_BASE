using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.Entities
{
    public class CreateAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(10)]
        public UserRole Role { get; set; } = UserRole.User;

        public List<CreateTask> Tasks { get; set; } = new List<CreateTask>(); 
    }

    public enum UserRole
    {
        Admin,
        User
    }

}
