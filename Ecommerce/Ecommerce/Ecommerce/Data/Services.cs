namespace Ecommerce.Data
{
    public class Services
    {
        private readonly IConfiguration _config;
        public Services(IConfiguration config)
        {
            _config = config;
        }

        public bool IsLoginAsync(string userid, string password)
        {
            bool islogin = false;
            return islogin;
        }
    }
}
