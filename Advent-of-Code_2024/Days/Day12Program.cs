using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;
using AofC.Models;

namespace AofC
{

    public class Day12Program
    {
        private Map myMap;
        private char[][] GardenMap;
        private Dictionary<char, int> gardenPlantsAera = new Dictionary<char, int>();
        private Dictionary<char, int> gardenPlantsPerimeter = new Dictionary<char, int>();
        private Dictionary<char, char[][]> PlantsSurface = new Dictionary<char, char[][]>();

        private Dictionary<char, char> gardenPlantTypes = new Dictionary<char, char>();
        private Dictionary<char, List<int[]>> gardenPlantsSurface = new Dictionary<char, List<int[]>>();
        private List<char[][]> RegionsOfType;

        private List<int[]> proximity = [[-1, 0], [0, -1], [0, 1], [1, 0]];

        public Day12Program()
        {
            Console.WriteLine($"Day12 Started!");
            string fileExample = "InputFiles/Day12Example.txt";
            string filePuzzle = "InputFiles/Day12Input.txt";
            var fileName = fileExample;
            //var fileName = filePuzzle;
            myMap = new Map(fileName);
            GardenMap = myMap.map;

            Day12Part1();
          
        }

        private void Day12Part1()
        {
            CatalogGardenPlants();
            CreateAreaMapOfPlants();
            
            //DisplayGardenPlants(gardenPlantTypes);
        }

        private void CatalogGardenPlants()
        {
            // outer for loop abscissa
            for (int j = 0; j < GardenMap.Length; j++)
            {
                // inner for loop ordinate
                for (int i = 0; i < GardenMap[j].Length; i++)
                {
                    char gardenPlant = GardenMap[i][j];
                    gardenPlantsAera.TryAdd(gardenPlant, 0);
                    gardenPlantTypes.TryAdd(gardenPlant, gardenPlant);
                }
            }
        }


        private void DisplayGardenPlants(Dictionary<char, char> gardenPlantTypes)
        {
            int totalPrice = 0;
            foreach (KeyValuePair<char, char> kvp in gardenPlantTypes)
            {
                Console.WriteLine("Region of = {0}, Surface = {1}, Perimeter = {2}, Price = {3}",
                    kvp.Key, gardenPlantsAera[kvp.Key], gardenPlantsPerimeter[kvp.Key],
                    gardenPlantsAera[kvp.Key] * gardenPlantsPerimeter[kvp.Key]);
                totalPrice += gardenPlantsAera[kvp.Key] * gardenPlantsPerimeter[kvp.Key];
            }
            

            Console.WriteLine($"Total price offencing all regions = {totalPrice}");
        }

        private void CreateAreaMapOfPlants()
        {
            foreach (KeyValuePair<char, int> kvp in gardenPlantsAera)
            {
                char plantType = kvp.Key;
                int surface = 0;
                List<int[]> regions = new List<int[]>();
                var areaMap = myMap.CreateEmptyMap();
                // outer for loop abscissa
                for (int j = 0; j < GardenMap.Length; j++)
                {
                    // inner for loop ordinate
                    for (int i = 0; i < GardenMap[j].Length; i++)
                    {
                        char gardenPlant = GardenMap[i][j];
                        if (plantType == gardenPlant)
                        {
                            areaMap[i][j] = plantType;
                            regions.Add(new int[] { i, j });
                            surface++;
                        }
                    }
                }

                PlantsSurface.TryAdd(plantType, areaMap);
                gardenPlantsSurface.TryAdd(plantType, regions);
            }
            // Make the island type for surface
            var plant = 'I';
            var type = gardenPlantTypes[plant];
            var surfaceTypeList = gardenPlantsSurface[plant];
            var surfaceTypeMap = PlantsSurface[plant];
            Console.WriteLine();
            myMap.PrintMapPlan(PlantsSurface[plant]);
            
            foreach (var element in surfaceTypeList)
            {
                Console.Write($"[{element[0]},{element[1]}]");
                
            }
            Console.WriteLine();
            var indexList = new List<int>(surfaceTypeList.Count);
            for (int index = 0; index < surfaceTypeList.Count; index += 1)
            {   
                var element = surfaceTypeList[index];
                //Console.Write($"[{element[0]},{element[1]}]");
                // create a list
                var newList = new List<int[]>();
               
                // add element touching to list
                foreach (var prox in proximity)
                {
                    var nextCol = element[0] + prox[0];
                    var nextRow = element[1] + prox[1];
                    if (myMap.inBounds(nextCol, nextRow))
                    {
                        if (surfaceTypeMap[nextCol][nextRow] == plant)
                        {
                            Console.Write($"[{nextCol},{nextRow}]");
                            Console.Write($"{surfaceTypeMap[nextCol][nextRow]}");
                            Console.WriteLine();
                            newList.Add(element);
                            //indexList.Add(index);
                            //break;
                            //surfaceTypeList.RemoveAt(index);
                        }
                       
                    }
                }
            
                    
            }
           
           
           
            
            //gardenPlantsAera[plantType] = surface;
            //isItAnIslandRegion(plantType);
            //gardenPlantsPerimeter[plantType] = CalculatePerimeterOfPlant(plantType);
            //Console.WriteLine();
            //myMap.PrintMapPlan(areaMap);

        }

        private int CalculatePerimeterOfPlant(char plantType)
        {
            int perimeter = 0;
            var mySurface = PlantsSurface[plantType];
            // outer for loop abscissa
            for (int col = 0; col < mySurface.Length; col++)
            {
                // inner for loop ordinate
                for (int row = 0; row < mySurface[col].Length; row++)
                {
                    if (mySurface[row][col] == plantType)
                    {
                        foreach (var prox in proximity)
                        {
                            var nextRow = row + prox[0];
                            var nextCol = col + prox[1];
                            if (!myMap.inBounds(nextCol, nextRow) || mySurface[nextRow][nextCol] == ' ')
                            {
                                //Console.WriteLine($"This is a perimeter [{nextRow}:{nextCol}]");
                                perimeter++;
                            }
                        }

                    }
                }
            }

            //Console.WriteLine(perimeter);
            return perimeter;
        }

        private void isItAnIslandRegion(char plantType)
        {
            var mySurface = gardenPlantsSurface[plantType];
            bool isNeighbors = false;
            for (int i = 0; i < mySurface.Count - 1; i++){
               
                for (int j = 1; j < mySurface.Count; j++)
                {
                  
                    var distance = GetDistance( mySurface[j][0],  mySurface[j][1],  mySurface[i ][0],  mySurface[i ][1]);
                    if (distance <= 1)
                    {
                        Console.WriteLine("neighbors");
                        isNeighbors = true;
                        if (isNeighbors) { break; }
                    }
                    else
                    {
                        Console.Write($"too far from {mySurface[i][1]}, {mySurface[i][0]} - ");
                        Console.WriteLine($"too far from {mySurface[j][1]}, {mySurface[j][0]}");
                    }
                }
                Console.WriteLine(isNeighbors);
                Console.WriteLine();
               
            }
        }
    
        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
        
        private void Day12Part2(){}
    }
}
