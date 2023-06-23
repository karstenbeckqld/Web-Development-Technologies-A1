using System.Collections.Generic;
using A1ClassLibrary.model;
using s3893749_s3912792_a1.project.model;

<<<<<<<< HEAD:s3893749-s3912792-a1/project/model/WebServiceObject.cs
namespace s3893749_s3912792_a1.project.model;

/* The WebServiceObject class serves as a DTO for data received from the web service via a HTTP Get request. */
public class WebServiceObject
========
namespace A1ClassLibrary.model;

/* The WebServiceDto class serves as a DTO for data received from the web service via a HTTP Get request. */
public class WebServiceDto
>>>>>>>> main:A1ClassLibrary/model/WebServiceDto.cs
{
    /* The CustomerId property receives the stored customer ID. */
    public int CustomerId { get; set; }
    
    /* The Name property receives the stored customer name. */
    public string Name { get; set; }
    
    /* The Address property receives the stored customer address. */
    public string Address { get; set; }
    
    /* The City property receives the stored customer city. */
    public string City { get; set; }
    
    /* The PostCode property receives the stored customer postcode. */
    public string PostCode { get; set; }
    
    /* The List<Account> property receives the stored customer accounts, including the transactions associated with them. */
    public List<Account> Accounts { get; set; }

    /* The Login property receives the stored customer login object. */
    public Login Login { get; set; }
}