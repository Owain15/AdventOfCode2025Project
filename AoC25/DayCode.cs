using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;

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

			for(int i = 0; i < input.Count; i++)
			{
				char firstChar = input[i][0];
				int move = int.Parse(input[i].Substring(1, input[i].Length - 1));  
				if(firstChar == 'L') { move = - move; }
				commandList.Add(move);
			}
			// run inputs
			foreach(var command in commandList)
			{
				dialIndex += command;
				
				while(dialIndex < 0 || dialIndex >= 100)
				{
					if(dialIndex < 0) { dialIndex += 100; }
					if(dialIndex >= 100) { dialIndex -= 100; }
				}
		
				if(dialIndex == 0) { zeroCount++; }
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
				.Select(x => (start: double.Parse( x.First()), finish: double.Parse(x.Last())))
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
					int midPoint = id.Length/2;
	
					if(id.Length % 2 == 0)
					{
						string st1 = id.Substring(0, midPoint);
						string st2 = id.Substring(midPoint);

						(string a, string b) part = (st1, st2);

						if (int.Parse(part.a) == int.Parse(part.b))
						{ invalidIds .Add(double.Parse(id));  }
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

			foreach(var range in spans)
			{
				for(double i = range.start; i <= range.finish; i++)
				{
					string id = i.ToString();

					for (int partLength = 1; partLength <= id.Length / 2; partLength++)
					{
						(string a, string b) part = (id.Substring(0, partLength), id.Substring(partLength));

						if (part.b.Replace(part.a,"").Length == 0)
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

			List<double> jolts = new List<double>();

			foreach (var bank in input)
			{
				string bRef = bank;
				// find smallest value
				for(int i = 0; i < 3; i++)
				{ 
					
					int lowestValue = 10;
					int lowestValueIndex = -1 ;

					for (int j = 0; j < bRef.Length; j++)
					{
						if (int.Parse(bRef[j].ToString()) < lowestValue)
						{
							lowestValue = int.Parse(bRef[j].ToString());
							lowestValueIndex = j;
						}
					}
				    
					// remove last case from bRef
                    
					bRef = bRef.Remove(lowestValueIndex, 1);
					//xx x             
					//234234234234278

				}

				jolts.Add(double.Parse(bRef));
			}

			return jolts.Sum().ToString();
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

			int[,] grid = new int[ input[0].Length, input.Count ];

			for(int y = 0; y < input.Count; y ++)
				for(int x = 0; x < input[y].Length; x ++)
				{ 	
					grid[x, y] = (input[y][x] == '@') ? 1 : 0;
				
					if(grid[x,y] == 1)
					{ paperLocations.Add((x,y)); }
				}
			List<(int x, int y)> adjaentLacations = new List<(int x, int y)> {
				
					(-1,-1),( 0,-1),( 1,-1),
					(-1, 0),        ( 1, 0),
					(-1, 1),( 0, 1),( 1, 1)
				};

			int rollCount = 0;

			foreach(var roll in paperLocations)
			{
				int adjCount = 0;
				
				foreach(var adj in adjaentLacations)
				{
					(int x, int y) checkLocation = (roll.x + adj.x, roll.y + adj.y);
				
					if(checkLocation.x >= 0 && checkLocation.x < grid.GetLength(0)
					&& checkLocation.y >= 0 && checkLocation.y < grid.GetLength(1))
					{
						if(grid[checkLocation.x, checkLocation.y] == 1)
						{ adjCount++; }
					}
				}

				if(adjCount < 4)
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
					{ paperLocations.Add((x, y,false)); }
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

				for(int i = 0; i < paperLocations.Count; i++)
				{
					if(paperLocations[i].canBeRemoved)
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
