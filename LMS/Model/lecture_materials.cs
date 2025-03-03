using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Model
{
    public class lecture_materials
    {
        public int id { get; set; }
        [ForeignKey("lectures")]
        public int lecture_id { get; set; }
        public lectures? lectures { get; set; }
        public string material_title { get; set; }
        public string material_video { get; set; }
        public string material_text { get; set; }
        public string material_pdf { get; set; }

    }
}
