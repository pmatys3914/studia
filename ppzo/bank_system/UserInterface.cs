namespace Banking
{
    class UserInterface
    {
        public void AppLoop()
        {
            while (true)
            {
                String currUser = Authenticator.GetInstance().CurrentUser;
                if (currUser == "")
                {
                    Console.WriteLine(
                  @"Banking System
                    {0} users in the system
                    [1] Login
                    [2] Create Account
                    [q] Exit", AccountDatabaseManager.GetInstance().GetCount());

                    char input = Console.ReadKey().KeyChar;
                    Console.WriteLine("\n");
                    switch (input)
                    {
                        case '1':
                            Login();
                            break;
                        case '2':
                            CreateAccount();
                            break;
                        default:
                            return;
                    }
                }
                else
                {
                    Console.WriteLine(
                  @"Welcome, {0}
                    {1}
                    [1] Balance
                    [2] Deposit Money
                    [3] Transfer Money
                    [4] Close Account
                    [5] Log Out
                    [q] Exit", AccountDatabaseManager.GetInstance().GetName(currUser), currUser);
                    
                    char input = Console.ReadKey().KeyChar;
                    Console.WriteLine("\n");
                    switch(input)
                    {
                        case '1':
                            Balance();
                            break;
                        case '2':
                            Deposit();
                            break;
                        case '3':
                            Transfer();
                            break;
                        case '4':
                            CloseAccount();
                            break;
                        case '5':
                            Logout();
                            break;
                        default:
                            return;
                    }
                }
            }
        }

        private void Login()
        {
            Console.WriteLine("Enter account id:");
            string accountId = Console.ReadLine();
            Console.WriteLine("Enter pin:");
            string pin = Console.ReadLine();
            Authenticator.GetInstance().Login(accountId, pin);
        }

        private void CreateAccount()
        {
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
            DisplayData dd = AccountManager.getInstance().Add(name);
            if (dd.pin != "")
            {
                Console.WriteLine("Account created.");
                Console.WriteLine("Account id: {0}", dd.accountId);
                Console.WriteLine("PIN: {0}", dd.pin);
            }
        }

        private void Balance()
        {
            Console.WriteLine("Your current balance is {0}", BalanceHelper.toString(AccountDatabaseManager.GetInstance().GetBalance(Authenticator.GetInstance().CurrentUser)));
        }

        private void Deposit()
        {
            Console.WriteLine("Enter amount in zł:");
            int amount = int.Parse(Console.ReadLine());
            if(amount < 1)
            {
                Console.WriteLine("Invalid amount.");
                return;
            }
            AccountDatabaseManager.GetInstance().AddBalance(Authenticator.GetInstance().CurrentUser, amount * 100);
        }

        private void Transfer()
        {
            Console.WriteLine("Enter recipient account id:");
            string recipientAccountId = Console.ReadLine();
            if(recipientAccountId == Authenticator.GetInstance().CurrentUser)
            {
                Console.WriteLine("You cannot transfer money to yourself.");
                return;
            }
            if(!AccountDatabaseManager.GetInstance().ExistsId(recipientAccountId))
            {
                Console.WriteLine("Recipient account does not exist.");
                return;
            }
            Console.WriteLine("Enter amount in zł:");
            int amount = int.Parse(Console.ReadLine()) * 100;
            if(amount < 100)
            {
                Console.WriteLine("Invalid amount.");
                return;
            }
            if(amount > AccountDatabaseManager.GetInstance().GetBalance(Authenticator.GetInstance().CurrentUser))
            {
                Console.WriteLine("Overdraft is not allowed.");
                return;
            }
            AccountManager.getInstance().TransferFunds(Authenticator.GetInstance().CurrentUser, recipientAccountId, amount);
            Console.WriteLine("{0} zł transfered to {1}", BalanceHelper.toString(amount), recipientAccountId);
        }

        private void CloseAccount()
        {
            if(AccountDatabaseManager.GetInstance().GetBalance(Authenticator.GetInstance().CurrentUser) != 0)
            {
                Console.WriteLine("You cannot close account with non-zero balance.");
                return;
            }
            Console.WriteLine("Are you sure you want to close your account? [y/n]");
            char input = Console.ReadKey().KeyChar;
            if(input == 'y')
            {
                AccountManager.getInstance().Remove(Authenticator.GetInstance().CurrentUser);
                Authenticator.GetInstance().Logout();
            }
        }

        private void Logout()
        {
            Authenticator.GetInstance().Logout();
        }
    }
}