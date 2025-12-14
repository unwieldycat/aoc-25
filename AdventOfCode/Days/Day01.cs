namespace AdventOfCode.Days;

public class Day01 : IDay
{
	private static int[] ParseInput(string path)
	{
		string input = File.ReadAllText(path);
		string[] lines = input.Trim().Split("\n");

		int[] turns = new int[lines.Length];

		for (int i = 0; i < lines.Length; i++)
		{
			string line = lines[i];

			try
			{
				turns[i] = int.Parse(line[1..]);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error encountered parsing file: {ex.Message}");
				throw;
			}

			if (line.StartsWith('L'))
			{
				turns[i] *= -1;
			}
		}

		return turns;
	}

	private static int TurnDial(int start, int[] turns)
	{
		int zeroes = 0;
		int value = start;

		foreach (int turn in turns)
		{
			value = (value + turn) % 100;
			if (value == 0) zeroes++;
		}

		return zeroes;
	}


	public static void Run(string[] args)
	{
		int[] testTurns = ParseInput("Inputs/day01test.txt");
		int testCode = TurnDial(50, testTurns);
		Console.WriteLine($"Test Code: {testCode}");

		int[] realTurns = ParseInput("Inputs/day01real.txt");
		int realCode = TurnDial(50, realTurns);
		Console.WriteLine($"Real Code: {realCode}");


	}

}