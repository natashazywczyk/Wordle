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
    private int rowNum;
    private int colNum;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

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


        if (response.IsSuccessStatusCode)
        {
            string contents = await response.Content.ReadAsStringAsync();
            wordList = JsonSerializer.Deserialize<List<Word>>(contents);

            //return await response.Content.ReadAsStringAsync();
        }

        return;
    }
}

