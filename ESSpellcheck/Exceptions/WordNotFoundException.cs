using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESSpellcheck
{
    public class WordNotFoundException : Exception
    {

        public WordNotFoundException()
        {
            
        }

        public WordNotFoundException(string message) : base(message)
        {
            
        }
    }
}
