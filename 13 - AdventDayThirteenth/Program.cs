//string data = File.ReadAllText($"{Environment.CurrentDirectory}/dataTest.txt");
//string[] lines = data.Split("\r\n\r\n");

//int result = 0;


//for (int i = 0; i < lines.Length; i++)
//{
//    string[] split = lines[i].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
//    var left = split[0].RemoveExtremities();
//    var right = split[1].RemoveExtremities();

//    if (string.IsNullOrEmpty(left) || string.IsNullOrEmpty(right))
//        return;

//    var leftTest = left.Split(',');
//    var rightTest = right.Split(',');
//    for (int j = 0; j < left.Length; j++)
//    {
//        if (leftTest[j] == rightTest[j])
//            continue;

//        if (leftTest[j].StartsWith('['))
//        {
//            if (rightTest[j].StartsWith('['))
//            {

//            }
//            else
//            {
//                rightTest[j] = $"[{rightTest[j]}]";

//                if (rightTest[j] == leftTest[j])
//                    continue;
//                else
//                {
//                    result++;
//                    continue;
//                }
//            }
//            Console.WriteLine("It does start with [");
//        }

//        if (int.TryParse(leftTest[j], out int leftValue) && int.TryParse(rightTest[j], out int rightValue))
//        {
//            if (leftValue < rightValue)
//            {
//                result += i;
//                break;
//            }
//        }
//    }

//    Console.WriteLine($"Left: {left} Right: {right}");
//}

//Console.WriteLine($"Result : {result}");

//static class Extensions
//{
//    public static string RemoveExtremities(this string str)
//    {
//        str = str.Remove(str.Length - 1);
//        str = str.Remove(0, 1);

//        return str;
//    }
//}

using System.Text.Json.Nodes;

var input = File.ReadAllText($"{Environment.CurrentDirectory}/dataTest.txt");

var pairs = input.Split("\r\n\r\n");
var pairIndex = 0;
var correctPairs = 0;

foreach (var pair in pairs)
{
    pairIndex++;

    string[] splitPair = pair.Split("\r\n");
    string left = splitPair[0];
    string right = splitPair[1];

    JsonNode? jsonLeft = JsonNode.Parse(left);
    JsonNode? jsonRight = JsonNode.Parse(right);
    
    bool? isCorrect = Compare(jsonLeft, jsonRight);
    
    if (isCorrect == true) 
        correctPairs += pairIndex;
}

Console.WriteLine($"Part 1: {correctPairs}");

// Part 2
var allPackets = input.Split("\r\n")
    .Where(packet => !string.IsNullOrEmpty(packet))
    .Select(l => JsonNode.Parse(l))
    .ToList();

var x = JsonNode.Parse("[[2]]");
var y = JsonNode.Parse("[[6]]");

allPackets.Add(x);
allPackets.Add(y);

allPackets.Sort((left, right) => Compare(left, right) == true ? -1 : 1);

Console.WriteLine($"Part 2: {(allPackets.IndexOf(x) + 1) * (allPackets.IndexOf(y) + 1)}");

bool? Compare(JsonNode left, JsonNode right)
{
    if (left is JsonValue leftVal && right is JsonValue rightVal)
    {
        var leftInt = leftVal.GetValue<int>();
        var rightInt = rightVal.GetValue<int>();
        
        return leftInt == rightInt 
            ? null 
            : leftInt < rightInt;
    }

    if (left is not JsonArray leftArray) 
        leftArray = new JsonArray(left.GetValue<int>());
    
    if (right is not JsonArray rightArray) 
        rightArray = new JsonArray(right.GetValue<int>());

    for (var i = 0; i < Math.Min(leftArray.Count, rightArray.Count); i++)
    {
        var res = Compare(leftArray[i], rightArray[i]);

        if (res.HasValue)
            return res.Value;
    }

    if (leftArray.Count < rightArray.Count)
        return true;
    
    if (leftArray.Count > rightArray.Count)
        return false;

    return null;
}