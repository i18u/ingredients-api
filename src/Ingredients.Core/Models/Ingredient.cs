using System.Runtime.Serialization;
using MongoDB.Bson;

namespace Ingredients.Core.Models
{
	/// <summary>
	/// Ingredients API core model.
	/// </summary>
	public class Ingredient
	{
		/// <summary>
		/// Unique ingredient ID.
		/// </summary>
		[DataMember(Name = "_id")]
		public ObjectId Id { get; set; }

		/// <summary>
		/// Human-readable ingredient name.
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Short ingredient description.
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; }

		/// <summary>
		/// Any tags / categorisation for this ingredient.
		/// </summary>
		[DataMember(Name = "tags")]
		public string[] Tags { get; set; }

		/// <summary>
		/// The location of any image resources.
		/// </summary>
		[DataMember(Name = "img")]
		public string ImageLocation { get; set; }
	}
}