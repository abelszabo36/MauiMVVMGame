using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MaciLaciMaui
{
    public class GameViewModel : ViewModelBase
    {
        private int size;
        private GameModel gameModel;
        private int time = 0;
        private int baskettCollected = 0;
        private bool enabled = true;
        public ObservableCollection<FieldViewModel> Fields { get;  set; }


        public Command Pause { get; set; }
        public Command Settings { get; set; }
        public event EventHandler PauseEvent;
        public event EventHandler SettingsEvent;

        public GameViewModel(GameModel model)
        {
            this.gameModel = model;
            this.size = gameModel.Table.TableSize;
            Fields = new ObservableCollection<FieldViewModel>();
            Pause = new Command(x => OnPause());
            Settings = new Command(x => OnSettings());
            GenerateField();
        }
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public int Time
        {
            get { return time; }
            set { time = value; OnpropertyChange(); }
        }

        public int Collected
        {
            get { return baskettCollected; }
            set {  baskettCollected = value; OnpropertyChange(); }
        }

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        private void OnPause()
        {
            PauseEvent.Invoke(this, EventArgs.Empty);
        }

        private void OnSettings()
        {
            SettingsEvent.Invoke(this, EventArgs.Empty);
        }


        public RowDefinitionCollection RowDefinicion
        {
            get => new RowDefinitionCollection(Enumerable.Repeat(new RowDefinition(GridLength.Star), Size).ToArray());
        }

        public ColumnDefinitionCollection ColumnDefinicion
        {
            get => new ColumnDefinitionCollection(Enumerable.Repeat(new ColumnDefinition(GridLength.Star), Size).ToArray());
        }

        private void GenerateField()
        {
            Fields.Clear();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (gameModel.Table.GetField(i,j) == 1)
                    {
                        Fields.Add(new FieldViewModel
                        {
                            X = i,
                            Y = j,
                            Color = Colors.Black,
                            Touch = new Command(param =>
                            {
                                if (param is FieldViewModel field)
                                {
                                    StepHero(field);
                                }
                            })
                              
                        });
                    }
                    else if (gameModel.Table.GetField(i,j) == 2)
                    {
                        Fields.Add(new FieldViewModel
                        {
                            X = i,
                            Y = j,
                            Color = Colors.Red,
                            Touch = new Command(param =>
                            {
                                if (param is FieldViewModel field)
                                {
                                    StepHero(field);
                                }
                            })
                        });
                    }
                    else if (gameModel.Table.GetField(i,j) == 3)
                    {
                        Fields.Add(new FieldViewModel
                        {
                            X = i,
                            Y = j,
                            Color = Colors.Brown,
                            Touch = new Command(param =>
                            {
                                if (param is FieldViewModel field)
                                {
                                    StepHero(field);
                                }
                            })
                        });
                    }
                    else if (gameModel.Table.GetField(i,j) == 4)
                    {
                        Fields.Add(new FieldViewModel
                        {
                            X = i,
                            Y = j,
                            Color = Colors.Grey,
                            Touch = new Command(param =>
                            {
                                if (param is FieldViewModel field)
                                {
                                    StepHero(field);
                                }
                            })
                        });
                    }
                    else
                    {
                        Fields.Add(new FieldViewModel
                        {
                            X = i,
                            Y = j,
                            Color = Colors.Green,
                            Touch = new Command(param =>
                            {
                                if (param is FieldViewModel field)
                                {
                                    StepHero(field);
                                }
                            })
                        });
                    }
                }
            }
        }


        private void StepType(int x, int y)
        {
            if (Enabled)
            {
                if (x >= 0)
                {
                    if (x > 0)
                    {
                        if (gameModel.Table.GetField(x - 1, y) == 1)
                        {
                            gameModel.HeroMove(x - 1, y, 1, "down");
                        }
                    }
                    if (x < size - 1)
                    {
                        if (gameModel.Table.GetField(x + 1, y) == 1)
                        {
                            gameModel.HeroMove(x + 1, y, 1, "up");
                        }
                    }
                }
                if (y >= 0)
                {
                    if (y > 0)
                    {
                        if (gameModel.Table.GetField(x, y - 1) == 1)
                        {
                            gameModel.HeroMove(x, y - 1, 1, "right");
                        }
                    }
                    if (y < size - 1)
                    {
                        if (gameModel.Table.GetField(x, y + 1) == 1)
                        {
                            gameModel.HeroMove(x, y + 1, 1, "left");
                        }
                    }
                }
            }
        }

        private void StepHero(FieldViewModel selectedField)
        {
            int x = selectedField.X;
            int y = selectedField.Y;
            StepType(x, y);
        }


        public void PlaceHero(int x, int y)
        {
            foreach (var fields in Fields)
            {
                if (fields.X == x && fields.Y == y)
                {
                    fields.Color = Colors.Black;
                }
            }
        }

        public void ClearHero()
        {
            foreach (var fields in Fields)
            {
                if (gameModel.Table.GetField(fields.X, fields.Y) == 0)
                {
                    fields.Color = Colors.Green;
                }
            }
        }
      

        public void ClearEnemyPosition()
        {
            foreach (var fields in Fields)
            {
                if (fields.Color == Colors.Red)
                {
                    fields.Color = Colors.Green;
                }
            }    
        }

        public void SetEnemyPosition()
        {
            foreach (var fields in Fields)
            {
                int x = fields.X;
                int y = fields.Y;
                if (gameModel.Table.GetField(x,y) == 2)
                {
                    fields.Color = Colors.Red;
                }
            }
        }


    }
}
