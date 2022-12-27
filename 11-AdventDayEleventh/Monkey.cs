public class Monkey
{
    private Func<int, int> _operation;

    public Queue<int> Items { get; set; } = new();
    
    public Monkey(Func<int, int> operation)
    {
        _operation = operation;
    }

    public void Parse(string[] data)
    {

    }

    private Func<int, int> GetOperation(string part1, string part2) =>
        (part1, part2) switch
        {
            ("*", "old") => old => old * old,
            ("*", _) => old => old * int.Parse(part2),
            ("+", "old") => old => old + old,
            ("+", _) => old => old + int.Parse(part2),
            _ => (old) => 0 
        };
}