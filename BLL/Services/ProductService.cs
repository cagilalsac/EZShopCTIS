using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IProductService
    {
        public Service Create(Product product);
        public Service Update(Product product);
        public Service Delete(int id);
        public IQueryable<ProductModel> Query();
    }

    public class ProductService : Service, IProductService
    {
        public ProductService(Db db) : base(db)
        {
        }

        public IQueryable<ProductModel> Query()
        {
            return _db.Products.Include(p => p.Category).OrderByDescending(p => p.ExpirationDate).ThenBy(p => p.StockAmount).ThenBy(p => p.Name).Select(p => new ProductModel() { Record = p });
        }

        public Service Create(Product product)
        {
            if (_db.Products.Any(p => p.Name.ToUpper() == product.Name.ToUpper().Trim()))
                return Error("Product with the same name exists!");
            if (product.StockAmount.HasValue && product.StockAmount.Value < 0)
                return Error("Stock amount must be a positive number!");
            product.Name = product.Name?.Trim();
            _db.Products.Add(product);
            _db.SaveChanges();
            return Success("Product created successfully.");
        }

        public Service Delete(int id)
        {
            // Way 1:
            //var product = _db.Products.Where(p => p.Id == id).SingleOrDefault();
            // Way 2:
            var product = _db.Products.Include(p => p.ProductStores).SingleOrDefault(p => p.Id == id);
            if (product is null)
                return Error("Product not found!");
            _db.ProductStores.RemoveRange(product.ProductStores);
            _db.Products.Remove(product);
            _db.SaveChanges();
            return Success("Product deleted successfully.");
        }

        

        public Service Update(Product product)
        {
            if (_db.Products.Any(p => p.Id != product.Id && p.Name.ToUpper() == product.Name.ToUpper().Trim()))
                return Error("Product with the same name exists!");
            if (product.StockAmount.HasValue && product.StockAmount.Value < 0)
                return Error("Stock amount must be a positive number!");
            var entity = _db.Products.SingleOrDefault(p => p.Id == product.Id);
            if (entity is null)
                return Error("Product not found!");
            entity.Name = product.Name?.Trim();
            entity.UnitPrice = product.UnitPrice;
            entity.StockAmount = product.StockAmount;
            entity.ExpirationDate = product.ExpirationDate;
            entity.CategoryId = product.CategoryId;
            _db.Products.Update(entity);
            _db.SaveChanges();
            return Success("Product updated successfully.");
        }
    }
}
