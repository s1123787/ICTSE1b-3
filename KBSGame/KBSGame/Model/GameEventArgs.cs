using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBSGame.Model
{
    public class GameEventArgs : EventArgs
    {
        public double x { get; set; }
        public double y { get; set; }
        public double bomx { get; set; }
        public double bomy { get; set; }

        public GameEventArgs(double x, double y, double bomx, double bomy)
        {
            this.x = x;
            this.y = y;
            this.bomx = bomx;
            this.bomy = bomy;
        }
    }
}
