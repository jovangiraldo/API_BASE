using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Application.DTOs
{
    public class CreateTaskDTO
    {
    [Required]
    public string NameTask { get; set; }

    [Required]
    public string DescriptionTask { get; set; }

    [Required]
    public int CreateAccountId { get; set; }
    }
}
