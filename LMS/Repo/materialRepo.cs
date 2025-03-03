using LMS.Interface;
using LMS.Model;

namespace LMS.Repo
{
    public class materialRepo : Imaterial
    {
        private readonly ProjectContext context;

        public materialRepo(ProjectContext context)
        {
            this.context = context;
        }
        public void Add(lecture_materials material)
        {
            context.Add(material);
        }

        public void Delete(lecture_materials material)
        {
            context.LectureMaterials.Remove(material);
        }

        public lecture_materials getById(int id)
        {
            lecture_materials lecture = context.LectureMaterials.SingleOrDefault(s => s.id == id);
            return lecture;
        }
        public List<lecture_materials> GetBylectureId(int id)
        {
            List<lecture_materials> matrials = context.LectureMaterials.Where(s => s.lecture_id == id).ToList();
            return matrials;
        }
        public void saveChange()
        {
            context.SaveChanges();
        }
    }
}
