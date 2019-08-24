using DatabaseIngredient = Ingredients.Web.Models.Database.Ingredient;
using TransportIngredient = Ingredients.Web.Models.Transport.Ingredient;
using NUnit.Framework;
using System;
using MongoDB.Bson;

namespace Tests
{
	[TestFixture]
	public class IngredientConversion
	{
		private const string TestName = "TestName";
		private const string TestDescription = "TestDescription";
		private static readonly string[] TestTags = new string[] { "TestTag1", "TestTag2", "TestTag3" };

		[Test]
		public void AssertThat_TransportFromDatabase_KeepsData() {

			var ingredientId = ObjectId.GenerateNewId(0);

			DatabaseIngredient dbIngredient = new DatabaseIngredient {
				Id = ingredientId,
				Name = TestName,
				Description = TestDescription,
				Tags = TestTags
			};

			TransportIngredient tpIngredient = TransportIngredient.FromDatabase(dbIngredient);

			Assert.AreEqual(ingredientId, tpIngredient.Id, "Transport 'Id' does not match Database 'Id'");
			Assert.AreEqual(TestName, tpIngredient.Name, "Transport 'Name' does not match Database 'Name'");
			Assert.AreEqual(TestDescription, tpIngredient.Description, "Transport 'Description' does not match Database 'Description'");
			Assert.AreEqual(TestTags, tpIngredient.Tags, "Transport 'Tags' does not match Database 'Tags'");
		}
	}
}
