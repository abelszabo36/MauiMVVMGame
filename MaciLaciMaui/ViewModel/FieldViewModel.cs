using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaciMaui
{
    public class FieldViewModel : ViewModelBase
    {
        private Color color;
      public  Command Touch { get; set;}
        public int X { get; set; }
        public int Y { get; set; }

        public Color Color
        {
            get { return color; }
            set { color = value; OnpropertyChange(); }
        }
    }
}
