using MyBank.framework.components;
using MyBank.framework.core;
using MyBank.framework.views.interfaces;
using MyBankDbAccess.Models;

namespace MyBank.project.views;

public class MyStatementsView : View, IConstructAfterLogin
{

    public void Construct()
    {
        
        var heading = new Message();
        
        heading.SetVariableKey("Heading");
        heading.SetColor(ConsoleColor.Yellow);
        AddComponent(heading);

        var account = new Message();
        
        account.SetVariableKey("Account");
        account.SetColor(ConsoleColor.DarkYellow);
        AddComponent(account);

        
        var savings = new Table<Transaction>();
        
        savings.SetVariableKey("SavingsTransactions");
        savings.SetPaginateLimit(4);

        AddComponent(savings);
        
        var credit = new Table<Transaction>();
        
        credit.SetVariableKey("CreditTransactions");
        credit.SetPaginateLimit(4);

        AddComponent(credit);

    }
}