using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Wordle;
public class WordleViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    WordleViewModel wordleModel;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void check(char[] answer)
    {
        
    }
}

