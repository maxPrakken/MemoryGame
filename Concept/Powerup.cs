using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

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
            List<int> scores = new List<int>();
            foreach(Player p in pl)
            {
                scores.Add(p.score);
            }
            scores = scores.OrderBy(emp => Guid.NewGuid()).ToList();

            for (int i = 0; i < pl.Count; i++)
            {
                pl[i].score = scores[i];
            }

            return pl;
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
}
