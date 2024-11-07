using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IStoreService
    {
        public IQueryable<StoreModel> Query();
        public Service Create(Store store);
        public Service Update(Store store);
        public Service Delete(int id);
    }

    public class StoreService : Service, IStoreService
    {
        public StoreService(Db db) : base(db)
        {
        }

        public IQueryable<StoreModel> Query()
        {
            return _db.Stores.Include(s => s.Country).Include(s => s.City).OrderByDescending(s => s.IsVirtual).ThenBy(s => s.Name)
                .Select(s => new StoreModel() { Record = s });
        }

        public Service Create(Store store)
        {
            if (_db.Stores.Any(s => s.IsVirtual == store.IsVirtual && s.Name.ToUpper() == store.Name.ToUpper().Trim()))
                return Error("Store with the same name exists!");
            _db.Stores.Add(store);
            _db.SaveChanges();
            return Success("Store created successfully.");
        }

        public Service Delete(int id)
        {
            var entity = _db.Stores.Include(s => s.ProductStores).SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return Error("Store not found!");
            _db.ProductStores.RemoveRange(entity.ProductStores);
            _db.Stores.Remove(entity);
            _db.SaveChanges();
            return Success("Store deleted successfully.");
        }

        public Service Update(Store store)
        {
            if (_db.Stores.Any(s => s.Id != store.Id && s.IsVirtual == store.IsVirtual && s.Name.ToUpper() == store.Name.ToUpper().Trim()))
                return Error("Store with the same name exists!");
            var entity = _db.Stores.Find(store.Id);
            if (entity is null)
                return Error("Store not found!");
            entity.Name = store.Name?.Trim();
            entity.IsVirtual = store.IsVirtual;
            _db.Stores.Update(entity);
            _db.SaveChanges();
            return Success("Store updated successfully.");
        }
    }
}
