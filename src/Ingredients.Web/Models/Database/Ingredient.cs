using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ingredients.Web.Models.Database
{
	/// <summary>
	/// Database model for an 'ingredient' object
	/// </summary>
	public class Ingredient : IMongoModel
	{
		/// <summary>
		/// Unique ingredient ID
		/// </summary>
		[BsonId]
		[BsonElement("_id")]
		public ObjectId Id { get; set; }

		/// <summary>
		/// Human-readable ingredient name
		/// </summary>
		[BsonElement("name")]
		public string Name { get; set; }

		/// <summary>
		/// Short ingredient description
		/// </summary>
		[BsonElement("description")]
		public string Description { get; set; }

		/// <summary>
		/// Any tags / categorisation for this ingredient
		/// </summary>
		[BsonElement("tags")]
		public string[] Tags { get; set; }

		/// <summary>
		/// The location of any image resources.
		/// </summary>
		public string ImageLocation { get; set; }

        public static Ingredient FromTransport(Models.Transport.Ingredient entity)
		{
			return new Ingredient
			{
				Id = entity.Id ?? ObjectId.GenerateNewId(DateTime.UtcNow),
				Name = entity.Name,
				Description = entity.Description,
				Tags = entity.Tags,
				ImageLocation = entity.ImageLocation
			};
		}
	}
}
