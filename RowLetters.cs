using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{

    public class WordGuess
    {
        public Letters[] Letter { get; set; }
    }

    public partial class Letters
    {
        char letter;
        public char userInput
        {
            get => letter;
            set
            {
                if (letter == value)
                    return;
                letter = value;
            }
        }

        private Color colour;
    }
}

