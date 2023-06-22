using A1ClassLibrary.model;

namespace A1ClassLibrary.BusinessLogic;

public class TransactionService
{
    
    private AccountManager AccountManager { get; }
    private TransactionManager TransactionManager { get; }
    public TransactionService(AccountManager accountManager, TransactionManager transactionManager)
    {
        AccountManager = accountManager;
        TransactionManager = transactionManager;
    }

    public List<Account> GetAccountDetails(int accountNumber)
    {
        return AccountManager.Get(accountNumber);
    }

    public List<Transaction> GetAccountTransactions(int accountNumber)
    {
        return TransactionManager.Get(accountNumber);
    }
}