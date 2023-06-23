namespace MyBank.framework.components.form;

public class InputField
{

    private string _prompt;
    private int _type;
    private Dictionary<string, string> _constraints;
    private bool _readLineHidden;
    
    public InputField(int type, string promptInput, string constraints, bool hideInput)
    {
        _prompt = promptInput;
        _readLineHidden = hideInput;
        _constraints = new Dictionary<string, string>();
        
        foreach (var entry in constraints.Split(","))
        {
            string[] values = entry.Split(":");
            _constraints.Add(values[0],values[1]);
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

            foreach (var constraint in _constraints)
            {

                switch (constraint.Key)
                {
                    
                    case "char_min":
                        if (input.Length < Int32.Parse(constraint.Value))
                        {
                            errors.Add(constraint.Key,"Minimum length not met, please enter an input with at least "+constraint.Value+" characters");
                        }

                        break;
                    case "char_max":
                        if (input.Length > Int32.Parse(constraint.Value))
                        {
                            errors.Add(constraint.Key,"Minimum length not met, please enter an input with at least "+constraint.Value+" characters");
                        }
                        break;

                }
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
    
    
}