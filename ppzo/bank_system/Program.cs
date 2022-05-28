namespace Banking
{
    class Program
    {
        static void Main(string[] args)
        {
            new BankSystem();
            AccountManager am = AccountManager.getInstance();
            for(int i = 0; i < 10; i++)
            {
            DisplayData dd = am.Add("Test Test" + i);
            Console.WriteLine(dd.accountId);
            Console.WriteLine(dd.pin);
            Console.WriteLine("============");
            }

        }
    }
}
