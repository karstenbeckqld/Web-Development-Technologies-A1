using A1ClassLibrary.Interfaces;
using A1ClassLibrary.model;

namespace A1ClassLibrary.DBControllers;

public class DbTransactionController
{
    private readonly IManager<Transaction> _transactionManagerObject;

    public DbTransactionController(IManager<Transaction> transactionObject)
    {
        _transactionManagerObject = transactionObject;
    }

    public List<Transaction> GetTransactions(int accountNumber)
    {
        return _transactionManagerObject.Get(accountNumber);
    }

    public List<Transaction> GetAllTransactions()
    {
        return _transactionManagerObject.GetAll();
    }

    public void InsertTransaction(Transaction data)
    {
        _transactionManagerObject.Insert(data);
    }
}