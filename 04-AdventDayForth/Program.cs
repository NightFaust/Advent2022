string data = File.ReadAllText($"{Environment.CurrentDirectory}/data.txt");
string[] lines = data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
int result = 0;
int newResult = 0;

foreach (string line in lines)
{
    string[] parts = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
    string[] part1 = parts[0].Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
    string[] part2 = parts[1].Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

    int start1 = int.Parse(part1[0]);
    int start2 = int.Parse(part2[0]);
    int end1 = int.Parse(part1[1]);
    int end2 = int.Parse(part2[1]);

    if ((start1 >= start2 && end1 <= end2) || (start2 >= start1 && end2 <= end1))
        result++;

    if ((start1 >= start2 && start1 <= end2) || (start2 >= start1 && start2 <= end1))
        newResult++;
}

Console.WriteLine($"Result 1 : {result}");
Console.WriteLine($"Result 2 : {newResult}");
