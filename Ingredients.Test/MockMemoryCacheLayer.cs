using System;
using Ingredients.Cache.Framework.Layers.Basic;

namespace Ingredients.Test
{
	public class MockMemoryCacheLayer<T> : MemoryCacheLayer<T>
	{
		internal EventHandler<EventArgs> LayerGetCalled;
		internal EventHandler<EventArgs> LayerSetCalled;
		internal EventHandler<EventArgs> LayerRemoveCalled;
		
		public override bool TryGet(string key, out T value)
		{
			LayerGetCalled?.Invoke(this, EventArgs.Empty);
			return base.TryGet(key, out value);
		}

		public override bool TrySet(string key, T value, TimeSpan? expiry = null)
		{
			LayerSetCalled?.Invoke(this, EventArgs.Empty);
			return base.TrySet(key, value, expiry);
		}

		public override bool TryRemove(string key)
		{
			LayerRemoveCalled?.Invoke(this, EventArgs.Empty);
			return base.TryRemove(key);
		}
	}
}