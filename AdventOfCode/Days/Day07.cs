using System.Text;

namespace AdventOfCode.Days;

public class Day07 : IDay
{
	private static char[,] ParseInput(string path)
	{
		string input = File.ReadAllText(path).Trim();
		string[] lines = input.Replace("\r", "").Split("\n");

		char[,] grid = new char[lines.Length, lines[0].Length];

		for (int r = 0; r < lines.Length; r++)
		{
			char[] chars = lines[r].ToCharArray();
			for (int c = 0; c < lines[0].Length; c++)
			{
				grid[r, c] = chars[c];
			}
		}

		return grid;
	}

	public static int GenerateBeam(char[,] grid)
	{
		int rows = grid.GetLength(0);
		int cols = grid.GetLength(1);
		int splits = 0;

		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				// Source beam
				if (grid[r, c] == 'S') grid[r + 1, c] = '|';

				if (r - 1 >= 0 && grid[r - 1, c] == '|')
				{
					// Space under beam
					if (grid[r, c] == '.')
					{
						grid[r, c] = '|';
					}
					else if (grid[r, c] == '^') // Splitter under beam
					{
						// Space to left of splitter
						if (c - 1 >= 0 && grid[r, c - 1] == '.')
						{
							grid[r, c - 1] = '|';
						}
						// Space to the right of splitter
						if (c + 1 < cols && grid[r, c + 1] == '.')
						{
							grid[r, c + 1] = '|';
						}

						splits++;
					}
				}
			}
		}

		return splits;
	}

	public static string CreateMemoKey(char[,] grid, int row)
	{
		int rows = grid.GetLength(0);
		int cols = grid.GetLength(1);
		StringBuilder sb = new();

		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				sb.Append(grid[r, c]);
			}
			sb.Append('\n');
		}

		return $"{row}:{sb}";
	}

	public static int GenerateTimelines(char[,] grid, int r, Dictionary<string, int> memo)
	{
		string memoKey = CreateMemoKey(grid, r);
		if (memo.TryGetValue(memoKey, out int cachedResult))
		{
			return cachedResult;
		}

		if (r >= grid.GetLength(0))
		{
			return 1;
		}

		int cols = grid.GetLength(1);
		int timelines = 0;

		List<char[,]> newGrids = [];
		bool hitSplitter = false;

		for (int c = 0; c < cols; c++)
		{
			// Source beam
			if (grid[r, c] == 'S')
			{
				grid[r + 1, c] = '|';
			}

			if (r - 1 >= 0 && grid[r - 1, c] == '|')
			{
				// Space under beam
				if (grid[r, c] == '.')
				{
					grid[r, c] = '|';
				}
				else if (grid[r, c] == '^') // Splitter under beam
				{
					hitSplitter = true;

					// Space to left of splitter
					if (c - 1 >= 0 && grid[r, c - 1] == '.')
					{
						char[,] newGrid = (char[,])grid.Clone();
						newGrid[r, c - 1] = '|';
						newGrids.Add(newGrid);
					}
					// Space to the right of splitter
					if (c + 1 < cols && grid[r, c + 1] == '.')
					{
						char[,] newGrid = (char[,])grid.Clone();
						newGrid[r, c + 1] = '|';
						newGrids.Add(newGrid);
					}
				}
			}
		}

		if (!hitSplitter) newGrids.Add(grid);

		foreach (char[,] newGrid in newGrids)
		{
			timelines += GenerateTimelines(newGrid, r + 1, memo);
		}

		memo[memoKey] = timelines;

		return timelines;
	}

	public static void PrintGrid(char[,] grid)
	{
		for (int r = 0; r < grid.GetLength(0); r++)
		{
			for (int c = 0; c < grid.GetLength(1); c++)
			{
				Console.Write(grid[r, c]);
			}
			Console.WriteLine();
		}
	}

	public static int GenerateTimelines(char[,] grid)
	{
		return GenerateTimelines(grid, 0, []);
	}

	public static void Run(string[] args)
	{
		char[,] testInput = ParseInput("Inputs/day07test.txt");
		int testSplits = GenerateBeam((char[,])testInput.Clone());
		Console.WriteLine($"Test Splits: {testSplits}");
		int testTimelines = GenerateTimelines(testInput);
		Console.WriteLine($"Test Timelines: {testTimelines}");

		char[,] input = ParseInput("Inputs/day07real.txt");
		int splits = GenerateBeam((char[,])input.Clone());
		Console.WriteLine($"Splits: {splits}");
		int timelines = GenerateTimelines(input);
		Console.WriteLine($"Timelines: {timelines}");
	}
}