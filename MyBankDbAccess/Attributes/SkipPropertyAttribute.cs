namespace MyBankDbAccess.attributes;

// The SkipPropertyAttribute class stores the attributes that must be omitted when accessing tables from the database.
// This got implemented because some types contain properties that are not present in their respective database table.
// Because we have implemented a generic database access, or ORM, we need to dynamically hide these properties to avoid
// SQL errors for not-present fields. 
public class SkipPropertyAttribute:Attribute
{
}