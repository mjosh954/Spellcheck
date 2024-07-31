using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ESSpellcheck.SpellCheck;

public static class SpellCheckFactory
{
    public static async Task<ISpellcheck> GetDictionary(string fileName)
    {
        var words = new List<string>();
        using StreamReader reader = File.OpenText(fileName);
        while (!reader.EndOfStream)
        {
            string line = await reader.ReadLineAsync();
            if (null == line)
                continue;
            words.Add(line);
        }

        if (words.Count == 0)
            throw new Exception("Dictionary file empty");

        return new SpellcheckDictionary(words);
    }
}
