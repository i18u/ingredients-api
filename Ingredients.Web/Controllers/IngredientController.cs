using Ingredients.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ingredients.Web.Controllers 
{
    [Route("/api/[controller]")]
    public class IngredientController : ControllerBase 
    {
        [HttpPost("create")]
        public void Create(Ingredient ingredient)
        {

        }
    }
}