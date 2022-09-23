using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Entities.Entities.Abstract;

namespace Users.Entities.Entities.Concreate
{
    public class Department:IEntity
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
