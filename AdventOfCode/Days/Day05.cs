namespace AdventOfCode.Days;

public class Day05 : IDay
{

	private static (List<(long, long)>, List<long>) ParseInput(string path)
	{
		string text = File.ReadAllText(path);
		string[] lines = text.Split("\n");

		bool partTwo = false;

		List<(long, long)> ranges = [];
		List<long> numbers = [];

		foreach (string line in lines)
		{
			string lineTrimmed = line.Trim();

			if (lineTrimmed.Equals(""))
			{
				partTwo = true;
				continue;
			}

			if (!partTwo)
			{
				string[] stringRange = lineTrimmed.Split("-");
				long lowerBound = long.Parse(stringRange[0]);
				long upperBound = long.Parse(stringRange[1]);
				ranges.Add((lowerBound, upperBound));
			}
			else
			{
				long num = long.Parse(lineTrimmed);
				numbers.Add(num);
			}
		}

		return (ranges, numbers);
	}

	private static long GetFreshCount(List<(long, long)> ranges, List<long> numbers)
	{
		long freshCount = 0;

		foreach (long num in numbers)
		{
			foreach (var range in ranges)
			{
				if (num >= range.Item1 && num <= range.Item2)
				{
					freshCount++;
					break;
				}
			}
		}

		return freshCount;

	}

	private static List<(long, long)> MergeRanges(List<(long, long)> ranges)
	{
		ranges.Sort((a, b) => a.Item1.CompareTo(b.Item1));
		List<(long, long)> newRanges = [];

		(long, long) currentRange = ranges[0];

		foreach (var nextRange in ranges[1..])
		{
			if (nextRange.Item1 <= currentRange.Item2)
			{
				currentRange.Item2 = Math.Max(nextRange.Item2, currentRange.Item2);
			}
			else
			{
				newRanges.Add(currentRange);
				currentRange = nextRange;
			}
		}

		newRanges.Add(currentRange);

		return newRanges;
	}

	private static long GetFreshRangesCount(List<(long, long)> ranges)
	{
		List<(long, long)> mergedRanges = MergeRanges(ranges);

		long total = 0;

		foreach (var range in mergedRanges)
		{
			total += range.Item2 - range.Item1 + 1;
		}

		return total;
	}

	public static void Run(string[] args)
	{
		(List<(long, long)>, List<long>) testInput = ParseInput("Inputs/day05test.txt");
		long testFresh = GetFreshCount(testInput.Item1, testInput.Item2);
		Console.WriteLine($"Test fresh count: {testFresh}");
		long testTotal = GetFreshRangesCount(testInput.Item1);
		Console.WriteLine($"Test fresh range count: {testTotal}");

		(List<(long, long)>, List<long>) realInput = ParseInput("Inputs/day05real.txt");
		long fresh = GetFreshCount(realInput.Item1, realInput.Item2);
		Console.WriteLine($"Real fresh count: {fresh}");
		long total = GetFreshRangesCount(realInput.Item1);
		Console.WriteLine($"Real fresh range count: {total}");

	}
}
