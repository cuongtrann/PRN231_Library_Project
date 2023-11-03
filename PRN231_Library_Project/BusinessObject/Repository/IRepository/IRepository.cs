using System.Drawing;

namespace PRN231_Library_Project.BusinessObject.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll(int page, int size);
    }
}
