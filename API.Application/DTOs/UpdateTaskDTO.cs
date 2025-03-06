using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Application.DTOs
{
    public class UpdateTaskDTO
    {

        public string NameTask { get; set; }

        public string DescriptionTask { get; set; }
    }
}
