class Utils
{
	public static int Modulus(int a, int b)
	{
		int r = a % b;
		return r < 0 ? r + b : r;
	}

	public static long SumList(List<long> nums)
	{
		long sum = 0;

		foreach (var num in nums)
		{
			sum += num;
		}

		return sum;
	}
}