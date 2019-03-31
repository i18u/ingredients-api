using System;
using NUnit.Framework;

namespace Ingredients.Test.Models
{
    [TestFixture]
    public class IngredientConversion
    {
        private const string TestName = "TestName";
        private const string TestDescription = "TestDescription";
        private static readonly string[] TestTags = { "TestTag1", "TestTag2", "TestTag3" };

        [Test]
        public void AssertThat_TransportFromDatabase_KeepsData() {

            var ingredientId = Guid.NewGuid();

            var dbIngredient = new Web.Models.Database.Ingredient 
            {
                Id = ingredientId,
                Name = TestName,
                Description = TestDescription,
                Tags = TestTags
            };

            var tpIngredient = Web.Models.Transport.Ingredient.FromIngredient(dbIngredient);

            Assert.AreEqual(ingredientId, tpIngredient.Id, "Transport 'Id' does not match Database 'Id'");
            Assert.AreEqual(TestName, tpIngredient.Name, "Transport 'Name' does not match Database 'Name'");
            Assert.AreEqual(TestDescription, tpIngredient.Description, "Transport 'Description' does not match Database 'Description'");
            Assert.AreEqual(TestTags, tpIngredient.Tags, "Transport 'Tags' does not match Database 'Tags'");
        }
    }
}