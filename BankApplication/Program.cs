using System;
using BankLibrary;

namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("BigBank");
            bool alive = true;
            while (alive)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. Open account \t 2. Withdraw money \t 3. Add founds");
                Console.WriteLine("4. Close account \t 5. Skip the day \t 6. Exit");
                Console.Write("Enter number of your choice: \n-> ");
                Console.ResetColor();
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());

                    switch (command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            Withdraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                    }
                    bank.CalculatePercentage();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
        }

        private static void OpenAccount(Bank<Account> bank)
        {
            Console.Write("Choose account type: 1. Demand \t 2. Deposit \n-> ");
            int type = Convert.ToInt32(Console.ReadLine());
            
            AccountType accountType;
            if (type == 2)
                accountType = AccountType.Deposit;
            else
                accountType = AccountType.Ordinary;

            Console.Write("Enter the amount to create the account: \n-> ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());

            bank.Open(accountType, sum, AddSumHandler, WithdrawSumHandler,
                      OpenAccountHandler, CloseAccountHandler, CalculationHandler);
        }

        private static void Withdraw(Bank<Account> bank)
        {
            Console.Write("Input account ID: \n-> ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the amount to withdraw from the account: \n-> ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());

            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.Write("Input account ID: \n-> ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the amount to add to the account: \n-> ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());

            bank.Put(sum, id);
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            Console.Write("Input account ID to close: \n-> ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }

        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void CalculationHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
