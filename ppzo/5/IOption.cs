namespace Tea
{
    // Option interface
    public interface IOption
    {
        // Method called when an option is selected, passes the tea list as argument and returns a code
        uint Execute(List<Tea> teas);
    }
}