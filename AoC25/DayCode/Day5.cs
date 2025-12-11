using System;
using System.Collections.Generic;
using System.Text;

namespace AoC25.Calendar
{
	internal class Day5
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
			var input = (runTestData) ? Code.GetTestData(5) : Code.GetData(5);

			var ranges = input
				.Where(x => x.Contains('-'))
				.Select(x => x.Split('-'))
				.Select(x => (start: double.Parse(x.First()), finish: double.Parse(x.Last())))
				.ToList();

			var inredeants = input
				.Where(x => (double.TryParse(x.ToString(), out double result)))
				.Select(x => double.Parse(x))
				.ToList();

			int freshIngeadeantCount = 0;

			foreach (var ingredeant in inredeants)
			{
				foreach (var range in ranges)
				{
					if (ingredeant >= range.start && ingredeant <= range.finish)
					{
						freshIngeadeantCount++;
						break;
					}
				}
			}


			return freshIngeadeantCount.ToString();
		}
		private static string PartTwo(bool runTestData)
		{
			var input = (runTestData) ? Code.GetTestData(5) : Code.GetData(5);

			var ranges = input
				.Where(x => x.Contains('-'))
				.Select(x => x.Split('-'))
				.Select(x => (start: double.Parse(x.First()), finish: double.Parse(x.Last())))
				.ToList()
				.OrderBy(x => x.start).ToList();



			for (int i = 0; i < ranges.Count - 1; i++)
			{
				for (int j = i + 1; j < ranges.Count; j++)
				{
					//(S2 - S1) < (e1 - s1) || (S1 - S2) < (e2 - s2)
					//if ((ranges[j].finish - ranges[i].start) < (ranges[j].finish - ranges[i].start) && ( <= ranges[i].start))
					if (RangesOverlap((ranges[i].start, ranges[i].finish), (ranges[j].start, ranges[j].finish)))
					{
						// (10,14) (12,18)
						double nStart = Math.Min(ranges[i].start, ranges[j].start);
						double nFinish = Math.Max(ranges[i].finish, ranges[j].finish);
						ranges.RemoveAt(j);
						ranges.RemoveAt(i);
						ranges.Add((nStart, nFinish));
						ranges = ranges.OrderBy(x => x.start).ToList();
						i--;
						break;

					}
				}


			}

			List<double> ids = new List<double>();

			foreach (var range in ranges)
			{
				ids.Add(range.finish - range.start + 1);
				//for(double i = range.start; i <= range.finish; i++)
				//{
				//	ids.Add(i);
				//}
			}

			return ids.Distinct().Sum().ToString();

			//Test rsult : 14
		}

		private static bool RangesOverlap((double start, double finish) range1, (double start, double finish) range2)
		{
			return (range1.start <= range2.finish) && (range2.start <= range1.finish);

		}


	}
}
