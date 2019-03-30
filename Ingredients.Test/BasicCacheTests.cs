using System.Collections.Generic;
using Ingredients.Test.Mocks;
using NUnit.Framework;

namespace Ingredients.Test
{
	public class BasicCacheTests
	{
		[Test]
		public void Get_ShouldReturnDefault_NonExistentKey()
		{
			const string key = "TEST1";

			var cache = GetStringCache();
			var returnValue = cache.Get(key);
			Assert.AreEqual(default(string), returnValue);
		}

		[Test]
		public void Get_ShouldCallDbGet_NonExistentKey()
		{
			const string key = "TEST1";
			
			var cache = GetStringCache();
			
			var dbGetCalls = 0;
			cache.DbGetCalled += (sender, args) => { dbGetCalls++; };
			
			cache.Get(key);
			Assert.AreEqual(1, dbGetCalls);
		}
		
		[Test]
		public void Set_ShouldPersistValue_String()
		{
			const string key = "TEST1";
			const string value = "MyValue";
			
			var cache = GetStringCache();
			cache.Set(key, value);

			var returnValue = cache.Get(key);
			Assert.AreEqual(value, returnValue);
		}

		[Test]
		public void Remove_ShouldRemoveKeyValue()
		{
			const string key = "TEST1";
			const string value = "MyValue";
			
			var cache = GetStringCache();
			cache.Set(key, value);

			cache.Remove(key);
			
			var returnValue = cache.Get(key);
			Assert.AreEqual(default(string), returnValue);
		}

		[Test]
		public void Get_ShouldCallLayerGet_OncePerLayer()
		{
			const string key = "TEST1";

			var cache = GetStringCache();
			var layerGetCalls = 0;
			cache.LayerGetCalled += (sender, args) => { layerGetCalls++; };
			
			cache.Get(key);
			
			Assert.AreEqual(1, layerGetCalls);
		}
		
		[Test]
		public void Set_ShouldCallLayerSet_OncePerLayer()
		{
			const string key = "TEST1";
			const string value = "MyValue";

			var cache = GetStringCache();
			var layerSetCalls = 0;
			cache.LayerSetCalled += (sender, args) => { layerSetCalls++; };
			
			cache.Set(key, value);
			
			Assert.AreEqual(1, layerSetCalls);
		}
		
		[Test]
		public void Remove_ShouldCallLayerRemove_OncePerLayer()
		{
			const string key = "TEST1";
			
			var cache = GetStringCache();
			var layerRemoveCalls = 0;
			cache.LayerRemoveCalled += (sender, args) => { layerRemoveCalls++; };

			cache.Remove(key);
			
			Assert.AreEqual(1, layerRemoveCalls);
		}
		
		private static MockBasicCacheManager<string> GetStringCache()
		{
			return new MockBasicCacheManager<string>(new Dictionary<string, string>
			{
				{"TEST-STRING", "MyValue"},
				{"TEST-STRING2", "AnotherValue"}
			});
		}
	}
}