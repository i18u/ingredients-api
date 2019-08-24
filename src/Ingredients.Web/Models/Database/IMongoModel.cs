using MongoDB.Bson;

namespace Ingredients.Web.Models.Database
{
	/// <summary>
	/// Interface defining an object that can be uniquely represented by an <see cref="ObjectId"/>
	/// </summary>
	public interface IMongoModel
	{
		/// <summary>
		/// Unique <see cref="ObjectId"/> identifier for this <see cref="IMongoModel"/>
		/// </summary>
		ObjectId Id { get; }
	}
}
