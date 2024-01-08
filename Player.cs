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
        private int dob1;
        private int dob2;

        //Gets username of player1
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

        //Gets username of player 2
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

        //Gets DOB of player 1
        public int DOB1
        {
            get => dob1;
            set
            {
                if(dob1 != value)
                {
                    dob1 = value;
                    OnPropertyChanged();
                }
            }
        }
        //Gets DOB of player 2
        public int DOB2
        {
            get => dob2;
            set
            {
                if (dob2 != value)
                {
                    dob2 = value;
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

        //Saves settings as a .json file
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
