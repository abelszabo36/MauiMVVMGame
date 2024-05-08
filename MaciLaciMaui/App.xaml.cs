namespace MaciLaciMaui
{
    public partial class App : Application
    {
        private readonly AppShell shell;
        private readonly GameModel gameModel;
        private readonly GameViewModel gameViewModel;
        private readonly DataAccess perisitence;
        public App()
        {
            InitializeComponent();
            perisitence = new DataAccess(FileSystem.AppDataDirectory);
            gameModel = new GameModel(perisitence);
            gameModel.NewGame("level1.txt");
            gameViewModel = new GameViewModel(gameModel);
            shell = new AppShell(perisitence, gameModel,gameViewModel);
            {
                BindingContext = gameViewModel;
            }
            MainPage = shell;
        }



        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);

           

            window.Created += (s, e) =>
            {
                shell.StartTimer();
            };

            // amikor az alkalmazás fókuszba kerül
            window.Activated += (s, e) =>
            {
                shell.StartTimer();
            };

            // amikor az alkalmazás fókuszt veszt
            window.Deactivated += (s, e) =>
            {
                shell.StopTimer();
            };

            return window;
        }
    }
}