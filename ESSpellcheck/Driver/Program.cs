using System;
using System.Collections.Generic;
using System.IO;
using ESSpellcheck.SpellCheck;

namespace ESSpellcheck.Driver
{
    internal class Program
    {
        private static ISpellcheck spellchecker;

        private static void Main(string[] args)
        {
            try
            {
                BuildSpellCheckDictionary();
                GetInputRun();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Put file in designated directory and re-run application");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Main run. Gets the input from the console and sends it to get closes word
        /// </summary>
        private static void GetInputRun()
        {
            while (true)
            {
                Console.Write(">>");
                string word = Console.ReadLine();
                try
                {
                    string suggestion = spellchecker.GetClosestWord(word);
                    Console.WriteLine(suggestion);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("Must input a word");
                }
                catch (WordNotFoundException ex)
                {
                    Console.WriteLine("NO SUGGESTION");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Builds the spell check dictionary.
        /// </summary>
        private static void BuildSpellCheckDictionary()
        {
            var words = new List<string>();
            using (StreamReader reader = File.OpenText(@"english.dict"))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;
                    words.Add(line);
                }

            if (words.Count == 0)
                throw new Exception("Dictionary file empty");

            spellchecker = new SpellcheckDictionary(words);
        }
    }
}