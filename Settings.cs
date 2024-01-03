using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//C# code for settings
namespace Wordle
{
    public class Settings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string correctSpace;
        private string incorrectSpace;

        public string CorrectSpace
        {
            get => correctSpace;
            set
            {
                if (correctSpace != value)
                {
                    correctSpace = value;
                    OnPropertyChanged();
                }
            }
        }
        public string IncorrectSpace
        {
            get => incorrectSpace;
            set
            {
                if (incorrectSpace != value)
                {
                    incorrectSpace = value;
                    OnPropertyChanged();
                }
            }
        }

        //Constructor
        public Settings()
        {
            CorrectSpace = "#2B0B98";
            IncorrectSpace = "#2B0B98";
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
