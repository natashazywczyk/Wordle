using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Wordle;
public partial class WordleViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    WordleViewModel wordleModel;
    HttpClient httpClient;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public WordleViewModel()
    {
        httpClient = new HttpClient();
    }

    public void check(char[] answer)
    {
        
    }

    private async Task GetWords()
    {
        var response = await httpClient.GetAsync("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/main/words.txt");
    }
}

