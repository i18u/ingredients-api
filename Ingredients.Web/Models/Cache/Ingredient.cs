using System;
using Ingredients.Web.Cache;

namespace Ingredients.Web.Models.Cache
{
    /// <summary>
    /// Cache model for an 'ingredient' object
    /// </summary>
    public class Ingredient : IIngredient, ICacheModel<Database.Ingredient> 
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

        public void SetFieldsFromDatabaseModel(Database.Ingredient model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            Tags = model.Tags;
        }

        public Database.Ingredient ToDatabaseModel()
        {
            return new Database.Ingredient
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Tags = Tags
            };
        }
    }
}