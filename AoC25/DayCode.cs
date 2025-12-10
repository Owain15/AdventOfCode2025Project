using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AoC25
{
	public static class Day1
	{
		public static int RunPartOne()
		{
			int zeroCount = 0;
			int dialIndex = 50;

			//get input data
			//var input = Code.GetTestData(1);
			var input = Code.GetData(1);

			// unpack input data
			var commandList = new List<int>();

			for (int i = 0; i < input.Count; i++)
			{
				char firstChar = input[i][0];
				int move = int.Parse(input[i].Substring(1, input[i].Length - 1));
				if (firstChar == 'L') { move = -move; }
				commandList.Add(move);
			}
			// run inputs
			foreach (var command in commandList)
			{
				dialIndex += command;

				while (dialIndex < 0 || dialIndex >= 100)
				{
					if (dialIndex < 0) { dialIndex += 100; }
					if (dialIndex >= 100) { dialIndex -= 100; }
				}

				if (dialIndex == 0) { zeroCount++; }
			}




			return zeroCount;
		}

		public static int RunPartTwo()
		{
			int zeroCount = 0;
			int dialIndex = 50;

			//get input data
			var input = Code.GetTestData(1);
			//var input = Code.GetData(1);

			// unpack input data
			var commandList = new List<int>();

			for (int i = 0; i < input.Count; i++)
			{
				char firstChar = input[i][0];
				int move = int.Parse(input[i].Substring(1, input[i].Length - 1));
				if (firstChar == 'L') { move = -move; }
				commandList.Add(move);
			}
			// run inputs
			foreach (var command in commandList)
			{
				int runningComand = command;

				while (runningComand != 0)
				{
					if (runningComand > 0)
					{
						dialIndex++;
						runningComand--;
					}
					else
					{
						dialIndex--;
						runningComand++;
					}

					while (dialIndex < 0 || dialIndex >= 100)
					{
						if (dialIndex < 0) { dialIndex += 100; }
						if (dialIndex >= 100) { dialIndex -= 100; }
					}
					if (dialIndex == 0) { zeroCount++; }
				}

			}


			return zeroCount;
		}
	}

	public static class Day2
	{
		public static string RunPartOne()
		{
			var input = Code.GetData(2);
			//var input = Code.GetTestData(2);


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
		public static string RunPartTwo()
		{
			var input = Code.GetData(2);
			//var input = Code.GetTestData(2);


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

	public static class Day3
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
			var input = (runTestData) ? Code.GetTestData(3) : Code.GetData(3);

			List<int> jolts = new List<int>();

			foreach (var bank in input)
			{
				int c1 = 0;
				int c2 = 0;

				for (int i = 0; i < bank.Length; i++)
				{
					for (int j = 0; j < bank.Length - 1; j++)
					{
						if (int.Parse(bank[j].ToString()) > c1)
						{
							c1 = int.Parse(bank[j].ToString());
							i = j + 1;
						}

						//c2 = ((int)bank[j] > c2) ? bank[j] : c2;
					}
					c2 = (int.Parse(bank[i].ToString()) > c2) ? int.Parse(bank[i].ToString()) : c2;
				}

				jolts.Add(int.Parse(c1.ToString() + c2.ToString()));
			}

			return jolts.Sum().ToString();
		}
		private static string PartTwo(bool runTestData)
		{
			var input = (runTestData) ? Code.GetTestData(3) : Code.GetData(3);

			List<decimal> jolts = new List<decimal>();
			foreach (var bank in input)
			{
				string bCop = bank;
				int removeCount = 0;

				for (int i = 0; i < bCop.Length - 1; i++)
				{
					if (int.Parse(bCop[i].ToString()) < int.Parse(bCop[i + 1].ToString()))
					{
						bCop = bCop.Remove(i, 1);
						i = -1;
						removeCount++;
					}
					if (removeCount >= 3)
					{
						jolts.Add(decimal.Parse(bCop));
						break;
					}
				}

				if (removeCount < 3)
				{
					while (removeCount < 3)
					{
						int lowestValue = 10;
						int lowestValueIndex = -1;

						for (int j = 0; j < bCop.Length; j++)
						{
							if (int.Parse(bCop[j].ToString()) < lowestValue)
							{
								lowestValue = int.Parse(bCop[j].ToString());
								lowestValueIndex = j;
							}
						}

						// remove last case from bRef

						bCop = bCop.Remove(lowestValueIndex, 1);

						removeCount++;

						if (removeCount >= 3)
						{
							jolts.Add(decimal.Parse(bCop));
							break;
						}

					}
				}

				//jolts.Add(double.Parse(bCop));

			}


			return jolts.Sum().ToString();
			//Test result : 3121910778619.
		}


	}

	public static class Day4
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

	public static class Day5
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

	public static class Day6
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
			var input = (runTestData) ? Code.GetTestData(6) : Code.GetData(6);

			int rowCount = input.Count - 1;
			int colCount = input[0].Split(' ').Where(x => double.TryParse(x, out double result)).Select(x => double.Parse(x)).ToList().Count;

			double[,] numbers = new double[colCount, rowCount];

			for (int i = 0; i < input.Count() - 1; i++)
			{
				var data = input[i].Split(' ').Where(x => double.TryParse(x, out double result)).Select(x => double.Parse(x)).ToArray();

				for (int j = 0; j < data.Length; j++)
				{
					numbers[j, i] = data[j];
				}

			}

			char[] operations = input.Last().Replace(" ", "").ToArray();

			if (operations.Length != colCount)
			{ return "Operation count does not match column count."; }

			List<double> results = new List<double>();

			for (int colIndex = 0; colIndex < colCount; colIndex++)
			{
				double colResult = numbers[colIndex, 0];

				for (int rowIndex = 1; rowIndex < rowCount; rowIndex++)
				{
					colResult = (operations[colIndex] == '+') ? colResult + numbers[colIndex, rowIndex]
							  : (operations[colIndex] == '*') ? colResult * numbers[colIndex, rowIndex]

							  : throw new Exception($"operation not found\n\rcollum : {colIndex}");
				}

				results.Add(colResult);
			}


			return results.Sum().ToString();

			//Test Result  4277556
		}
		private static string PartTwo(bool runTestData)
		{
			var input = (runTestData) ? Code.GetTestData(6) : Code.GetData(6);


			var lineData = input
			.Select(x => x.Split(' '))
			.ToList();


			var colSizeList = new List<double>();
			var opData = lineData.Last();

			while (opData.Count() < input.Last().Replace(" ", "").Length)            // - colom count
			{ opData = opData.Append("").ToArray(); }

			for (int i = 0; i < opData.Length - 1; i++)
			{
				if (opData[i] != "")
				{
					for (int j = i + 1; j < opData.Length; j++)
					{
						if (opData[j] != "")
						{
							colSizeList.Add(j - i);
							i = j - 1;
							break;
						}

						if (j == opData.Length - 1)
						{
							colSizeList.Add(j - i);
							i = j;
							break;
						}
					}
				}



			}

			var operations = opData
				.Where(x => x != "")
				.ToList();

			List<List<string>> Lines = new List<List<string>>();

			for (int i = 0; i < lineData.Count - 1; i++)
			{
				List<string> line = new List<string>();

				string stringData = "";

				for (int j = 0; j < lineData[i].Length; j++)
				{
					if (lineData[i][j] == "")
					{ stringData += " "; }
					else if (double.TryParse(lineData[i][j], out double r))
					{ stringData += lineData[i][j]; }

					if (stringData.Length == colSizeList[i])
					{
						line.Add(stringData);
						stringData = "";
					}
				}

				Lines.Add(line);
			}

			var colCount = Lines[0].Count;
			var rowCount = Lines.Count;

			List<double> results = new List<double>();

			for (int stringIndex = colCount - 1; stringIndex >= 0; stringIndex--)
			{
				var colData = new List<double>();

				for (int charIndex = (int)colSizeList[stringIndex] - 1; charIndex >= 0; charIndex--)
				{
					string cd = "";

					for (int lineIndex = 0; lineIndex < Lines.Count; lineIndex++)
					{
						cd += Lines[lineIndex][stringIndex][charIndex];
					}

					colData.Add(double.Parse(cd));

				}

				var colResult = colData[0];

				var t1 = operations[stringIndex];

				for (int i = 1; i < colData.Count; i++)
				{
					colResult = (operations[stringIndex] == "+") ? colResult + colData[i]
							  : (operations[stringIndex] == "*") ? colResult * colData[i]

					  : throw new Exception($"operation not found\n\rcollum : {stringIndex}");
				}

				results.Add(colResult);

			}

			return results.Sum().ToString();

			//Data 81444036920374 x to high 

			//Test Result  3263827
		}


	}

	public class Day7 
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

	public static class Day8
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


		private class Box
		{
			public double Id { get; }
			public (double x, double y, double z) Location { get; }
			public bool IsConnected { get; set; }

			public (double x, double y, double z)? ClosestLocation { get; set; }
			public double? ClosestBoxDistence { get; set; }


			public Box(double id, (double x, double y, double z) location)
			{
				Id = id;
				Location = location;
				IsConnected = false;
				ClosestLocation = null;
				ClosestBoxDistence = null;

			}

			public static double CalculateDistance((double x, double y, double z) p1, (double x, double y, double z) p2)
			{
				double deltaX = p2.x - p1.x;
				double deltaY = p2.y - p1.y;
				double deltaZ = p2.z - p1.z;

				double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);

				return distance;
			}

			public bool SetClosestBox(List<Box> boxes)
			{
				ClosestLocation = null;
				ClosestBoxDistence = null;

				var minDistance = double.MaxValue;
				var minBoxId = -1;

				for (int b2 = 0; b2 < boxes.Count(); b2++)
				{
					if (Location == boxes[b2].Location)
					{ continue; }

					var distance = Box.CalculateDistance(Location, boxes[b2].Location);

					if (distance < minDistance)
					{
						minDistance = distance;
						minBoxId = b2;
					}
				}

				this.ClosestLocation = boxes[minBoxId].Location;
				this.ClosestBoxDistence = minDistance;

				return (ClosestLocation != null);

			}

		}

		private static string PartOne(bool runTestData)
		{

			var input = (runTestData) ? Code.GetTestData(8) : Code.GetData(8);

			var boxLocations = input
			.Select(line => line.Split(',')
			.Select(double.Parse)
			.ToArray())
			.Where(p => p.Length == 3)
			.Select(p => (x: p[0], y: p[1], z: p[2]))
			.ToList();

			List<Box> boxes = new List<Box>();

			for (int i = 0; i < boxLocations.Count; i++)
			{ boxes.Add(new Box(i, boxLocations[i])); }

			for (int b1 = 0; b1 < boxes.Count; b1++)
			{
				bool set = boxes[b1].SetClosestBox(boxes);

				if (!set)
				{ throw new Exception($"Box location ({boxes[b1].Location.x},{boxes[b1].Location.y},{boxes[b1].Location.z}) closest box could not be found."); }

			}

			boxes = boxes.OrderBy(x => x.ClosestBoxDistence).ToList();

			int conCount = (runTestData) ? 10 : 1000;
			
			//build circits

			List<List<(Box b1, Box b2)>> circits = new List<List<(Box,Box)>>();

			for(int cc = 0; cc < conCount; cc++)
				for(int i = 0; i < boxes.Count; i++)
				{

					(Box b1, Box b2) conection = (boxes[i], boxes.Find(x => x.Location == boxes[i].ClosestLocation));

					int b1Index = circits.FindIndex(c => c.Any(p => p.b1 == conection.b1 || p.b2 == conection.b1));
					int b2Index = circits.FindIndex(c => c.Any(p => p.b1 == conection.b2 || p.b2 == conection.b2));


					//both boxes are within the same circit       //may need better check to see if exact connection exists 
					if (b1Index == b2Index && b1Index > -1)
					{ continue; }

					//nither box is currently in a circet
					if(b1Index == -1 && b2Index == -1)
					{ circits.Add(new List<(Box b1, Box b2)> { conection }); }


					// one box is alredy within a circit
					if (b1Index > -1 && b2Index == -1)
					{
						circits[b1Index].Add(conection);
					}

					if (b1Index == -1 && b2Index > -1)
					{
						circits[b2Index].Add(conection);
					}

					//if each box is already within seperate circits. 
					if(b1Index > -1 && b2Index > -1 && b1Index != b2Index)
					{
						circits[b1Index].Add(conection);
						circits[b1Index].AddRange(circits[b2Index]);
						circits.RemoveAt(b2Index);
					}

				}

			circits = circits.OrderByDescending(x => x.Count()).ToList();


			//multipy top 3 circits

			return (circits[0].Count * circits[1].Count * circits[2].Count).ToString();
		}
		private static string PartTwo(bool runTestData)
		{
			return "part two not implemented yet.";
		}


	}

	public static class Day9
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

			var dataList = new List<(((long x, long y)t1,(long x, long y)t2)points,long area)>();

			foreach ( var r in rtList )
			{
				var data = FindLargestPair(r, rtList);

				if(!dataList.Any(x => x.points == data.points) && !dataList.Any(x => x.points == (data.points.t2,data.points.t1)))
				{
					dataList.Add(data);
				}
			}

			return dataList.Select(x => x.area).Max().ToString();
		
			// Test result : 50
		
		}

		private static (((long x, long y)t1,(long x, long y)t2)points,long area) FindLargestPair((long x, long y)r,List<(long x, long y)>rtList)
		{
			long aCache = 0;
			int tCache = -1;
			
			for (int t = 0; t < rtList.Count; t++)
			{
				if(r == rtList[t]) { continue; }

				long l = (Math.Max(r.x, rtList[t].x)) - (Math.Min(r.x, rtList[t].x))+1;
				long h = (Math.Max(r.y, rtList[t].y)) - (Math.Min(r.y, rtList[t].y))+1;

					long a = l * h;

					if(a > aCache)
					{
						aCache = a;
						tCache = t;
					}

				}

			return ((r,(rtList[tCache].x, rtList[tCache].y)), aCache);
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

	public static class Day10
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
			var input = (runTestData) ? Code.GetTestData(10) : Code.GetData(10);

			return "part one not implemented yet.";
		}
		private static string PartTwo(bool runTestData)
		{
			return "part two not implemented yet.";
		}


	}

	//public static class Day
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
	//	}

	//	private static string PartOne(bool runTestData)
	//	{
	//		var input = (runTestData) ? Code.GetTestData() : Code.GetData();

	//		return "part one not implemented yet.";
	//	}
	//	private static string PartTwo(bool runTestData)
	//	{
	//		return "part two not implemented yet.";
	//	}


	//}

	//public static class Day
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
	//	}

	//	private static string PartOne(bool runTestData)
	//	{
	//		var input = (runTestData) ? Code.GetTestData() : Code.GetData();

	//		return "part one not implemented yet.";
	//	}
	//	private static string PartTwo(bool runTestData)
	//	{
	//		return "part two not implemented yet.";
	//	}


	//}
}
