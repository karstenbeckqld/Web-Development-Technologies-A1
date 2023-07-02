using System.Globalization;
using MyBank.framework.facades;

namespace MyBank.framework.components.form;

public class InputField
{

    private string _prompt;
    private int _type;
    private Dictionary<string, string> _constraints;
    private bool _readLineHidden;
    private bool _freeForm;
    
    public InputField(int type, string promptInput, string constraints, bool hideInput)
    {
        _prompt = promptInput;
        _readLineHidden = hideInput;
        _constraints = new Dictionary<string, string>();
        _type = type;

        if (constraints != null)
        {
            _freeForm = false;
            foreach (var entry in constraints.Split(","))
            {
                string[] values = entry.Split(":");
                _constraints.Add(values[0], values[1]);
            }
        }
        else
        {
            _freeForm = true;
        }
    }

    public string ReadLine()
    {
        if (_prompt != null)
        {
            Console.WriteLine(_prompt);
        }
        bool valid = false;
        string input = "";

        while(!valid)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (!_readLineHidden)
            {
                input = Console.ReadLine();
            }
            else
            {
                input = ReadLineHidden();
            }

            if (input.Equals("/back"))
            {
                App.BackToPreviousView();
            }

            if (!_freeForm)
            {

                if (_type == Form.Integer)
                {
                    if (!IsInteger(input))
                    {
                        errors.Add("Integer", "Input must be a integer only");
                    }
                    else
                    {

                        foreach (var constraint in _constraints)
                        {

                            switch (constraint.Key)
                            {

                                //-----------------------------------------------\\
                                case "char_min":

                                    if (!ValidateMinCharLength(input, Int32.Parse(constraint.Value)))
                                    {
                                        errors.Add(constraint.Key,
                                            "Minimum length not met, please enter an input with at least " +
                                            constraint.Value +
                                            " characters");
                                    }
                                    break;
                                
                                //-----------------------------------------------\\
                                case "char_max":

                                    if (!ValidateMaxCharLength(input, Int32.Parse(constraint.Value)))
                                    {
                                        errors.Add(constraint.Key,
                                            "Minimum length not met, please enter an input with no more than " +
                                            constraint.Value +
                                            " characters");
                                    }
                                    break;
                                
                                //-----------------------------------------------\\
                                case "min":

                                    if (!ValidateIntegerMinNumber(int.Parse(input), int.Parse(constraint.Value)))
                                    {
                                        errors.Add(constraint.Key,
                                            "Please ensure your number is " + constraint.Value + " or larger");
                                    }
                                    
                                    break;
                                
                                //-----------------------------------------------\\
                                case "max":
                                    
                                    if (!ValidateIntegerMaxNumber(int.Parse(input), int.Parse(constraint.Value)))
                                    {
                                        errors.Add(constraint.Key,
                                            "Please ensure your number is no larger than "+constraint.Value);
                                    }
                                    
                                    break;
                            }
                        }
                    }
                }

                if (_type == Form.Float)
                {
                    if (!IsDecimal(input))
                    {
                        errors.Add("Decimal", "Input must be a decimal number");
                    }
                    else
                    {
                        
                        foreach (var constraint in _constraints)
                        {

                            switch (constraint.Key)
                            {

                                case "char_min":

                                    if (!ValidateMinCharLength(input, Int32.Parse(constraint.Value)))
                                    {
                                        errors.Add(constraint.Key,
                                            "Minimum length not met, please enter an input with at least " +
                                            constraint.Value +
                                            " characters");
                                    }
                                    break;
                                
                                case "char_max":

                                    if (!ValidateMaxCharLength(input, Int32.Parse(constraint.Value)))
                                    {
                                        errors.Add(constraint.Key,
                                            "Minimum length not met, please enter an input with no more than " +
                                            constraint.Value +
                                            " characters");
                                    }
                                    break;
                                
                                case "min":
                                    decimal userInput = Decimal.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
                                    decimal min = Decimal.Parse(constraint.Value,
                                        CultureInfo.InvariantCulture.NumberFormat);

                                    if (!ValidateDecimalMinNumber(userInput, min))
                                    {
                                        errors.Add(constraint.Key,
                                            "Please ensure your number is " + constraint.Value + " or larger");
                                    }
                                    
                                    break;
                                
                                case "max":
                                    
                                    userInput = Decimal.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
                                    decimal max = Decimal.Parse(constraint.Value,
                                        CultureInfo.InvariantCulture.NumberFormat);
                                    
                                    if (!ValidateDecimalMaxNumber(userInput, max))
                                    {
                                        errors.Add(constraint.Key,
                                            "Please ensure your number is " + constraint.Value + " or larger");
                                    }
                                    
                                    break;
                            }
                        }
                    }
                }

                if (_type == Form.Text)
                {

                    if (input == null)
                    {
                        errors.Add("Text","Input cannot be null");
                    }
                    else
                    {

                        foreach (var constraint in _constraints)
                        {

                            switch (constraint.Key)
                            {

                                case "char_min":

                                    if (!ValidateMinCharLength(input, int.Parse(constraint.Value)))
                                    {
                                        errors.Add(constraint.Key,
                                            "Minimum length not met, please enter an input with at least " +
                                            constraint.Value +
                                            " characters");
                                    }

                                    break;

                                case "char_max":

                                    if (!ValidateMaxCharLength(input, int.Parse(constraint.Value)))
                                    {
                                        errors.Add(constraint.Key,
                                            "Minimum length not met, please enter an input with no more than " +
                                            constraint.Value +
                                            " characters");
                                    }

                                    break;
                            }
                        }
                    }
                }

                foreach (var error in errors)
                {
                    Console.WriteLine("[Input Constraint Error] " + error.Value);
                }
                
            }

            if (errors.Count == 0)
            {
                valid = true;
            }
            
        }
        
        return input;
    }

    private string ReadLineHidden()
    {
        string input = null;
        bool completed = false;
        
        while (!completed)
        {
            var key = System.Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                completed = true;
            }
            else
            {
                input += key.KeyChar;
            }
        }
        
        return input;
    }

    private bool IsDecimal(string input)
    {
        bool outcome = true;
        
        try
        {
            var f = decimal.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
        }
        catch (Exception e)
        {
            outcome = false;
        }

        return outcome;
    }

    private bool IsInteger(string input)
    {
        bool outcome = true;
        
        try
        {
            var f = int.Parse(input);
        }
        catch (Exception e)
        {
            outcome = false;
        }

        return outcome;
    }

    private bool ValidateMinCharLength(string input, int min)
    {
        bool outcome = true;
        
        if (input.Length < min)
        {
            outcome = false;
        }

        return outcome;
    }
    
    private bool ValidateMaxCharLength(string input, int max)
    {
        bool outcome = true;

        if (input.Length > max)
        {
            outcome = false;
        }

        return outcome;
    }

    private bool ValidateIntegerMaxNumber(int input, int max)
    {
        bool outcome = true;

        if (input > max)
        {
            outcome = false;
        }

        return outcome;
    }
    
    private bool ValidateIntegerMinNumber(int input, int min)
    {
        bool outcome = true;

        if (input < min)
        {
            outcome = false;
        }

        return outcome;
    }

    private bool ValidateDecimalMinNumber(decimal input, decimal min)
    {
        bool outcome = true;

        if (input < min)
        {
            outcome = false;
        }

        return outcome;
    }

    private bool ValidateDecimalMaxNumber(decimal input, decimal max)
    {
        bool outcome = true;

        if (input > max)
        {
            outcome = false;
        }

        return outcome;
    }
    

}