using LMS.Model;

namespace LMS.Interface
{
    public interface Icategories
    {
        public List<categories> GetAll();
        public void AddCategory(categories category);
        public void saveChange();

    }
}
