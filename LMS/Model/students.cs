namespace LMS.Model
{
    public class students
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public bool isAdmin { get; set; }
        public DateTime created_at { get; set; }
        public ICollection<enrollments> courses { get; set; }
    }
}
