using System;
using System.Linq;
using System.Net;
using Ingredients.Core.Models;
using Ingredients.Web.Helpers;
using Ingredients.Web.Models.Transport;
using Ingredients.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Ingredients.Web.Controllers
{
	/// <summary>
	/// API controller to interact with <see cref="Ingredient"/> objects.
	/// </summary>
	[Route("/api/[controller]")]
	public class IngredientController : ControllerBase
	{
		/// <summary>
		/// Create a new <see cref="Ingredient"/> object, specified as a JSON body.
		/// </summary>
		/// <param name="ingredient"><see cref="Ingredient"/> to create.</param>
		/// <returns><see cref="ApiResponse{ObjectId}"/> object with generated <see cref="ObjectId"/>.</returns>
		[HttpPost]
		public ApiResponse<ObjectId> Create([FromBody] Ingredient ingredient)
		{
			if (ingredient == null)
			{
				Console.WriteLine("Error with ingredient object");
				return ApiResponse<ObjectId>.WithStatus(400).WithMessage("Error creating new ingredient.");
			}

			Console.WriteLine($"Inserting new ingredient: {ingredient.Name}");

			var ingredientRepo = new IngredientRepository(MongoManager.Connection);
			var createdId = ingredientRepo.Upsert(ingredient);

			Console.WriteLine($"Successfully created item with {createdId}");

			return ApiResponse<ObjectId>.WithStatus(HttpStatusCode.OK)
				.WithData(createdId);
		}

		/// <summary>
		/// Get a collection of <see cref="Ingredient"/> objects based on the pagination parameters provided.
		/// </summary>
		/// <param name="page">Page number.</param>
		/// <param name="limit">Number of results to return.</param>
		/// <returns>Collection of <see cref="Ingredient"/> objects.</returns>
		[HttpGet]
		public ApiResponse<Ingredient[]> Get([FromQuery] int page, [FromQuery] int limit)
		{
			var results = new Ingredient[] { };

			// These checks are implemented lower down, however let's check them here as well
			if(page <= 0 || limit <= 0 || limit > 100)
			{
				return ApiResponse<Ingredient[]>.WithStatus(HttpStatusCode.BadRequest).WithData(results);
			}

			var ingredientRepo = new IngredientRepository(MongoManager.Connection);

			results =
				ingredientRepo
					.Get(page, limit)
					.Select(Ingredient.FromDatabase)
					.ToArray();

			return ApiResponse<Ingredient[]>.WithStatus(HttpStatusCode.OK).WithData(results);
		}

		/// <summary>
		/// Get a single <see cref="Ingredient"/> object by unique identifier.
		/// </summary>
		/// <param name="id">Unique <see cref="Ingredient"/> identifier as a <see langword="string"/>.</param>
		/// <returns>Single <see cref="Ingredient"/> object with specified identifier.</returns>
		[HttpGet("{id}")]
		public ApiResponse<Ingredient> Get(string id)
		{
			Console.WriteLine($"Getting ingredient with id {id}");

			if (!ObjectId.TryParse(id, out var objectId))
			{
                return ApiResponse<Ingredient>.WithStatus(HttpStatusCode.BadRequest).WithMessage("Incorrectly formatted ingredient ID provided.");
            }

			var ingredientRepo = new IngredientRepository(MongoManager.Connection);
			var item = Ingredient.FromDatabase(ingredientRepo.Get(objectId));

			if (item == null)
			{
				return ApiResponse<Ingredient>.WithStatus(HttpStatusCode.BadRequest);
			}

			return ApiResponse<Ingredient>.WithStatus(HttpStatusCode.OK).WithData(item);
		}

		/// <summary>
		/// Return the <see cref="Manifest"/> object for this API.
		/// </summary>
		/// <returns><see cref="Manifest"/> with API information.</returns>
		[HttpGet("/info")]
		public Manifest Info()
		{
			return Api.GetInformation();
		}
	}
}
