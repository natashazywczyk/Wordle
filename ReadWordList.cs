using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    public class ReadWordList
    {
        Random rand;

        private List<string> wordsFromList;
        private int length;

        public ReadWordList(Random rand)
        {
            this.rand = rand;
        }

        public List<string> WordsFromList
        {
            get { return wordsFromList; }  
            set { wordsFromList = value; }
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        public async void ReadFileIntoList()
        {
            string numLine;
            MainPage mPage = new MainPage();
            try
            {
                using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("word.txt");
                using StreamReader reader = new StreamReader(fileStream);
                while ((numLine = reader.ReadLine()) != null)
                {
                    mPage = new MainPage();
                    mPage.Answer = numLine;
                }
            }
            catch (Exception ex)
            {
                length = -1;
                MainPage error = new MainPage();
                error.Answer = ex.ToString();
            }
        }

        public ReadWordList()
        {
            wordsFromList = new List<string>();
            string savedfilelocation = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "word.txt");
            if (File.Exists(savedfilelocation))
            {
                ReadFileIntoList();
            }
            else
            {
            }
        }
    }
    
}
