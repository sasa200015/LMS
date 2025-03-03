using LMS.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.DTO
{
    public class lectureDto
    {
        public int id { get; set; }
        public int course_id { get; set; }
        public string lecture_title { get; set; }
        public string lecture_description { get; set; }
    }
}
