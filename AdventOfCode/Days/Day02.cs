namespace AdventOfCode.Days;

public class Day02 : IDay
{
	public static Tuple<long, long>[] ParseInput(string path)
	{
		string input = File.ReadAllText(path);
		string[] ranges = input.Split(",");

		Tuple<long, long>[] result = new Tuple<long, long>[ranges.Length];

		for (int i = 0; i < ranges.Length; i++)
		{
			string[] numberStrings = ranges[i].Split("-");
			long firstNumber = long.Parse(numberStrings[0]);
			long secondNumber = long.Parse(numberStrings[1]);
			result[i] = new Tuple<long, long>(firstNumber, secondNumber);
		}

		return result;
	}


	public static List<long> GetInvalidIDs(Tuple<long, long>[] ids)
	{
		List<long> invalidIds = [];

		foreach (var range in ids)
		{
			for (long num = range.Item1; num <= range.Item2; num++)
			{
				string stringNum = num.ToString();

				// Odd lengths cant have a twice repeat
				if (stringNum.Length % 2 != 0) continue;

				string firstHalf = stringNum[(stringNum.Length / 2)..];
				string secondHalf = stringNum[0..(stringNum.Length / 2)];

				if (firstHalf == secondHalf) invalidIds.Add(num);
			}
		}

		return invalidIds;
	}

	public static List<long> GetAllInvalidIDs(Tuple<long, long>[] ids)
	{
		List<long> invalidIds = [];

		foreach (var range in ids)
		{
			for (long num = range.Item1; num <= range.Item2; num++)
			{
				string stringNum = num.ToString();

				for (int i = 1; i <= (stringNum.Length / 2); i++)
				{
					string splice = stringNum[0..i];
					string newString = stringNum.Replace(splice, "");

					if (newString.Length == 0)
					{
						invalidIds.Add(num);
						break;
					}
				}
			}
		}

		return invalidIds;
	}

	public static void Run(string[] args)
	{
		Tuple<long, long>[] testRanges = ParseInput("Inputs/day02test.txt");
		List<long> invalidTest = GetInvalidIDs(testRanges);
		List<long> allInvalidTest = GetAllInvalidIDs(testRanges);
		Console.WriteLine($"Invalid Sum (Test): {Utils.SumList(invalidTest)}");
		Console.WriteLine($"All Invalid (Test): {Utils.SumList(allInvalidTest)}");


		Tuple<long, long>[] ranges = ParseInput("Inputs/day02real.txt");
		List<long> invalid = GetInvalidIDs(ranges);
		List<long> allInvalid = GetAllInvalidIDs(ranges);
		Console.WriteLine($"Invalid Sum: {Utils.SumList(invalid)}");
		Console.WriteLine($"All Invalid: {Utils.SumList(allInvalid)}");

	}
}