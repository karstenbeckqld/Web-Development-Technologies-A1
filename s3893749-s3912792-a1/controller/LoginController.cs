using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using s3893749_s3912792_a1.builder;
using s3893749_s3912792_a1.interfaces;
using s3893749_s3912792_a1.model;

namespace s3893749_s3912792_a1.controller
{
    internal class LoginController
    {
        private IManager<Login> _loginManagerDataAccess;

        public LoginController(IManager<Login> loginManagerDataAccess)
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
