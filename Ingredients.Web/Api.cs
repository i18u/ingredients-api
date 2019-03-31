namespace Ingredients.Web
{
    public static class Api 
    {
        public static Manifest GetInformation()
        {
            return Manifest.Instance;
        }
    }
}