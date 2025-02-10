using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using AofC.Models;

namespace AofC
{

    public class Day09Program
    {
        List<int> diskMap;
        List<int> diskBlockFile;
        List<int> diskBlockFreeSpace;

        List<string> fileSystemList;
        List<int[]> fileSystemListPart2;
        List<string> compactedFile;
        List<int> checksumValue;

        // part 2 

        public Day09Program()
        {
            string fileExample = "Day09Example.txt";

            string filePuzzle = "Day09Input.txt";

            var fileName = fileExample;
            //var fileName = filePuzzle;

            diskMap = new List<int>();
            diskBlockFile = new List<int>();
            diskBlockFreeSpace = new List<int>();
            fileSystemList = new List<string>();
            compactedFile = new List<string>();
            checksumValue = new List<int>();
            fileSystemListPart2 = new List<int[]>();
            CreateDiskMap(fileName);

            // part 1
            // Day09Part1();
            // part 2
            // creating filesystem with direct compacting
            Day09Part2();

        }




        private void Day09Part1()
        {


            CreateFileSystem();
            FileCompacting();
            Checksum();
        }

        private void Day09Part2()
        {

            CreateFileSystem();
            FileCompactingByBlock();
            //CreateFileSystemPart2();
            //FileCompactingPart2();
            // Checksum();
        }
        // part2

        private void CreateFileSystemPart2()
        {
            for (int index = 0; index < diskBlockFile.Count; index++)
            {
                var idx = index;
                var block = diskBlockFile[index];
                var free = diskBlockFreeSpace[index];
                fileSystemListPart2.Add([idx, block, free]);
                //  Console.WriteLine($"fileSys: [{idx},{block},{free}] ");
            }

            // create index for processing -> id = fileSystemListPart2.Count increment --;
            // when placement successful -> next to read is then id + 1 in the modified fileSystemListPart2

            // step1 


            {
               
                    for (int i = 0; i <  diskBlockFile.Count; i++)
                    {
                        var id = fileSystemListPart2.Count - 1 - i;
                        var value = fileSystemListPart2[id];
                        var ind = value[0];
                        var length = value[1];
                        for (int idfree = 0; idfree < id; idfree++)
                        {
/*
                                for (int index = 0; index < fileSystemListPart2.Count; index++)
                                {
                                    Console.WriteLine(
                                        $"[{fileSystemListPart2[index][0]},{fileSystemListPart2[index][1]},{fileSystemListPart2[index][2]}]");
                                }
*/

                            //Console.WriteLine($"{ind}{length}");
                            if (fileSystemListPart2[idfree][2] >= length)
                            {
                                //Console.WriteLine(
                                //    $"There is a free spot at idfree:{idfree} value:[{fileSystemListPart2[idfree][0]},{fileSystemListPart2[idfree][1]},{fileSystemListPart2[idfree][2]}]");
                                var free = fileSystemListPart2[idfree][2] - length;
                                int[] old = new[] { 0, 0, length };
                                int[] insert = new int[] { ind, length, free };
                                int[] replace = [fileSystemListPart2[idfree][0], fileSystemListPart2[idfree][1], 0];
                                fileSystemListPart2[idfree] = replace;
                                fileSystemListPart2[id - 1][2] += length + value[2];
                                fileSystemListPart2.RemoveAt(id);
                                fileSystemListPart2.Insert(idfree + 1, insert);
                                ;
                                break;
                            }

                        }
                    }
                }
            }

        

        
            
        

        // part 1
        private void DisplayListStr(List<string> aList)
        {
            foreach (var el in aList)
            {
                Console.Write($"|{el}| ");
            }

            Console.WriteLine();

        }
        private void DisplayListInt(List<int> aList)
        {
            foreach (var el in aList)
            {
                Console.Write($"{el} ");
            }

            Console.WriteLine();

        }

        private void CreateDiskMap(string fileName)
        {
            var reader = new StreamReader(fileName);
            while (!reader.EndOfStream)
            {
                var element = (char)reader.Read() - '0';
                diskMap.Add(element);

            }

            
            for (int index = 0; index < diskMap.Count; index++)
            {
                
                if (int.IsEvenInteger(index))
                {
                    diskBlockFile.Add(diskMap[index]);
                }
                else
                {
                    diskBlockFreeSpace.Add(diskMap[index]);
                }
                
            }

            Console.WriteLine($"diskMap : {diskMap.Count}");
            Console.WriteLine($"diskBlockFile #: {diskBlockFile.Count} ");
            Console.WriteLine($"diskBlockFreeSpace #: {diskBlockFreeSpace.Count} ");
            DisplayListInt(diskMap);
            DisplayListInt(diskBlockFile);
            DisplayListInt(diskBlockFreeSpace);
        }

        private void CreateFileSystem()
        {
           
            for (int index = 0; index < diskBlockFile.Count; index++)
            {
                var id = Convert.ToString(index);
                
                for (var fileLength = 0; fileLength < diskBlockFile[index]; fileLength++)
                {
                   
                        fileSystemList.Add(id);
                       // Console.Write(id);
                    
                   
                }
                
                if (index < diskBlockFreeSpace.Count)
                {
                    for (var blockLength = 0; blockLength < diskBlockFreeSpace[index]; blockLength++)
                    {
                        fileSystemList.Add(".");
                        //Console.Write(".");
                    }
                }

                
            }

            DisplayListStr(fileSystemList);
            Console.WriteLine();
        } 
        
        private void FileCompacting()
        {

            var leftIndex = 0;
            var rightIndex = fileSystemList.Count - 1;
            while (leftIndex <= rightIndex)
            {
                var disk = fileSystemList[leftIndex];
                if (disk != ".")
                {
                   
                    //Console.Write(disk);
                     compactedFile.Add(disk);
                    leftIndex++;
                }
                else
                {
                    var compacting = fileSystemList[rightIndex];
                    if (compacting != ".")
                    {
                        
                        //Console.Write(compacting);
                        compactedFile.Add(compacting);
                        rightIndex--;
                        leftIndex++;
                    }
                    else
                    {rightIndex--;
                    }
                }


            }
            DisplayListStr(compactedFile);
        }


        private void FileCompactingByBlock()
        {
            // need to find length of id - find "." in the begining to fit 
            // replace id pos with . and move file to begining.
            Console.WriteLine($"Compacting by Block");
            Console.WriteLine($" {fileSystemList.Count}");
          
            
            for (int endIndex = 0; endIndex < 10; endIndex++)
            {
               var  id = fileSystemList[fileSystemList.Count-endIndex-1];
               if (id != ".")
               {
                    Console.WriteLine($"endIndex: {fileSystemList.Count-endIndex-1}");
                    Console.WriteLine($"id: {id}");
                    var fileId = Int32.Parse(id);
                    var fileLength = diskBlockFile[(fileId)];
                    Console.WriteLine($"fileLength: {fileLength}");
                    endIndex += fileLength;
                    // looking for empty spots at begining of file.
                }
               // if (fileSystemList)
              
            }

        }



        private void Checksum()
        {
            //checksum
            long sum = 0;

            for (int index = 0; index < compactedFile.Count; index += 1)
            {
                if (compactedFile[index] != ".")
                {
                    var fileId = Int32.Parse(compactedFile[index]);
                    var value = fileId * index;

                    if (value != 0)
                    {
                        sum = sum + value;
                        checksumValue.Add(value);
                        //Console.Write($"index: {index}|{compactedFile[index]}, value: {value} ||");
                    }
                }

            }
            DisplayListInt(checksumValue);
            Console.WriteLine();
            Console.WriteLine($"The file checksum is: {sum}");

        }
        
    }
}