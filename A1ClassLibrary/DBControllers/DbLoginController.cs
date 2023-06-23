using A1ClassLibrary.Interfaces;
using A1ClassLibrary.model;

namespace A1ClassLibrary.DBControllers
{
    internal class DbLoginController
    {
        private readonly IManager<Login> _loginManagerDataAccess;

        public DbLoginController(IManager<Login> loginManagerDataAccess)
        {
            _loginManagerDataAccess = loginManagerDataAccess;
        }

        public List<Login> GetLogin(int customerId)
        {
            // Returns all Login data for given customer ID (CustomerID, LoginID and PasswordHash)
            return _loginManagerDataAccess.Get(customerId);
        }

        public List<Login> GetAllLogins()
        {
            return _loginManagerDataAccess.GetAll();
        }

        public void InsertLogin(Login data)
        {
            _loginManagerDataAccess.Insert(data);
        }
    }
}
