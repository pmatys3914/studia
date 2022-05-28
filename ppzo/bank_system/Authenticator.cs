namespace Banking
{
    class Authenticator
    {
        private static Authenticator? instance;

        public static Authenticator getInstance()
        {
            if (instance == null)
            {
                instance = new Authenticator();
            }
            return instance;
        }

        private AccountDatabaseManager dbManager;
        private String currentUser = "";

        private Authenticator()
        {
            dbManager = AccountDatabaseManager.getInstance();
        }

        public bool Login(String accountId, string pin)
        {
            if(currentUser != "")
            {
                Console.WriteLine("Already logged in.");
                return false;
            }
            if (dbManager.ExistsId(accountId))
            {
                if (PINUtils.Validate(pin, dbManager.GetSalt(accountId), dbManager.GetHash(accountId)))
                {
                    currentUser = accountId;
                    return true;
                }
                else
                {
                    Console.WriteLine("Wrong PIN.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Account doesn't exist.");
                return false;
            }
        }

        public void Logout()
        {
            currentUser = "";
            Console.WriteLine("Logged out.");
        }
    }
}