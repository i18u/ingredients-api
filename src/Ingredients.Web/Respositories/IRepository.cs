using System.Collections.Generic;

namespace Ingredients.Web.Repositories
{
    interface IRepository<TModel, TId>
    {
        IEnumerable<TModel> Get(int page, int limit);
        TModel GetOne(TId id);
        TModel UpsertOne(TModel model);
    }
}