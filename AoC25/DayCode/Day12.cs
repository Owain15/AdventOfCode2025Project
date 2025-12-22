using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AoC25.Calendar
{
	internal class Day12
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
			var input = (runTestData) ? Code.GetTestData(12) : Code.GetData(12);

			//Get Raw Data
			var data = input
			.Aggregate((a, b) => a + "\n" + b)
			.Split("\n\n")
			.ToArray();


			//Parse Shape Data
			//consider changing to arrays

			List<List<string>> shapes = new List<List<string>>();

			for(int i = 0; i <= 5; i++)
			{
				var shape = data[i]
					.Split("\n")
					.ToList()
					.Skip(1)
					.ToList()
				;

				shapes.Add(shape);
			}

			//Parsing Region Data

			List <((long x, long y) region, long[] shapeCount)> lineData = new List<((long x, long y) region, long[] shapeCount)>();

			foreach (var line in data[6].Split("\n"))
			{
				(long x, long y) regionData;
				//long[] shapeCountData = new long[shapes.Count];

				var splitLine = line
					.Split(":");

				var re = splitLine
					.First()
					.Split("x")
					.Select(x => long.Parse(x))
					.ToList();
					regionData = (re[0], re[1]);
					//.Select(section => (x: long.Parse(section[0].ToString()), y: long.Parse(section[1].ToString())));

				var shapeCountData = splitLine
					.Last()
					.Split(" ")
					.Skip(1)
					.Select(x => long.Parse(x))
					.ToArray();

				lineData.Add((regionData, shapeCountData));
			}

			//Run Data by line

			var fitCount = 0;
			foreach (var line in lineData)
			{
				if(CanAllPresentsFitInRegion(line,shapes))
				{ fitCount++; }

			}

            return fitCount.ToString();

			//Test result : 2
		}

		private static bool CanAllPresentsFitInRegion(((long x, long y) region, long[] shapeCount) line, List<List<string>> shapes)
		{
			bool result = false;

			//Quick test
			//once working try removing
			if(PresentVolumeExceedsRegion(line,shapes))
			{ return false; }

			//Do work

			List<int> shapeIndexList = new List<int>();

			for(int i= 0; i < line.shapeCount.Length; i++)
			for(int j= 0; j < line.shapeCount[i]; j++)
			{
					shapeIndexList.Add(i);
			}

			//add function, combine combination to optimum new shapes?

			foreach (var shapeIndex in shapeIndexList)
			{
				List<int> shadowShapesList = shapeIndexList;//shapeIndexList.FindIndex(shapeIndex));
				//shapeIndexList = shadowShapesList.RemoveAt(2);

				var shadowGrid = shapes[shapeIndex];
				

			
			}


			return true;
			//return result;
		}

		private static bool PresentVolumeExceedsRegion(((long x, long y) region, long[] shapeCount) line, List<List<string>> shapes)
		{
			long regionVolume = line.region.x * line.region.y;
			long presentVolume = 0;
			
			for(int i = 0; i < line.shapeCount.Length; i ++)
			{
				if (line.shapeCount[i] == 0)
				{ continue; }

				var pv = shapes[i]
					.SelectMany(x => x)
					.Count(x => x == '#');

				presentVolume += (pv * line.shapeCount[i]); 
			}

			return presentVolume > regionVolume;
		}

		private static string PartTwo(bool runTestData)
		{
			return "part two not implemented yet.";
		}

	}
}
