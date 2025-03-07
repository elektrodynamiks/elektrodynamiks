using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using AofC.Models;

namespace AofC.Days
{

    public class Day10Program
    {
        private Map myMap;

        private char[][] TopographicMap;

        // private char[][] TrailMap;
        private List<int[]> startingPoints;
        private List<int[]> nextTrailHeads;
        private int trailheads = 0;

        private Dictionary<int, char> trekPath = new()
        {
            { 0, '0' }, { 1, '1' }, { 2, '2' }, { 3, '3' }, { 4, '4' }, { 5, '5' }, { 6, '6' }, { 7, '7' }, { 8, '8' },
            { 9, '9' }
        };

        private List<int[]> trekMove = [[-1, 0], [1, 0], [0, -1], [0, 1]];

        public Day10Program()
        {
            Console.WriteLine($"Day10 Started!");
            string fileExample = "InputFiles/Day10Example.txt";
            string filePuzzle = "InputFiles/Day10Input.txt";
            //var fileName = fileExample;
            var fileName = filePuzzle;
            myMap = new Map(fileName);
            TopographicMap = myMap.map;
            Day10Part1();
            Day10Part2();
            
        }

        private void Day10Part1()
        {
            //finds all the starting coordinates
            Console.WriteLine("Day 10 - Part One");
            FindAntennaPositions();
            // for each start point check if there is a Trail or more that can reach summit
            Console.WriteLine("");
            Console.WriteLine($"Level '9' ({trailheads}): ");
           


        }

        private void Day10Part2()
        {
            Console.WriteLine("Day 10 - Part Two");
            FindAntennaPositions_all();
           
        }

        private void FindAntennaPositions_all()
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

           
            Console.Write($"Level '0' ({startingPoints.Count}): ");
            foreach (var element in startingPoints)
            {
                Console.Write($"[{element[0]},{element[1]}]");
                
            }
            Console.WriteLine();
            FindTrailhead_all(startingPoints, 1);
        }

        private void FindTrailhead_all(List<int[]> trailHeads, int Level)
        {
            if (Level <= 9)
            {
                nextTrailHeads = new List<int[]>();
                var nextTrekLevel = Level++;
                var nextPath = trekPath[nextTrekLevel];

                foreach (var trail in trailHeads)
                {
                    foreach (var movement in trekMove)
                    {
                        var nextRow = trail[0] + movement[0];
                        var nextCol = trail[1] + movement[1];
                        if (myMap.inBounds(nextCol, nextRow))
                        {
                            if (TopographicMap[nextRow][nextCol] == nextPath)
                            {
                                //Console.WriteLine($"{nextTrekLevel}:{nextPath}");
                                TopographicMap[nextRow][nextCol] = nextPath;
                                nextTrailHeads.Add([nextRow, nextCol]);
                            }
                        }
                    }
                }

                Console.Write($"Level '{Level - 1}' ({nextTrailHeads.Count}): ");
                
                //foreach (var element in nextTrailHeads)
                //{
                //    Console.Write($"[{element[0]},{element[1]}]");
                //}

                Console.WriteLine();
                FindTrailhead_all(nextTrailHeads, Level);
            }
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
            
            Console.Write($"Level '0' ({startingPoints.Count}): ");
            foreach (var element in startingPoints)
            {
                Console.Write($"[{element[0]},{element[1]}]");
                var TrailMap = myMap.CreateEmptyMap();
                TrailMap[element[0]][element[1]] = '0';
                FindTrailhead(TrailMap, 0);
            }

        }

        private void FindTrailhead(char[][] trailMap, int Level)
        {
            for (int trekLevel = Level; trekLevel < 9; trekLevel++)
            {


                // outer for loop abscissa
                for (int col = 0; col < TopographicMap.Length; col++)
                {
                    // inner for loop ordinate
                    for (int row = 0; row < TopographicMap[col].Length; row++)
                    {

                        if (trailMap[row][col] == trekPath[trekLevel])
                        {

                            var nextTrekLevel = trekLevel + 1;
                            var nextPath = trekPath[nextTrekLevel];

                            foreach (var movement in trekMove)
                            {
                                var nextRow = row + movement[0];
                                var nextCol = col + movement[1];
                                if (myMap.inBounds(nextCol, nextRow))
                                {
                                    if (TopographicMap[nextRow][nextCol] == nextPath)
                                    {
                                        //Console.WriteLine($"{nextTrekLevel}:{nextPath}");
                                        trailMap[nextRow][nextCol] = nextPath;


                                    }
                                }


                            }


                        }
                    }
                }

                for (int col = 0; col < TopographicMap.Length; col++)
                {
                    // inner for loop ordinate
                    for (int row = 0; row < TopographicMap[col].Length; row++)
                    {

                        if (trailMap[row][col] == '9')
                            
                        {
                            trailheads++;
                        }
                    }
                }
            }
            //myMap.PrintMapPlan(trailMap);
            //Console.WriteLine(trailheads);
    }  }
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