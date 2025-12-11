using System;
using System.Collections.Generic;
using System.Text;

namespace AoC25.Calendar
{
	internal class Day4
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
			var input = (runTestData) ? Code.GetTestData(4) : Code.GetData(4);

			List<(int x, int y)> paperLocations = new List<(int x, int y)>();

			int[,] grid = new int[input[0].Length, input.Count];

			for (int y = 0; y < input.Count; y++)
				for (int x = 0; x < input[y].Length; x++)
				{
					grid[x, y] = (input[y][x] == '@') ? 1 : 0;

					if (grid[x, y] == 1)
					{ paperLocations.Add((x, y)); }
				}
			List<(int x, int y)> adjaentLacations = new List<(int x, int y)> {

					(-1,-1),( 0,-1),( 1,-1),
					(-1, 0),        ( 1, 0),
					(-1, 1),( 0, 1),( 1, 1)
				};

			int rollCount = 0;

			foreach (var roll in paperLocations)
			{
				int adjCount = 0;

				foreach (var adj in adjaentLacations)
				{
					(int x, int y) checkLocation = (roll.x + adj.x, roll.y + adj.y);

					if (checkLocation.x >= 0 && checkLocation.x < grid.GetLength(0)
					&& checkLocation.y >= 0 && checkLocation.y < grid.GetLength(1))
					{
						if (grid[checkLocation.x, checkLocation.y] == 1)
						{ adjCount++; }
					}
				}

				if (adjCount < 4)
				{
					rollCount++;
				}
			}


			return rollCount.ToString();
		}
		private static string PartTwo(bool runTestData)
		{
			var input = (runTestData) ? Code.GetTestData(4) : Code.GetData(4);

			List<(int x, int y, bool canBeRemoved)> paperLocations = new List<(int x, int y, bool canBeRemoved)>();

			int[,] grid = new int[input[0].Length, input.Count];

			for (int y = 0; y < input.Count; y++)
				for (int x = 0; x < input[y].Length; x++)
				{
					grid[x, y] = (input[y][x] == '@') ? 1 : 0;

					if (grid[x, y] == 1)
					{ paperLocations.Add((x, y, false)); }
				}

			List<(int x, int y)> adjaentLacations = new List<(int x, int y)> {

					(-1,-1),( 0,-1),( 1,-1),
					(-1, 0),        ( 1, 0),
					(-1, 1),( 0, 1),( 1, 1)
				};

			int rollCount = 0;
			int totalRollCount = 0;

			do
			{
				totalRollCount += rollCount;
				rollCount = 0;

				for (int i = 0; i < paperLocations.Count; i++)
				{
					int adjCount = 0;

					foreach (var adj in adjaentLacations)
					{
						(int x, int y) checkLocation = (paperLocations[i].x + adj.x, paperLocations[i].y + adj.y);

						if (checkLocation.x >= 0 && checkLocation.x < grid.GetLength(0)
						&& checkLocation.y >= 0 && checkLocation.y < grid.GetLength(1))
						{
							if (grid[checkLocation.x, checkLocation.y] == 1)
							{
								adjCount++;
							}
						}
					}

					if (adjCount < 4)
					{
						rollCount++;
						paperLocations[i] = (paperLocations[i].x, paperLocations[i].y, true);
					}


				}

				for (int i = 0; i < paperLocations.Count; i++)
				{
					if (paperLocations[i].canBeRemoved)
					{
						grid[paperLocations[i].x, paperLocations[i].y] = 0;

					}
				}

				paperLocations.RemoveAll(x => x.canBeRemoved);

			}
			while (rollCount > 0);


			return totalRollCount.ToString();
		}


	}
}
