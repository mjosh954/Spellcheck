using System;
using System.Collections.Generic;
using System.Linq;

namespace ESSpellcheck.SpellCheck
{
    public class SpellcheckDictionary : HashSet<string>, ISpellcheck
    {
        private static readonly char[] alphabetSet = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};

        public SpellcheckDictionary(IEnumerable<string> words) : base(words)
        {
            
        }

        /// <summary>
        /// Input word misspelled word and get closest one to it
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Can't get closest suggestion on empty word</exception>
        /// <exception cref="ESSpellcheck.WordNotFoundException">Word not found in dictionary</exception>
        public string GetClosestWord(string word)
        {
            if(string.IsNullOrEmpty(word))
                throw new ArgumentNullException("Can't get closest suggestion on empty word");

            if (Contains(word)) // word exists already
                return word;

            HashSet<string> candidates = new HashSet<string>();
            
            OneLetterOffWords(word, candidates); 
            OneLetterRemoval(word, candidates);
            SingleLetterAddition(word, candidates);

            if (candidates.Count == 0)
                throw new WordNotFoundException("Word not found in dictionary");

            
            if (candidates.Count == 1)
                return candidates.First();

            Random r = new Random();
            return candidates.ElementAt(r.Next(candidates.Count));
            
        }

        /// <summary>
        /// O(n)
        /// n = size of word
        /// m = 27
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="candidates">The candidates.</param>
        private void OneLetterOffWords(string word, HashSet<string> candidates)
        {
            var charArr = word.ToCharArray();
            foreach (char a in alphabetSet)
            {
                for (int i = 0; i < charArr.Length; i++)
                {
                    char hold = charArr[i];
                    charArr[i] = a;
                    string candidate = new string(charArr);
                    if (Contains(candidate))
                        candidates.Add(candidate);
                    charArr[i] = hold;
                }
            }
        }

        /// <summary>
        /// Called when [letter removal].
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="candidates">The candidates.</param>
        private void OneLetterRemoval(string word, HashSet<string> candidates)
        {
            var charArr = word.ToCharArray();
            for (var i = 0; i < charArr.Length; i++)
            {
                string candidate = charArr.Where((t, j) => i != j).Aggregate(string.Empty, (current, t) => current + t); // auto-gen from resharper
                if (Contains(candidate))
                    candidates.Add(candidate);
            }
        }

        private void SingleLetterAddition(string word, HashSet<string> candidates)
        {
            var charArr = word.ToCharArray();
            foreach (var a in alphabetSet)
            {
                for (int i = 0; i < charArr.Length + 1; i++)
                {
                    char[] extended = new char[charArr.Length + 1];
                    extended[i] = a;
                    bool addOne = false;
                    for (int j = 0; j < charArr.Length; j++)
                    {
                        if (i.Equals(j))
                        {
                            extended[j + 1] = charArr[j];
                            addOne = true;
                        }
                        else
                        {
                            if (!addOne)
                            {
                                extended[j] = charArr[j];
                            }
                            else
                            {
                                extended[j + 1] = charArr[j];
                            }
                        }
                    }

                    String candidate = new String(extended);

                    if (Contains(candidate))
                        candidates.Add(candidate);
                    
                }
            }
        }
        
    }
    
}
