namespace _07_AdventDaySeventh;

public class DirectoryModel
{
    public DirectoryModel(string name, DirectoryModel? parent)
    {
        Name = name;
        Parent = parent;
    }

    public string Name { get; set; }

    public DirectoryModel? Parent { get; set; }

    public List<DirectoryModel> SubDirectories { get; set; } = new();

    public List<FileModel> Files { get; set; } = new();

    public int Size { get; set; }
}