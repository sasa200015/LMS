using LMS.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.DTO
{
    public class materialDto
    {
        public int id { get; set; }
        public int lecture_id { get; set; }
        public string material_title { get; set; }
        public string material_video { get; set; }
        public string material_text { get; set; }
        public string material_pdf { get; set; }
    }
}
