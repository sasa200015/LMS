using LMS.Model;

namespace LMS.Interface
{
    public interface Imaterial
    {
        public List<lecture_materials> GetBylectureId(int id);
        public lecture_materials getById(int id);
        public void Add(lecture_materials material);
        public void Delete(lecture_materials material);
        public void saveChange();

    }
}
