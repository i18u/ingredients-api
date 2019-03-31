namespace Ingredients.Web.Repositories
{
    interface IRepository<TModel, TId>
    {
        TModel GetOne(TId id);
        TModel UpsertOne(TModel model);
    }
}