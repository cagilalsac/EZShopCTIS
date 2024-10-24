using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;

namespace BLL.Services
{
    public interface IProductService
    {
        public Service Create(Product product);
        public Service Update(Product product);
        public Service Delete(int id);
        public IQueryable<ProductModel> Query();
    }

    public class ProductService
    {
    }
}
