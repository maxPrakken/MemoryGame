using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concept
{
    class Player
    {
        public string name;
        public int score = 0;
        public Powerup powerup;

        public Player()
        {

        }
        public Player(string name)
        {
            this.name = name;
        }
    }
}
