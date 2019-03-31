namespace Ingredients.Web.Repositories
{
    public interface IRepository<TModel, TId>
    {
        TModel GetOne(TId id);
        TModel UpsertOne(TModel model);
    }
}