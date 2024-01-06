using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{

    public class WordGuess
    {
        int loop = 0;
        public Letters[] Letter { get; set; }

        public void checkLetters(char[] correctAnswer)
        {

        }
    }

    public partial class Letters 
    {
        private char userInput { get; set; }
        private Color Color { get; set; }  
    }
}

