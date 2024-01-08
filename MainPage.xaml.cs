using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;


namespace Wordle
{
    public partial class MainPage : ContentPage
    {
        WordleViewModel viewModel;
        Random random;
        private int interval = 1000;
        private int countdown = 60;
        private bool run = false;
        private Settings set;
        private Player player;
        private bool fromsettingspage = false;
        private bool fromplayerpage = false;
        private bool allInitialised = false;
        private int currentRow = 0;
        private int currentColumn = 0;
        private int lettersClicked = 0;
        private int score = 0;
        private int letterEnteredCount = 0;
        char[] GuessWord = { '-', '-', '-', '-', '-' };
        char[] lettersArray = { '-', '-', '-', '-', '-' };
        private int cont = 0;
        public int GamesPlayedTotal { get; set; }
        public int GuessesTotal { get; set; }  
        public string filePath = "words.txt";


        private System.Timers.Timer timer;

        public MainPage()
        {
            InitializeComponent();
            LoadRandomWord();

            viewModel = new WordleViewModel();
            BindingContext = viewModel;

            GamesPlayedTotal = Preferences.Default.Get("playedGamesTotal", 0);
            GuessesTotal = Preferences.Default.Get("guessTotal", ++GuessesTotal);


            SetUpTimers();
            timer_lbl.IsVisible = false;
        }
   
        private void SetUpTimers()
        {

            timer = new System.Timers.Timer
            {
                Interval = interval
            };

            timer.Elapsed += Timer_Elapsed;

        }
        
        //Have the text show the countdown from 60 seconds and to stop at 0
        private void TimerFunction()
        {
            --countdown;
            timer_lbl.Text = countdown.ToString();

            if (countdown <= 0)
            {
                End();
            }
        }

        //Ends timer once countdown reaches 0 and asks the player if they wishb to play again
        private async void End()
        {
            timer.Stop();

            run = false;

            if (countdown == 0)
            {
                bool choice = await DisplayAlert("Uh-Oh,", "Looks like you ran out of time\nWould you like to start the game?", "Yes ", "No ");
                if (choice)
                {
                    WordleStart();
                    Preferences.Default.Set("playedGamesTotal", ++GamesPlayedTotal);
                }
            }
            
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Dispatch
                (
                    () =>
                    {
                        TimerFunction();
                    }
                );
        }

        //Start Button function asking player if they want to start
        public async void StartBtn_Clicked(object sender, EventArgs e)
         {
             StartBtn.Opacity = 1;
             await StartBtn.FadeTo(0, 500);

             bool choice = await DisplayAlert("Hello There!", "Would you like to start the game?", "Yes ", "No ");

             if (choice)
             {
                 WordleStart();
                 Preferences.Default.Set("playedGamesTotal", ++GamesPlayedTotal);
             }

             //Hides buttons after start is pressed, has the keyboard appear
             PlayerBtn.IsVisible = false;
             RuleBtn.IsVisible = false;
             StatBtn.IsVisible = false;
             backspaceEnteredGrid.IsVisible = true;

         }
        private async Task InitialiseObjectVariables()
        {
            //Check to see if a settings .json already exists
            string settingsfilename = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "settings.json");
            if (File.Exists(settingsfilename))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(settingsfilename))
                    {
                        string jsonstring = reader.ReadToEnd();
                        set = JsonSerializer.Deserialize<Settings>(jsonstring);
                    }
                }
                catch
                {
                    set = new Settings();
                }
            }
            //If it doesnt:
            else
                set = new Settings();
                UpdateSettings();
                initializeLetterGrid();

            allInitialised = true;
        }

        //Checks if playerinfo.json exists
        private async Task InitialiseObjectVariables2()
        {
            //Check to see if a settings .json already exists
            string playerinfofilename = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "playerinfo.json");
            if (File.Exists(playerinfofilename))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(playerinfofilename))
                    {
                        string jsonstring = reader.ReadToEnd();
                        player = JsonSerializer.Deserialize<Player>(jsonstring);
                    }
                }
                catch
                {
                    player = new Player();
                }
            }
            //If it doesnt:
            else
                player = new Player();
            UpdatePlayerInfo();
            initializeLetterGrid();

            allInitialised = true;
        }

        //Reads file and adds strings to an array, and chooses one at random
        private async Task PickRandomWord()
        {
            //read in the words.txt and add the current word to correct answer for the round, which guesses are then compared to this
            var folder = FileSystem.AppDataDirectory;
            var filePath = Path.Combine(folder, "words.txt");

            //Checks if the file is within the project
            if (File.Exists(filePath))
            {
                //Reads words into an array of strings
                var wordList = await File.ReadAllLinesAsync(filePath);
                if (wordList.Length > 0)
                {
                    var random = new Random();
                    string randWord = wordList[random.Next(wordList.Length)].ToUpper();

                    //Changes the string into a char array
                    GuessWord = randWord.ToCharArray();
                }
            }
        }
        
        //Stores and loads random word selected from words.txt
        private async void LoadRandomWord()
        {
            await PickRandomWord();
        }

        //Reads characters entered from keyboard and moves over to next column
        private void charBtnClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                //Checks if lettersEntered is greater than four
                if (letterEnteredCount > 4)
                    return;

                //Array of which the letters are going to be stored in an array
                char selectedLetter = button.Text.Length > 0 ? button.Text[0] : 'A';
                lettersArray[currentColumn] = selectedLetter;

                foreach (var child in wordGuessCharacters.Children)
                {
                    if (wordGuessCharacters.GetRow(child) == currentRow && wordGuessCharacters.GetColumn(child) == currentColumn && child is Label letter)
                    {
                        letter.Text = "" + selectedLetter;
                        break;
                    }
                }
                //Moves to next row while adding to letterEnteredCount
                currentColumn++;
                letterEnteredCount++;

                if(letterEnteredCount > 4 && currentColumn >= lettersArray.Length)
                {
                    currentColumn = 0;
                }

            }
        }

        //Move to next row, if row > 5, output that they have no more tries left for the round
        private async void enterBtnClicked(object sender, EventArgs e)
        {
            if (letterEnteredCount < 4)
                return;

            //If the column is at 5 letters, it will then allow the user to move down
            if (letterEnteredCount > 4)
            {
                currentRow++;
                currentColumn = 0;
                letterEnteredCount = 0;

                Preferences.Default.Set("guessTotal", ++GuessesTotal);
            }

            //Display message when they guess the word correctly
            if(lettersArray == GuessWord)
            {
                bool choice = await DisplayAlert("Congrats!", "You Won!\nWould you like to start the game?", "Yes ", "No ");
                if (choice)
                {
                    WordleStart();
                    Preferences.Default.Set("playedGamesTotal", ++GamesPlayedTotal);
                }
            }
            
            if(CorrectAnswerGuessed())
            {
                bool choice = await DisplayAlert("Congrats!", "You Won!\nWould you like to start the game?", "Yes ", "No ");
                if (choice)
                {
                    WordleStart();
                    Preferences.Default.Set("playedGamesTotal", ++GamesPlayedTotal);
                }
            }
            
            //Display a pop up to the user that the round is over
            if(currentRow > 4)
            {
                bool choice = await DisplayAlert("Uh-Oh,", "Looks like you ran out of guesses\nWould you like to start the game?", "Yes ", "No ");
                if (choice)
                {
                    WordleStart();
                    Preferences.Default.Set("playedGamesTotal", ++GamesPlayedTotal);
                }
            }

        }

        //Check and validate if the characters entered matches that of the guess word
        private char validateCharEntered()
        {
            if(lettersArray.Length > 4)
            {
                for(int i = 0; i < lettersArray.Length; i++)
                {
                    if(lettersArray[i] != GuessWord[i])
                    {
                        
                    }
                    else
                    {

                    }
                }
            }
            return ' ';
        }
        private Boolean CorrectAnswerGuessed()
        {
            int point = 0;
            for (int i = 0; i < GuessWord.Length; i++)
            {
                char answerChar = GuessWord[i];
                char guessChar = lettersArray[i];
                if (answerChar == guessChar)
                {
                    point++;
                }
            }

            //If the user has 5 "points", they win
            if (point == 5)
            {
                return true;
            }

            return false;
        }

        //Set up wordle guess grid 
        private void initializeLetterGrid()
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    var letter = new Label
                    {
                        Text = " ",
                        FontSize = 40,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,

                        TextColor = Colors.White,
                        BackgroundColor = Colors.LightSteelBlue,
                        Margin = new Thickness(5),
                        HeightRequest = 80,
                        WidthRequest = 80,
                    };


                    wordGuessCharacters.Children.Add(letter);
                    Grid.SetRow(letter, x);
                    Grid.SetColumn(letter, y);

                }
            }
        }

        //Start the game, timer and form the grid
         private void WordleStart()
         {
             initializeLetterGrid();

             countdown = 60;
             timer.Start();
             run = true;
             timer_lbl.IsVisible = true;
             timer_lbl.Text = countdown.ToString();
         }

        //To go to settings page
        private async void SettingsBtn_Clicked(object sender, EventArgs e)
        {
            SettingsPage setpage = new SettingsPage(set);
            fromsettingspage = true;
            await Navigation.PushAsync(setpage);
        }

        //Updates values inputed in settings
        private void UpdateSettings()
        {
             Resources["CorrectSpaceColour"] = Color.FromArgb(set.CorrectSpace);
             Resources["IncorrectSpaceColour"] = Color.FromArgb(set.IncorrectSpace);
        }

        private void UpdatePlayerInfo()
        {
            Resources["Player1Username"] = player.Username1;
            Resources["Player1DOB"] = player.DOB1;
            Resources["Player2Username"] = player.Username2;
            Resources["Player2DOB"] = player.DOB2;
        }

        //To go to rule page
        private async void RuleBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RulesPage());
        }

        //To navigate t0 player info page
        private async void PlayerBtn_Clicked(object sender, EventArgs e)
        {
            PlayerPage setpage2 = new PlayerPage(player);
            fromplayerpage = true;
            await Navigation.PushAsync(setpage2);
        }

        //To navigate to stats page
        private async void StatsBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatsPage());
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            //If the Navigation has come from the Welcome Page and the game has already started, reset the game
            if (allInitialised && !fromsettingspage)
            {
                initializeLetterGrid();
            }
            else if (fromsettingspage)
            {
                //Update settings if navigated from the settings page
                //UpdateSettings();
                fromsettingspage = false;
            }
            base.OnNavigatedTo(args);
        }

        /*
         * private void InitialiseGrid()
         {
             for (int i = 0; i < 6; i++)
             {
                 for (int j = 0; j < 5; j++)
                 {
                     Entry inputBox = new Entry
                     {
                         WidthRequest = 50,
                         HeightRequest = 50
                     };

                     Input.Add(inputBox, j, i);

                 }
             }
         }
        public async void StartBtn_Clicked(object sender, EventArgs e)
         {
             StartBtn.Opacity = 1;
             await StartBtn.FadeTo(0, 1000);

             bool choice = await DisplayAlert("Question,", "Would you like to start the game?", "Yes ", "No ");

             if (choice)
             {
                 WordleStart();
             }

         }
         private void InitialiseGrid1()
         {
             for (int i = 0; i < 1; i++)
             {
                 for (int j = 0; j < 5; j++)
                 {

                     BoxView box1 = new BoxView()
                     {
                         CornerRadius = 6,
                         Color = Color.FromRgb(255, 255, 0)
                     };

                     wordleGrid1.Add(box1, j, i);

                 }
             }
         }


         private void InitialiseGrid2()
         {
             for (int i = 0; i < 1; i++)
             {
                 for (int j = 0; j < 5; j++)
                 {

                     BoxView box2 = new BoxView()
                     {
                         CornerRadius = 5,
                         Color = Color.FromRgb(255, 0, 255)
                     };

                     wordleGrid2.Add(box2, j, i);

                 }
             }
         }

         private void InitialiseGrid3()
         {
             for (int i = 0; i < 1; i++)
             {
                 for (int j = 0; j < 5; j++)
                 {

                     BoxView box3 = new BoxView()
                     {
                         CornerRadius = 5,
                         Color = Color.FromRgb(0, 255, 255)
                     };

                     wordleGrid3.Add(box3, j, i);

                 }
             }
         }

        private void SetUpTimers()
        {

            timer = new System.Timers.Timer
            {
                Interval = interval
            };

            timer.Elapsed += Timer_Elapsed;

        }
        private void TimerFunction()
        {
            --countdown;
            timer_lbl.Text = countdown.ToString();

            if (countdown <= 0)
            {
                End();
            }
        }

        private void End()
        {
            timer.Stop();

            run = false;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Dispatch
                (
                    () =>
                    {
                        TimerFunction();
                    }
                );
        }*/

        /*private void WordleStart()
        {
            InitialiseGrid();
            InitialiseGrid2();
            InitialiseGrid3();

            timer.Start();

            run = true;

            timer_lbl.Text = countdown.ToString();

        }
        
         
        public void PressEnter()
        {
            if (currentColumn != 5)
                App.Current.MainPage.DisplayAlert("Uh-Oh", "Invalid Input!", "OK");

            var correct = Rows[currentRow].checkLetters(correctAnswer);

            if (correct)
            {
                if (cont == 0)
                {
                    TotalGamesPlayed(0);
                    App.Current.MainPage.DisplayAlert("You Win!", "Excellent Work!", "OK");
                }

                return;
            }

            if (currentRow == 5)
            {
                App.Current.MainPage.DisplayAlert("Game Over!", "No more tries left!", "OK");
            }
            else
            {
                currentRow++;
                currentColumn = 0;
            }
        }*/

    }
}


/*public void readWordList()
 {
     Assembly assembly = Assembly.GetExecutingAssembly();
     Stream? directWordList = assembly.GetManifestResourceStream("Wordle.Resources.words.txt");

     using (StreamReader read = new StreamReader(directWordList))
     {
         string wordList = read.ReadToEnd();
         // Process words here
     }
 }*/

