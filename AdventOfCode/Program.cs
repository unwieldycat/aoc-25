using AdventOfCode.Days;

class Program
{
	public static void Main(string[] args)
	{
		if (args.Length == 0)
		{
			Console.WriteLine("No arguments provided.");
			return;
		}

		string day = args[0];
		string[] remainingArgs = args[1..];

		switch (day.ToLower())
		{
			case "day01":
				Day01.Run(remainingArgs);
				break;
			default:
				Console.WriteLine($"Invalid day: {day}");
				break;
		}
	}
}