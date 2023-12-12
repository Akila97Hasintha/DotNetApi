using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class EnrollmentModel
    {
       
        public int EnrollmentID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        public CourseModel Course { get; set; }
        public StudentModel Student { get; set; }
    }
}