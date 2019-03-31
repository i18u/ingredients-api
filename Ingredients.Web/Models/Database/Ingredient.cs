using System;
using Cassandra;
using Ingredients.Web.Cache;
using Ingredients.Web.Models.Transport;

namespace Ingredients.Web.Models.Database
{
    /// <summary>
    /// Database model for an 'ingredient' object
    /// </summary>
    public class Ingredient : IIngredient
    {
        /// <summary>
        /// Unique ingredient ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Human-readable ingredient name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Short ingredient description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Any tags / categorisation for this ingredient
        /// </summary>
        public string[] Tags { get; set; }

        public static Ingredient FromRow(Row row)
        {
            var ingr = new Ingredient();

            ingr.Id = row.GetValue<Guid>("id");
            ingr.Name = row.GetValue<string>("name");
            ingr.Description = row.GetValue<string>("description");
            ingr.Tags = row.GetValue<string[]>("tags");

            return ingr;
        }

        public static Ingredient FromRow(Guid id, Row row)
        {
            var ingr = new Ingredient();

            ingr.Id = id;
            ingr.Name = row.GetValue<string>("name");
            ingr.Description = row.GetValue<string>("description");
            ingr.Tags = row.GetValue<string[]>("tags");

            return ingr;
        }

        public static Ingredient FromIngredient(IIngredient entity)
        {
            return new Ingredient
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Tags = entity.Tags
            };
        }
    }
}