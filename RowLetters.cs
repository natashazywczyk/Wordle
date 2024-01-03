using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{

    public class Word
    {
        public Letters[] Letter { get; set; }
    }

    public partial class Letters
    {
        private char userInput;

        private Color colour;
    }
}

