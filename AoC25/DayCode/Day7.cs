using System;
using System.Collections.Generic;
using System.Text;

namespace AoC25.Calendar
{
	internal class Day7
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
			var input = (runTestData) ? Code.GetTestData(7) : Code.GetData(7);

			////initalize grid
			//char[,] grid = new char[input.Count-1,input.First().Count()-1];

			//for(int y = 0; y < input.First().Count()-1; y++)
			//	for(int x = 0; x < input.Count()-1; x++)
			//	{ grid[x,y] = input[x][y]; }

			//List<(int x, int y)> beamLocations = new List<(int x, int y)> { (input.First().IndexOf('S'),0) };

			if (input.First().IndexOf('S') == -1)
			{ return " Start location not found."; }

			List<int> beamLocation = new List<int> { input.First().IndexOf('S') };

			int splitCout = 0;

			for (int i = 0; i < input.Count() - 1; i++)
			{
				List<int> newLocations = new List<int>();

				foreach (var beam in beamLocation)
				{
					if (input[i + 1][beam] == '^')
					{
						splitCout++;
						newLocations.Add(beam - 1);
						newLocations.Add(beam + 1);
					}
					else
					{ newLocations.Add(beam); }

				}

				beamLocation = newLocations.Distinct().ToList();
			}

			return splitCout.ToString();
		}
		private static string PartTwo(bool runTestData)
		{

			return "not implemented yet.";

			// help sheet

			// .......S.......
			// .......1.......
			// ......1^1......
			// ......1.1......
			// .....1^2^1.....
			// .....1.2.1.....
			// ....1^3^3^1....
			// ....1.2.3.1....
			// ...1^3^231^1...
			// Each branch is an addition of the previous two.

			var input = (runTestData) ? Code.GetTestData(7) : Code.GetData(7);

			if (input.First().IndexOf('S') == -1)
			{ return " Start location not found."; }

			List<int> beamLocation = new List<int> { input.First().IndexOf('S') };

			int splitCout = 0;

			for (int i = 0; i < input.Count() - 1; i++)
			{
				List<int> newLocations = new List<int>();

				foreach (var beam in beamLocation)
				{
					if (input[i + 1][beam] == '^')
					{
						splitCout++;
						newLocations.Add(beam - 1);
						newLocations.Add(beam + 1);
					}
					else
					{ newLocations.Add(beam); }

				}

				beamLocation = newLocations.ToList();
			}

			return splitCout.ToString();
		}


	}
}
