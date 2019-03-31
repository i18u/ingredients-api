using System;
using System.Threading.Tasks;

namespace Ingredients.Web.Cache
{
	public interface IAsyncCache<TCacheModel, TDatabaseModel, TId>
		where TCacheModel : ICacheModel<TDatabaseModel>, new()
	{
		Task<TCacheModel> GetOneAsync(TId key, Func<Task<TDatabaseModel>> cacheMissFunc);

		Task SetOneAsync(TId key, TDatabaseModel repoModel, TimeSpan? expiry = null);

		Task AddTagsAsync(TId key, string[] tags);
		
		Task RemoveTagsAsync(TId key, string[] tags);
	}
}