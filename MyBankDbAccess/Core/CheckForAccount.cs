using MyBankDbAccess.Models;

namespace MyBankDbAccess.Core;

// When working with accounts, we need to check if the account really exists. Therefore, the CheckForAccount class
// provides a method to do this. The static method AccountCheck will return false if the entered account number is not
// in the database. 
public static class CheckForAccount
{
    public static bool AccountCheck(Account account)
    {
        var result = false;
        
        var availableAccounts = new Database<Account>().GetAll().GetResult();

        foreach (var item in availableAccounts.Where(item => item.AccountNumber == account.AccountNumber))
        {
            result= true;
        }

        return result;
    } 
}