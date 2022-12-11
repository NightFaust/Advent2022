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
            result += char.IsUpper(firstHalf[i])
                ? firstHalf[i] - 38
                : char.ToUpper(firstHalf[i]) - 64;

            break;
        }
    }
}

Console.WriteLine($"Result 1 : {result}");
