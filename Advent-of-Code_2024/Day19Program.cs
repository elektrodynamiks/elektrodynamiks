using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using AofC.Models;


namespace AofC
{

    public class Day19Program
    {
        List<string> patterns;
        List<string> designs;
        List<string> validDesigns;
        //List<string> combination;
        List<string> distinctItems;
     
        private int possibleDesign = 0;
        private int possibleCombination = 0;
        
        public Day19Program()
        {
            Console.WriteLine($"Day19 Started!");
            string fileExample = "Day19Example.txt";
            string filePuzzle = "Day19Input.txt";
            string validDesign = "Day19ValidDesign.txt";
            //var fileName = fileExample;
            var fileName = filePuzzle;
            
            patterns = new List<string>();
            designs = new List<string>();
            validDesigns = new List<string>();
          
            //Lists of patterns,designs    
            CreatePatternsDesigns(fileName);
            CreateValidDesignList(validDesign);
            
            //Day19Part1();
            Day19Part2();


        }
        private void Day19Part2()
        {
            var i = 0;
            var startAt = 295;
            foreach (var valid in validDesigns)
            {
                if (i >= (startAt))
                {
                    if (i == (startAt))
                    {
                        Console.WriteLine(startAt);
                    }

                    CountPossibleCombination(valid);
                }
                if (i >= startAt) {break;}
                i++;
            }
            Console.WriteLine(possibleCombination);
        }

        private void CountPossibleCombination(string valid)
        {   distinctItems = new List<string>();
            var i = 0;
            Console.WriteLine($"creating: {valid}");
            do
            {   
                
                var matchPattern = GetThePatternIncluded(valid, "random");
                var regexPattern = CreateRegexPatternPart2(matchPattern);
                //Console.WriteLine(regexPattern);
                CheckForPatternDesignMatchPart2(valid, regexPattern);
                distinctItems.Sort();
                distinctItems.Distinct().ToList();
                i++;
            } while (i <= 15000);

            distinctItems.Sort();
            distinctItems.Distinct().ToList();
            var uniqueStrings = distinctItems.Distinct().ToList();
            var items = 0;
            foreach (var iterm in uniqueStrings)
            {
                items++;
            }
            possibleCombination += items;
            Console.WriteLine(items);
          
            //DisplayListStr(uniqueStrings);
        }
        private void CheckForPatternDesignMatchPart2(string valid, string regexPattern)
        {
            var i = 0;
            var run = true;
            var combination = new List<string>(); 
            do
            { //CheckForPatternDesignMatch(design, regexPattern);
               
                string pattern = @regexPattern ;
                foreach (Match match in Regex.Matches(valid, pattern, RegexOptions.Compiled))
                {
                    //Console.WriteLine("Found '{0}' at position {1}", match.Value, match.Index);
                    combination.Add(match.Value);
                    valid = valid.Replace(match.Value,"");
                }

                if (valid.Length == 0)
                { 
                    //Console.Write($"Pattern is matching!:  ");
                    combination.Sort((x, y) => x.Length.CompareTo(y.Length));
                    combination.Sort();
                    run = false;
                    //DisplayListStr(combination);
                    var key = String.Concat(combination.ToArray());
                    distinctItems.Add(key);
                    //Console.WriteLine(key);
                }
                i++;
            } while (i <= 100 && run);
            
           
        }

        private void Day19Part1()
        {
           var i = 0;
           foreach (var design in designs)
           {
             CheckForPatternDesignMatchRandom(design);
               //if (i >= 10) {break;}
               i++;
           }
           Console.WriteLine(possibleDesign);
        }
        


        private void CheckForPatternDesignMatchRandom(string design)
        {
            var i = 0;
            var match = false;
            var order = "decreasing";
            //Console.WriteLine($"creating: {design}");
            do
            {
                if (i >= 1) { order = "random"; }
                var matchPattern = GetThePatternIncluded(design, order);
                var regexPattern = CreateRegexPattern(matchPattern);
                //Console.WriteLine(regexPattern);
                match = CheckForPatternDesignMatch(design, regexPattern);
                //Console.WriteLine($"try {i} :{match}");
                i++;
            } while (i <= 5 && !match);
        }

     
        private bool CheckForPatternDesignMatch(string design, string regexPattern)
        {
            var copy = design;
            string pattern = @regexPattern ;
            
            foreach (Match match in Regex.Matches(design, pattern, RegexOptions.Compiled))
            {
                Console.WriteLine("Found '{0}' at position {1}", match.Value, match.Index);
                design = design.Replace(match.Value,"");
            }

            if (design.Length == 0)
            { 
                // Console.WriteLine($"Pattern is matching!");
                Console.WriteLine($"{copy}");
                possibleDesign++;
                return true;
            }
            //Console.WriteLine(design);
            return false;
        }
        

        private List<string> GetThePatternIncluded(string design, string order = "none")
        {
            var matchingPattern = new List<string>();   
            foreach (var pattern in patterns)
            {
                var include = design.Contains(pattern);
                if (include)
                {
                    matchingPattern.Add(pattern);
                }
                   
            }
            // order the list 
            if (order == "none")
            { 
            }
            else if (order == "increasing")
            { 
                matchingPattern.Sort((x, y) => x.Length.CompareTo(y.Length));
            }
            else if (order == "decreasing")
            { 
                matchingPattern.Sort((x, y) => y.Length.CompareTo(x.Length));
            }
            else if (order == "random")
            {
                Random rnd = new Random(DateTime.Now.Millisecond);
                matchingPattern = matchingPattern.Select(item => new { item, order = rnd.Next()})
                    .OrderBy(x => x.order).Select(x => x.item)
                    .ToList();
            }

            return matchingPattern;
        }
        
        private string CreateRegexPattern(List<string> matchingPattern)
        {
            string regex = "(";
            foreach (var strPattern in matchingPattern )
            {
                regex += ($"({strPattern})|");
            }

            regex = regex.TrimEnd('|');
            regex += ")+";
            return regex;
        }
        private string CreateRegexPatternPart2(List<string> matchingPattern)
        {
            string regex = "(";
            foreach (var strPattern in matchingPattern )
            {
                regex += ($"({strPattern})|");
            }

            regex = regex.TrimEnd('|');
            regex += ")";
            return regex;
        }

        private void CreatePatternsDesigns(string fileName)
        {
            string Pattern = @"(\w)+";

            IEnumerable<string> lines = File.ReadLines(fileName);
            foreach (var line in lines)
            {
                if (line.Contains(","))
                {
                    foreach (Match match in Regex.Matches(line, Pattern, RegexOptions.Compiled))
                    {
                        patterns.Add(match.Value);
                        // Console.WriteLine("Found '{0}' at position {1}", match.Value, match.Index);
                    }
                }
                else
                {
                    if (line != "")
                    {
                        foreach (Match match in Regex.Matches(line, Pattern, RegexOptions.Compiled))
                        {
                            designs.Add(match.Value);
                            //Console.WriteLine("Found '{0}' at position {1}", match.Value, match.Index);
                        }
                    }
                }
            }
        }
        private void DisplayListStr(List<string> aList)
        {
            foreach (var el in aList)
            { Console.WriteLine($"{el} "); }
            Console.WriteLine();

        }

        // part2
            private void CreateValidDesignList(string fileName)
            {
                IEnumerable<string> lines = File.ReadLines(fileName);
                foreach (var line in lines)
                {
                    validDesigns.Add(line);
                }                       
            }
    }
}
