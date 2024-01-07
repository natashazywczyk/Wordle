using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wordle
{
    public class Player
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string username1;
        private string username2;
        private int DOB1;
        private int DOB2;

        public string Username1
        {
            get => username1;
            set
            {
                if (username1 != value)
                {
                    username1 = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Username2
        {
            get => username2;
            set
            {
                if (username2 != value)
                {
                    username1 = value;
                    OnPropertyChanged();
                }
            }
        }

        //Constructor, default settings inside
        public Player()
        {
            Username1 = "";
            Username2 = "";
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SaveJson()
        {
            string jsonstring = JsonSerializer.Serialize(this);
            string filename = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "playerinfo.json");
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.Write(jsonstring);
            }
        }

    }
}
