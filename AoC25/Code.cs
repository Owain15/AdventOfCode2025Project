namespace AoC25
{
    public static class Code
    {
        internal static List<string> GetTestData(int dayIndex)
        {
			//validate input ...
			//$(MSBuildProjectName)
			return System.IO.File.ReadAllLines($"C:\\Users\\ojdav\\source\\repos\\AdventOfCode2025Project\\AoC25\\res\\TestData\\D{dayIndex}.txt").ToList();
		}

		internal static List<string> GetData(int dayIndex)
		{
			//validate input ...

			return System.IO.File.ReadAllLines($"C:\\Users\\ojdav\\source\\repos\\AdventOfCode2025Project\\AoC25\\res\\Data\\D{dayIndex}.txt").ToList();
		}
	}
}
