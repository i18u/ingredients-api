using System;
using System.Net;
using System.Threading.Tasks;
using Ingredients.Web.Cache;
using Ingredients.Web.Models.Transport;
using Ingredients.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ingredients.Web.Controllers 
{
    [Route("/api/[controller]")]
    public class IngredientController : ControllerBase 
    {
        [HttpPost]
        public async Task<ApiResponse<Ingredient>> Create([FromBody] Ingredient ingredient)
        {
            Console.WriteLine($"Inserting new ingredient: {ingredient.Name}");

            var ingredientRepo = new IngredientRepository(CassandraDatabase.GetSession());
            var ingredientCache = new IngredientCache(ingredientRepo);
            var createdItem = await ingredientCache.UpsertOneAsync(Models.Database.Ingredient.FromIngredient(ingredient));

            Console.WriteLine($"Successfully created item with {createdItem.Id}");

            return ApiResponse<Ingredient>.WithStatus(HttpStatusCode.OK)
                .WithData(Ingredient.FromIngredient(createdItem));
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<Ingredient>> Get(Guid id)
        {
            Console.WriteLine($"Getting ingredient with id {id}");

            var ingredientRepo = new IngredientRepository(CassandraDatabase.GetSession());
            var ingredientCache = new IngredientCache(ingredientRepo);
            var item = Ingredient.FromIngredient(await ingredientCache.GetOneAsync(id));

            if (item == null) 
            {
                return ApiResponse<Ingredient>.WithStatus(HttpStatusCode.BadRequest);
            }

            return ApiResponse<Ingredient>.WithStatus(HttpStatusCode.OK).WithData(item);
        }

        [HttpGet]
        public async Task<string> Info() 
        {
            return await Task.FromResult("{ \"version\": \"test\" }");
        }
    }
}