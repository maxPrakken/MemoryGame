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

        public virtual void Use()
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
        }

        public override void Use()
        {
            base.Use();

            ShuffleScore(this.pl);
        }

        public void ShuffleScore(List<Player> pl)
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
        }
    }

    class ThemeSwap : Powerup
    {
        Memorygame _mg;

        public ThemeSwap()
        {

        }

        public ThemeSwap(Memorygame _mg)
        {
            this._mg = _mg;
        }

        public override void Use()
        {
            base.Use();
            _mg.SwapTheme();
        }
    }

    class ShuffleCards : Powerup
    {
        Memorygame _mg;

        public ShuffleCards()
        {

        }

        public ShuffleCards(Memorygame _mg)
        {
            this._mg = _mg;
        }

        public override void Use()
        {
            base.Use();
            _mg.RandomOrder();
        }
    }
}
