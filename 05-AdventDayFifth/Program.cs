using System.Text;

string data = File.ReadAllText($"{Environment.CurrentDirectory}/data.txt");
string[] lines = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
List<string> crateLines = new();
string columnLine = string.Empty;
List<string> orders = new();

foreach (string line in lines)
{
    if (line.Contains('['))
        crateLines.Add(line);
    else if (char.IsDigit(line.TrimStart().First()))
        columnLine = line;
    else
        orders.Add(line);
}

int columns = int.Parse(columnLine.TrimEnd().Last().ToString());
List<Stack<string>> crates = new();

for (int i = 0; i < columns; i++)
    crates.Add(new Stack<string>());

crateLines.Reverse();

foreach (string crateLine in crateLines)
{
    string test = $"{crateLine} ";
    List<string> toAdd = new();
    for (int j = 0; j < test.Length; j += 4)
        toAdd.Add(test.Substring(j, 4));

    for (int i = 0; i < columns; i++)
        if (toAdd[i].Trim().Length > 0)
            crates[i].Push(toAdd[i]);
}

foreach (string order in orders)
{
    string[] splits = order.Split(' ');
    int cratesQuantity = int.Parse(splits[1]);
    int starting = int.Parse(splits[3]);
    int ending = int.Parse(splits[5]);

    List<string> temp = new();
    for (int i = 0; i < cratesQuantity; i++)
        temp.Add(crates[starting - 1].Pop());

    for (int j = 0; j < temp.Count; j++)
        crates[ending - 1].Push(temp[j]);
}

StringBuilder result = new();

foreach (Stack<string> crate in crates)
    result.Append(crate.Pop()[1]);

Console.WriteLine($"Result : {result}");
