
namespace AoC25
{
	//public class Aoc25
	//{
	//	public string Run(int day, int part, bool test)
	//	{
			
	//		var result = (day == 1) ? new AoC25.Aoc25() ; 

	//		//switch (day)
	//		//{
	//		//	//case 1: return Day1.Run(part, runTestData);
	//		//	//case 2: return Day2.Run(part, runTestData);
	//		//	case 3: return Day3.Run(part, runTestData);
	//		//	case 4: return Day4.Run(part, runTestData);
	//		//	case 5: return Day5.Run(part, runTestData);
	//		//	case 6: return Day6.Run(part, runTestData);
	//		//	case 7: return Day7.Run(part, runTestData);
	//		//	case 8: return Day8.Run(part, runTestData);
	//		//	//case 9: return Day9.Run(part, runTestData);
	//		//	//case 10: return Day10.Run(part, runTestData);
	//		//	//case 11: return Day11.Run(part, runTestData);
	//		//	//case 12: return Day12.Run(part, runTestData);

	//		//	default: return "Invalid day index.";
	//		//}
	//		throw new NotImplementedException();
	//	}

	//}
	public static class Code
	{
		public static string Run(int day, int part, bool runTestData)
		{
			switch(day)
			{
				case 1:  return Calendar.Day1. Run(part, runTestData);
				case 2:  return Calendar.Day2. Run(part, runTestData);
				case 3:  return Calendar.Day3. Run(part, runTestData);
				case 4:  return Calendar.Day4. Run(part, runTestData);
				case 5:  return Calendar.Day5. Run(part, runTestData);
				case 6:  return Calendar.Day6. Run(part, runTestData);
				case 7:  return Calendar.Day7. Run(part, runTestData);
				case 8:  return Calendar.Day8. Run(part, runTestData);
				case 9:  return Calendar.Day9. Run(part, runTestData);
				case 10: return Calendar.Day10.Run(part, runTestData);
				case 11: return Calendar.Day11.Run(part, runTestData);
				case 12: return Calendar.Day12.Run(part, runTestData);

				default: return "Invalid day index.";
			}
		}


		internal static List<string> GetTestData(int dayIndex)
		{
			//validate input ...
			//$(MSBuildProjectName)

			//return System.IO.File.ReadAllLines($"C:\\Users\\ojdav\\source\\repos\\AdventOfCode2025Project\\AoC25\\res\\TestData\\D{dayIndex}.txt").ToList();
			var root = System.IO.Directory.GetCurrentDirectory().Substring(0, (System.IO.Directory.GetCurrentDirectory().IndexOf("AdventOfCode2025Project\\") + 24));
			return System.IO.File.ReadAllLines($"{root}\\AoC25\\res\\TestData\\D{dayIndex}.txt").ToList();
		}

		internal static List<string> GetData(int dayIndex)
		{
			//validate input ...

			//return System.IO.File.ReadAllLines($"C:\\Users\\ojdav\\source\\repos\\AdventOfCode2025Project\\AoC25\\res\\Data\\D{dayIndex}.txt").ToList();
			var root = System.IO.Directory.GetCurrentDirectory().Substring(0, (System.IO.Directory.GetCurrentDirectory().IndexOf("AdventOfCode2025Project\\") + 24));
			return System.IO.File.ReadAllLines($"{root}\\AoC25\\res\\Data\\D{dayIndex}.txt").ToList();

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
