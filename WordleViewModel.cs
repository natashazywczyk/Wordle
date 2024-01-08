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
    private bool isBusy;

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



}

