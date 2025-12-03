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
		public static int RunPartOne()
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
			
			//Test expected result
			//1227775554
			return (int)invalidIds.Sum();

			//RunData
			//2147483647
			//wrong

		}
		public static int RunPartTwo()
		{
			return 0;
		}
	}


}
