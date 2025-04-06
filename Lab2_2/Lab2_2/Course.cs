using System;
using System.Collections.Generic;

namespace Lab2_2;

public partial class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int Duration { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
