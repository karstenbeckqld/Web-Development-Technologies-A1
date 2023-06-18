using s3893749_s3912792_a1.interfaces;
using s3893749_s3912792_a1.model;
using Transaction = System.Transactions.Transaction;

namespace s3893749_s3912792_a1.controller;

public class TransactionController
{
    private IManager<Transaction> _transactionManagerDataAccess;

    public TransactionController(IManager<Transaction> transactionDataAccess)
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