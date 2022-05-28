namespace Banking
{
    class BalanceHelper
    {
        public static string toString(int balance)
        {
            return (float)(balance) / 100 + "zł";
        }
    }
}