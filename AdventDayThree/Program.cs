string data = File.ReadAllText($"{Environment.CurrentDirectory}/data.txt");
string[] lines = data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

int result = 0;

foreach (string line in lines)
{
    string firstHalf = line[0..(line.Length / 2)];
    string secondHalf = line[(line.Length / 2)..];

    for (int i = 0; i < firstHalf.Length; i++)
    {
        if (secondHalf.Contains(firstHalf[i]))
        {
            result += GetPriority(firstHalf[i]);
            break;
        }
    }
}

Console.WriteLine($"Result 1 : {result}");

// Part 2
List<string[]> groups = new();
int newResult = 0;

for (int i = 0; i < lines.Length; i += 3)
    groups.Add(lines.Skip(i).Take(3).ToArray());

foreach (string[] group in groups)
{
    for (int i = 0; i < group[0].Length; i++)
    {
        if (group[1].Contains(group[0][i]) && group[2].Contains(group[0][i]))
        {
            newResult += GetPriority(group[0][i]);
            break;
        }
    }
}

Console.WriteLine($"Result 2 : {newResult}");

int GetPriority(char c) =>
    char.IsUpper(c) ? c - 38 : char.ToUpper(c) - 64;