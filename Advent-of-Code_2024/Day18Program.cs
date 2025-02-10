using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using AofC.Models;

namespace AofC
{

    public class Day18Program
    {


        public Day18Program()
        {
            Console.WriteLine($"Day18 Started!");
            string fileExample = "Day18Example.txt";
            string filePuzzle = "Day18Input.txt";
            //var fileName = fileExample;
            var fileName = filePuzzle;

            Day18Part1();

        }

        private void Day18Part1()
        {
        }
        private void Day18Part2(){}
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