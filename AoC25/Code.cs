namespace AoC25
{
	public static class Code
	{
		internal static List<string> GetTestData(int dayIndex)
		{
			//validate input ...
			//$(MSBuildProjectName)
			return System.IO.File.ReadAllLines($"C:\\Users\\ojdav\\source\\repos\\AdventOfCode2025Project\\AoC25\\res\\TestData\\D{dayIndex}.txt").ToList();
		}

		internal static List<string> GetData(int dayIndex)
		{
			//validate input ...

			return System.IO.File.ReadAllLines($"C:\\Users\\ojdav\\source\\repos\\AdventOfCode2025Project\\AoC25\\res\\Data\\D{dayIndex}.txt").ToList();
		}

		//	internal interface IDay
		//	{
		//		public string Run(int partIndex);
		//		public string Run(int partIndex, bool runTestData);


		//		private static string PartOne(bool runTestData)
		//		{
		//			throw new NotImplementedException();
		//		}
		//		private static string PartTwo(bool runTestData)
		//		{
		//			throw new NotImplementedException();
		//		}

		//	}
		//}

		//public class Day : AoC25.Code.IDay
		//{
		//	public static string Run(int partIndex)
		//	{
		//		switch (partIndex)
		//		{
		//			case 1: return PartOne(false);
		//			case 2: return PartTwo(false);
		//			default: return "Invalid part index.";
		//		}
		//	}
		//	public static string Run(int partIndex, bool runTestData)
		//	{
		//		switch (partIndex)
		//		{
		//			case 1: return PartOne(runTestData);
		//			case 2: return PartTwo(runTestData);
		//			default: return "Invalid part index.";
		//		}
		//	

		//	private static string PartOne(bool runTestData)
		//	{
		//		throw new NotImplementedException();
		//	}

		//	private static string PartTwo(bool runTestData)
		//	{
		//		throw new NotImplementedException();
		//	}
		//
		// }
		//}
	}
}
