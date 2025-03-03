using LMS.Interface;
using LMS.Model;

namespace LMS.Repo
{
    public class coursesRepo : Icourses
    {
        private readonly ProjectContext context;

        public coursesRepo(ProjectContext context)
        {
            this.context = context;
        }
        public void addCourse(courses course)
        {
            context.Courses.Add(course);
        }

        public List<courses> getByLevelId(int levelId)
        {
            List<courses> courses = context.Courses.Where(m => m.level_id == levelId).ToList();
            return courses;
        }
        public void saveChange()
        {
            context.SaveChanges();
        }

    }
}
