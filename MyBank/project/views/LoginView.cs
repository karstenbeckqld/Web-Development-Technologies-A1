﻿using MyBank.framework.components;
using MyBank.framework.core;

namespace MyBank.project.views;

public class LoginView : View
{

    public LoginView()
    {
        var loginFailedMessage = new Message();
        loginFailedMessage.SetColor(ConsoleColor.Red);
        loginFailedMessage.SetVariableKey("LoginFailed");
        
        AddComponent(loginFailedMessage);
        
        var logoutMessage = new Message();
        logoutMessage.SetColor(ConsoleColor.Green);
        logoutMessage.SetVariableKey("LogoutSuccess");
        
        AddComponent(logoutMessage);
        
        var form = new Form();
        
        form.SetController("LoginController");
        
        form.AddInput(Form.Integer,"Login ID",null,"char_min:8,char_max:8");
        form.AddInput(Form.Text,"Password",null,"char_min:2,char_max:20",true);
        
        AddComponent(form);
    }
    
}