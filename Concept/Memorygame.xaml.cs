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
        
        StackPanel SP = new StackPanel(); // main panel


        List<string> playerNames = new List<string>();
        List<Card> pc = new List<Card>();
        List<Player> players = new List<Player>();

        private Memorygame _mg;

        Player cp;
        int currentCP = 0;

        public Memorygame() // main function
        {
            InitializeComponent(); // no clue what it does but dont remove it

            wp.Width = 500; // set wrappanel width
            wp.Height = 500; // set wrappanel height

            CreateGrid(4); // create grid with set size, give it a even number or it'll throw an exception

            this.Content = SP; // give the content
        }
        public Memorygame(List<string> players)
        {
            InitializeComponent(); // no clue what it does but dont remove it

            SP.Orientation = Orientation.Horizontal;
            CreatePlayerScores();
            SP.Children.Add(wp);

            wp.Width = 500; // set wrappanel width
            wp.Height = 500; // set wrappanel height

            playerNames = players;

            _mg = this;
                       
            CreatePlayers();
            CyclePlayers();
            AssignGrid();

            this.Content = SP; // give the content
        }

        public void CreatePlayerScores()
        {
            StackPanel tbList = new StackPanel();
            tbList.Orientation = Orientation.Vertical;

            TextBox cpp = new TextBox();
            cpp.IsReadOnly = true;
            cpp.BorderThickness = new Thickness(0);
            cpp.Width = 150;
            cpp.Height = 75;
            SP.Children.Add(cpp);

            for(int i = 0; i < players.Count; i++)
            {
                TextBox p = new TextBox();
                p.IsReadOnly = true;
                p.BorderThickness = new Thickness(0);
                p.Text = players[i].name;
                p.Width = 150;
                p.Height = 75;
                SP.Children.Add(p);

                TextBox sc = new TextBox();
                sc.IsReadOnly = true;
                sc.BorderThickness = new Thickness(0);
                sc.Text = players[i].score.ToString();
                sc.Width = 150;
                sc.Height = 75;
                SP.Children.Add(sc);
            }

            //SP.Children.Add(tbList);
        }

        public void CyclePlayers()
        {
            if(currentCP < players.Count)
            {
                cp = players[currentCP];
                currentCP++;
            }
            else
            {
                currentCP = 0;
                cp = players[currentCP];
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

                Card btn = new Card(type, wp.Width, size, wp, _mg); // initialize class Card, Card derives from Button
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

    public partial class Card : Button // card class, card is a button
    {
        public int type = -1; // default type, -1 so you know when somethings wrong
        private List<Card> pc = new List<Card>();
        WrapPanel wp = new WrapPanel();
        Memorygame _mg;

        public Card() // default constructor
        {
            type = 0; // set default type
            Content = "test"; // set default content
            Width = 50; // set default width
            Height = 50; // set default height
            this.Click += Button1_Click; // subscribes the click event to a function
        }
        public Card(int type, double width, int size, WrapPanel wp, Memorygame _mg) // override constructor [USE THIS ONE]
        {

            this.type = type; // assign type given by main
            Content = "test"; // temporary content
            Width = width / size; // dynamic width according to amount of Cards given by size
            Height = width / size; // dynamic height according to amount of Cards given by size
            this.Click += Button1_Click; // subscribes the click event to a function
            this.pc = pc;
            this.wp = wp;
            this._mg = _mg;
        }

        private void Button1_Click(object sender, RoutedEventArgs e) // click event/function [works more like event]
        {
            this.Content = type; // show type as

            if (this.IsEnabled)
            {
                FlipManager();
            }
        }

        private void FlipManager()
        {
            if (pc.Count < 2)
            {
                pc.Add(this);
            }
            if (pc.Count == 2)
            {
                wp.IsHitTestVisible = false;

                if (pc[0].type == pc[1].type && pc[0] != pc[1])
                {
                    _mg.AddPoints(100);
                    pc[0].IsEnabled = false;
                    pc[1].IsEnabled = false;
                    pc.Clear();

                    wp.IsHitTestVisible = true;
                }
                else if(pc[0] == pc[1]) 
                {
                    pc.Clear();
                }
                else
                {
                    Task.Delay(2000).ContinueWith(_ =>
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            pc[0].Content = "test";
                            pc[1].Content = "test";
                            pc.Clear();

                            wp.IsHitTestVisible = true;

                            _mg.CyclePlayers();
                        });
                    }
                    );
                }
            }
        }
    }

    public class Player
    {
        public string name;
        public int score = 0;
        //public Powerup pu;

        public Player()
        {

        }
        public Player(string name)
        {
            this.name = name;
        }
    }

}
    

