using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Concept
{
    public partial class Memorygame : Page
    {

        WrapPanel wp = new WrapPanel(); // initialise wrap panel
        
        DockPanel DP = new DockPanel(); // main panel

        StackPanel psList = new StackPanel(); // list of score textboxes
        TextBox cpp = new TextBox(); // current player display

        List<string> playerNames = new List<string>();
        List<Card> pc = new List<Card>(); // selected cards
        List<Player> players = new List<Player>(); // list of players

        private Memorygame _mg;

        Player cp;
        int currentCP = 0;

        public Memorygame() // main function
        {
            InitializeComponent(); // no clue what it does but dont remove it

            wp.Width = 500; // set wrappanel width
            wp.Height = 500; // set wrappanel height

            CreateGrid(4); // create grid with set size, give it a even number or it'll throw an exception

            this.Content = DP; // give the content
        }
        public Memorygame(List<string> players)
        {
            InitializeComponent(); // no clue what it does but dont remove it

            wp.Width = 500; // set wrappanel width
            wp.Height = 500; // set wrappanel height

            playerNames = players;

            _mg = this;
                       
            CreatePlayers();
            
            CreatePlayerScores();

            DP.Children.Add(psList);
            DP.Children.Add(wp);

            CyclePlayers();
            AssignGrid();

            this.Content = DP; // give the content
        }

        public void UpdateScores()
        {
            foreach(TextBox t in psList.Children)
            {
                if(t.Text.Contains("score") && t.Text.Contains(cp.name))
                {
                    string input = cp.name + " score: " + cp.score.ToString();
                    t.Text = input;
                }
            }
        }

        public void CreatePlayerScores()
        {
            psList.Orientation = Orientation.Vertical;

            cpp.IsReadOnly = true;
            cpp.BorderThickness = new Thickness(0);
            cpp.Width = 150;
            cpp.Height = 75;
            psList.Children.Add(cpp);

            for (int i = 0; i < players.Count; i++)
            { 
                TextBox sc = new TextBox();
                sc.IsReadOnly = true;
                sc.BorderThickness = new Thickness(0);
                string input = players[i].name + " score: " + players[i].score.ToString();
                sc.Text = input;
                sc.Width = 150;
                sc.Height = 75;
                psList.Children.Add(sc);
            }
        }

        public void CyclePlayers()
        {
            if(currentCP < players.Count)
            {
                cp = players[currentCP];
                cpp.Text = cp.name;
                currentCP++;
            }
            else
            {
                currentCP = 0;
                cp = players[currentCP];
                cpp.Text = cp.name;
            }
        }

        public void CreatePlayers()
        {
            for (int i = 0; i < playerNames.Count; i++)
            {
                Player p = new Player(playerNames[i]);
                players.Add(p);
            }
        }

        public void AssignGrid()
        {
            if (players.Count == 1)
            {
                CreateGrid(4);
            }
            else if (players.Count == 2)
            {
                CreateGrid(6);
            }
            else if (players.Count == 3)
            {
                CreateGrid(8);
            }
            else if (players.Count == 4)
            {
                CreateGrid(10);
            }
            else
            {
                throw new System.ArgumentException("player count is 0 or more then 4, please be more carefull in the future");
            }
        }

        public void CreateGrid(int size) // give even number
        {
            if (size % 2 != 0) // failsafe for idiots <3
            {
                throw new System.ArgumentException("give a even number for the grid");
            }

            for (int i = 0; i < size * size; i++) // loop the amount of the size * size so you get a even grid
            {
                int type = i / 2; // set type

                Card btn = new Card(type, wp.Width, size, wp, pc, _mg); // initialize class Card, Card derives from Button
                wp.Children.Add(btn); // add the card to the  wrappanel
            }

            RandomOrder(); // give the wrappanel a random order
        } 

        public void RandomOrder() // function that empties and fills the wrappanel for randomisation
        {
            List<Card> cl = new List<Card>(); // list to hold the cards in temporary

            foreach (Card c in wp.Children) // loop through wrappanel children
            {
                cl.Add(c); // add child to temporary list
            }
            ShuffleCards(cl); // call shufflecards function

            wp.Children.Clear(); // empty all children from wrappanel

            foreach (Card ca in cl) // loop through cards in list
            {
                wp.Children.Add(ca); // add children back to wrappanel but now shuffled
            }
        }

        public void AddPoints(int points)
        {
            cp.score += points;
            Console.WriteLine(cp.name + " " + cp.score);
        }

        private static Random rng = new Random(); // initialize RNJezus
        public static void ShuffleCards<T>(IList<T> list) // function that shuffles cards
        {
            int n = list.Count; // size of list
            while (n > 1) // loop through list till at end
            {
                n--; // lower the counter
                int k = rng.Next(n + 1); //get new random order
                T value = list[k]; // put Card in new index
                list[k] = list[n]; // give Card new value
                list[n] = value; // assign new value
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) // key down function, is any key
        {
            if (e.Key == Key.E) // check key
            {
                //e.Handled = true; // set handled to true
                //this.Content = m; // swap content
            }
        }
    }
}
    

