using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC25.Calendar
{
	internal class Day10
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
			// desired output,  buttns        ,     joltages
			//[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
			var machines = input
				.Select
				(x =>
					(
						DesiredOutput: Regex.Match(x, @"\[.*\]").Value.Replace("[", "").Replace("]", "")
						, Buttons: Regex.Match(x, @"\(.*\)").Value.Replace("(", "").Replace(")", "").Split(" ").Select(xx => xx.Split(",")).ToArray()
						, Joltages: Regex.Match(x, @"\{.*\}").Value.Replace("{", "").Replace("}", "").Split(",").ToArray()
					)
				)
				.ToList();
			
			// posibly change to a long once debugging is complete
			var shotestRoutePerMachine = new List<long>();
			
			// try changing to LINQ
			int machineIndex = 0;
			foreach (var machine in machines)
			{
				// Convert machine.DesiredOutput to BitArray
				var target = new BitArray
					(machine.DesiredOutput
					.Select(x => x switch
					{
						'#' => true,
						'.' => false,
						_ => throw new Exception($"Machine : {machineIndex}. faild to convert target.")
					}).ToArray()
					);

				// Convert machine.Buttons to list of BitArray
				var buttons = new List<BitArray>();
				foreach(var button in machine.Buttons)
				{
					var result = new BitArray(machine.DesiredOutput.Length);
					foreach(var bit in button)
					{ result[int.Parse(bit)] = true; }
					buttons.Add( result );
				}


				shotestRoutePerMachine.Add(FindShortestRoute(target, buttons));
	
			}



			return shotestRoutePerMachine.Sum().ToString();
		}

		private static long FindShortestRoute(BitArray target, List<BitArray> buttons)
		{
			//initalize new output
			var output = new BitArray(target.Length);

			List<BitArray> visitedStats = new List<BitArray>();
			List<BitArray> stateList = new List<BitArray>();
			stateList.Add(output);
			visitedStats.Add(output);
	

			long moveCount = 0;

			while (!stateList.Any(x => x.IsEqualTo(target)))
			{
				var newStateList = new List<BitArray>();

				foreach (var state in stateList)
					foreach (var button in buttons)
					{
						var nextState = new BitArray(state.Length);
						nextState = (BitArray)state.Clone();
						nextState = nextState.Xor(button);

						if (!visitedStats.Any(x => x.IsEqualTo(nextState)))
						{
							newStateList.Add(nextState);
							visitedStats.Add(nextState);
						}
					}

				stateList = newStateList;
				moveCount++;

				if(stateList.Count == 0)
				{ throw new Exception("no route found"); }

			}

			return moveCount;//stateList.Count-1;
		}

		
		

		private static string PartTwo(bool runTestData)
		{
			var input = (runTestData) ? Code.GetTestData(10) : Code.GetData(10);

			return "part two not implemented yet.";
		}


	}
	public static class BitArrayExtention
	{
			public static bool IsEqualTo(this BitArray bit1, BitArray bit2)
			{
				for (var i = 0; i <bit1.Length; i++)
				{
					if (bit1[i] != bit2[i])
					{ return false; }
				}

				return true;
			}
	}
}
