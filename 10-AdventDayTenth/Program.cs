using System.Drawing;

string[] data = File.ReadAllLines($"{Environment.CurrentDirectory}/data.txt");

int register = 1;
int cycle = 0;
int signalStrength = 0;
string crt = string.Empty;

foreach (string line in data)
{
    LogLine(line);

    (Instruction instruction, int value) = Parse(line);

    switch (instruction)
    {
        case Instruction.addx:
            Addx(value);
            break;
        case Instruction.noop:
            Noop();
            break;
    }
}

Console.WriteLine($"Result 1 : {signalStrength}");
Console.WriteLine("Result 2 : ");
for (int i = 0; i < 6; i++)
{
    Console.WriteLine(crt.Substring(i * 40, 40));
}

void Draw()
{
    int diff = cycle % 40 - register;

    if (diff >= 0 && diff <= 2)
        crt += "#";
    else
        crt += ".";
}

(Instruction instruction, int value) Parse(string line)
{
    string[] split = line.Split(' ');

    return ((Instruction)Enum.Parse(typeof(Instruction), split[0]), split.Length == 2 ? int.Parse(split[1]) : 0);
}

void Addx(int value)
{
    cycle += 1;
    CheckSignalStrength();
    Draw();
    Log();
    cycle += 1;
    CheckSignalStrength();
    Draw();
    Log();
    register += value;
}

void Noop()
{
    cycle++;
    CheckSignalStrength();
    Draw();
    Log();
}

void CheckSignalStrength()
{
    if ((cycle == 20 || (cycle - 20) % 40 == 0) && cycle <= 220)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($">>>>>> On est dans le check : {cycle}");
        signalStrength += cycle * register;
    }
}

void LogLine(string line)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($">>> {line} <<<");
}

void Log()
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine($"Cycle = {cycle} | Register = {register} | Signal Strength = {cycle * register}");
}

public enum Instruction
{
    addx,
    noop
}