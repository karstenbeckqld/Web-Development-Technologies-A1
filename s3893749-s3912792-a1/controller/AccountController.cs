using s3893749_s3912792_a1.interfaces;
using s3893749_s3912792_a1.model;

namespace s3893749_s3912792_a1.controller;

public class AccountController
{
    private IManager<Account> _accountManagerDataAccess;

    public AccountController(IManager<Account> accountDataAccess)
    {
        _accountManagerDataAccess = accountDataAccess;
    }

    public List<Account> GetAccountDetails(int customerId)
    {
        return _accountManagerDataAccess.Get(customerId);
    }

    public List<Account> GetAllAccounts()
    {
        return _accountManagerDataAccess.GetAll();
    }

    public void InsertAccount(Account data)
    {
        _accountManagerDataAccess.Insert(data);
    }
}