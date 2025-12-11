using System;
using System.Collections.Generic;
using System.Text;

namespace AoC25.Calendar
{
	internal class Day11
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
			var input = (runTestData) ? Code.GetTestData(11) : Code.GetData(11);

			// network = new List<(int ID , List<string>)>(); 

			var network = input
				.Select
				(
					line =>
					line.Split(":")
					.ToArray()
					
				)
				.Select
				(
					x => (ID: x[0], Outputs: x[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList())
				)
				.ToList();
			

            return "part one not implemented yet.";
		}
		private static string PartTwo(bool runTestData)
		{
			var input = (runTestData) ? Code.GetTestData(11) : Code.GetData(11);

			return "part two not implemented yet.";
		}

	}


}
