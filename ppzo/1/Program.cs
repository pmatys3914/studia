namespace Tea
{
    class Program
    {
        public static string pathInput = "tea-data.txt";
        public static string pathOutput = "result-1.txt";
        static void Main(string[] args)
        {
            // Check if the input file exists, return otherwise
            if (!File.Exists(pathInput))
            {
                Console.Write("File missing!");
                return;
            }
            // Create a list for the records
            List<string> list = new List<string>();
            // Read the list with a stream reader
            using (StreamReader sr = File.OpenText(pathInput))
            {
                string? s;
                while((s = sr.ReadLine()) != null)
                {
                    // If the line is not empty and doesn't begin with #, add it to the list
                    if(s != "" && s[0] != '#')
                    {
                        list.Add(s);
                    }
                }
            }

            // Reverse the list
            list.Reverse();

            // Create the output file and dump the list into it
            using (StreamWriter sw = File.CreateText(pathOutput))
            {
                foreach( string s in list)
                {
                    sw.WriteLine(s);
                }
            }
        }
    }
}