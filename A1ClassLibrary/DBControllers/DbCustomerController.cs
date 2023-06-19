using A1ClassLibrary.Interfaces;
using A1ClassLibrary.model;

namespace A1ClassLibrary.DBControllers;

public class DbCustomerController
{
    private IManager<Customer> _customerManagerDataAccess;

    public DbCustomerController(IManager<Customer> customerDataAccess)
    {
        _customerManagerDataAccess = customerDataAccess;
    }

    public List<Customer> GetCustomer(int customerId)
    {
        return _customerManagerDataAccess.Get(customerId);
    }

    public List<Customer> GetAllCustomers()
    {
        return _customerManagerDataAccess.GetAll();
    }

    public void InsertCustomer(Customer data)
    {
        _customerManagerDataAccess.Insert(data);
    }
}