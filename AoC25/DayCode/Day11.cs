using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
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
			//var input = (runTestData) ? Code.GetTestData(11) : Code.GetData(11);
			var input = ("svr: aaa bbb\naaa: fft\nfft: ccc\nbbb: tty\ntty: ccc\nccc: ddd eee\nddd: hub\nhub: fff\neee: dac\ndac: fff\nfff: ggg hhh\nggg: out\nhhh: out").Split("\n").ToList();

            // network = new List<(int ID , List<string>)>(); 
            var network = input
                .Select( line => line.Split(":").ToArray())
                .Select( x => (ID: x[0], Outputs: x[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ToList();

			string startNode = "svr";
			List<string> visitedNodes = new List<string>();
 
            return GetValidOutputPathCount(startNode).ToString();

            // test result : only 2 paths from svr to out visit both dac and fft
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

		private static long GetValidOutputPathCount(string currentNode)
			{
			
				return 0;
            }


	}


}
