using System.Security.Cryptography;
namespace Banking
{
    struct PINData
    {
        public String PIN;
        public String salt;
        public String hash;
    }

    class PINGenerator
    {
        public static PINData Generate()
        {
            PINData data = new PINData();
            data.PIN = GeneratePIN();
            data.salt = System.Text.Encoding.UTF8.GetString(GenerateSalt());
            data.hash = System.Text.Encoding.UTF8.GetString(GenerateHash(data.PIN, data.salt));
            return data;
        }

        public static bool Validate(String PIN, String salt, String hash)
        {
            String newHash = System.Text.Encoding.UTF8.GetString(GenerateHash(PIN, salt));
            return newHash == hash;
        }

        private static String GeneratePIN()
        {
            Random rnd = new Random();
            String pin = "";
            for (int i = 0; i < 8; i++)
            {
                pin += (char)('0' + (int)(rnd.NextDouble() * 10));
            }
            return pin;
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            Random rnd = new Random();
            rnd.NextBytes(salt);
            return salt;
        }

        private static byte[] GenerateHash(String pin, String salt)
        {
            HashAlgorithm algorithm = SHA256Managed.Create();

            byte[] pinWithSalt = new byte[pin.Length + salt.Length];

            for (int i = 0; i < pin.Length; i++)
            {
                pinWithSalt[i] = (byte)pin[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                pinWithSalt[pin.Length + i] = (byte)salt[i];
            }

            return algorithm.ComputeHash(pinWithSalt);
        }
    }
}