namespace BLL.Services.Bases
{
    // Way 1:
    //public interface IService
    //{
    //    public IQueryable<TModel> Query<TModel>() where TModel : class, new();
    //    public Service Create<TEntity>(TEntity entity) where TEntity : class, new();
    //}

    // Way 2:
    public interface IService<TEntity, TModel> where TEntity : class, new() where TModel : class, new()
    {
        public IQueryable<TModel> Query();
        public Service Create(TEntity entity);
        public Service Update(TEntity entity);
        public Service Delete(int id);
    }
}
