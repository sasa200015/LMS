using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Model
{
    public class enrollments
    {
        public int id { get; set; }
        [ForeignKey("student")]
        public int student_id { get; set; }
        public students student { get; set; }
        [ForeignKey("course")]
        public int course_id { get; set; }
        public courses course { get; set; }
    }
}
