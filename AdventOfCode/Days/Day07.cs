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

	public static void Run(string[] args)
	{
		char[,] testInput = ParseInput("Inputs/day07test.txt");
		int testSplits = GenerateBeam(testInput);
		Console.WriteLine($"Test Splits: {testSplits}");

		char[,] input = ParseInput("Inputs/day07real.txt");
		int splits = GenerateBeam(input);
		Console.WriteLine($"Splits: {splits}");
	}
}