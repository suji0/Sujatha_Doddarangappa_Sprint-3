using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }

        public string Detail { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
