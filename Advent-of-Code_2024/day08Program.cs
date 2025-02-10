using System;
using System.Collections.Generic;
using AofC.Models;

namespace AofC
{

    public class AofCDay08
    {
        private Map myMap;
        private char[][] map;
        private char[][] antinodes;
        private char[][] harmonicsResonance;
        private int antinodeCount = 0;

        List<int[]> positions;
        private Dictionary<char, List<int[]>> antennas;

        public AofCDay08()
        {
            string fileExample = "day08_example.txt";
            string filePuzzle = "day08_input.txt";
            //var fileName = fileExample;
             var fileName = filePuzzle;

            myMap = new Map(fileName);
            map = myMap.map;
            antinodes = myMap.initializeMap();
            harmonicsResonance = myMap.initializeMap();
            //Day08Part1();
            Day08Part2();
        }

        private void Day08Part1()
        {

            // read the map and store positions for each antenna
            // antennas are stored in dictionary with key as char named: 'antenna'.
            // each antennas has positions [x,y] int[] of size 2 stored in a lis
            FindAntennaPositions();


            // Fin antinodes for each antenna
            FindFrequencyAntinodes();
           

            // print the final number
            myMap.PrintMapPlan(antinodes);
            CountAntinodes();
            Console.WriteLine(antinodeCount);
        }

        private void Day08Part2()
        {
            FindAntennaPositions();
            // Find  Harmonics Resonance
            FindFrequencyAntinodes("part2");
            // print anc count
            myMap.PrintMapPlan(antinodes);
            CountAntinodes();
            Console.WriteLine(antinodeCount);
        }


        private void FindAntennaPositions()
        {
            antennas = new Dictionary<char, List<int[]>>();


            // outer for loop abscissa
            for (int j = 0; j < map.Length; j++)
            {
                // inner for loop ordinate
                for (int i = 0; i < map[j].Length; i++)
                {
                    char antenna = map[i][j];
                    if (antenna != '.')
                    {
                        // Console.WriteLine(antenna);
                        AddToAntennas(antenna, [i, j]);
                    }
                }

            }
        }

        private void AddToAntennas(char antenna, int[] position)
        {
            // add or create position for antenna 'char' 
            if (antennas.ContainsKey(antenna))
            {
                positions = antennas[antenna];
                positions.Add([position[0], position[1]]);
                antennas[antenna] = positions;
                // Console.WriteLine($"Position added to antenna '{antenna}'");
            }
            else
            {
                positions = new List<int[]>();
                positions.Add([position[0], position[1]]);
                antennas.Add(antenna, positions);
                // Console.WriteLine("New antenna created");
            }

            // Console.WriteLine($"'{antenna}':[{position[0]}, {position[1]}]");
        }

        private void DisplayAntennas()
        {
            foreach (var key in antennas.Keys)
            {
                Console.Write($"{key}: {antennas[key].Count}");
                DisplayPositions(antennas[key]);
                Console.WriteLine();
            }
        }

        private void DisplayPositions(List<int[]> positions)
        {
            if (positions.Count != 0)
            {
                foreach (var pos in positions)
                    Console.Write($"[{pos[0]}, {pos[1]}]");
            }
        }

        private void FindFrequencyAntinodes(string part = "part1")
        {
            foreach (var key in antennas.Keys)
                for (var index = 0; index < antennas[key].Count; index += 1)
                {
                    for (var target = index + 1; target < antennas[key].Count; target += 1)
                    {
                        var antennaList = antennas[key];
                        Console.WriteLine($"Compare for '{key}': List[{index}] with List[{target}]");
                        if (part == "part1")
                        {
                            Antinodes(antennaList[index], antennaList[target]);
                        }

                        if (part == "part2")
                        {
                            HarmonicsResonance(antennaList[index], antennaList[target]);
                        }
                    }
                }
        }

        private void Antinodes(int[] pos1, int[] pos2)
        {
            var xdistance = pos2[0] - pos1[0];
            var ydistance = pos2[1] - pos1[1];

            int[] antinode1 = [pos1[0] - xdistance, pos1[1] - ydistance];
            int[] antinode2 = [pos2[0] + xdistance, pos2[1] + ydistance];
            MarkAntinodes(antinode1);
            MarkAntinodes(antinode2);

        }

        private void MarkAntinodes(int[] antinode)
        {
            if (myMap.inBounds(antinode[0], antinode[1]))
            {
                Console.WriteLine($"antinode at [{antinode[0]}, {antinode[1]}]");
                var x = antinode[0];
                var y = antinode[1];
                antinodes[x][y] = '#';


            }
        }

        public void CountAntinodes(char track = '#')
        {
            for (int j = 0; j < antinodes.Length; j++)
            {
                // inner for loop ordinate
                for (int i = 0; i < antinodes[j].Length; i++)

                {
                    if (antinodes[i][j] == track)
                    {
                        antinodeCount++;
                    }
                }
            }

            Console.WriteLine("Number of antinodes: {0}", antinodeCount);
        }

    

    private void HarmonicsResonance(int[] pos1, int[] pos2)
    {
        // linear function from pos1 and pos2

        var xdistance = pos2[0] - pos1[0];
                var ydistance = pos2[1] - pos1[1];

                for (int i = -map.Length; i <= map.Length; i++)
                {
                    int[] antinode = [pos1[0] - xdistance*i, pos1[1] - ydistance*i];
                  
                    MarkAntinodes(antinode);
                    
                }
           
           
    }

       

    }
}


/* iterate by key for Dictionary
    foreach (var key in dictionary.Keys)
        Console.WriteLine(${key}={dictionary[key]}}
or by pair
    foreach (var pair in dictionary)
        Console.WriteLine($"{pair.key} = {pair.value}")};
*/
/* iterate a list
    foreach (var element in list)
        Console.Write(element);
or like an array
    for (int index = 0; index < list.Count; index += 1)
        Console.Write(list[index]);
*/