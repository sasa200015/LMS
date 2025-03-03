using LMS.Interface;
using LMS.Model;

namespace LMS.Repo
{
    public class lecturesRepo : Ilectures
    {
        private readonly ProjectContext context;

        public lecturesRepo(ProjectContext context)
        {
            this.context = context;
        }
        public void Add(lectures lectures)
        {
            context.Add(lectures);
        }

        public void Delete(lectures lecture)
        {
            context.Lectures.Remove(lecture);
        }

        public List<lectures> GetAll()
        {
            List<lectures> lec = context.Lectures.ToList();
            return lec;
        }

        public lectures GetById(int id)
        {
            lectures lecture = context.Lectures.SingleOrDefault(s => s.id == id);
            return lecture;
        }

        public void Update( lectures lecture)
        {

            context.Lectures.Update(lecture);
        }
        public void saveChange()
        {
            context.SaveChanges();
        }
    }
}
