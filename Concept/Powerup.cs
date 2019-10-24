using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Concept
{
    class Powerup
    {
        public bool isused;

        public Powerup()
        {
            isused = false;
        }
    }

    class ScoreSwap : Powerup
    {
        List<Player> pl = new List<Player>();
        private Random rng = new Random();

        public ScoreSwap()
        {
            
        }
        public ScoreSwap(List<Player> pl)
        {
            this.pl = pl;

            Shuffle<Player>(pl);
        }

        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {   
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    class ThemeSwap : Powerup
    {
        public ThemeSwap()
        {
            
        }
    }

    class ShuffleCards : Powerup
    {
        public ShuffleCards()
        {

        }
    }
}
