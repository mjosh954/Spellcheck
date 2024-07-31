using ESSpellcheck.SpellCheck;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ESSpellcheck;

internal class Program
{
    private static async Task Main(string[] args)
    {
        try
        {
            var spellchecker = await SpellCheckFactory.GetDictionary("english.dict");
            GetInputRun(spellchecker);
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
    private static void GetInputRun(ISpellcheck spellchecker)
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
            catch (ArgumentNullException)
            {
                Console.WriteLine("Must input a word");
            }
            catch (WordNotFoundException)
            {
                Console.WriteLine("NO SUGGESTION");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}