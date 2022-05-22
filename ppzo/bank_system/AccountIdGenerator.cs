namespace Banking
{
    class AccountIdGenerator
    {
        public static String Generate(uint count)
        {
            String id = "400000";
            id += count.ToString("D9");
            return id + GetChecksum(id);
        }

        private static char GetChecksum(string id)
        {
            int sum = 0;
            for (int i = id.Length - 1; i >= 0; i--)
            {
                sum += i % 2 == 0 ? Double(id[i] - '0') : id[i] - '0'; 
            }
            return ((10 - (sum % 10)) % 10).ToString()[0];
        }

        private static int Double(int i)
        {
            return i * 2 > 9 ? 1 + (i*2)%10: i*2;
        }
    }
}