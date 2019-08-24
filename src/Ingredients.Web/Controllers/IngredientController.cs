using System;
using System.Linq;
using System.Net;
using Ingredients.Web.Helpers;
using Ingredients.Web.Models.Transport;
using Ingredients.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ingredients.Web.Controllers
{
	[Route("/api/[controller]")]
	public class IngredientController : ControllerBase
	{
		[HttpPost]
		public ApiResponse<Guid> Create([FromBody] Ingredient ingredient)
		{
			Console.WriteLine($"Inserting new ingredient: {ingredient.Name}");

			var ingredientRepo = new IngredientRepository(MongoManager.Connection);
			var createdGuid = ingredientRepo.Upsert(Models.Database.Ingredient.FromTransport(ingredient));

			Console.WriteLine($"Successfully created item with {createdGuid}");

			return ApiResponse<Guid>.WithStatus(HttpStatusCode.OK)
				.WithData(createdGuid);
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
		public ApiResponse<Ingredient> Get(Guid id)
		{
			Console.WriteLine($"Getting ingredient with id {id}");

			var ingredientRepo = new IngredientRepository(MongoManager.Connection);
			var item = Ingredient.FromDatabase(ingredientRepo.Get(id));

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
