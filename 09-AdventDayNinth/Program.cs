var data = File.ReadAllLines($"{Environment.CurrentDirectory}/data.txt")
    .Select(line => new { Direction = line[0], Distance = int.Parse(line.Substring(2)) });

Console.WriteLine($"Result 1 : {Visited(2)}");
Console.WriteLine($"Result 2 : {Visited(10)}");

int Visited(int numberOfKnots)
{
    HashSet<(int, int)> visited = new();
    var knots = new (int X, int Y)[numberOfKnots];

    foreach (var move in data)
    {
        for (int i = 0; i < move.Distance; i++)
        {
            knots[0] = move.Direction switch
            {
                'L' => (--knots[0].X, knots[0].Y),
                'R' => (++knots[0].X, knots[0].Y),
                'U' => (knots[0].X, ++knots[0].Y),
                'D' => (knots[0].X, --knots[0].Y),
                _ => throw new InvalidOperationException("we broke"),
            };

            for (int j = 1; j < numberOfKnots; j++)
            {
                int xDist = knots[j - 1].X - knots[j].X;
                int yDist = knots[j - 1].Y - knots[j].Y;

                if (Math.Abs(xDist) > 1 || Math.Abs(yDist) > 1)
                {
                    knots[j].X += Math.Sign(xDist);
                    knots[j].Y += Math.Sign(yDist);
                }
            }

            visited.Add(knots.Last());
        }
    }

    return visited.Count;
}