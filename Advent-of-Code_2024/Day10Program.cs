using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using AofC.Models;

namespace AofC
{

    public class Day10Program
    {   private Map myMap;
        private char[][] TopographicMap;

        private Dictionary<int, char> trekPath = new() { { 0, '0' } , { 1, '1' }, { 3, '3' }, {4, '4' }, { 5, '5' },{ 6, '6' },{ 7, '7' },{8, '8' },{9, '9' }};
        public Day10Program()
        {
            Console.WriteLine($"Day10 Started!");
            string fileExample = "Day10Example.txt";
            string filePuzzle = "Day10Input.txt";
            var fileName = fileExample;
            //var fileName = filePuzzle;
            myMap = new Map(fileName);
            TopographicMap = myMap.map;
            
            Day10Part1();

        }

        private void Day10Part1()
        {
        }

        private void Day10Part2()
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