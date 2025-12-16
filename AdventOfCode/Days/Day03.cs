namespace AdventOfCode.Days;

public class Day03 : IDay
{

	public static List<int[]> ParseInput(string path)
	{
		string input = File.ReadAllText(path);
		string[] lines = input.Split("\n");
		List<int[]> result = [];

		foreach (string line in lines)
		{
			if (line.Length == 0) continue;

			int[] convertedLine = new int[line.Length - 1];

			for (int i = 0; i < line.Length - 1; i++)
			{
				string slice = line.Substring(i, 1);
				int intval = int.Parse(slice);
				convertedLine[i] = intval;
			}

			result.Add(convertedLine);
		}

		return result;
	}

	private static int LargestJoltage(int[] jolts)
	{
		int largest = 0;

		for (int i = 0; i < jolts.Length; i++)
		{
			int firstDigit = jolts[i] * 10;

			for (int j = i + 1; j < jolts.Length; j++)
			{
				int secondDigit = jolts[j];
				int number = firstDigit + secondDigit;
				if (number > largest)
				{
					largest = number;
				}
			}
		}

		return largest;
	}

	private static long LargestDangerJoltage(int[] jolts, int[] permutation, int index)
	{
		if (index >= jolts.Length)
		{
			if (permutation.Length == 12)
			{
				long number = 0;
				for (int i = 0; i < permutation.Length; i++)
				{
					long value = permutation[i];
					for (int p = permutation.Length - 1; p > i; p--)
					{
						value *= 10;
					}
					number += value;
				}
				return number;
			}
			else
			{
				return -1;
			}
		}

		int currentElement = jolts[index];
		long branchWithCurrent = LargestDangerJoltage(jolts, [.. permutation, currentElement], index + 1);
		long branchWithoutCurrent = LargestDangerJoltage(jolts, permutation, index + 1);

		return Math.Max(branchWithCurrent, branchWithoutCurrent);
	}

	private static long LargestDangerJoltage(int[] jolts)
	{
		return LargestDangerJoltage(jolts, [], 0);
	}

	public static void Run(string[] args)
	{
		List<int[]> testInput = ParseInput("Inputs/day03test.txt");
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

		List<int[]> realInput = ParseInput("Inputs/day03real.txt");
		int realTotal = realInput.Aggregate(0, (acc, line) =>
		{
			int joltage = LargestJoltage(line);
			return acc + joltage;
		});
		long realDangerTotal = realInput.Aggregate(0L, (acc, line) =>
		{
			long joltage = LargestDangerJoltage(line);
			Console.WriteLine(joltage);
			return acc + joltage;
		});
		Console.WriteLine($"Real Total: {realTotal}");
		Console.WriteLine($"Real Danger Total: {realDangerTotal}");


	}
}