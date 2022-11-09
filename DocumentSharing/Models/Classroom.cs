using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSharing.Models
{
    public class Classroom
    {
        public int Id { get; set; }

        [Required]
        public String ClassName { get; set; }

        [Required]
        public String Subject { get; set; }

        public ICollection<FileClass> FileClasses {get; set;}
    }
}
