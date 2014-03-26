namespace ESSpellcheck.SpellCheck
{
    public interface ISpellcheck
    {
        /// <summary>
        /// Gets the closest word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        string GetClosestWord(string word);
    }
}
