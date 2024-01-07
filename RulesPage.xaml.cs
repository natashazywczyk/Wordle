using System.Runtime.CompilerServices;

namespace Wordle;

public partial class RulesPage : ContentPage
{
	public RulesPage()
	{
		InitializeComponent();

		var Info = new Label
		{
			Text = "Welcome to Wordle!",
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = 20
        };

		Content = Info;
	}

}