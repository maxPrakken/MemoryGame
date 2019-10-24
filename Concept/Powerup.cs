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
        
        public List<Player> ShuffleScore(List<Player> pl)
        {
            Shuffle(pl);

            return pl;
        }

        private void Shuffle<T>(IList<T> list) // input players list
        {
            List<Player> ns = (List<Player>)list;

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            List<Player> sl = (List<Player>)list;

            for(int i = 0; i < list.Count; i++)
            {
                ns[i].score = sl[i].score;
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
