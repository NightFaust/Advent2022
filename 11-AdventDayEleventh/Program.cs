string[] data = File.ReadAllLines($"{Environment.CurrentDirectory}/data.txt");

Parse(data);
foreach (string line in data)
{
    Console.WriteLine(line);
}

void Parse(string[] data)
{
    foreach (string line in data)
    {
        string[] split = line.Split(' ');

        switch (split[0])
        {

        }
    }
}
