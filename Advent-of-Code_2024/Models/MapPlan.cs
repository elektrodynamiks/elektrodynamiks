namespace MapPlanClass
{
    public class MapPlan
    {
        // MapPlan Navigation is same as Array index
        private char[][] plan; //field in class

        // only be accessed within the same class
        public char[][] Plan // property
        {
            get { return plan; }
            // set { plan = value; }
        }
        private int[] mapSize;
        private int[] MapSize
        {
            get { return mapSize; }
            // set { mapSize = value; }
        }

        public MapPlan(string mapFileName)
        {
            mapSize = GetMapSize(mapFileName);
            plan = CreateMapPlan(mapFileName);
            // PrintMapPlan(Plan);
        }

        public int[] GetMapSize(string mapFileName)
        {
            var columns = Math.Abs(File.ReadLines(mapFileName).First().Length);
            var rows = File.ReadAllLines(mapFileName).Length;
            int[] mapSize = [rows, columns];
            Console.WriteLine("The maze size is {0} x {1}", mapSize[0], mapSize[1]);
            return mapSize;
        }

        public char[][] initializePlan(int[] mapSize)
        {
            var row = mapSize[0];
            var col = mapSize[1];
            char[][] jaggedArray = new char[row][];
            for (int i = 0; i < row; i++)
            {
                jaggedArray[i] = new char[col];
            }
            return jaggedArray;
        }

        public char[][] CreateMapPlan(string mapFileName)
        {
            // initialize the mapPlan
            var result = initializePlan(mapSize);
            // read the map and assign the content
            IEnumerable<string> lines = File.ReadLines(mapFileName);
            var row = 0;
            foreach (var line in lines)
            {
                // Console.WriteLine(line);
                char[] xCordinates = line.ToArray();
                var col = 0;
                foreach (var xCord in xCordinates)
                {
                    result[row][col] = xCord;
                    Console.Write("[{0},{1}] {2} ", row, col, result[row][col]);
                    col += 1;
                }
                row += 1;
                Console.WriteLine();
            }
            // PrintMapPlan(result);
            return result;
        }

        public void PrintMapPlan(char[][] jaggedArray)
        {
            // outer for loop
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                // Console.Write("Element " + i + ": ");
                // inner for loop
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    Console.Write(jaggedArray[i][j] + " ");
                }
                //Console.WriteLine();
            }
        }

        public bool inBounds(int yCord, int xCord)
        {
            // inside the map
            return (0 <= xCord && xCord < mapSize[1]) & (0 <= yCord && yCord < mapSize[0]);
        }

        public char ReadMap(int yCord, int xCord)
        {
            if (inBounds(yCord, xCord))
            {
                return plan[yCord][xCord];
            }
            return 'Œ';
        }

        public class Navigation
        {
            public char dir { get; set; }
            public int xCord { get; set; }
            public int yCord { get; set; }
            public int step { get; set; }
            public bool block { get; set; }
        }

        public Navigation Navigate(Navigation nav)
        {
            switch (nav.dir)
            {
                case '>':
                    //
                    break;
                case 'v':
                    //
                    break;
                case '<':
                    //
                    break;
                case '^':
                    //
                    break;
                default:
                    //
                    break;
            }
            return null;
        }
    }
}
