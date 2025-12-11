using System;
using System.Collections.Generic;
using System.Text;

namespace AoC25.Calendar
{
	internal class Day8
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

			List<List<(Box b1, Box b2)>> circits = new List<List<(Box, Box)>>();

			for (int cc = 0; cc < conCount; cc++)
				for (int i = 0; i < boxes.Count; i++)
				{

					(Box b1, Box b2) conection = (boxes[i], boxes.Find(x => x.Location == boxes[i].ClosestLocation));

					int b1Index = circits.FindIndex(c => c.Any(p => p.b1 == conection.b1 || p.b2 == conection.b1));
					int b2Index = circits.FindIndex(c => c.Any(p => p.b1 == conection.b2 || p.b2 == conection.b2));


					//both boxes are within the same circit       //may need better check to see if exact connection exists 
					if (b1Index == b2Index && b1Index > -1)
					{ continue; }

					//nither box is currently in a circet
					if (b1Index == -1 && b2Index == -1)
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
					if (b1Index > -1 && b2Index > -1 && b1Index != b2Index)
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
}
