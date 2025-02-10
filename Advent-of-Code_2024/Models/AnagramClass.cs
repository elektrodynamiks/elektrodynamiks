using MapPlanClass;

namespace AnagramClass
{
    class Anagram
    {
        // counting the anagram
        public int numberOfAnagram;

        // the reference to check against
        public char[] myAnagram;

        public int anagramLength;

        public Anagram(string word)
        { //init
            numberOfAnagram = 0;
            myAnagram = word.ToArray();
            anagramLength = word.Length;
            //BL
            Console.WriteLine("Anagram Length: {0}", anagramLength);
        }

        public void CheckAnagram(MapPlan myWordMap, string ordinals, char firstLetter)
        {
            Console.WriteLine("Start Anagram Count");
            // test one location
            // gridNavidation(5, 6, "N", myWordMap);
            // iterate over the wordMap.
            // outer for loop
            for (int i = 0; i < myWordMap.Plan.Length; i++)
            {
                // inner for loop
                for (int j = 0; j < myWordMap.Plan[i].Length; j++)
                {
                    //Console.Write(myWordMap.Plan[i][j]);
                    if (IsTheFistLetterCorrect(myWordMap.Plan[i][j], firstLetter))
                    {
                        if (ordinals == "N")
                        {
                            // gridNavidation(i, j, ordinals, myWordMap);
                        }
                        if (ordinals == "NE")
                        {
                            crossNavidation(i, j, ordinals, myWordMap);
                        }
                    }
                }
                //Console.WriteLine();
            }
        }

        public bool IsTheFistLetterCorrect(char letter, char firstLetter)
        {
            var isCorrect = false;
            if (letter == firstLetter)
            {
                isCorrect = true;
            }
            // Console.WriteLine("IsTheFistLetterCorrect: " + isCorrect);

            return isCorrect;
        }

        public void ItIsAnAnagram(char[] word)
        {
            var validAnagram = true;
            for (int i = 0; i < anagramLength; i++)
            {
                validAnagram = validAnagram & (myAnagram[i] == word[i]);
            }

            if (validAnagram)
            {
                numberOfAnagram += 1;
                Console.WriteLine(
                    "Word: {1}{2}{3} {0}, Count: {4}.",
                    validAnagram,
                    word[0],
                    word[1],
                    word[2],
                    numberOfAnagram
                );
            }
        }

        public void ItIsAnAnagram(char[] word1, char[] word2)
        {
            char[] charArray = myAnagram;
            Array.Sort(charArray);

            char[] charArray1 = word1;
            Array.Sort(charArray1);
            char[] charArray2 = word2;
            Array.Sort(charArray2);

            var validAnagram = true;
            for (int i = 0; i < anagramLength; i++)
            {
                validAnagram = validAnagram & (myAnagram[i] == charArray1[i]);
            }
            for (int i = 0; i < anagramLength; i++)
            {
                validAnagram = validAnagram & (myAnagram[i] == charArray2[i]);
            }
            if (validAnagram)
            {
                numberOfAnagram += 1;
                Console.WriteLine("Word: {0}.{1}", word1[0], word2[0]);
                Console.WriteLine("Word: .{0}.", word1[1]);
                Console.WriteLine("Word: {0}.{1}", word2[2], word1[2]);
                Console.WriteLine("Word: {0}, Count: {1}.", validAnagram, numberOfAnagram);
            }
        }

        public void gridNavidation(int yCord, int xCord, string cardinal, MapPlan myWordMap)
        {
            var gridWord = new char[anagramLength];
            gridWord[0] = myWordMap.ReadMap(yCord, xCord);

            switch (cardinal)
            {
                case "N":
                    {
                        Console.Write("dir N: [0]:{0}, ", gridWord[0]);
                        for (int i = 1; i < anagramLength; i++)
                        {
                            gridWord[i] = myWordMap.ReadMap(yCord - i, xCord);
                            Console.Write("[{0}]:{1}, ", i, gridWord[i]);
                        }

                        ItIsAnAnagram(gridWord);
                        Console.WriteLine();
                        goto case "E";
                    }
                    ;
                case "E":
                {
                    Console.Write("dir E: [0]:{0}, ", gridWord[0]);
                    for (int i = 1; i < anagramLength; i++)
                    {
                        gridWord[i] = myWordMap.ReadMap(yCord, xCord + i);
                        Console.Write("[{0}]:{1}, ", i, gridWord[i]);
                    }
                    ItIsAnAnagram(gridWord);
                    Console.WriteLine();
                    goto case "S";
                }
                case "S":
                {
                    Console.Write("dir S: [0]:{0}, ", gridWord[0]);
                    for (int i = 1; i < anagramLength; i++)
                    {
                        gridWord[i] = myWordMap.ReadMap(yCord + i, xCord);
                        Console.Write("[{0}]:{1}, ", i, gridWord[i]);
                    }
                    ItIsAnAnagram(gridWord);
                    Console.WriteLine();
                    goto case "W";
                }

                case "W":
                {
                    Console.Write("dir W: [0]:{0}, ", gridWord[0]);
                    for (int i = 1; i < anagramLength; i++)
                    {
                        gridWord[i] = myWordMap.ReadMap(yCord, xCord - i);
                        Console.Write("[{0}]:{1}, ", i, gridWord[i]);
                    }
                    ItIsAnAnagram(gridWord);
                    Console.WriteLine();
                    goto case "NE";
                }
                // Ordinals
                case "NE":
                {
                    Console.Write("dir NE: [0]:{0}, ", gridWord[0]);
                    for (int i = 1; i < anagramLength; i++)
                    {
                        gridWord[i] = myWordMap.ReadMap(yCord - i, xCord + i);
                        Console.Write("[{0}]:{1}, ", i, gridWord[i]);
                    }
                    ItIsAnAnagram(gridWord);
                    Console.WriteLine();
                    goto case "SE";
                }
                case "SE":
                {
                    Console.Write("dir SE: [0]:{0}, ", gridWord[0]);
                    for (int i = 1; i < anagramLength; i++)
                    {
                        gridWord[i] = myWordMap.ReadMap(yCord + i, xCord + i);
                        Console.Write("[{0}]:{1}, ", i, gridWord[i]);
                    }
                    ItIsAnAnagram(gridWord);
                    Console.WriteLine();
                    goto case "SW";
                }
                case "SW":
                {
                    Console.Write("dir SW: [0]:{0}, ", gridWord[0]);
                    for (int i = 1; i < anagramLength; i++)
                    {
                        gridWord[i] = myWordMap.ReadMap(yCord + i, xCord - i);
                        Console.Write("[{0}]:{1}, ", i, gridWord[i]);
                    }
                    ItIsAnAnagram(gridWord);
                    Console.WriteLine();
                    goto case "NW";
                }
                case "NW":
                {
                    Console.Write("dir NW: [0]:{0}, ", gridWord[0]);
                    for (int i = 1; i < anagramLength; i++)
                    {
                        gridWord[i] = myWordMap.ReadMap(yCord - i, xCord - i);
                        Console.Write("[{0}]:{1}, ", i, gridWord[i]);
                    }
                    ItIsAnAnagram(gridWord);
                    Console.WriteLine();
                    break;
                }
            }
        }

        public void crossNavidation(int yCord, int xCord, string cardinal, MapPlan myWordMap)
        {
            var xWord1 = new char[anagramLength];
            xWord1[0] = myWordMap.ReadMap(yCord - 1, xCord - 1);
            xWord1[1] = myWordMap.ReadMap(yCord, xCord);
            xWord1[2] = myWordMap.ReadMap(yCord + 1, xCord + 1);

            var xWord2 = new char[anagramLength];
            xWord2[0] = myWordMap.ReadMap(yCord - 1, xCord + 1);
            xWord2[1] = myWordMap.ReadMap(yCord, xCord);
            xWord2[2] = myWordMap.ReadMap(yCord + 1, xCord - 1);
            /*
                        Console.WriteLine("[{1},{0}]", xCord, yCord);
                        Console.WriteLine("Cross: {0}.{1}", xWord1[0], xWord2[0]);
                        Console.WriteLine("Cross: .{0}.", xWord1[1]);
                        Console.WriteLine("Cross: {0}.{1}", xWord2[2], xWord1[2]);
                        */
            ItIsAnAnagram(xWord1, xWord2);
        }
    }
}
