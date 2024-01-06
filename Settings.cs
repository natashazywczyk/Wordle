using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

//C# code for settings
namespace Wordle
{
    public class Settings
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

        //Constructor, default settings inside
        public Settings()
        {
            CorrectSpace = "#4dba47";
            IncorrectSpace = "#bbbf47";
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SaveJson()
        {
            string jsonstring = JsonSerializer.Serialize(this);
            string filename = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "settings.json");
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.Write(jsonstring);
            }
        }
    }
}
