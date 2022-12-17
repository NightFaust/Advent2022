string data = File.ReadAllText($"{Environment.CurrentDirectory}/data.txt");

int result = 0;
int newResult = 0;

for (int i = 0; i < data.Length - 4; i++)
{
    if (IsStart(data[i..(i + 4)], true))
    {
        result = i + 4;
        break;
    }
}

for (int i = 0; i < data.Length - 14; i++)
{
    if (IsStart(data[i..(i + 14)], false))
    {
        newResult = i + 14;
        break;
    }
}

Console.WriteLine($"Result 1 : {result}");
Console.WriteLine($"Result 2 : {newResult}");

bool IsStart(string txt, bool isPartOne) =>
    isPartOne switch
    {
        true => txt.Distinct().Count() == 4,
        false => txt.Distinct().Count() == 14
    };