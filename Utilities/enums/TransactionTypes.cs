namespace Utilities.enums;

public enum TransactionType
{
    Deposit='D', // Credit (Deposit money)
    Withdrawal='W', // Debit (Withdraw money)
    Transaction='T', // Credit and Debit (Transferring money between accounts)
    ServiceCharge='S'  // Debit (Service charge)
}