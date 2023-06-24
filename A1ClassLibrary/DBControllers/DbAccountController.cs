using System.Collections.Generic;
using A1ClassLibrary.Interfaces;
using A1ClassLibrary.model;

namespace A1ClassLibrary.DBControllers;

public class DbAccountController
{
    private readonly IManager<Account> _accountManagerObject;

    public DbAccountController(IManager<Account> accountObject)
    {
        _accountManagerObject = accountObject;
    }

    public List<Account> GetAccountDetails(int accountNumber)
    {
        return _accountManagerObject.Get(accountNumber);
    }

    public List<Account> GetAllAccounts()
    {
        return _accountManagerObject.GetAll();
    }

    public void InsertAccount(Account data)
    {
        _accountManagerObject.Insert(data);
    }

    public void UpdateAccount(Account data)
    {
        _accountManagerObject.Update(data);
    }
}