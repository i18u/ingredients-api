namespace Ingredients.Cache.Framework.Layers
{
	/// <summary>
	/// Represents a single layer in a multi-layer cache where objects of the given type can be stored and retrieved
	/// </summary>
	/// <typeparam name="T">The object type which this cache layer handles</typeparam>
	public interface ICacheLayer<T>
	{
	}
}