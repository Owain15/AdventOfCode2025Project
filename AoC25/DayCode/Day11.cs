using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

            var startPos = network.FindIndex(n => n.ID == "you");
       
            long result = GetOutputPathsCount(network[startPos].Outputs,network);
         

            return result.ToString();
		}

        private static string PartTwo(bool runTestData)
		{
			var input = (runTestData) ? Code.GetTestData(11) : Code.GetData(11);

			return "part two not implemented yet.";
		}


		private static long GetOutputPathsCount(List<string> outputs, List<(string ID,List<string>Outputs)> Network)
		{
			long result = 0;

            foreach (var output in outputs)
			{ 
				if(output == "out")
				{ 
					result++;
					continue;
				}

                result += GetOutputPathsCount(Network[Network.FindIndex(n => n.ID == output)].Outputs ,Network);
            
			}

			return result;
        }


	}


}
