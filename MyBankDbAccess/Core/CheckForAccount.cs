using MyBankDbAccess.Models;

namespace MyBankDbAccess.Core;

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