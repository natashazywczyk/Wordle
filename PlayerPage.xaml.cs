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
}