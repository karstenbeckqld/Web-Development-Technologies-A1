using A1ClassLibrary.Interfaces;
using A1ClassLibrary.model;

namespace A1ClassLibrary.DBControllers;

public class DbTransactionController
{
    private readonly IManager<Transaction> _transactionManagerDataAccess;

    public DbTransactionController(IManager<Transaction> transactionDataAccess)
    {
        _transactionManagerDataAccess = transactionDataAccess;
    }

    public List<Transaction> GetTransaction(int accountNumber)
    {
        return _transactionManagerDataAccess.Get(accountNumber);
    }

    public List<Transaction> GetAllTransactions()
    {
        return _transactionManagerDataAccess.GetAll();
    }

    public void InsertTransaction(Transaction data)
    {
        _transactionManagerDataAccess.Insert(data);
    }
}