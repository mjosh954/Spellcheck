using System;

namespace ESSpellcheck;

public class WordNotFoundException : Exception
{

    public WordNotFoundException()
    {
    }

    public WordNotFoundException(string message) : base(message)
    {
    }
}
