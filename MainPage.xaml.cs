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

            //if (!run)
            // {
            //    MoleStart();
            // }
            //  else
            // {
            bool choice = await DisplayAlert("Question,", "Would you like to start the game?", "Yes ", "No ");

            if (choice)
            {
                WordleStart();
            }
            //}

        }
        private void InitialiseGrid()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {

                    BoxView box1 = new BoxView()
                    {
                        CornerRadius = 5,
                        Color = Color.FromRgb(20, 200, 20)
                    };
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
            InitialiseGrid();

        }

    }
}

