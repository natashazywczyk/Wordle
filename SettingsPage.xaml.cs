namespace Wordle;

public partial class SettingsPage : ContentPage
{
    Settings set;
    public SettingsPage(Settings s)
    {
        this.set = s;
        InitializeComponent();
        BindingContext = set;
    }

    //Use SaveJson method when saved btn is clicked to save the new settings as a .json file and go back to MainPage
    private async void SaveBtn_Clicked(object sender, EventArgs e)
    {
        set.SaveJson();
        await Navigation.PopAsync();
    }
}