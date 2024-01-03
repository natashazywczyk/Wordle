using IntelliJ.Lang.Annotations;
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
    public event PropertyChangedEventHandler PropertyChanged;

    WordleViewModel wordleModel;
    HttpClient httpClient;
    List<Word> wordList;
    private bool isBusy;
    private int rowNum;
    private int colNum;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
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


    private bool IsNotBusy => !IsBusy;


    public WordleViewModel()
    {
        httpClient = new HttpClient();
        wordList = new();
    }

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

    public void LetterEnter(char letter)
    {

    }

    private async Task GetWords()
    {
        if (wordList.Count > 0)
        {
            return;
        }

        var response = await httpClient.GetAsync("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/main/words.txt");
    }
}

