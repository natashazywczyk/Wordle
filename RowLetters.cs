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
        public Letters[] Letter { get; set; }

        public void checkLetters(char[] correctAnswer)
        {

        }
    }

    public partial class Letters : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private char userInput;
        public char UserInput
        {
            get => userInput;
            set
            {
                if (userInput != value)
                {
                    userInput = value;
                    OnPropertyChanged(nameof(UserInput));
                }
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Color colour;
    }
}

