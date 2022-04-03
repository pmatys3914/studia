namespace Tea
{
    // A struct holding all tea data
    public struct Tea {
        // Construct from data
        public Tea(string name, string type, uint temp, uint time, string? special = null)
        {
            Name = name;
            Type = type;
            Temp = temp;
            Time = time;
            Special = special;
        }


        // Construct from string
        public Tea(string s)
        {
            // Split the string and return the Tea struct
            string[] data = s.Split(", ");
            Name = data[0];
            Type = data[1];
            Temp = UInt32.Parse(data[2]);
            Time = UInt32.Parse(data[3]);
            Special = data[4];
        }


        // Serialise to string
        public string Serialize()
        {
            return Name + ", " + Type + ", " + Temp + ", " + Time + ", " + Special;
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

        // List of teas
        private List<Tea> teas = new List<Tea>();

        // Context
        private Context? context;

        static void Main(string[] args)
        {
            // Instantiate the program class for non-static members
            Program p = new Program();

        }


        // Program constructor initialising all data
        public Program()
        {
            // Load the teas
            LoadTeas();
            
            // Initialise the context
            context = new Context(teas);
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
                        teas.Add(new Tea(s));
                    }
                }
            }
        }
    }
}