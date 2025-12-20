using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public partial class Day06 : IDay
{
	[GeneratedRegex(@"\s+")]
	private static partial Regex WhitespaceSequence();

	private static (List<List<long>>, List<List<long>>) ParseInput(string path)
	{
		string text = File.ReadAllText(path).Trim();
		string[] lines = text.Split("\n");
		List<string[]> grid = [];

		foreach (var line in lines)
		{
			string normalizedLine = WhitespaceSequence().Replace(line.Trim(), " ");
			string[] lineSplit = normalizedLine.Split(" ");
			grid.Add(lineSplit);
		}

		List<List<long>> multiply = [];
		List<List<long>> add = [];

		for (int c = 0; c < grid[0].Length; c++)
		{
			bool isMultiply = grid[^1][c].Equals("*");
			List<long> nums = [];

			for (int r = 0; r < grid.Count - 1; r++)
			{
				long asLong = long.Parse(grid[r][c].Trim());
				nums.Add(asLong);
			}

			if (isMultiply)
			{
				multiply.Add(nums);
			}
			else
			{
				add.Add(nums);
			}
		}

		return (multiply, add);
	}

	private static long ParseInputCephalopod(string path)
	{
		string text = File.ReadAllText(path);
		string[] lines = text.Replace("\r", "").Split("\n");

		string[,] grid = new string[lines.Length, lines[0].Length];

		for (int i = 0; i < lines.Length; i++)
		{
			char[] lineSplit = lines[i].ToCharArray();
			for (int j = 0; j < lineSplit.Length; j++)
			{
				grid[i, j] = lineSplit[j].ToString();
			}
		}

		int gridWidth = grid.GetLength(1);
		int gridHeight = grid.GetLength(0);

		string lastOperator = grid[gridHeight - 1, 0];
		List<long> nums = [];
		long total = 0;

		for (int c = 0; c < gridWidth; c++)
		{
			string joinedColumn = "";
			for (int r = 0; r < gridHeight - 1; r++)
			{
				joinedColumn += grid[r, c];
			}
			joinedColumn = joinedColumn.Trim();

			if (joinedColumn.Length > 0)
			{
				long asLong = long.Parse(joinedColumn);
				nums.Add(asLong);
			}

			if (joinedColumn.Length == 0 || c == gridWidth - 1)
			{

				if (lastOperator.Equals("+"))
				{
					long localTotal = 0;

					foreach (long num in nums)
					{
						localTotal += num;
					}

					total += localTotal;
				}
				else
				{
					long localTotal = 0;
					foreach (long num in nums)
					{
						if (localTotal == 0)
						{
							localTotal = num;
						}
						else
						{
							localTotal *= num;
						}
					}
					total += localTotal;
				}
				nums.Clear();
				if (c < gridWidth - 1) lastOperator = grid[gridHeight - 1, c + 1];
			}

		}

		return total;
	}

	private static long GrandTotal(List<List<long>> multiply, List<List<long>> add)
	{
		long total = 0;

		foreach (var nums in multiply)
		{

			long result = 0;

			foreach (long num in nums)
			{
				if (result == 0)
				{
					result = num;
					continue;
				}

				result *= num;
			}

			total += result;
		}

		foreach (var nums in add)
		{
			long result = 0;

			foreach (long num in nums)
			{
				result += num;
			}

			total += result;
		}

		return total;
	}

	public static void Run(string[] args)
	{
		(List<List<long>>, List<List<long>>) testInput = ParseInput("Inputs/day06test.txt");
		long testTotal = GrandTotal(testInput.Item1, testInput.Item2);
		Console.WriteLine($"Test Grand Total: {testTotal}");
		long testCephalopodTotal = ParseInputCephalopod("Inputs/day06test.txt");
		Console.WriteLine($"Test Cephalopod Total: {testCephalopodTotal}");

		(List<List<long>>, List<List<long>>) input = ParseInput("Inputs/day06real.txt");
		long total = GrandTotal(input.Item1, input.Item2);
		Console.WriteLine($"Grand Total: {total}");
		long cephalopodTotal = ParseInputCephalopod("Inputs/day06real.txt");
		Console.WriteLine($"Cephalopod Total: {cephalopodTotal}");
	}
}