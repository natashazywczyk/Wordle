namespace Wordle;

public partial class Welcome : ContentPage
{
    public Welcome()
    {
        InitializeComponent();
    }
    private async void StartBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage", true);
    }
}