using System;
using System.Linq;
using System.Net;
using Ingredients.Web;
using Ingredients.Web.Models;
using Ingredients.Web.Models.Transport;
using Ingredients.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ingredients.Web.Controllers
{
    [Route("/api/[controller]")]
    public class IngredientController : ControllerBase
    {
        [HttpPost]
        public ApiResponse<Models.Transport.Ingredient> Create([FromBody] Models.Transport.Ingredient ingredient)
        {
            Console.WriteLine($"Inserting new ingredient: {ingredient.Name}");

            var ingredientRepo = new IngredientRepository(CassandraDatabase.GetSession());
            var createdItem = ingredientRepo.UpsertOne(Models.Database.Ingredient.FromTransport(ingredient));

            Console.WriteLine($"Successfully created item with {createdItem.Id}");

            return ApiResponse<Models.Transport.Ingredient>.WithStatus(HttpStatusCode.OK)
                .WithData(Models.Transport.Ingredient.FromDatabase(createdItem));
        }

        [HttpGet]
        public ApiResponse<Models.Transport.Ingredient[]> Get([FromQuery] int page, [FromQuery] int limit)
        {
            var results = new Models.Transport.Ingredient[] { };

            // These checks are implemented lower down, however let's check them here as well
            if(page <= 0 || limit <= 0 || limit > 100)
            {
                return ApiResponse<Models.Transport.Ingredient[]>.WithStatus(HttpStatusCode.BadRequest).WithData(results);
            }

            var ingredientRepo = new IngredientRepository(CassandraDatabase.GetSession());

            results =
                ingredientRepo
                    .Get(page, limit)
                    .Select(Models.Transport.Ingredient.FromDatabase)
                    .ToArray();

            return ApiResponse<Models.Transport.Ingredient[]>.WithStatus(HttpStatusCode.OK).WithData(results);
        }

        [HttpGet("{id}")]
        public ApiResponse<Models.Transport.Ingredient> Get(Guid id)
        {
            Console.WriteLine($"Getting ingredient with id {id}");

            var ingredientRepo = new IngredientRepository(CassandraDatabase.GetSession());
            var item = Models.Transport.Ingredient.FromDatabase(ingredientRepo.GetOne(id));

            if (item == null)
            {
                return ApiResponse<Models.Transport.Ingredient>.WithStatus(HttpStatusCode.BadRequest);
            }

            return ApiResponse<Models.Transport.Ingredient>.WithStatus(HttpStatusCode.OK).WithData(item);
        }

        [HttpGet("/info")]
        public Manifest Info()
        {
            return Api.GetInformation();
        }
    }
}