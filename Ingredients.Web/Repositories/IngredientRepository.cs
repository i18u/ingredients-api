using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using Ingredients.Web.Cache;
using Ingredients.Web.Models.Database;

namespace Ingredients.Web.Repositories
{
    public class IngredientRepository 
        : IRepository<Ingredient, Guid>, IAsyncRepository<Ingredient, Guid>
    {
        private ISession _session;

        public IngredientRepository(ISession session)
        {
            _session = session;
        }

        public Ingredient GetOne(Guid id)
        {
            return GetOneAsync(id).GetAwaiter().GetResult();
        }

        public Task<Ingredient> GetOneAsync(Guid id)
        {
            var task = _session.PrepareAsync("SELECT name, description, tags FROM ingredient WHERE id = ?")
                .ContinueWith(prepareTask =>
                {
                    var ps = prepareTask.Result;
                    var statement = ps.Bind(id);
                    return _session.ExecuteAsync(statement);
                })
                .ContinueWith(queryTask =>
                {
                    var results = queryTask.Result.Result;
                    var memResults = new List<Ingredient>();

                    foreach (var row in results) 
                    {
                        var item = Ingredient.FromRow(id, row);

                        memResults.Add(item);
                    }

                    if (memResults.Any())
                    {
                        var result = memResults.First();
                        return result;
                    }

                    return null;
                });
            
            return task;
        }

        public Ingredient UpsertOne(Ingredient model)
        {
            return UpsertOneAsync(model).GetAwaiter().GetResult();
        }

        public Task<Ingredient> UpsertOneAsync(Ingredient model)
        {
            var entityId = Guid.NewGuid();
            var name = model.Name;
            var description = model.Description;
            var tags = model.Tags;

            if (model.Id != Guid.Empty) 
            {
                entityId = model.Id;
            }
            
            return _session.PrepareAsync("INSERT INTO ingredient (id, name, description, tags) VALUES (?, ?, ?, ?)")
                .ContinueWith(prepareTask =>
                {
                    var ps = prepareTask.Result;
                    var statement = ps.Bind(entityId, name, description, tags);
                    return _session.ExecuteAsync(statement);
                })
                .ContinueWith(updateTask =>
                {
                    model.Id = entityId;
                    return model;
                });
        }
    }
}