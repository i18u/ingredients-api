namespace Ingredients.Web
{
    /// <summary>
    /// Represents meta information regarding the API
    /// </summary>
    public static class Api 
    {
        /// <summary>
        /// Retrieves all information regarding the current API in manifest format
        /// </summary>
        /// <returns>The <see cref="Manifest"/> object, representing current API information</returns>
        public static Manifest GetInformation()
        {
            return Manifest.Instance;
        }
    }
}