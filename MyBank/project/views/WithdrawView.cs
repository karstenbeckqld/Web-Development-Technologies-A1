using MyBank.framework.components;
using MyBank.framework.core;

namespace MyBank.project.views;

public class WithdrawView : View
{
    public WithdrawView()
    {
        var getDepositAmountFromUser = new Form();
        
        getDepositAmountFromUser.AddInput(Form.Float,"Amount","Please enter the amount you would like to withdraw.","min:1");
        getDepositAmountFromUser.AddInput(Form.Text,"Comment","If you would like to enter a comment please enter it now\nor simple press enter to skip",null);
        
        getDepositAmountFromUser.SetController("WithdrawController");
        
        AddComponent(getDepositAmountFromUser);    
    }
}