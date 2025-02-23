using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using AofC.Models;

namespace AofC
{

    public class Day11Program
    {
        long count = 0;

        public Day11Program()
        {
            Day11Part1();
        }


        public IEnumerable<long> GetFilmsByGenre(string fileName)
        {
            var pebbles = Regex.Matches(File.ReadAllText(fileName), @"(\d)+")
                .Select(match => long.Parse(match.Value));
            return pebbles.ToList();
        }

        static IEnumerable<string> DisplayList(IEnumerable<long> aList) => aList.Select( list => $"|{list}| ");

        public void Display(IEnumerable<long> aList)
        {
            foreach (var element in DisplayList(aList))
                Console.Write(element);
            Console.WriteLine();
        }


        private void Day11Part1()
        {
            //string filePuzzle = "Day11Input.txt";
            //var fileName = filePuzzle;
            // List<long> pebbles = new List<long>();
            var pebbles = GetFilmsByGenre("Day11Input.txt");
            Display(pebbles);


        }

    }
}

/*
 private List<long> CreatePebblesList(string fileName)
          {
              string lines = File.ReadAllText(fileName);
              string regPattern = @"(\d)+";
              List<long> pebbles = new List<long>();
              foreach (Match match in Regex.Matches(lines, regPattern))
              {
                  pebbles.Add(Int32.Parse(match.Value));
              }
              return pebbles;
          }

private void DisplayListInt(IEnumerable<long> aList)
   {
       foreach (var el in aList)
       {
           Console.Write($"|{el}| ");
       }
       Console.WriteLine();
   }
*/
