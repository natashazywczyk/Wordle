using System;
using System.IO;
using System.Reflection;


namespace Wordle
{
    public partial class MainPage : ContentPage
    {
        Random random;
        private int interval = 1000;
        private int countdown = 60;
        private bool run = false;

        private System.Timers.Timer timer;

        public MainPage()
        {
            InitializeComponent();

            SetUpTimers();
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
        }

        private void WordleStart()
        {
            InitialiseGrid1();
            InitialiseGrid2();
            InitialiseGrid3();

            timer.Start();

            run = true;

            timer_lbl.Text = countdown.ToString();

        }

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

