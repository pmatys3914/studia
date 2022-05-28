namespace Banking
{
    class Program
    {
        static void Main(string[] args)
        {
            new BankSystem();
            AccountManager am = AccountManager.getInstance();
            Authenticator a = Authenticator.getInstance();
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                DisplayData dd = am.Add("Test Test" + r.Next(100000));
                Console.WriteLine(dd.accountId);
                Console.WriteLine(dd.pin);
                Console.WriteLine("============");
                Console.WriteLine(a.Login(dd.accountId, dd.pin));
                a.Logout();
                Console.WriteLine("============");
            }

        }
    }
}
