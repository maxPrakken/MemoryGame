using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace Concept
{
    /*! \brief powerup base class
        */
    class Powerup
    {
        public bool isused; /*!< boolean that turns false when powerup is used */
        public string name; /*!< name of the powerup, used to identify it later on */

        /*! \brief powerup base constructor
       */
        public Powerup()
        {
            isused = false;
        }

        /*! \brief base class for the Use method
       */
        public virtual void Use()
        {
            isused = false;
        }
    }

    /*! \brief scoreswap derived class of powerup
       */
    class ScoreSwap : Powerup
    {
        List<Player> pl = new List<Player>();
        private Random rng = new Random();

        /*! \brief base constructor
       */
        public ScoreSwap()
        {

        }


        /*! \brief override constructor that takes player list
       */
        public ScoreSwap(List<Player> pl)
        {
            this.pl = pl;
            name = "Score Swap";
        }


        /*! \brief overridden Use method that excecutes shufflescore method
       */
        public override void Use()
        {
            base.Use();

            ShuffleScore(this.pl);
        }


        /*! \brief shuffles the cards 
       */
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

    /*! \brief themeswap derived class of powerup
      */
    class ThemeSwap : Powerup
    {
        Memorygame _mg; /*!< instance of the MemoryGame */


        /*! \brief base constructor
       */
        public ThemeSwap()
        {

        }

        /*! \brief override constructor that takes MemoryGame instance
      */
        public ThemeSwap(Memorygame _mg)
        {
            this._mg = _mg;
            name = "Theme Swap";
        }

        /*! \brief override Use method that uses swaptheme method from MemoryGame instance
      */
        public override void Use()
        {
            base.Use();
            _mg.SwapTheme();
        }
    }

    /*! \brief shufflecards derived class of powerup
      */
    class ShuffleCards : Powerup
    {
        Memorygame _mg;/*!< instance of the MemoryGame */

        /*! \brief base constructor
      */
        public ShuffleCards()
        {

        }

        /*! \brief override constructor that takes MemoryGame instance
     */
        public ShuffleCards(Memorygame _mg)
        {
            this._mg = _mg;
            name = "Shuffle Cards";
        }

        /*! \brief override Use method that uses Randomorder method from MemoryGame instance
      */
        public override void Use()
        {
            base.Use();
            _mg.RandomOrder();
        }
    }
}
