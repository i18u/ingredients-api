using System;

namespace Ingredients.Web.Models.Database
{
	/// <summary>
	/// Interface defining an object that can be uniquely represented by an <see cref="Guid"/>
	/// </summary>
	public interface IMongoModel
	{
		/// <summary>
		/// Unique <see cref="Guid"/> identifier for this <see cref="IMongoModel"/>
		/// </summary>
		Guid Id { get; }
	}
}
