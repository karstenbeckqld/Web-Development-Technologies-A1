using MyBankDbAccess.Models;
using MyBankDbAccess.Core;

namespace MyBankDbAccess.businessLogic;

public class TransactionService
{

    public TransactionService()
    {
        
    }

    public List<Account> GetAccountDetails(int accountNumber)
    {
        return new Database<Account>().GetAll().Where("AccountNumber", accountNumber.ToString()).GetResult();
    }

    public List<Transaction> GetAccountTransactions(int accountNumber)
    {
        return new Database<Transaction>().GetAll().Where("AccountNumber", accountNumber.ToString()).GetResult();
    }
}