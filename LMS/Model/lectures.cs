using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Model
{
    public class lectures
    {
        public int id { get; set; }

        [ForeignKey("courses")]
        public int course_id { get; set; }
        public courses? courses { get; set; }
        public string lecture_title { get; set; }
        public string lecture_description { get; set; }
        public lecture_materials? material { get; set; }
    }
}
