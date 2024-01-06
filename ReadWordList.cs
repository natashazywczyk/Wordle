using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    public class ReadWordList
    {
        Random rand;

        private List<string> wordsFromList;
        private int length;

        public ReadWordList(Random rand)
        {
            this.rand = rand;
        }

        public List<string> WordsFromList
        {
            get { return wordsFromList; }  
            set { wordsFromList = value; }
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }
    }
}
