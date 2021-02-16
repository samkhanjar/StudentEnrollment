using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Service.Entities
{
    public class LectureTheatre : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        

        public string Name { get; set; }

        public int Capacity { get; set; }
    }
}
