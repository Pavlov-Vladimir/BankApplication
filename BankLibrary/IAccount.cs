namespace BankLibrary
{
    interface IAccount
    {
        void Put(decimal sum);

        decimal Withdraw(decimal sum);
    }
}
