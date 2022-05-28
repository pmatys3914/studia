namespace Banking
{
    public struct DisplayData
    {
        public String accountId;
        public String pin;
    }

    class AccountManager
    {
        private static AccountManager? instance;
        
        public static AccountManager getInstance()
        {
            if(instance == null)
            {
                instance = new AccountManager();
            }
            return instance;
        }

        private AccountDatabaseManager dbManager;

        private AccountManager()
        {
            dbManager = AccountDatabaseManager.GetInstance();
        }

        public DisplayData Add(String name)
        {
            AccountData data = new AccountData();
            data.name = name;
            data.accountId = AccountIdGenerator.Generate(dbManager.GetCount());
            data.balance = 0;
            PINData pin = PINUtils.Generate();
            data.hash = pin.hash;
            data.salt = pin.salt;
            if(dbManager.Add(data))
            {
                return new DisplayData()
                {
                    accountId = data.accountId,
                    pin = pin.PIN
                };
            }
            else
            {
                return new DisplayData()
                {
                    accountId = "",
                    pin = "",
                };
            }
        }

        public bool Remove(String accountId)
        {
            return dbManager.Remove(accountId);
        }

        public bool TransferFunds(String fromAccountId, String toAccountId, int amount)
        {
            if(dbManager.GetBalance(fromAccountId) < amount)
            {
                return false;
            }
            else
            {
                dbManager.AddBalance(fromAccountId, -amount);
                dbManager.AddBalance(toAccountId, amount);
                return true;
            }
        }
    }
}