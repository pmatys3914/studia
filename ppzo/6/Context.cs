namespace Tea
{
    // Context class controlling what menu option is currently in use
    class Context
    {
        // Current option(strategy)
        private IOption _option;

        private List<Tea> _teas;


        // Constructor setting the initial option to the menu
        public Context(List<Tea> teas)
        {
            _teas = teas;
            _option = new MenuOption();
            Execute();
        }

        
        // Replace the current context
        public void SetOption(IOption option)
        {
            _option = option;
            Execute();
        }


        // Execute the option

        public void Execute()
        {
            uint code = _option.Execute(_teas);

            // Change the option depending on the code
            switch(code)
            {
                case 0:
                    System.Environment.Exit(0);
                    break;
                case 1:
                    SetOption(new ReverseOption());
                    break;
                case 2:
                    SetOption(new SortOption());
                    break;
                case 3:
                    SetOption(new MakeOption());
                    break;
                case 4:
                    SetOption(new MakeMultipleOption());
                    break;
                case 5:
                    SetOption(new TouaregOption());
                    break;
                default:
                    SetOption(new MenuOption());
                    break;
            }
        }
    }
}