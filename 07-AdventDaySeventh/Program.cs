using _07_AdventDaySeventh;

string[] data = File.ReadAllLines($"{Environment.CurrentDirectory}/data.txt");

DirectoryModel root = new("/", null);
ParseOutput(root, data);

AllocateDirectoriesSizes(root);

// Print recursively all size of directories of root
int result = CalculateResult(root);

Console.WriteLine($"Result : {result}");

void ParseOutput(DirectoryModel directoryModel, string[] data)
{
    DirectoryModel currentDirectory = directoryModel;
    
    foreach (string line in data)
    {
        switch (line)
        {
            case string s when s.StartsWith(@"$ cd /"):
                currentDirectory = directoryModel;
                break;
            case string s when s.StartsWith(@"$ cd .."):
                currentDirectory = currentDirectory.Parent;
                break;
            case string s when s.StartsWith(@"$ cd "):
                var test = line.Split(' ').Last().Trim();
                currentDirectory = currentDirectory.SubDirectories.FirstOrDefault(x => x.Name == test);
                break;
            case string s when s.StartsWith("dir"):
                var dir = line.Split(' ').Last().Trim();
                currentDirectory.SubDirectories.Add(new(dir, currentDirectory));
                break;
            default:
                if (Char.IsNumber(line[0]))
                {
                    var fileName = line.Split(' ').Last().Trim();
                    var fileSize = int.Parse(line.Split(' ').First().Trim());
                    currentDirectory.Files.Add(new(fileName, fileSize));
                }
                break;
        }
    }
}

void AllocateDirectoriesSizes(DirectoryModel directoryModel)
{
    foreach (DirectoryModel subDirectory in directoryModel.SubDirectories)
    {
        AllocateDirectoriesSizes(subDirectory);
        directoryModel.Size += subDirectory.Size;
    }

    foreach (FileModel file in directoryModel.Files)
    {
        directoryModel.Size += file.Size;
    }
}

int CalculateResult(DirectoryModel directoryModel)
{
    int result = 0;

    foreach (DirectoryModel subDirectory in directoryModel.SubDirectories)
    {
        result += CalculateResult(subDirectory);
    }

    if (directoryModel.Size <= 100000)
        result += directoryModel.Size;

    return result;
}
