using System;

namespace BankLibrary
{
    public enum AccountType
    {
        Ordinary,
        Deposit
    }

    public class Bank<T> where T : Account
    {
        T[] accounts;

        public string Name { get; private set; }

        public Bank(string name)
        {
            Name = name;
        }

        public void Open(AccountType accountType, decimal sum,
            AccountStateHandler addSumHandler, AccountStateHandler withdrawSumHandler,
            AccountStateHandler openAccountHandler, AccountStateHandler closeAccountHandler,
            AccountStateHandler calculationHandler)
        {
            T newAccount = null;
            switch (accountType)
            {
                case AccountType.Ordinary:
                    newAccount = new DemandAccount(sum, 1) as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepositAccount(sum, 15) as T;
                    break;                
            }

            if (newAccount == null)
                throw new Exception("Account creation error.");

            if (accounts == null)
                accounts = new T[] { newAccount };
            else
            {
                T[] temp = new T[accounts.Length + 1];
                for (int i = 0; i < accounts.Length; i++)
                    temp[i] = accounts[i];
                temp[^1] = newAccount;
                accounts = temp;
            }

            newAccount.Added += addSumHandler;
            newAccount.Withdrawed += withdrawSumHandler;
            newAccount.Opened += openAccountHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Calculated += calculationHandler;

            newAccount.Open();
        }

        public void Put(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Account not found.");

            account.Put(sum);
        }

        public void Withdraw(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Account not found.");

            account.Withdraw(sum);
        }

        public void Close(int id)
        {
            T account = FindAccount(id, out int index);
            if (account == null)
                throw new Exception("Account not found.");

            account.Close();

            if (accounts.Length <= 1)
                accounts = null;
            else
            {
                T[] temp = new T[accounts.Length - 1];
                for (int i = 0, j = 0; i < accounts.Length; i++)
                {
                    if (i != index)
                        temp[j++] = accounts[i];
                }
                accounts = temp;
            }
        }

        public void CalculatePercentage()
        {
            if (accounts == null)
                return;

            for (int i = 0; i < accounts.Length; i++)
            {
                accounts[i].IncrementDays();
                accounts[i].Calculate();
            }
        }

        public T FindAccount(int id)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].Id == id)
                    return accounts[i];
            }
            return null;
        }

        public T FindAccount(int id, out int index)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].Id == id)
                {
                    index = i;
                    return accounts[i];
                }
            }
            index = -1;
            return null;
        }
    }
}
