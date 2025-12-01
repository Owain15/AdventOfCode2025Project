using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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


}
