using System.Diagnostics;

namespace MaciLaciMaui
{
    public partial class AppShell : Shell
    {
        private GameModel model = null;
        private DataAccess persistence = null;
        private GameViewModel viewModel = null;
        private int time = 0;
        private IDispatcherTimer timer;
        private SettingsPageViewModel settingsPageViewModel = null;





        public AppShell(DataAccess data, GameModel model, GameViewModel viewmodel)
        {
            this.persistence = data;
            this.model = model;
            this.viewModel = viewmodel;

            InititializeGame();

            timer = Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => TimeTick();
            timer.Start();

            InitializeComponent();

        }


        private void InititializeGame()
        {
            model.Motion += new EventHandler<MotionEventArgs>(UpdateHeroPosition);
            model.Collect += new EventHandler<CollectedEventArgs>(BaskettCollected);
            model.Result += new EventHandler<ResultEventArgs>(EndGame);

            viewModel.PauseEvent += new EventHandler(Pause);
            viewModel.SettingsEvent += new EventHandler(Settings);



            settingsPageViewModel = new SettingsPageViewModel();
            settingsPageViewModel.Level1Event += new EventHandler(Level1);
            settingsPageViewModel.Level2Event += new EventHandler(Level2);
            settingsPageViewModel.Level3Event += new EventHandler(Level3);
            BindingContext = viewModel;
        }


        internal void StartTimer() => timer.Start();
        internal void StopTimer() => timer.Stop();  


        private void NewGame(string level)
        {
            time = 0;
            model = new GameModel(persistence);
            model.NewGame(level);
            viewModel = new GameViewModel(model);
        }

        private void TimeTick()
        {
            ++time;
            viewModel.Time = time;
            UpdateEnemyPosition();
        }


        private void BaskettCollected(object sender, CollectedEventArgs e)
        {
            viewModel.Collected = model.Collected;
        }


        private void EndGame(object sender, ResultEventArgs e)
        {
            timer.Stop();
            viewModel.Enabled = false;
            if (e.Win)
            {
                DisplayAlert("Gratulálok", viewModel.Time.ToString() + " másodperc alatt teljesítetted a pályát", "Ok");
            }
            else if (e.Lose)
            {
                DisplayAlert("Sajnálom", "Sajnálom vesztettél", "Ok");
            }
        }

        private void UpdateHeroPosition(object sender, MotionEventArgs e)
        {
            viewModel.ClearHero();
            viewModel.PlaceHero(e.X, e.Y);
        }


        public void UpdateEnemyPosition()
        {
            viewModel.ClearEnemyPosition();
            model.EnemyMove();
            viewModel.SetEnemyPosition();
        }


        private void Pause(object sender, EventArgs e)
        {
            if (viewModel.Enabled)
            {
                timer.Stop();
                viewModel.Enabled = false;
            }
            else
            {
                timer.Start();
                viewModel.Enabled = true;
            }
        }


        private async void Settings(object sender, EventArgs e)
        {
            timer.Stop();
            await Navigation.PushAsync(new SettingsPage
            {
                BindingContext = settingsPageViewModel
            });
        }


        private async void Level1(object sender, EventArgs e)
        {
            try
            {
              
                await Navigation.PopAsync();
                NewGame("level1.txt");
                InititializeGame();
                timer.Start();
                BindingContext = viewModel;
            }
            catch
            {

                throw;
            }
        }



        private async void Level2(object sender, EventArgs e)
        {

            try
            {

                await Navigation.PopAsync();
                NewGame("level2.txt");
                InititializeGame();
                timer.Start();
                BindingContext = viewModel;
            }
            catch
            {

                throw;
            }
        }


        private async void Level3(object sender, EventArgs e)
        {
            try
            {

                await Navigation.PopAsync();
                NewGame("level3.txt");
                InititializeGame();
                timer.Start();
                BindingContext = viewModel;
            }
            catch
            {

                throw;
            }
        }

    }
}