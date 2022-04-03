namespace Tea
{
    // Reverse Records and save them to file
    class ReverseOption : IOption
    {
        const string pathOutput = "result-1.txt";


        // Execute override
        public uint Execute(List<Tea> teas)
        {
            // Copy the list for manipulation
            List<Tea> temp = new List<Tea>(teas);

            // Reverse the list
            temp.Reverse();

            // Save it to file, overwrite if exists
            using (StreamWriter sw = new StreamWriter(pathOutput, false))
            {
                foreach(Tea t in temp)
                {
                    sw.WriteLine(t.Serialize());
                }
            }
            Console.WriteLine("Records reversed!");
            return 6;
        }
    }
}