using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSharing.Models
{
    public class FileClass
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public String Name { get; set; } = "";
        public String Path { get; set; } = "";

        public List<FileClass> Files { get; set; } = new List<FileClass>();

        public int ClassroomId {get; set;}
        public Classroom Classroom {get; set;}
    }
}
