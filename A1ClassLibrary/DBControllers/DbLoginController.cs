using A1ClassLibrary.Interfaces;
using A1ClassLibrary.model;

namespace A1ClassLibrary.DBControllers
{
    internal class DbLoginController
    {
        private readonly IManager<Login> _loginManagerObject;

        public DbLoginController(IManager<Login> loginManagerObject)
        {
            _loginManagerObject = loginManagerObject;
        }

        public List<Login> GetLogin(int customerId)
        {
            // Returns all Login data for given customer ID (CustomerID, LoginID and PasswordHash)
            return _loginManagerObject.Get(customerId);
        }

        public List<Login> GetAllLogins()
        {
            return _loginManagerObject.GetAll();
        }

        public void InsertLogin(Login data)
        {
            _loginManagerObject.Insert(data);
        }
    }
}
