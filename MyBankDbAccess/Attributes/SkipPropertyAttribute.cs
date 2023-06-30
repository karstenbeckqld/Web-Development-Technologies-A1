namespace MyBankDbAccess.attributes;

// The SkipPropertyAttribute class extends the Attribute class and marks properties tha are not present in their
// respective database tables. The generic Database access that we implemented (ORM) is based on the properties of the
// model classes. Because not all properties of all models are present in the database table for this class (eg.
// List<Transaction> in Account), this class stub gets used to mark these properties, so that the query doesn't produce
// SQL exceptions. 
public class SkipPropertyAttribute:Attribute
{
}