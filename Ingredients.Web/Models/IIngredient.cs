using System;

namespace Ingredients.Web.Models
{
	public interface IIngredient
	{
		/// <summary>
		/// Unique ingredient ID
		/// </summary>
		Guid Id { get; set; }

		/// <summary>
		/// Human-readable ingredient name
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Short ingredient description
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Any tags / categorisation for this ingredient
		/// </summary>
		string[] Tags { get; set; }
	}
}