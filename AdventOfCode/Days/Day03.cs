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
		List<int> optimizedNumber = [];

		int prevMaxIdx = 0;

		for (int n = 11; n >= 0; n--)
		{
			int max = 0;
			int maxIdx = 0;

			for (int i = prevMaxIdx; i < jolts.Count - n; i++)
			{
				int digit = jolts[i];
				if (digit > max)
				{
					maxIdx = i;
					max = digit;
				}
			}

			jolts[maxIdx] = -1;
			prevMaxIdx = maxIdx;
			optimizedNumber.Add(max);
		}


		// Makes the final number a number
		long number = 0;
		for (int i = 0; i < optimizedNumber.Count; i++)
		{
			long value = optimizedNumber[i];
			for (int p = optimizedNumber.Count - 1; p > i; p--)
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
			return acc + joltage;
		});
		Console.WriteLine($"Real Total: {realTotal}");
		Console.WriteLine($"Real Danger Total: {realDangerTotal}");

	}
}