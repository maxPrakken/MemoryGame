using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concept
{
    /*! \brief player class
       */
    class Player
    {
        public string name; /*!< player name */
        public int score = 0; /*!< player score */
        public Powerup powerup;  /*!< player powerup */
        public string creditPU = "";  /*!< credit to powerup, used in loading due to dependency */

        /*! \brief default constructor
       */
        public Player()
        {

        }

        /*! \brief overridden constructor that takes name
       */
        public Player(string name)
        {
            this.name = name;
        }
    }
}
