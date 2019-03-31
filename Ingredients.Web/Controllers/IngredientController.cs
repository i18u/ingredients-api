using System;
using System.Net;
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

        [HttpGet]
        public Manifest Info() 
        {
            return Api.GetInformation();
        }
    }
}