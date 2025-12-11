using System;
using System.Collections.Generic;
using System.Text;

namespace AoC25.Calendar
{
	internal class Day9
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
			var input = (runTestData) ? Code.GetTestData(9) : Code.GetData(9);

			var rtList = input
				.Select(
						line => line.Split(',')
						.Select(long.Parse)
						.ToArray()
						)
				.Select(p => (x: p[0], y: p[1]))
				.ToList();

			var dataList = new List<(((long x, long y) t1, (long x, long y) t2) points, long area)>();

			foreach (var r in rtList)
			{
				var data = FindLargestPair(r, rtList);

				if (!dataList.Any(x => x.points == data.points) && !dataList.Any(x => x.points == (data.points.t2, data.points.t1)))
				{
					dataList.Add(data);
				}
			}

			return dataList.Select(x => x.area).Max().ToString();

			// Test result : 50

		}

		private static (((long x, long y) t1, (long x, long y) t2) points, long area) FindLargestPair((long x, long y) r, List<(long x, long y)> rtList)
		{
			long aCache = 0;
			int tCache = -1;

			for (int t = 0; t < rtList.Count; t++)
			{
				if (r == rtList[t]) { continue; }

				long l = (Math.Max(r.x, rtList[t].x)) - (Math.Min(r.x, rtList[t].x)) + 1;
				long h = (Math.Max(r.y, rtList[t].y)) - (Math.Min(r.y, rtList[t].y)) + 1;

				long a = l * h;

				if (a > aCache)
				{
					aCache = a;
					tCache = t;
				}

			}

			return ((r, (rtList[tCache].x, rtList[tCache].y)), aCache);
		}

		private static string PartTwo(bool runTestData)
		{
			var input = (runTestData) ? Code.GetTestData(9) : Code.GetData(9);

			var rtList = input
				.Select(
						line => line.Split(',')
						.Select(long.Parse)
						.ToArray()
						)
				.Select(p => (x: p[0], y: p[1]))
				.ToList();

			//var boundery = ? 

			return "part two not implemented yet.";
		}


	}
}
