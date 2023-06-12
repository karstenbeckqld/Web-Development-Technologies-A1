using System.Data.SqlTypes;

namespace s3893749_s3912792_a1.model
{
    public class AccountManager
    {

        private List<AccountHolder> _customers;
        
        public AccountManager()
        {
            
        }

        public List<AccountHolder> Customers { get; set; }

    }
}