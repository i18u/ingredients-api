using System;
using System.Collections.Generic;
using System.Linq;
using Cassandra;
using Ingredients.Web.Models.Database;

namespace Ingredients.Web.Repositories
{
    public class IngredientRepository : IRepository<Ingredient, Guid>
    {
        private ISession _session;

        public IngredientRepository(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Ingredient> Get(int page = 1, int limit = 20)
        {
            // Keep limit within the (0,100] range
            if (limit > 100)
            {
                limit = 100;
            }
            else if (limit <= 0)
            {
                return new List<Ingredient>();
            }

            var ps = _session.Prepare("SELECT * FROM ingredient");
            var statement = ps.Bind().SetPageSize(limit);

            var results = _session.Execute(statement);
            var memResults = new List<Ingredient>();

            if(page > 1)
            {
                var resultsToSkip = page * limit;
                results.Skip(resultsToSkip);
            }

            foreach (var row in results)
            {
                // Results is an enumerator of ALL the rows in the table, however
                // it retrieves them from Cassandra in blocks of the specified size
                var item = Ingredient.FromRow(row);

                memResults.Add(item);
            }

            return memResults;
        }

        public Ingredient GetOne(Guid id)
        {
            var ps = _session.Prepare("SELECT name, description, tags FROM ingredient WHERE id = ?");
            var statement = ps.Bind(id);

            var results = _session.Execute(statement);
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
        }

        public Ingredient UpsertOne(Ingredient model)
        {
            var entityId = Guid.NewGuid();
            var name = model.Name;
            var description = model.Description;
            var tags = model.Tags;

            if (model.Id != Guid.Empty)
            {
                entityId = model.Id;
            }

            var ps = _session.Prepare("INSERT INTO ingredient (id, name, description, tags) VALUES (?, ?, ?, ?)");
            var statement = ps.Bind(entityId, name, description, tags);

            _session.Execute(statement);

            model.Id = entityId;

            return model;
        }
    }
}