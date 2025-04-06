using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_2 {
    public class Teacher {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
