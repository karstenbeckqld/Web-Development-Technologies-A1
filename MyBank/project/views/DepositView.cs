using MyBank.framework.components;
using MyBank.framework.core;

namespace MyBank.project.views;

public class DepositView : View
{
    public DepositView()
    {
        var getDepositAmountFromUser = new Form();
        
        getDepositAmountFromUser.AddInput(Form.Float,"Amount","Please enter the amount you would like to deposit too.","min:0.01");
        getDepositAmountFromUser.AddInput(Form.Text,"Comment","If you would like to enter a comment please enter it now\nor simple press enter to skip",null);
        
        getDepositAmountFromUser.SetController("DepositController");
        
        AddComponent(getDepositAmountFromUser);    
    }
}