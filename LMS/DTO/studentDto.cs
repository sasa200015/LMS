using LMS.Model;

namespace LMS.DTO
{
    public class studentDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public bool isAdmin { get; set; }
        public DateTime created_at { get; set; }
    }
}
