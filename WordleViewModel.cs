using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wordle;
public partial class WordleViewModel : INotifyPropertyChanged
{
    //PropertyChanged event declaration
    public event PropertyChangedEventHandler PropertyChanged;

    //Variables
    WordleViewModel wordleModel;
    HttpClient httpClient;
    List<Word> wordList;
    private bool isBusy;
    private int rowNum;
    private int colNum;

    //PropertyChanged method
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    //IsBusy method to check whether or not the program is busy
    public bool IsBusy
    {
        get => isBusy;
        set
        {
            if (isBusy == value)
                return;
            isBusy = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsNotBusy));

        }
    }
    public bool IsNotBusy => !isBusy;


    public WordleViewModel()
    {
        httpClient = new HttpClient();
        wordList = new();
    }

    //RowLetters.cs referenced
    private Word[] rows
    {
        get => rows;
        set
        {
            if(rows ==  value) 
                return;
            rows = value;   
            OnPropertyChanged();
        }
    }

    public void check(char[] answer)
    {
        
    }

    //If enter is pressed, to run this method
    public void EnterPressed()
    {
        if (colNum != 5)
            return;
     
        var allowed = true;

        //if they reach 6 rows, they,ve failed
        //If they are at row < 6, continue to move down a row
        if (allowed)
        {
            if (rowNum == 5)
            {
                //end turn
            }
            else
            {
                rowNum++;
                colNum = 0;
            }

        }

    }

    //Method to check letters they've entered
    public void LetterEntered(char letter)
    {
        if (colNum == 5)
            return;
    }

    //Read the .json file for words
    private async Task GetWords()
    {
        if (wordList.Count > 0)
        {
            return;
        }

        var response = await httpClient.GetAsync("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/main/words.txt");

        if (response.IsSuccessStatusCode)
        {
            string contents = await response.Content.ReadAsStringAsync();
            wordList = JsonSerializer.Deserialize<List<Word>>(contents);

            //return await response.Content.ReadAsStringAsync();
        }

        return;
    }
}

