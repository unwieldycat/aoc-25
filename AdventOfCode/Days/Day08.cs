namespace AdventOfCode.Days;

class Day08 : IDay
{
	private static List<(int, int, int)> ParseInput(string path)
	{
		string input = File.ReadAllText(path).Trim();
		string[] lines = input.Replace("\r", "").Split("\n");

		List<(int, int, int)> coordinates = [];

		foreach (string line in lines)
		{
			string[] nums = line.Split(",");
			int[] ints = nums.Select((str, _) => { return int.Parse(str); }).ToArray();
			coordinates.Add((ints[0], ints[1], ints[2]));
		}

		return coordinates;
	}

	private static double GetDistance((int, int, int) a, (int, int, int) b)
	{
		return Math.Sqrt(Math.Pow(b.Item1 - a.Item1, 2) + Math.Pow(b.Item2 - a.Item2, 2) + Math.Pow(b.Item3 - a.Item3, 2));
	}

	private static void MakeShortestConnections(List<(int, int, int)> junctions)
	{
		List<List<(int, int, int)>> connected = [];
		int numConnections = 0;

		foreach (var jct in junctions)
		{
			if (numConnections > 10) break;
			double shortestDistance = double.MaxValue;
			(int, int, int) shortestOther = (-1, -1, -1);
			foreach (var otherJct in junctions)
			{
				if (otherJct == jct) continue;

				double distance = GetDistance(jct, otherJct);
				if (distance < shortestDistance)
				{
					shortestDistance = distance;
					shortestOther = otherJct;
				}
			}

			bool addedToCircuit = false;
			foreach (var circuit in connected)
			{
				if (circuit.Contains(jct) && !circuit.Contains(shortestOther))
				{
					circuit.Add(shortestOther);
					addedToCircuit = true;
				}
				else if (circuit.Contains(shortestOther) && !circuit.Contains(jct))
				{
					circuit.Add(jct);
					addedToCircuit = true;
				}
				else if (circuit.Contains(jct) && circuit.Contains(shortestOther))
				{
					addedToCircuit = true;
				}
			}
			if (!addedToCircuit) connected.Add([jct, shortestOther]);
			numConnections++;
		}

		Console.WriteLine($"Total circuits formed: {connected.Count}");
		foreach (var circuit in connected)
		{
			Console.WriteLine($"Circuit with {circuit.Count} junctions: ");
			foreach (var jct in circuit)
			{
				Console.WriteLine($"  Junction at ({jct.Item1}, {jct.Item2}, {jct.Item3})");
			}
		}
	}

	public static void Run(string[] args)
	{
		var testInput = ParseInput("Inputs/day08test.txt");
		MakeShortestConnections(testInput);
	}
}