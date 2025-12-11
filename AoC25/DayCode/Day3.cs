using System;
using System.Collections.Generic;
using System.Text;

namespace AoC25.Calendar
{
	internal class Day3
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
}
