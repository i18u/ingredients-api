using System;
using System.Linq;
using System.Net;
using Ingredients.Web.Helpers;
using Ingredients.Web.Models.Transport;
using Ingredients.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Ingredients.Web.Controllers
{
	[Route("/api/[controller]")]
	public class IngredientController : ControllerBase
	{
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
			var createdId = ingredientRepo.Upsert(Models.Database.Ingredient.FromTransport(ingredient));

			Console.WriteLine($"Successfully created item with {createdId}");

			return ApiResponse<ObjectId>.WithStatus(HttpStatusCode.OK)
				.WithData(createdId);
		}

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

		[HttpGet("/info")]
		public Manifest Info()
		{
			return Api.GetInformation();
		}
	}
}
