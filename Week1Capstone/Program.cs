using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Week1Capstone
{
    class Program
    {
        static void Main()
        {
            string goAgain = "Y";
            while (goAgain == "Y" || goAgain == "YES")
            {
                string[] words = GetSentence().Split();    //Gets the sentence to translate and splits it by spaces into a string array.
                Console.WriteLine("\n");
                Console.WriteLine(SentenceCreator(words)); //Sends the string array to the SectenceCreator Method and displays.
                Console.WriteLine("\n");
                goAgain = GoAgain();                       //Asks user if they want to translate another senctence.
            }
            Console.WriteLine("Goodbye!");
            Console.ReadKey();
        }

        public static StringBuilder SentenceCreator(string[] words)
        {
            StringBuilder sentence = new StringBuilder("");  //Creates empty string builder type in which to build the Pig Latin sentence.
            foreach (string str in words)                    //Sends each word in the array to the Translate method which will return                                       
            {                                                //the Pig Latin tranlated word.
                StringBuilder translatedWord = Translate(str);
                sentence.Append(translatedWord);             //Adds Pig Latin word to sentence.
                if (words[words.Length-1] != str)            //If the word is not the last word of the sentence, this adds a space.
                    sentence.Append(" "); 
            }
            return sentence;
        }

        private static StringBuilder Translate(string str)
        {
            string patternUppercase = @"^[A-Z']+$";                    //Regex string to find word in UPPERCASE with no punctuation at the end.
            string patternUppercasePunc = @"^[A-Z']+[!\.?;,]+$";       //Regex string to find word in UPPERCASE with punctuation at the end.
            string patternTitlecase = @"^[A-Z][a-z']+[A-Z]?[a-z']?$";  //Regex string to find word in Titlecase with no punctuation at the end.
            string patternTitlecasePunc = @"^[A-Z][a-z']+[A-Z]?[a-z']?[!\.?;,]+$";  //Regex string to find word in Titlecase with punctuation at the end.
            string patternLowercase = @"^[a-z']+$";                    //Regex string to find word in lowercase with no punctuation at the end. 
            string patternLowercasePunc = @"^[a-z']+[!\.?;,]+$";       //Regex string to find word in lowercase with punctuation at the end.   

            if (Regex.IsMatch(str, patternUppercase))            //This section finds which pattern the string matches and sends the string
                goto UPPERCASE;                                  //to the appropriate section below to be tranlated into Pig Latin.

            if (Regex.IsMatch(str, patternUppercasePunc))
                goto UPPERCASEPUNC;

            if (Regex.IsMatch(str, patternTitlecase))
                goto Titlecase;

            if(Regex.IsMatch(str,patternTitlecasePunc))
                goto TitlecasePunc;

            if(Regex.IsMatch(str, patternLowercase))
                goto lowercase;

            if(Regex.IsMatch(str, patternLowercasePunc))
                goto lowercasepunc;

            else                                                   //If the string matches none of the above.  Then it has some sort of symbol
            {                                                      //in the middle of the word (e.i. "@", ".",) so this returns the string as is. 
                StringBuilder newWord = new StringBuilder(str);
                return newWord;
            }

        Titlecase:
            {
                int firstVowelIndex = GetFirstVowelIndex(str);
                StringBuilder newWord = new StringBuilder("");
                if (firstVowelIndex == 0)
                {
                    newWord.Append(str + "way");
                    return newWord;
                }

                else
                {
                    str = char.ToLower(str[0]) + str.Substring(1);
                    return newWord.Append(char.ToUpper(str[firstVowelIndex]) + str.Substring(firstVowelIndex + 1) + str.Substring(0, firstVowelIndex) + "ay");    
                }

            }
        TitlecasePunc:
            {
                int firstVowelIndex = GetFirstVowelIndex(str);
                int puncIndex = IndexOfPunc(str);     //Finds starting index of punctuation in case there's multiple in a row (ie. !!!! or ????)
                StringBuilder newWord = new StringBuilder("");
                if (firstVowelIndex == 0)
                {
                    newWord.Append(str.Substring(0, puncIndex) + "way" + str.Substring(puncIndex));
                    return newWord;
                }

                else
                {
                    str = char.ToLower(str[0]) + str.Substring(1);
                    return newWord.Append(char.ToUpper(str[firstVowelIndex]) + str.Substring(firstVowelIndex + 1, puncIndex - firstVowelIndex - 1) + str.Substring(0, firstVowelIndex) + "ay" + str.Substring(puncIndex));
                }
            }

        UPPERCASE:
            {
                int firstVowelIndex = GetFirstVowelIndex(str);
                StringBuilder newWord = new StringBuilder("");
                if (firstVowelIndex == 0)
                {
                    newWord.Append(str + "WAY");
                    return newWord;
                }
                else
                {
                    return newWord.Append(str.Substring(firstVowelIndex) + str.Substring(0, firstVowelIndex) + "AY");
                }
            }

        UPPERCASEPUNC:
            {
                int firstVowelIndex = GetFirstVowelIndex(str);
                int puncIndex = IndexOfPunc(str);     //Finds starting index of punctuation in case there's multiple in a row (ie. !!!! or ????)
                StringBuilder newWord = new StringBuilder("");
                if (firstVowelIndex == 0)
                {
                    newWord.Append(str.Substring(0, puncIndex) + "WAY" + str.Substring(puncIndex));
                    return newWord;
                }
                else
                {
                    return newWord.Append(str.Substring(firstVowelIndex, puncIndex - 1) + str.Substring(0, firstVowelIndex) + "AY" + str.Substring(puncIndex));
                }
            }
        
        lowercase:

            {
                int firstVowelIndex = GetFirstVowelIndex(str);
                StringBuilder newWord = new StringBuilder("");
                if (firstVowelIndex == 0)
                {
                    newWord.Append(str + "way");
                    return newWord;
                }
                else
                {
                    return newWord.Append(str.Substring(firstVowelIndex) + str.Substring(0, firstVowelIndex) + "ay");
                }
            }

        lowercasepunc:
            {

                int firstVowelIndex = GetFirstVowelIndex(str);
                int puncIndex = IndexOfPunc(str);     //Finds starting index of punctuation in case there's multiple in a row (ie. !!!! or ????)
                StringBuilder newWord = new StringBuilder("");
                if (firstVowelIndex == 0)
                {
                    newWord.Append(str.Substring(0, puncIndex) + "way" + str.Substring(puncIndex));
                    return newWord;
                }
                else
                {
                    return newWord.Append(str.Substring(firstVowelIndex, puncIndex - 1) + str.Substring(0, firstVowelIndex) + "ay" + str.Substring(puncIndex));
                }
            }
            
        }

        public static string GetSentence()     //Method that returns the sentence to be tranlated into Pig Latin
        {
            Console.Write("Provide a sentance to be translated into Pig Latin: ");
            string sentence = Console.ReadLine();
            while (sentence.Trim() == "")
            {
                Console.Write("You did not provide anything. Please provide a sentance to be translated into Pig Latin: ");
                sentence = Console.ReadLine();
            }

            return sentence;
        }

        public static string GoAgain()     //Method that checks if the users wants to translate another sentence.
        {
            Console.Write("Translate another sentence? Y/N: ");
            string answer = Console.ReadLine().ToUpper();
            while (answer != "Y" && answer != "YES" && answer != "N" && answer != "NO")
            {
                Console.Write("Invalid input. Translate another sentence? Y/N: ");
                answer = Console.ReadLine().ToUpper();
            }
            return answer;
        }

        public static int IndexOfPunc(string str)     //Method that returns the index (as an int) of the first occurance of punctuation in a string
        {
            Regex regex = new Regex(@"[!?;,\.]");
            Match match = regex.Match(str);
            return match.Index;
        }

        public static int GetFirstVowelIndex(string str)  //Method that returns the index (as a int) for the first vowel in a word.
        {                                                 //Returns 0 is there is no vowel in the word.
            Regex regex = new Regex(@"[aeiouAEIOU]");
            Match match = regex.Match(str);
            return match.Index;

        }       
    }
}
