namespace Tea
{
    // Sort the records by given column and save the result to file
    class SortOption : IOption
    {
        const string pathOutput = "result-2.txt";

        public uint Execute(List<Tea> teas)
        {
            // Copy the list for manipulation
            List<Tea> temp = new List<Tea>(teas);

            uint? cc = null; // Choice column
            uint? co = null; // Choice order
            Console.Write(
              @"Choose which column to sort by
                [1] Name
                [2] Type
                [3] Temperature
                [4] Time
                [5] Special
                [0] Cancel"
            );

            while(cc == null || cc > 5) {
                cc = Convert.ToUInt32(Console.ReadLine());
                if(cc == 0)
                {
                    return 6;
                }
            }

            Console.Write(
              @"Choose sort order
                [1] Ascending
                [2] Descending
                [0] Cancel"
            );
            
            while(co == null || co > 2) {
                co = Convert.ToUInt32(Console.ReadLine());
                if(co == 0)
                {
                    return 6;
                }
            }

            // Sort the list by given column
            switch(cc)
            {
                case 1:
                    temp.Sort((a,b) => a.Name.CompareTo(b.Name));
                    break;
                case 2:
                    temp.Sort((a,b) => a.Type.CompareTo(b.Type));
                    break;
                case 3:
                    temp.Sort((a,b) => a.Temp.CompareTo(b.Temp));
                    break;
                case 4:
                    temp.Sort((a,b) => a.Time.CompareTo(b.Time));
                    break;
                case 5:
                    temp.Sort((a,b) => a.Special.CompareTo(b.Special));
                    break;
            }
            // If the sort is descending, reverse the list
            if(co == 2) {
                temp.Reverse();
            }

            // Save it to file, overwrite if exists
            using (StreamWriter sw = new StreamWriter(pathOutput, false))
            {
                foreach(Tea t in temp)
                {
                    sw.WriteLine(t.Serialize());
                }
            }

            Console.WriteLine("Records sorted!");
            return 6;
        }
    }
}