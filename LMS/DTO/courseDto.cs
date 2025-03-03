using LMS.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.DTO
{
    public class courseDto
    {
        public int id { get; set; }
        public string course_name { get; set; }
        public string course_des { get; set; }
        public string course_img { get; set; }
        public int level_id { get; set; }
        public string dep_name { get; set; }

    }
}
