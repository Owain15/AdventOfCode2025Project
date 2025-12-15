using System;
using System.Collections.Generic;
using System.Text;

namespace AoC25.Calendar
{
	internal class Day12
	{
		public static string Run(int partIndex)
		{
			switch (partIndex)
			{
				case 1: return PartOne(false);
				case 2: return PartTwo(false);
				default: return "Invalid part index.";
			}
		}
		public static string Run(int partIndex, bool runTestData)
		{
			switch (partIndex)
			{
				case 1: return PartOne(runTestData);
				case 2: return PartTwo(runTestData);
				default: return "Invalid part index.";
			}
		}

		private static string PartOne(bool runTestData)
		{
			var input = (runTestData) ? Code.GetTestData(12) : Code.GetData(12);

			
			var data = input
			.Aggregate((a, b) => a + "\n" + b)
			.Split("\n\n")
			.ToList()
			;

            return "part one not implemented yet.";

			//Test result : 2
		}
		private static string PartTwo(bool runTestData)
		{
			return "part two not implemented yet.";
		}

	}
}
