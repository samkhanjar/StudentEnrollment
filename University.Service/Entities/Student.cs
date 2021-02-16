using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Service.Entities
{
    public class Student : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }               

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }                
    }
}
