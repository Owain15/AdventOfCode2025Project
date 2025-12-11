using System;
using System.Collections.Generic;
using System.Text;

namespace AoC25.Calendar
{
	internal class Day6
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
			var input = (runTestData) ? Code.GetTestData(6) : Code.GetData(6);

			int rowCount = input.Count - 1;
			int colCount = input[0].Split(' ').Where(x => double.TryParse(x, out double result)).Select(x => double.Parse(x)).ToList().Count;

			double[,] numbers = new double[colCount, rowCount];

			for (int i = 0; i < input.Count() - 1; i++)
			{
				var data = input[i].Split(' ').Where(x => double.TryParse(x, out double result)).Select(x => double.Parse(x)).ToArray();

				for (int j = 0; j < data.Length; j++)
				{
					numbers[j, i] = data[j];
				}

			}

			char[] operations = input.Last().Replace(" ", "").ToArray();

			if (operations.Length != colCount)
			{ return "Operation count does not match column count."; }

			List<double> results = new List<double>();

			for (int colIndex = 0; colIndex < colCount; colIndex++)
			{
				double colResult = numbers[colIndex, 0];

				for (int rowIndex = 1; rowIndex < rowCount; rowIndex++)
				{
					colResult = (operations[colIndex] == '+') ? colResult + numbers[colIndex, rowIndex]
							  : (operations[colIndex] == '*') ? colResult * numbers[colIndex, rowIndex]

							  : throw new Exception($"operation not found\n\rcollum : {colIndex}");
				}

				results.Add(colResult);
			}


			return results.Sum().ToString();

			//Test Result  4277556
		}
		private static string PartTwo(bool runTestData)
		{
			var input = (runTestData) ? Code.GetTestData(6) : Code.GetData(6);


			var lineData = input
			.Select(x => x.Split(' '))
			.ToList();


			var colSizeList = new List<double>();
			var opData = lineData.Last();

			while (opData.Count() < input.Last().Replace(" ", "").Length)            // - colom count
			{ opData = opData.Append("").ToArray(); }

			for (int i = 0; i < opData.Length - 1; i++)
			{
				if (opData[i] != "")
				{
					for (int j = i + 1; j < opData.Length; j++)
					{
						if (opData[j] != "")
						{
							colSizeList.Add(j - i);
							i = j - 1;
							break;
						}

						if (j == opData.Length - 1)
						{
							colSizeList.Add(j - i);
							i = j;
							break;
						}
					}
				}



			}

			var operations = opData
				.Where(x => x != "")
				.ToList();

			List<List<string>> Lines = new List<List<string>>();

			for (int i = 0; i < lineData.Count - 1; i++)
			{
				List<string> line = new List<string>();

				string stringData = "";

				for (int j = 0; j < lineData[i].Length; j++)
				{
					if (lineData[i][j] == "")
					{ stringData += " "; }
					else if (double.TryParse(lineData[i][j], out double r))
					{ stringData += lineData[i][j]; }

					if (stringData.Length == colSizeList[i])
					{
						line.Add(stringData);
						stringData = "";
					}
				}

				Lines.Add(line);
			}

			var colCount = Lines[0].Count;
			var rowCount = Lines.Count;

			List<double> results = new List<double>();

			for (int stringIndex = colCount - 1; stringIndex >= 0; stringIndex--)
			{
				var colData = new List<double>();

				for (int charIndex = (int)colSizeList[stringIndex] - 1; charIndex >= 0; charIndex--)
				{
					string cd = "";

					for (int lineIndex = 0; lineIndex < Lines.Count; lineIndex++)
					{
						cd += Lines[lineIndex][stringIndex][charIndex];
					}

					colData.Add(double.Parse(cd));

				}

				var colResult = colData[0];

				var t1 = operations[stringIndex];

				for (int i = 1; i < colData.Count; i++)
				{
					colResult = (operations[stringIndex] == "+") ? colResult + colData[i]
							  : (operations[stringIndex] == "*") ? colResult * colData[i]

					  : throw new Exception($"operation not found\n\rcollum : {stringIndex}");
				}

				results.Add(colResult);

			}

			return results.Sum().ToString();

			//Data 81444036920374 x to high 

			//Test Result  3263827
		}


	}
}
