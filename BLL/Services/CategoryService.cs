using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;

namespace BLL.Services
{
    public interface ICategoryService
    {
        public Service Create(Category category);
        public Service Update(Category category);
        public Service Delete(int id);
        public IQueryable<CategoryModel> Query();
    }

    public class CategoryService : Service, ICategoryService
    {
        public CategoryService(Db db) : base(db) // base: super in Java
        {
        }

        public Service Create(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return Success();
        }

        public Service Delete(int id)
        {
            var category = _db.Categories.Find(id);
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return Success();
        }

        public IQueryable<CategoryModel> Query()
        {
            throw new NotImplementedException();
        }

        public Service Update(Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
            return Success();
        }
    }
}
