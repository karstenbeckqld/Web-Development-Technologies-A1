using A1ClassLibrary.Interfaces;
using A1ClassLibrary.model;

namespace A1ClassLibrary.DBControllers;

public class DbAccountController
{
    private IManager<Account> _accountManagerDataAccess;

    internal DbAccountController(IManager<Account> accountDataAccess)
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