using LMS.Model;

namespace LMS.Interface
{
    public interface Ilectures
    {
        public List<lectures> GetAll();
        public lectures GetById(int id);
        public void Add(lectures lectures);
        public void Update(lectures lecture);
        public void Delete(lectures lecture);
        public void saveChange();

    }
}
