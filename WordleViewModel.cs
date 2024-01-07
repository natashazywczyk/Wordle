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
    List<WordGuess> wordList;
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


    public void colourCorrect(char[] answer)
    {
        
    }


    //Read the .json file for words
    private async Task GetWords()
    {
        if (wordList.Count > 0)
        {
            return;
        }

        var response = await httpClient.GetAsync("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/main/words.txt");

        /*to change words in json file to char array
        for(int i = 0; i < 3000; i++)
        {
            ... = ... .ToCharArray();
        }
        */

        if (response.IsSuccessStatusCode)
        {
            string contents = await response.Content.ReadAsStringAsync();
            wordList = JsonSerializer.Deserialize<List<WordGuess>>(contents);

            //return await response.Content.ReadAsStringAsync();
        }

        return;
    }
}

