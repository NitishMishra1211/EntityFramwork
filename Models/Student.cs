using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Student
    {
        [Key]
        public int rollno { get; set; }

        [Column("Student_Name", TypeName = "varchar(100)")]
        public string Name { get; set; } = null;

        [Column("Student_Gender", TypeName = "varchar(100)")]
        public string Gender { get; set; } = null;

        public string UserName { get; set; } = null;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null;
        public int age { get; set; }
    }
}
