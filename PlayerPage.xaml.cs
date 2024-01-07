namespace Wordle;

public partial class PlayerPage : ContentPage
{
	Player player;
	public PlayerPage(Player p)
	{
		this.player = p;
		InitializeComponent();
		BindingContext = player;
	}

    private async void SaveBtn2_Clicked(object sender, EventArgs e)
    {
        player.SaveJson();
        await Navigation.PopAsync();
    }

}