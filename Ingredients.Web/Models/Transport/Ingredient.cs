using System;
using System.Runtime.Serialization;

namespace Ingredients.Web.Models.Transport
{
    /// <summary>
    /// Transport model for the <see cref="Database.Ingredient"/> model
    /// </summary>
    [DataContract]
    public class Ingredient : IIngredient
    {
        /// <summary>
        /// Unique ingredient ID
        /// </summary>
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Human-readable ingredient name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Short ingredient description
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Any tags / categorisation for this ingredient
        /// </summary>
        [DataMember(Name = "tags")]
        public string[] Tags { get; set; }

        /// <summary>
        /// Convert a <see cref="IIngredient"/> instance to a <see cref="Transport.Ingredient"/> model
        /// </summary>
        /// <param name="entity"><see cref="IIngredient"/> to convert</param>
        /// <returns>Converted <see cref="Transport.Ingredient"/> model</returns>
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