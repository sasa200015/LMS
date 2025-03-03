using LMS.Interface;
using LMS.Model;

namespace LMS.Repo
{
    public class studentsRepo:Istudents
    {
        private readonly ProjectContext context;

        public studentsRepo(ProjectContext context)
        {
            this.context = context;
        }

        void Istudents.Add(students students)
        {
            context.Students.Add(students);
        }

        public students GetByEmail(string email)
        {
            students student =context.Students.FirstOrDefault(s=>s.email == email);
            return student;
        }

        public void saveChange()
        {
            context.SaveChanges();
        }
    }
}
