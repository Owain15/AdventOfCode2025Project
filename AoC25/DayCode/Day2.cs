using System;
using System.Collections.Generic;
using System.Text;

namespace AoC25.Calendar
{
	internal class Day2
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
			var input = (runTestData) ? Code.GetTestData(2) : Code.GetData(2);
	
			var spans = input
				.First()
				.Split(',')
				.Select(x => x.Split('-'))
				.Select(x => (start: double.Parse(x.First()), finish: double.Parse(x.Last())))
				.ToList();


			////Test Inputs
			//span.Add(("100","102"));
			////998 - 1012 has one invalid ID, 1010.
			//span.Add(("998", "1012"));
			//span.Add(("123120", "123126")); //123123

			//0101 isn't an ID at all. (101 is a valid)


			List<double> invalidIds = new List<double>();

			foreach (var range in spans)
			{
				for (double i = range.start; i <= range.finish; i++)
				{
					string id = i.ToString();
					int midPoint = id.Length / 2;

					if (id.Length % 2 == 0)
					{
						string st1 = id.Substring(0, midPoint);
						string st2 = id.Substring(midPoint);

						(string a, string b) part = (st1, st2);

						if (int.Parse(part.a) == int.Parse(part.b))
						{ invalidIds.Add(double.Parse(id)); }
					}

				}


			}

			//IEnumerable<double> distinceIds = invalidIds;
			//return (int)hu.Distinct().Sum();
			double ijjn = invalidIds.Sum();
			//Test expected result
			//1227775554
			//return (int)invalidIds.Sum();
			return invalidIds.Sum().ToString();
			//RunData
			//2147483647
			//wrong

		}
		private static string PartTwo(bool runTestData)
		{
			var input = (runTestData) ? Code.GetTestData(2) : Code.GetData(2);

			var spans = input
				.First()
				.Split(',')
				.Select(x => x.Split('-'))
				.Select(x => (start: double.Parse(x.First()), finish: double.Parse(x.Last())))
				.ToList();

			List<double> invalidIds = new List<double>();

			foreach (var range in spans)
			{
				for (double i = range.start; i <= range.finish; i++)
				{
					string id = i.ToString();

					for (int partLength = 1; partLength <= id.Length / 2; partLength++)
					{
						(string a, string b) part = (id.Substring(0, partLength), id.Substring(partLength));

						if (part.b.Replace(part.a, "").Length == 0)
						{ invalidIds.Add(double.Parse(id)); break; }
					}
				}
			}

			return (invalidIds.Count > 0) ? invalidIds.Sum().ToString() : "not found";

		}

	}
}
