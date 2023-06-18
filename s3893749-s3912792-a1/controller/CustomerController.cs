using s3893749_s3912792_a1.builder;
using s3893749_s3912792_a1.interfaces;
using s3893749_s3912792_a1.model;

namespace s3893749_s3912792_a1.controller;

public class CustomerController
{
    private IManager<Customer> _customerManagerDataAccess;

    public CustomerController(IManager<Customer> customerDataAccess)
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