using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using AofC.Models;

namespace AofC
{

    public class day05Program
    {
        List<int[]> rules;
        List<int[]> prints;
        List<int[]> validPrints;
        List<int> validPrintsMiddlePage;
        // part 2 

        public day05Program()
        {
            string fileExample = "day05Example.txt";

            string filePuzzle = "day05Input.txt";

            //var fileName = fileExample;
             var fileName = filePuzzle;


            // part 1
            // Day05Part1(fileName);
            // part 2
            Day05Part1(fileName, "part2");
           
        }

        private void Day05Part1(string fileName, string part = "part1")
        {
            // separates rules and prints
            CreateRulesPrints(fileName);
            // check prints - agains rules
            CheckPrints(part);
            // not nice to be refactored.
            CheckPrints(part);
            CheckPrints(part);CheckPrints(part);
            CheckPrints(part);CheckPrints(part);
            CheckPrints(part);CheckPrints(part);
               CheckPrints(part);CheckPrints(part);
               CheckPrints(part);CheckPrints(part);

            // part 1 example 143; puzzle 5747
            MiddlePagesSum();

        }

        private void CreateRulesPrints(string fileName)
        {
            rules = new List<int[]>();
            prints = new List<int[]>();
            // regex separate with |
            string patternRule = @"(\d)+";
            // regex separate with '
            string patternPrint = @"(\d)+";
            //
            IEnumerable<string> lines = File.ReadLines(fileName);
            foreach (var line in lines)
            {
                if (line.Contains("|"))
                {
                    var Rule = new List<int>();
                    foreach (Match match in Regex.Matches(line, patternRule))
                    {
                        Rule.Add(Int32.Parse(match.Value));
                        // Console.WriteLine("Found '{0}' at position {1}", match.Value, match.Index);
                    }

                    rules.Add(Rule.ToArray());


                }
                else // (line.Contains(","));
                {
                    if (line != "")
                    {
                        var Print = new List<int>();
                        foreach (Match match in Regex.Matches(line, patternPrint))
                        {
                            Print.Add(Int32.Parse(match.Value));
                           
                            //Console.WriteLine("Found '{0}' at position {1}", match.Value, match.Index);
                        }
                        prints.Add(Print.ToArray());
                    }
                }
            }

            Console.WriteLine($"Rules has {rules.Count} rules");
            Console.WriteLine($"Prints has {prints.Count} prints");
        }

        private void CheckPrints(string part = "part1")
        
        { 
            validPrints = new List<int[]>();
            validPrintsMiddlePage = new List<int>() ;
            int printListIndex = 0;
            foreach (var print in prints)
            {
               
                Console.WriteLine($"Print {print}:");
                bool valid = true;
                bool ruleisBroken = false;
                
                /*
                    for (int index = 0; index < print.Count(); index += 1)
                    {
                        for (int target = index + 1; target < print.Count(); target += 1)
                        {
                            ruleisBroken = CheckRulesForPrint(print[index], print[target]);
                            valid = valid && ruleisBroken;
                            Console.WriteLine($"Compare for print: page[{print[index]}] | [{print[target]}]: {valid}");
                            
                            if (!ruleisBroken &&  (part == "part2"))
                            {
                                Console.WriteLine("reorder the pages!");
                                ReorderPrint(printListIndex, print, index,print[index], target,print[target]);
                                //valid = true;
                            }
                        }
                       
                    }
                */
                for (int index = 0; index < print.Count()-1; index++)
                {
                    ruleisBroken = false;
                    ruleisBroken = CheckRulesForPrint(print[index], print[index + 1]);
                    valid = valid && ruleisBroken;
                    // Console.WriteLine($"Compare for print: page[{print[index]}] | [{print[index + 1]}]: {valid}");
                    if (!ruleisBroken &&  (part == "part2"))
                    {
                        Console.WriteLine("reorder the pages!");
                        var firstpage = print[index];
                        var secondpage = print[index + 1];
                        print[index] = secondpage;
                        print[index + 1] = firstpage;
                        valid = true;
                    }

                }
                
                if (valid)
                {
                    //store the print and the middle page
                    validPrints.Add(print);
                    Console.WriteLine($"The middle page number is:{print[print.Count()/2]}");
                    validPrintsMiddlePage.Add(print[print.Count() / 2 ]);
                }
               
                printListIndex++;
            }
        }

        private bool CheckRulesForPrint(int firstPage, int secondPage)
        {
            var valid = true;
            foreach (var rule in rules)
            {
                // if page are in order or only if rule prevent the print
                valid = valid && PageOrderIsCorrect(firstPage, secondPage, rule);
            }
            return valid;
        }
        

        private bool PageOrderIsCorrect(int firstPage, int secondPage,int[] rule, string part = "part1")
        {
            
            if ((secondPage == rule[0])&&(firstPage == rule[1]))
            {  
                Console.Write($"checking rule {rule[0]}|{rule[1]} for first page {firstPage} and second page {secondPage}:");
                Console.WriteLine((secondPage == rule[0]) && (firstPage == rule[1]));
                return false;
                
            }
            return true;
        }

       

        private void MiddlePagesSum()
        {
            var sum = 0;
            foreach (var number in validPrintsMiddlePage)
            {
                sum += number;
            }
            // part 1 example 143; puzzle 5747
            Console.WriteLine(sum - 5747);
          
        }
        
        // part 2
        private void ReorderPrint(int printListIndex, int[] print,int index,int firstpage, int target, int secondpage)
        {
            Console.WriteLine($"reordering it");
            print[index] = secondpage;
            print[target] = firstpage;
            // prints[printListIndex] = print;
            
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