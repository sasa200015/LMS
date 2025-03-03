using LMS.Model;

namespace LMS.Interface
{
    public interface Icourses
    {
        public List<courses> getByLevelId(int levelId);
        public void addCourse(courses course);
        public void saveChange();
    }
}
