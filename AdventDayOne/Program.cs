string data = File.ReadAllText($"{Environment.CurrentDirectory}/data.txt");
string[] removedEmptyLines = data.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
List<int> results = new();

foreach (var entry in removedEmptyLines)
{
    string[] lines = entry.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
    int calories = 0;
    for (int i = 0; i < lines.Length; i++)
        calories += int.Parse(lines[i]);

    results.Add(calories);
}

// Part 1
Console.WriteLine($"Result 1: {results.Max()}");
// Part 2
Console.WriteLine($"Result 2: {results.OrderByDescending(x => x).Take(3).Sum()}");
