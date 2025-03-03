using LMS.Interface;
using LMS.Model;

namespace LMS.Repo
{
    public class categoriesRepo : Icategories
    {
        private readonly ProjectContext context;

        public categoriesRepo(ProjectContext context)
        {
            this.context = context;
        }
        public void AddCategory(categories category)
        {
            context.Categories.Add(category);
        }

        public List<categories> GetAll()
        {
            List<categories> categories = context.Categories.ToList(); 
            return categories;
        }

        public void saveChange()
        {
            context.SaveChanges();
        }
    }
}
