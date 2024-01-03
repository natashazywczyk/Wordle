using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{

    public class RowLetters
    {
        public Answer[] Answers { get; set; }
    }

    public partial class Answer
    {
        private char userInput;

        private Color colour;
    }
}

