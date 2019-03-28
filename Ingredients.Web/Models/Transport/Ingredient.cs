using System;
using System.Runtime.Serialization;

namespace Ingredients.Web.Models.Transport
{
    /// <summary>
    /// Transport model for the <see cref="Database.Ingredient"/> model
    /// </summary>
    [DataContract]
    public class Ingredient
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
        /// Convert a <see cref="Database.Ingredient"/> instance to a <see cref="Transport.Ingredient"/> model
        /// </summary>
        /// <param name="entity"><see cref="Database.Ingredient"/> to convert</param>
        /// <returns>Converted <see cref="Transport.Ingredient"/> model</returns>
        public static Ingredient FromDatabase(Models.Database.Ingredient entity)
        {
            var ingr = new Ingredient();

            ingr.Id = entity.Id;
            ingr.Name = entity.Name;
            ingr.Description = entity.Description;
            ingr.Tags = entity.Tags;

            return ingr;
        }
    }
}