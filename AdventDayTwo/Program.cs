using AdventDayTwo;

string data = File.ReadAllText($"{Directory.GetCurrentDirectory()}/data.txt");
string[] lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
int result = 0;

foreach (string line in lines)
{
    string[] parts = line.Split(' ');

    int playerChoice = (int)(PlayerChoice)Enum.Parse(typeof(PlayerChoice), parts[1]);
    int opponentChoice = (int)(OpponentChoice)Enum.Parse(typeof(OpponentChoice), parts[0]);

    if (opponentChoice % 3 + 1 == playerChoice)
        result += (int)MatchResult.Win + playerChoice;
    else if (playerChoice % 3 + 1 == opponentChoice)
        result += (int)MatchResult.Loss + playerChoice;
    else
        result += (int)MatchResult.Draw + playerChoice;
}

Console.WriteLine($"Result 1 : {result}");

// Part 2
int newResult = 0;
foreach (string line in lines)
{
    string[] parts = line.Split(' ');
    
    int opponentChoice = (int)(OpponentChoice)Enum.Parse(typeof(OpponentChoice), parts[0]);
    int expectedMatchResult = (int)(PartTwoMatchResult)Enum.Parse(typeof(PartTwoMatchResult), parts[1]);

    newResult += expectedMatchResult switch
    {
        0 => opponentChoice switch { 1 => 3, 2 => 1, 3 => 2, _ => throw new ArgumentException("Invalid opponent choice") },
        3 => 3 + opponentChoice,
        6 => 6 + opponentChoice % 3 + 1,
        _ => throw new Exception("Invalid match result")
    };
}

Console.WriteLine($"Result 2 : {newResult}");