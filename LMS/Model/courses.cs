using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Model
{
    public class courses
    {
        public int id { get; set; }
        public string course_name { get; set; }
        public string course_des { get; set; }
        public string course_img { get; set; }
        [ForeignKey("categories")]
        public int level_id { get; set; }
        public categories? categories { get; set; }
        public string dep_name { get; set; }
        public ICollection<enrollments>? students { get; set; }
        public List<lectures>? lectures { get; set; }
    }
}
