using System;

namespace University.Service.Entities
{
    public class EntityBase
    {
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
