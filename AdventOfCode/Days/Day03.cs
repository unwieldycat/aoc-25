namespace AdventOfCode.Days;

public class Day03 : IDay
{

	public static List<List<int>> ParseInput(string path)
	{
		string input = File.ReadAllText(path);
		string[] lines = input.Split("\n");
		List<List<int>> result = [];

		foreach (string line in lines)
		{
			if (line.Length == 0) continue;

			List<int> convertedLine = [];

			for (int i = 0; i < line.Length - 1; i++)
			{
				string slice = line.Substring(i, 1);
				int intval = int.Parse(slice);
				convertedLine.Add(intval);
			}

			result.Add(convertedLine);
		}

		return result;
	}

	private static int LargestJoltage(List<int> jolts)
	{
		int largest = 0;

		for (int i = 0; i < jolts.Count; i++)
		{
			int firstDigit = jolts[i] * 10;

			for (int j = i + 1; j < jolts.Count; j++)
			{
				int number = firstDigit + jolts[j];
				if (number > largest)
				{
					largest = number;
				}
			}
		}

		return largest;
	}

	private static long LargestDangerJoltage(List<int> jolts)
	{

		while (jolts.Count > 12)
		{
			for (int i = 0; i < jolts.Count - 1;)
			{
				if (jolts[i] <= jolts[i + 1])
				{

					jolts.RemoveAt(i);
					if (jolts.Count == 12) break;
				}
				else
				{
					i++;
				}
			}
		}

		long number = 0;
		for (int i = 0; i < jolts.Count; i++)
		{
			long value = jolts[i];
			for (int p = jolts.Count - 1; p > i; p--)
			{
				value *= 10;
			}
			number += value;
		}
		return number;
	}

	public static void Run(string[] args)
	{
		List<List<int>> testInput = ParseInput("Inputs/day03test.txt");

		int testTotal = testInput.Aggregate(0, (acc, line) =>
		{
			int joltage = LargestJoltage(line);
			return acc + joltage;
		});
		long testDangerTotal = testInput.Aggregate(0L, (acc, line) =>
		{
			long joltage = LargestDangerJoltage(line);
			Console.WriteLine(joltage);
			return acc + joltage;
		});
		Console.WriteLine($"Test Total: {testTotal}");
		Console.WriteLine($"Test Danger Total: {testDangerTotal}");

		List<List<int>> realInput = ParseInput("Inputs/day03real.txt");
		int realTotal = realInput.Aggregate(0, (acc, line) =>
		{
			int joltage = LargestJoltage(line);
			return acc + joltage;
		});
		long realDangerTotal = realInput.Aggregate(0L, (acc, line) =>
		{
			long joltage = LargestDangerJoltage(line);
			//Console.WriteLine(joltage);
			return acc + joltage;
		});
		Console.WriteLine($"Real Total: {realTotal}");
		Console.WriteLine($"Real Danger Total: {realDangerTotal}");

	}
}