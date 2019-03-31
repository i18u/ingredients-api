namespace Ingredients.Web.Cache
{
	public interface ICacheModel<TDatabaseModel>
	{
		void SetFieldsFromDatabaseModel(TDatabaseModel model);

		TDatabaseModel ToDatabaseModel();
	}
}