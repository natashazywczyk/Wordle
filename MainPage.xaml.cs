﻿using System;
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
        char[] lettersArray = { '-', '-', '-', '-', '-' };
        private int cont = 0;
        string[] allAvailableWords;
        public int GamesPlayedTotal { get; set; }
        public WordGuess[] Rows { get; }
        public string filePath = "C:\\Zywczyk_Natasha_Wordle\\Wordle\\Resources\\Raw\\words.txt";


        private System.Timers.Timer timer;

        public MainPage()
        {
            InitializeComponent();
            viewModel = new WordleViewModel();
            BindingContext = viewModel;

            Rows = new WordGuess[6]
            {
            new WordGuess(),
            new WordGuess(),
            new WordGuess(),
            new WordGuess(),
            new WordGuess(),
            new WordGuess()
            };

            correctAnswer = "GROWN".ToCharArray();

            GamesPlayedTotal = Preferences.Default.Get("playedGamesTotal", 0);


            SetUpTimers();
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
        }

        private string answer;
        public string Answer
        {
            get { return answer; }
            set { answer = value; }
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

             PlayerBtn.IsVisible = false;
             RuleBtn.IsVisible = false;

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
            else
                set = new Settings();
            //UpdateSettings();
            //SetUpGrid();

            allInitialised = true;
        }

        //Reads file and adds strings to an array, then converting each string into an array of characters
        public static string[] ReadFileAndConvertToChars(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                string[] wordsArray = new string[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] words = lines[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                }

                List<char[]> WordListCharaters = new List<char[]>();

                foreach (string word in wordsArray)
                {
                    // Convert each word to a char array
                    char[] characters = word.ToCharArray();

                    // Add the char array to the list of character arrays
                    WordListCharaters.Add(characters);
                }

               // char[][] allCharactersArrays = WordListCharacters.ToArray();

                return wordsArray;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading the file: {ex.Message}");
            }
        }




        char[] correctAnswer;

        //Reads characters entered from keyboard and moves over to next column
        private void charBtnClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (letterEnteredCount > 4)
                    return;

                
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
                currentColumn++;
                letterEnteredCount++;

                if(letterEnteredCount > 4 && currentColumn >= lettersArray.Length)
                {
                    currentColumn = 0;
                }

            }
        }

        //Move to next row
        private void enterBtnClicked(object sender, EventArgs e)
        {
            if (letterEnteredCount > 4)
            {
                currentRow++;
                currentColumn = 0;
                letterEnteredCount = 0;
            }
        }


        //Adds up total games played
        public void TotalGamesPlayed(int choice)
        {
            if (choice == 0)
            {
                Preferences.Default.Set("playedGamesTotal", ++GamesPlayedTotal);
            }

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
                        FontSize = 30,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,

                        TextColor = Colors.White,
                        BackgroundColor = Colors.LightSteelBlue,
                        Margin = new Thickness(5),
                        HeightRequest = 60,
                        WidthRequest = 60,
                    };


                    wordGuessCharacters.Children.Add(letter);
                    Grid.SetRow(letter, x);
                    Grid.SetColumn(letter, y);
                }
            }
        }
         private void WordleStart()
         {
             initializeLetterGrid();

             timer.Start();

             run = true;

             timer_lbl.Text = countdown.ToString();

         }


        private async void SettingsBtn_Clicked(object sender, EventArgs e)
        {
            SettingsPage setpage = new SettingsPage(set);
            fromsettingspage = true;
            await Navigation.PushAsync(setpage);
        }

        /* private void UpdateSettings()
         {
             Resources["CorrectSpaceColour"] = Color.FromArgb(set.CorrectSpace);
             Resources["IncorrectSpaceColour"] = Color.FromArgb(set.IncorrectSpace);
         }*/

        private async void RuleBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RulesPage());
        }

        private async void PlayerBtn_Clicked(object sender, EventArgs e)
        {
            PlayerPage setpage2 = new PlayerPage(player);
            fromplayerpage = true;
            await Navigation.PushAsync(setpage2);
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

