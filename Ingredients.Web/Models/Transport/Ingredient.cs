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
        [DataMember(Name = "id")]
        public Guid Id { get; set; }


        [DataMember(Name = "name")]
        public string Name { get; set; }


        [DataMember(Name = "description")]
        public string Description { get; set; }


        [DataMember(Name = "tags")]
        public string[] Tags { get; set; }

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