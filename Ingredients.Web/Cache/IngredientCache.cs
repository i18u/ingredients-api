using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ingredients.Web.Models.Cache;
using Ingredients.Web.Repositories;

namespace Ingredients.Web.Cache
{
	public class IngredientCache : Cache<Ingredient, Models.Database.Ingredient, Guid>
	{
		public IngredientCache(IngredientRepository repository)
			: base(repository, "Ingredients")
		{
		}
		
		public Models.Database.Ingredient UpsertOne(Models.Database.Ingredient ingredient)
		{
			// Add the model to the database
			var repoModel = Repository.UpsertOne(ingredient);
			
			// Add this repo model to cache
			SetOne(repoModel.Id, repoModel);
			if (repoModel.Tags?.Any() ?? false)
			{
				AddTags(repoModel.Id, repoModel.Tags);
			}
			
			return repoModel;
		}
		
		public Task<Models.Database.Ingredient> UpsertOneAsync(Models.Database.Ingredient ingredient)
		{
			// Add the model to the database
			if (Repository is IAsyncRepository<Models.Database.Ingredient, Guid> asyncRepository)
			{
				return asyncRepository.UpsertOneAsync(ingredient)
					.ContinueWith(upsertTask =>
					{
						var repoModel = upsertTask.Result;
						
						var tasks = new List<Task>();
						// Add this repo model to cache
						tasks.Add(SetOneAsync(repoModel.Id, repoModel));
						if (repoModel.Tags?.Any() ?? false)
						{
							tasks.Add(AddTagsAsync(repoModel.Id, repoModel.Tags));
						}

						Task.WaitAll(tasks.ToArray());
						return repoModel;
					});
			}

			throw new Exception("Repository is not Async-compatible");
		}

		public Ingredient GetOne(Guid key)
		{
			// Return the ingredient from cache
			return GetOne(key, () => Repository.GetOne(key));
		}

		public Task<Ingredient> GetOneAsync(Guid key)
		{
			// Return the ingredient from cache
			return GetOneAsync(key, () =>
			{
				if (Repository is IAsyncRepository<Models.Database.Ingredient, Guid> asyncRepository)
				{
					return asyncRepository.GetOneAsync(key);
				}
				
				throw new Exception("Repository is not Async-compatible");
			});
		}
	}
}