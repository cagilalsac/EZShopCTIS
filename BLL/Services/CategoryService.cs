using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

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
            if (_db.Categories.Any(c => c.Name == category.Name))
                return Error("Category with the same name exists!");
            _db.Categories.Add(category);
            _db.SaveChanges();
            return Success();
        }

        public Service Delete(int id)
        {
            var category = _db.Categories.Include(c => c.Products).SingleOrDefault(c => c.Id == id);
            if (category is not null && category.Products.Any()) // Any(): category.Products.Count > 0
                return Error("Category cannot be deleted because it has relational products!");
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return Success();
        }

        public IQueryable<CategoryModel> Query()
        {
            return _db.Categories.OrderBy(c => c.Name).Select(c => new CategoryModel() { Record = c });
        }

        public Service Update(Category category)
        {
            if (_db.Categories.Any(c => c.Id != category.Id && c.Name == category.Name))
                return Error("Category with the same name exists!");
            _db.Categories.Update(category);
            _db.SaveChanges();
            return Success();
        }
    }
}
