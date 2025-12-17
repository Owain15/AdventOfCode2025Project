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

			public static bool IsConnectionInCircit((Box b1, Box b2)connection, List<(Box b1, Box b2)> circit)
			{
                if (circit == null || circit.Count == 0)
                { return false; }

                // Compare by Id to determine if the same boxes appear in either order.
                for (int i = 0; i < circit.Count; i++)
                {
                    var c = circit[i];

                    bool sameOrder = c.b1.Id == connection.b1.Id && c.b2.Id == connection.b2.Id;
                    bool reverseOrder = c.b1.Id == connection.b2.Id && c.b2.Id == connection.b1.Id;

                    if (sameOrder || reverseOrder)
                    { return true; }
                }

                return false;
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

			
			var conList = new List<(Box b1, Box b2)>();
            
			int cc = 0;
            //for(int cc = 0; cc < conCount; cc++ )
            for (int i = 0; i < boxes.Count; i++)
			{
				(Box b1, Box b2) connection = (boxes[i], boxes.First(b => b.Location == boxes[i].ClosestLocation));

                if (Box.IsConnectionInCircit(connection,conList))
                {continue; }

				conList.Add(connection);
				cc++;

				if(cc >= conCount)
				{ break; }
            }
			
			//build circits
			var circits = new List<List<Box>>();

			foreach(var box in boxes)
			{ 
				circits.Add(new List<Box>() { box });
			}

			foreach(var connection in conList)
			{

				circits = HandleConnection(circits,connection);
                //circit contains nether or both box


                //circit contains one box

            }


            //multipy top 3 circits

            if (circits.Count<3)
			{ return "Insufficient data to find result."; }

            circits = circits.OrderByDescending(x => x.Count()).ToList();

            return (circits[0].Count()* circits[1].Count() * circits[2].Count()).ToString();
		}

		private static List<List<Box>> HandleConnection(List<List<Box>>Circits,(Box b1, Box b2)Connection)
		{
			//Ether box cant be found
			var b1CircitIndex = Circits.FindIndex(x => x.Any(xx => xx == Connection.b1));
            var b2CircitIndex = Circits.FindIndex(x => x.Any(xx => xx == Connection.b2));

			if(b1CircitIndex < 0 || b2CircitIndex < 0)
			{ throw new Exception($"Connection ({Connection.b1.Id} ,{Connection.b2.Id}) could not be found."); }

            //both in the same circit (Do nothing?)
            if (b1CircitIndex == b2CircitIndex)
            { return Circits; }

            //in seperat circits
            if (b1CircitIndex != b2CircitIndex)
			{
				var circit = Circits[b2CircitIndex];
                Circits[b1CircitIndex].AddRange(circit);
                Circits[b1CircitIndex] = Circits[b1CircitIndex].Distinct().ToList();
				Circits.RemoveAt(b2CircitIndex);
			}

            return Circits;
		}




		private static string PartTwo(bool runTestData)
		{
			return "part two not implemented yet.";
		}


	}
}
