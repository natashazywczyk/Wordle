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
        public WordGuess()
        {
            LettersTogether = new Letters[5]
            {
            new Letters(),
            new Letters(),
            new Letters(),
            new Letters(),
            new Letters()
            };
        }

        public Letters[] LettersTogether { get; set; }

        //Reads and Validates the users input
        public bool checkLetters(char[] correctAnswer)
        {
            int loop = 0;
            for (int i = 0; i < LettersTogether.Length; i++)
            {
                var letter = LettersTogether[i];
                if (letter.userInput == correctAnswer[i])
                {
                    letter.Color = Colors.Green;
                    loop++;
                }
                else if (correctAnswer.Contains(letter.userInput))
                {
                    letter.Color = Colors.Yellow;
                }
                else
                {
                    letter.Color = Colors.DimGray;
                }

                return loop == 5;
            }
        }
    }

    public partial class Letters 
    {
        public char userInput { get; set; }
        public Color Color { get; set; }  
    }
}

