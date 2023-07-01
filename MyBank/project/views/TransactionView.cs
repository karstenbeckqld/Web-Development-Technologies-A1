using MyBank.framework.components;
using MyBank.framework.core;

namespace MyBank.project.views;

public class TransactionView: View
{

    public TransactionView()
    {

        var form = new Form();
        
        form.AddInput(Form.Float,"Amount","Please enter the amount you would like to transfer","min:1");
        form.AddInput(Form.Text,"Comment","If you would like to enter a comment please enter it now\nor simple press enter to skip",null);
        form.AddInput(Form.Integer,"Destination Account","Please enter the destination account you wish to transfer to.","char_min:4,char_max:4");
        
        form.SetController("TransactionController");
        
        AddComponent(form);
    }
    
    
}