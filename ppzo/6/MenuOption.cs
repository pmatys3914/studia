namespace Tea
{
    // Option displaying the menu and waiting for the user input
    class MenuOption : IOption
    {
        // Execute override
        public uint Execute(List<Tea> teas)
        {
            Console.WriteLine(
              @"Tea Program
                
                [1] Reverse Records
                [2] Sort
                [3] Make a tea
                [4] Make mutiple teas
                [5] Make touareg
                [0] Quit
               "
            );
            while(true)
            {
                uint c = Convert.ToUInt32(Console.ReadLine());
                if(c <= 5)
                {
                    return c;
                }
            }

        }
    }
}