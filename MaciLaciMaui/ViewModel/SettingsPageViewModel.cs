using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MaciLaciMaui
{
    public class SettingsPageViewModel
    {
        public Command Level1 { get; set; }
        public Command Level2 { get; set; }
        public Command Level3 { get; set; }
        public event EventHandler Level1Event;
        public event EventHandler Level2Event;
        public event EventHandler Level3Event;


        public SettingsPageViewModel()
        {
            Level1 = new Command(x => onLevel1());
            Level2 = new Command(x => onLevel2());
            Level3 = new Command(x => onLevel3());
        }
        

        private void onLevel1()
        {
            Level1Event.Invoke(this, EventArgs.Empty);
        }

        private void onLevel2()
        {
            Level2Event.Invoke(this, EventArgs.Empty);
        }

        private void onLevel3()
        {
           Level3Event.Invoke(this,EventArgs.Empty);
        }
    }
}
