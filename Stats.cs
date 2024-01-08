using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    public class Stats
    {
        private int gamesplayedtotal;
        private int guessestotal;

        public event PropertyChangedEventHandler PropertyChanged;

        public int GamesPlayedTotal
        {
            get => gamesplayedtotal;
            set
            {
                if (gamesplayedtotal != value)
                {
                    gamesplayedtotal = value;
                    OnPropertyChanged();
                }
            
            }
        }

        public int GuessesTotal
        {
            get => guessestotal;
            set
            {
                if(guessestotal != value)
                {
                    guessestotal = value;   
                    OnPropertyChanged();
                }
            }
        }
        public Stats()
        {
            GamesPlayedTotal = 0;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
