using EasyDB.core;
using MyBank.project.models;
using Transaction = MyBank.project.models.Transaction;

namespace MyBank.project.businessLogic;

public class TransactionService
{

    public TransactionService()
    {
        
    }

    public List<Account> GetAccountDetails(int accountNumber)
    {
        return new Database<Account>().Where("AccountNumber", accountNumber.ToString()).GetFirst();
    }

    public List<Transaction> GetAccountTransactions(int accountNumber)
    {
        return new Database<Transaction>().Where("AccountNumber", accountNumber.ToString()).GetAll();
    }
}