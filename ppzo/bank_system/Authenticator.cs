namespace Banking
{
    class Authenticator
    {
        private static Authenticator? instance;

        public static Authenticator GetInstance()
        {
            if (instance == null)
            {
                instance = new Authenticator();
            }
            return instance;
        }

        private AccountDatabaseManager dbManager;
        private String currentUser = "";
        public String CurrentUser
        {
            get
            {
                return currentUser;
            }
        }

        private Authenticator()
        {
            dbManager = AccountDatabaseManager.GetInstance();
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
                    Console.WriteLine("Login successful.");
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