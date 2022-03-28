namespace Tea
{
    // A struct holding all tea data
    struct Tea {
        public Tea(string name, string type, uint temp, uint time, string? special = null)
        {
            Name = name;
            Type = type;
            Temp = temp;
            Time = time;
            Special = special;
        }
        public string Name { get; }
        public string Type { get; }
        public uint Temp { get; }
        public uint Time { get; }
        public string? Special { get; }
    }


    class Program
    {
        // File paths
        public static string pathInput = "tea-data.txt";
        public static string pathOutput = "result-2.txt";

        // List of teas
        private List<Tea> teas = new List<Tea>();

        static void Main(string[] args)
        {
            // Instantiate the program class for non-static members
            Program p = new Program();

            // Load the teas
            p.LoadTeas();
            
            // Resolve the arguments and sort the teas
            p.ResolveArgs(args);

            // Save the teas to the output file
            p.SaveTeas();
        }


        // Load tea data from file into the list
        private void LoadTeas()
        {
            // Check if the input file exists, return otherwise
            if (!File.Exists(pathInput))
            {
                Console.Write("File missing!");
                return;
            }
            // Read the list with a stream reader
            using (StreamReader sr = File.OpenText(pathInput))
            {
                string? s;
                while((s = sr.ReadLine()) != null)
                {
                    // If the line is not empty and doesn't begin with #, parse and add to the list
                    if(s != "" && s[0] != '#')
                    {
                        teas.Add(ParseTea(s));
                    }
                }
            }
        }


        // Parse a tea data string and return a Tea struct
        private Tea ParseTea(string s)
        {
            // Split the string and return the Tea struct
            string[] data = s.Split(", ");
            return new Tea(data[0], data[1], UInt32.Parse(data[2]), UInt32.Parse(data[3]), data[4]);
        }


        // Resolve arguments passed to the program
        private void ResolveArgs(string[] args)
        {
            // Default empty args
            if(args.Length == 0)
            {
                args = new string[2] {"name", "asc"};
            }
            if(args.Length == 1)
            {
                args = new string[2] {args[0], "asc"};
            }
            // Check if first arg is valid; display help otherwise
            var strings = new List<string> { "name", "type", "temp", "time", "special" };
            if(!strings.Contains(args[0])) {
                DisplayHelp();
            } else
            {
                // Sort the list
                Sort(args[0], args[1]);
            }
        }


        // Display help in the console
        private void DisplayHelp()
        {
            Console.WriteLine("usage: 2 [NAME, type, temp, time, special] [ASC, desc]");
        }


        // Sort the array
        private void Sort(string val, string asc)
        {
            // This code could be improved by making a dictionary of compare functions keyed by the strings
            switch(val)
            {
                case "name":
                    teas.Sort((a,b) => a.Name.CompareTo(b.Name));
                    break;
                case "type":
                    teas.Sort((a,b) => a.Type.CompareTo(b.Type));
                    break;
                case "temp":
                    teas.Sort((a,b) => a.Temp.CompareTo(b.Temp));
                    break;
                case "time":
                    teas.Sort((a,b) => a.Time.CompareTo(b.Time));
                    break;
                case "special":
                    teas.Sort((a,b) => a.Special.CompareTo(b.Special));
                    break;
            }
            // If the sort is descending, reverse the list
            if(asc == "desc") {
                teas.Reverse();
            }
        }

        
        // Save the data to the output file
        private async void SaveTeas()
        {
            using (StreamWriter sw = File.CreateText(pathOutput))
            {
                foreach( Tea t in teas)
                {
                    sw.WriteLine(t.Name + ", " + t.Type + ", " + t.Temp + ", " + t.Time + ", " + t.Special);
                }
            }
        }
    }
}