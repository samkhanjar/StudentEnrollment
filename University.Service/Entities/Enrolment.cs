using System;
using System.ComponentModel.DataAnnotations;

namespace University.Service.Entities
{
    public class Enrollment : EntityBase
    {        
        [Key]
        public int StudentId { get; set; }

        [Key]
        public int SubjectId { get; set; }        
    }
}
