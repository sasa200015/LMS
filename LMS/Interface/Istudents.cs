using LMS.Model;

namespace LMS.Interface
{
    public interface Istudents
    {
        public students GetByEmail(string email);
        public void Add(students students);
        public void saveChange();

    }
}
