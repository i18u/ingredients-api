using System.Threading.Tasks;

namespace Ingredients.Web.Repositories
{
	public interface IAsyncRepository<TModel, TId>
	{
		Task<TModel> GetOneAsync(TId id);
		Task<TModel> UpsertOneAsync(TModel model);
	}
}