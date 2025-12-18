namespace AdventOfCode.Days;

public class Day04 : IDay
{
	private static List<List<string>> ParseInput(string path)
	{
		List<List<string>> board = [];
		string input = File.ReadAllText(path);
		string[] lines = input.Split("\n");
		foreach (string line in lines)
		{
			List<string> lineSplit = [.. line.Trim().ToCharArray().Select(c => c.ToString())];
			board.Add(lineSplit);
		}
		return board;
	}

	private static int CheckTile(List<List<string>> board, int row, int col)
	{
		if (row < 0 || row >= board.Count) return 0;
		if (col < 0 || col >= board[0].Count) return 0;

		return board[row][col] == "@" ? 1 : 0;
	}

	private static List<(int, int)> GetAccessible(List<List<string>> board)
	{
		List<(int, int)> accessible = [];

		for (int r = 0; r < board.Count; r++)
		{
			for (int c = 0; c < board[0].Count; c++)
			{
				if (board[r][c] != "@") continue;
				int adjacent = 0;

				// Hella nested loops but this looks better than 8 function calls
				for (int or = -1; or <= 1; or++)
				{
					for (int oc = -1; oc <= 1; oc++)
					{
						adjacent += CheckTile(board, r + or, c + oc);
					}
				}

				if (adjacent <= 4) accessible.Add((r, c));
			}
		}

		return accessible;
	}

	private static int CountAccessible(List<List<string>> board)
	{
		List<(int, int)> accessible = GetAccessible(board);
		return accessible.Count;
	}

	private static int TotalRemovable(List<List<string>> board)
	{
		List<(int, int)> accessible;
		int count = 0;

		do
		{
			accessible = GetAccessible(board);
			count += accessible.Count;
			foreach (var (row, col) in accessible)
			{
				board[row][col] = ".";
			}
		} while (accessible.Count > 0);

		return count;

	}

	public static void Run(string[] args)
	{
		List<List<string>> testBoard = ParseInput("Inputs/day04test.txt");
		int testAccessible = CountAccessible(testBoard);
		Console.WriteLine($"Test accessible {testAccessible} ");
		int testRemovable = TotalRemovable(testBoard);
		Console.WriteLine($"Test removable {testRemovable}");

		List<List<string>> realBoard = ParseInput("Inputs/day04real.txt");
		int accessible = CountAccessible(realBoard);
		Console.WriteLine($"Real accessible {accessible} ");
		int removable = TotalRemovable(realBoard);
		Console.WriteLine($"Real removable {removable}");
	}
}