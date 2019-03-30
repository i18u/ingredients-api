using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ingredients.Cache.Framework.Layers;

namespace Ingredients.Cache.Framework.Managers
{
	/// <inheritdoc />
	/// <summary>
	/// Provides the base implementation for a cache with multiple layers
	/// </summary>
	/// <typeparam name="TLayer">The type of layer that this cache accepts</typeparam>
	/// <typeparam name="T">The object type which this cache handles</typeparam>
	public abstract class CacheManager<TLayer, T> : ICacheManager<T> 
		where TLayer : ICacheLayer<T>
	{
		private readonly List<TLayer> _layers = new List<TLayer>();
		
		internal void AddLayer(TLayer layer)
		{
			_layers.Add(layer);
		}

		protected ReadOnlyCollection<TLayer> Layers => new ReadOnlyCollection<TLayer>(_layers);
		protected virtual TimeSpan? DefaultExpiry => null;
	}
}