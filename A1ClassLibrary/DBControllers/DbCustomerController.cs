using System.Collections.Generic;
using A1ClassLibrary.Interfaces;
using A1ClassLibrary.model;

namespace A1ClassLibrary.DBControllers;

public class DbCustomerController
{
    private readonly IManager<Customer> _customerManagerObject;

    public DbCustomerController(IManager<Customer> customerObject)
    {
        _customerManagerObject = customerObject;
    }

    public List<Customer> GetCustomer(int customerId)
    {
        return _customerManagerObject.Get(customerId);
    }

    public List<Customer> GetAllCustomers()
    {
        return _customerManagerObject.GetAll();
    }

    public void InsertCustomer(Customer data)
    {
        _customerManagerObject.Insert(data);
    }
}