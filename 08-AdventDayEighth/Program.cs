using System.Text;

string[] data = File.ReadAllLines($"{Environment.CurrentDirectory}/data.txt");
int result = 0;

for (int i = 0; i < data.Length; i++)
{
    if (i == 0 || i == data.Length - 1)
    {
        result += data[i].Length;
        continue;
    }

    result += 2;

    for (int j = 1; j < data[i].Length - 1; j++)
    {
        int tree = int.Parse(data[i][j].ToString());

        int left = CheckLeft(tree, ConvertToIntArray(data[i][0..j]));
        int right = CheckRight(tree, ConvertToIntArray(data[i][(j+1)..]));
        int top = CheckTop(tree, ConvertToIntArray(ConvertColumnToRow(data, j, i, true)));
        int bottom = CheckBottom(tree, ConvertToIntArray(ConvertColumnToRow(data, j, i, false)));

        if (left ==1 || right == 1 || top == 1 || bottom == 1)
            result++;
    }
}

Console.WriteLine($"Result 1 : {result}");

// Part 2
int newResult = 0;
for (int i = 0; i < data.Length; i++)
{
    for (int j = 0; j < data[i].Length; j++)
    {
        int tree = int.Parse(data[i][j].ToString());

        int leftSight = CheckSight(tree, ConvertToIntArray(data[i][0..j]).Reverse().ToArray());
        int rightSight = CheckSight(tree, ConvertToIntArray(data[i][(j + 1)..]));
        int bottomSight = CheckSight(tree, ConvertToIntArray(ConvertColumnToRow(data, j, i, false)));
        int topSight = CheckSight(tree, ConvertToIntArray(ConvertColumnToRow(data, j, i, true)).Reverse().ToArray());

        int total = leftSight * rightSight * bottomSight * topSight;

        if (newResult < total)
            newResult = total;
    }
}

Console.WriteLine($"Result 2 : {newResult}");

int CheckSight(int treeSize, int[] leftTrees)
{
    int result = 0;
    for (int i = 0; i < leftTrees.Length; i++)
    {
        if (leftTrees[i] >= treeSize)
        {
            result++;
            return result;
        }

        result++;
    }

    return result;
}

int CheckLeft(int tree, int[] leftTrees)
{
    for (int i = 0; i < leftTrees.Length; i++)
    {
        if (leftTrees[i] >= tree)
            return 0;
    }

    return 1;
}

int CheckRight(int tree, int[] rightTrees)
{
    for (int i = 0; i < rightTrees.Length; i++)
    {
        if (rightTrees[i] >= tree)
            return 0;
    }

    return 1;
}

int CheckTop(int tree, int[] topTrees)
{
    for (int i = 0; i < topTrees.Length; i++)
    {
        if (topTrees[i] >= tree)
            return 0;
    }

    return 1;
}

int CheckBottom(int tree, int[] bottomTrees)
{
    for (int i = 0; i < bottomTrees.Length; i++)
    {
        if (bottomTrees[i] >= tree)
            return 0;
    }

    return 1;
}

int[] ConvertToIntArray(string trees) =>
    trees.Select(x => int.Parse(x.ToString())).ToArray();

string ConvertColumnToRow(string[] data, int column, int index, bool isTop)
{
    StringBuilder result = new();

    for (int i = 0; i < data.Length; i++)
    {
        if (i == index || (isTop && i > index) || (!isTop && i < index))
            continue;

        result.Append(data[i][column]);
    }

    return result.ToString();
}