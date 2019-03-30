namespace Ingredients.Cache.Framework.Serialisation
{
	/// <summary>
	/// Represents the ability to serialise and deserialise an object 
	/// </summary>
	public interface ISerialiser<T>
	{
		/// <summary>
		/// Serialises the given object model into a series of bytes
		/// </summary>
		/// <param name="objModel">The object to serialise</param>
		/// <returns>A set of bytes which represent the provided object</returns>
		byte[] Serialise(T objModel);

		/// <summary>
		/// Deserialises the given bytes into an object model of type <see cref="T"/>, if possible
		/// </summary>
		/// <param name="objBytes">The bytes to deserialise</param>
		/// <returns>
		/// The object model represented by the bytes, if deserialisation is possible;
		/// otherwise, the default value of the <see cref="T"/> type is returned
		/// </returns>
		T Deserialise(byte[] objBytes);
	}
}