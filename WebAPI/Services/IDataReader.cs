namespace WebAPI.Services
{
    public interface IDataReader
    {
        public string GetJsonData(string product);

        //private string GetImageUrl(int productId);

        public string GetImagePath(string productCategory, string productId);
    }
}
