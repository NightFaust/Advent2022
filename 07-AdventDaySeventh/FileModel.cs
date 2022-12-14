namespace _07_AdventDaySeventh;

public class FileModel
{
    public FileModel(string name, int size)
    {
        Name = name;
        Size = size;
    }

    public string Name { get; set; }

    public int Size { get; set; }
}
