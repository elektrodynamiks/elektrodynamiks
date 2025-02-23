using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using AofC.Models;

namespace AofC.Days
{

    public class Day10Program
    {   private Map myMap;
        private char[][] TopographicMap;
        private List<int[]> startingPoints;

        private Dictionary<int, char> trekPath = new() { { 0, '0' } , { 1, '1' }, { 3, '3' }, {4, '4' }, { 5, '5' },{ 6, '6' },{ 7, '7' },{8, '8' },{9, '9' }};
        public Day10Program()
        {
            Console.WriteLine($"Day10 Started!");
            string fileExample = "InputFiles/Day10Example.txt";
            string filePuzzle = "InputFiles/Day10Input.txt";
            var fileName = fileExample;
            //var fileName = filePuzzle;
            myMap = new Map(fileName);
            TopographicMap = myMap.map;
            
            Day10Part1();

        }

        private void Day10Part1()
        {
            //finds all the starting coordinates
            FindAntennaPositions();
            // for each start point check if there is a Trail or more that can reach summit


        }

        private void Day10Part2()
        {
            
        }

        private void FindAntennaPositions()
        {
            startingPoints = new List<int[]>();
            // outer for loop abscissa
            for (int j = 0; j < TopographicMap.Length; j++)
            {
                // inner for loop ordinate
                for (int i = 0; i < TopographicMap[j].Length; i++)
                {
                    char antenna = TopographicMap[i][j];

                    if (antenna == '0')
                    {
                        //Console.WriteLine($"{i}, {j}");
                        startingPoints.Add([i, j]);

                    }
                }

            }
            Console.WriteLine(startingPoints.Count);

            foreach (var element in startingPoints)
            {
                Console.Write($"[{element[0]},{element[1]}]");
            }

        }

        private void FindTrailhead(int startx, int starty, int trek)
        {

        }

        private void TrailHike(int posx, int posy)
        {

        }

    }
}

/* iterate a list
       foreach (var element in list)
           Console.Write(element);
   or like an array
       for (int index = 0; index < list.Count; index += 1)
           Console.Write(list[index]);
    iterate a dictionary
foreach(KeyValuePair<string, string> ele2 in My_dict2)
  {
    Console.WriteLine("{0} and {1}", ele2.Key, ele2.Value);
    }
*/