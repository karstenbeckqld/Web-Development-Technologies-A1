using MyBank.framework.core;
using MyBank.framework.facades;

namespace MyBank.framework.components;

public class Table<T> : Component
{
    
    private List<T> _values;
    private string _displayKey;
    private int _paginateLimit;
    private int _currentPage;
    private string _error;

    public Table()
    {
        _currentPage = 0;
        _error = String.Empty;
    }
    
    public void SetVariableKey(string key)
    {
        _displayKey = key;
    }

    public void SetPaginateLimit(int limit)
    {
        _paginateLimit = limit;
    }
    
    public override Event Process()
    {

        if (_view.GetVariableOrNull(_displayKey) != null)
        {

            Console.WriteLine("|----|--------------|----------|--------------------|--------------------------------|-----------------------|");
            Console.WriteLine("|Id  |Type          |Amount    |Destination Account |Comment                         |Date                   |");
            Console.WriteLine("|----|--------------|----------|------------------- |--------------------------------|-----------------------|");

            List<T> data = (List<T>)_view.GetVariableOrNull(_displayKey);

            int pageCount = (data.Count / _paginateLimit);

            int offset = _currentPage * _paginateLimit;
            int offsetMax = offset + _paginateLimit;

            int counter = 0;
            foreach (var value in (List<T>)_view.GetVariableOrNull(_displayKey))
            {
                if (counter >= offset && counter < offsetMax)
                {
                    Console.WriteLine(value.ToString());
                }
                counter++;
            }
            
            Console.WriteLine("|----|--------------|----------|------------------- |--------------------------------|-----------------------|");

            Console.WriteLine("Page: "+(_currentPage+1)+" of "+(pageCount+1)+" (Showing "+(_paginateLimit)+" records per page)");
            Console.WriteLine("Please enter the page number to toggle page, or 'back' to return");

            if (_error != String.Empty)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(_error);
                Console.ResetColor();
            }
            
            string input = Console.ReadLine();
        
            if (input.ToLower().Equals("back"))
            {
                _currentPage = 0;
                _error = String.Empty;
                _view.ClearVariable(_displayKey);
                App.SwitchView("AccountSelectionView");
                Kernal.Instance().Process();
            }

            int selection;

            if (Int32.TryParse(input, out selection))
            {
                if (selection >= 1 && selection <= pageCount+1)
                {
                    _currentPage = selection-1;
                    _error = String.Empty;
                }else
                {
                    _error = "Invalid selection provided, please enter a valid page";
                }
            }
            else
            {
                _error = "Invalid selection provided, please enter a valid page";
            }

            Kernal.Instance().Process();
        }
        
        return null;
    }
}