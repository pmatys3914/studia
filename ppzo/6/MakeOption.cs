namespace Tea
{
    // Make a tea with selected parameters and return its quality
    class MakeOption : IOption
    {
        private string pathOutput = "result-3.txt";

        public uint Execute(List<Tea> teas)
        {
            // Keep asking for tea name until it matches the database or quit is called
            Console.WriteLine("Enter tea name, [0] to cancel:");
            string name;
            int index = -1;
            do {
                name = Console.ReadLine();
                if(name == "0")
                {
                    return 6;
                }
                if((index = teas.FindIndex(t => t.Name == name)) == -1)
                {
                    Console.WriteLine("Invalid tea name.");
                }
            } while (index == -1);

            // Read the temperature and time
            int temp;
            int time;
            Console.WriteLine("Enter the temperature in degrees Celsius");
            temp = Int32.Parse(Console.ReadLine());
            
            Console.WriteLine("Enter the time in seconds");
            time = Int32.Parse(Console.ReadLine());

            // Get the tea reference for calculations
            Tea tea = teas[index];

            // Save the result to file, overwrite if exists
            using (StreamWriter sw = new StreamWriter(pathOutput, false))
            {
                // Calculate and return the quality
                if(temp > tea.Temp * 1.1f || time > tea.Time * 1.1f * 60)
                {
                    sw.WriteLine("yucky");
                } else if(temp < tea.Temp * 0.9f || time < tea.Time * 0.9f * 60)
                {
                    sw.WriteLine("weak");
                } else
                {
                    sw.WriteLine("perfect");
                }
            }


            return 6;
        }
    }
}