namespace AofC.Models
{
    public class Map
    {
        public int[] mapSize;
        public char[][] map;

        public Map(string mapFileName)
        {
            Console.WriteLine("MapClass initialized");
            //Get the size, work with index!
            mapSize = GetmapSize(mapFileName);
            // Create the int[][] map from file
            map = CreateMap(mapFileName);
            PrintMapPlan(map);


        }

        public int[] GetmapSize(string mapFileName)
        {
            var width = Math.Abs(File.ReadLines(mapFileName).First().Length);
            var depth = File.ReadAllLines(mapFileName).Length;
            int[] mapSize = [depth, width];
            Console.WriteLine("The maze size is {0} x {1}", mapSize[0], mapSize[1]);
            return mapSize;
        }

        public char[][] CreateMap(string mapFileName)
        {
            // initialize the mapPlan
            var result = initializeMap();
            // read the map and assign the content
            IEnumerable<string> lines = File.ReadLines(mapFileName);
            var row = 0;
            foreach (var line in lines)
            {
                // Console.WriteLine(line);
                char[] ordinates = line.ToArray();
                var col = 0;
                foreach (var ordinate in ordinates)
                {
                    result[col][row] = ordinate;
                    Console.Write("[{0},{1}] {2} ", col, row, result[col][row]);
                    col += 1;
                }

                row += 1;
                Console.WriteLine();
            }

            // PrintMapPlan(result);
            return result;
        }

        public char[][] CreateEmptyMap()
        {
            var ordinate = mapSize[0];
            var abscissa = mapSize[1];
            char[][] jaggedArray = initializeMap();
            for (int row = 0; row < ordinate; row++)
            {
                for (int col = 0; col < ordinate; col++)
                {
                    jaggedArray[row][col] = ' ';
                }
            }
            return jaggedArray;
        }

        public char[][] initializeMap()
        {
            var ordinate = mapSize[0];
            var abscissa = mapSize[1];
            char[][] jaggedArray = new char[ordinate][];
            for (int j = 0; j < ordinate; j++)
            {
                jaggedArray[j] = new char[abscissa];
            }

            return jaggedArray;
        }

        public void PrintMapPlan(char[][] jaggedArray)
        {
            // outer for loop
            for (int j = 0; j < jaggedArray.Length; j++)
            {
                // inner for loop
                for (int i = 0; i < jaggedArray[j].Length; i++)
                {
                   
                    Console.Write("[{0}]", jaggedArray[i][j]);
                }

                Console.WriteLine();
            }
        }

         public bool inBounds(int abscissa, int ordinate)

                {
                    // inside the map

                    bool insideX = (0 <= abscissa && abscissa < mapSize[0]);
                    bool insideY = (0 <= ordinate && ordinate < mapSize[1]);

                    //Console.WriteLine("[{0},{1}] insideX:{2}", abscissa, ordinate, insideX && insideY);
                    return  insideX && insideY;
                }


    }
}