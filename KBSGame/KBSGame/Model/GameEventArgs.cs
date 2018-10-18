using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBSGame.Model
{
    //this is information which is needed when playerwalkedonbomb event is raised
    public class GameEventArgs : EventArgs
    {
        //the position where the player is
        public double x { get; set; }
        public double y { get; set; }
        //the position where the bomb is
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
