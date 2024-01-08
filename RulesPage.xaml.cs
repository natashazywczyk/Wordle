using System.Runtime.CompilerServices;

namespace Wordle;

public partial class RulesPage : ContentPage
{
	public RulesPage()
	{
		InitializeComponent();

		var Info = new Label
		{
			Text = "Welcome to Wordle!" +
			"\n\nIn this game, your goal to to guess a random 5 digit word, from a bank of 3000 words" +
			"\n\nYou are given 6 chances to guess one word per round" +
			"\nIf you fail to guess it correctly in 6 turns, you lose!" +
			"\n\nIf you have a letter that's included in the word, in the correct space, the box will turn green" +
			"\nIf you guess a letter that's included in the word, but in the incorrect space, the box will turn yellow" +
			"\nIncorrect answers will remain the same colour as it was before submitting your guess" +
			"\n\nYou have 60 seconds to guess the correct word" +
			"\n\n\nGood Luck!!!",
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = 20
        };
		BackgroundColor = Color.FromArgb("#A468CF");


        Content = Info;
	}

}