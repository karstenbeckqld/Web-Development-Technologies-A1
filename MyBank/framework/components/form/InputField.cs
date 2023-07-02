using System.Globalization;

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
            
            if (!_freeForm)
            {
                
                if (_type == Form.Integer)
                {
                    if (!IsInteger(input))
                    {
                        errors.Add("Integer","Input must be a integer only");
                    }
                }
                
                foreach (var constraint in _constraints)
                {

                    switch (constraint.Key)
                    {

                        case "char_min":
                            if (input.Length < Int32.Parse(constraint.Value))
                            {
                                errors.Add(constraint.Key,
                                    "Minimum length not met, please enter an input with at least " + constraint.Value +
                                    " characters");
                            }

                            break;
                        case "char_max":
                            if (input.Length > Int32.Parse(constraint.Value))
                            {
                                errors.Add(constraint.Key,
                                    "Minimum length not met, please enter an input with at least " + constraint.Value +
                                    " characters");
                            }

                            break;
                        case "min":

                            if (!IsDecimal(input))
                            {
                                errors.Add(constraint.Key, "Please enter a float only! Example: '1.23'");
                                break;
                            }

                            var f = decimal.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
                            var min = decimal.Parse(constraint.Value, CultureInfo.InvariantCulture.NumberFormat);
                            if (f < min)
                            {
                                errors.Add(constraint.Key,
                                    "Your selection must be at least '" + constraint.Value + "'");
                            }

                            break;
                    }
                }
            }
            else
            {
                valid = true;
            }

            foreach (var error in errors)
            {
                Console.WriteLine("[Input Constraint Error] "+error.Value);
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
    

}