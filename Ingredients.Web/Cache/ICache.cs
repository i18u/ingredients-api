using System;

namespace Ingredients.Web.Cache
{
	public interface ICache<TCacheModel, TDatabaseModel, TId>
		where TCacheModel : ICacheModel<TDatabaseModel>, new()
	{
		TCacheModel GetOne(TId key, Func<TDatabaseModel> cacheMissFunc);

		void SetOne(TId key, TDatabaseModel repoModel, TimeSpan? expiry = null);

		void AddTags(TId key, string[] tags);
		
		void RemoveTags(TId key, string[] tags);
	}
}