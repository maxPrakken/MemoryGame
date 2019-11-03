using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Concept
{
    /*! \brief MemoryGame class, the main class of the game
       */
    public partial class Memorygame : Page
    {
        DispatcherTimer dt = new DispatcherTimer(); /*!< timer responsible for time limit */

        WrapPanel wp = new WrapPanel(); /*!<  initialise wrap panel */

        Canvas DP = new Canvas(); /*!<  main panel */

        StackPanel psList = new StackPanel(); /*!<  list of score textboxes */
        TextBox cpp = new TextBox(); /*!<  current player display */

        StackPanel pmenu = new StackPanel(); /*!<  pause menu panel */
        bool paused = false; /*!< to check if game should be frozen */

        int turntime = 0; /*!< lowest point of timer */
        public int curtime = 30; /*!< reset point of timer */
        TextBox turnCountdown = new TextBox(); /*!< displays timer */

        List<string> playerNames = new List<string>(); /*!< player names */
        List<Card> pc = new List<Card>(); // selected cards
        List<Player> players = new List<Player>(); // list of players

        Button powerbutton = new Button(); /*!< activated current players powerup */

        private Memorygame _mg; /*!< local instance of MemoryGame class */

        private int powerupDropChance = 20; /*!< procentage change to get a powerup after score a point */

        Player cp; /*!< current player */
        int currentCP = 0;

        public Memorygame() /*!<  main contructor */
        {
            InitializeComponent(); // no clue what it does but dont remove it

            wp.Width = 500; // set wrappanel width
            wp.Height = 500; // set wrappanel height

            CreateGrid(4); // create grid with set size, give it a even number or it'll throw an exception

            this.Content = DP; // give the content\
        }

        /*! \brief overridden constructor for getting player names
        */
        public Memorygame(List<string> players)
        {
            InitializeComponent(); // no clue what it does but dont remove it

            wp.Width = 500; // set wrappanel width
            wp.Height = 500; // set wrappanel height

            turnCountdown.Width = 300;
            turnCountdown.Height = 200;

            powerbutton.Width = 300;
            powerbutton.Height = 200;
            powerbutton.Click += PowerButton_Click;

            playerNames = players;

            _mg = this;

            dt.Tick += new EventHandler(timer_Tick);
            dt.Interval = new TimeSpan(0, 0, 1); // execute every second
            dt.Start();

            CreatePlayers();

            CreatePlayerScores();

            psList.Children.Add(turnCountdown);

            DP.Children.Add(psList);
            DP.Children.Add(wp);
            DP.Children.Add(powerbutton);
            DP.Children.Add(pmenu);

            Canvas.SetLeft(wp, 500);
            Canvas.SetLeft(powerbutton, 1200);

            CyclePlayers();
            AssignGrid();
            BuildPauseMenu();

            SetTheme();
            this.Content = DP; // give the content
        }

        /*! \brief overridden constructor
        */
        public Memorygame(string s)
        {
            InitializeComponent(); // no clue what it does but dont remove it

            wp.Width = 500; // set wrappanel width
            wp.Height = 500; // set wrappanel height

            _mg = this;

            LoadGame(s); // loads the game from previous saved file
            CreatePlayerScores();

            turnCountdown.Width = 300;
            turnCountdown.Height = 200;

            powerbutton.Width = 300;
            powerbutton.Height = 200;
            powerbutton.Click += PowerButton_Click;

            dt.Tick += new EventHandler(timer_Tick);
            dt.Interval = new TimeSpan(0, 0, 1); // execute every second
            dt.Start();

            psList.Children.Add(turnCountdown);

            DP.Children.Add(psList);
            DP.Children.Add(wp);
            DP.Children.Add(powerbutton);
            DP.Children.Add(pmenu);

            Canvas.SetLeft(wp, 500);
            Canvas.SetLeft(powerbutton, 1200);

            BuildPauseMenu();

            this.Content = DP; // give the content
        }

        /*! \brief  updates the score of the current player in the corresponding textbox
        */
        public void UpdateScores()
        {
            foreach (TextBox t in psList.Children)
            {
                if (t.Text.Contains("score") && t.Text.Contains(cp.name))
                {
                    string input = cp.name + " score: " + cp.score.ToString();
                    t.Text = input;
                }
            }
        }

        /*! \brief updates the scores of all the players
        */
        public void UpdateAllScores()
        {
            foreach (TextBox t in psList.Children)
            {
                foreach (Player p in players)
                {
                    if (t.Text.Contains("score") && t.Text.Contains(p.name))
                    {
                        string input = p.name + " score: " + p.score.ToString();
                        t.Text = input;
                    }
                }
            }
        }

        /*! \brief creates the textboxes for the player scores
        */
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

        /*! \brief cycles to the next player for their turn
        */
        public void CyclePlayers()
        {
            try
            {
                currentCP++;
                cp = players[currentCP];
            }
            catch
            {
                currentCP = 0;
                cp = players[currentCP];
            }
            cpp.Text = cp.name;

            if (cp.powerup != null)
            {
                powerbutton.Content = cp.powerup.name;
            }else
            {
                powerbutton.Content = "No powerup available right now";
            }

            curtime = 30;
        }

        /*! \brief creates the players gotten from the playernames at the start of the game
        */
        public void CreatePlayers()
        {
            for (int i = 0; i < playerNames.Count; i++)
            {
                Player p = new Player(playerNames[i]);
                players.Add(p);
            }
        }

        /*! \brief decides how big the grid is supposed to be according to the amount of players
        */
        public void AssignGrid()
        {
            switch (players.Count)
            {
                case 1:
                    CreateGrid(4);
                    break;
                case 2:
                    CreateGrid(6);
                    break;
                case 3:
                    CreateGrid(8);
                    break;
                case 4:
                    CreateGrid(10);
                    break;
                default:
                    throw new System.ArgumentException("player count is 0 or more then 4, please be more carefull in the future");
            }
        }

        /*! \brief creates the grid
        */
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

        /*! \brief sets the theme for the cards
        */
        public void SetTheme()
        {
            foreach (Card c in wp.Children)
            {
                string s = "1_" + c.type.ToString() + ".png";

                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(s, UriKind.Relative));
                c.backgroundimg = brush;
            }
        }

        /*! \brief swaps the theme for the cards to the other theme
        */
        public void SwapTheme()
        {

            foreach (Card c in wp.Children)
            {
                ImageBrush IB = c.backgroundimg as ImageBrush;
                string source = IB.ImageSource.ToString();
                string s;
                if (source.Contains("1_"))
                {
                    s = "2_" + c.type.ToString() + ".png";
                }

                else
                {
                    s = "1_" + c.type.ToString() + ".png";                   
                }

                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(s, UriKind.Relative));
                c.backgroundimg = brush;
            }
        }

        /*! \brief puts the cards in a random order
        */
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

        /*! \brief gives you a chance to get a powerup everytime you get a pair
        */
        public void PowerupLottery()
        {
            Random random = new Random();
            int randomNum = random.Next(100);
            if(randomNum <= powerupDropChance)
            {
                int randomP = random.Next(1, 4);

                switch(randomP)
                {
                    case 1:
                        Powerup p = new ShuffleCards(this);
                        cp.powerup = p;
                        break;
                    case 2:
                        Powerup p1 = new ScoreSwap(players);
                        cp.powerup = p1;
                        break;
                    case 3:
                        Powerup p2 = new ThemeSwap(this);
                        cp.powerup = p2;
                        break;
                    default:
                        throw new System.ArgumentException("please fix");
                        break;
                }
            }
        }

        /*! \brief  add points to current player and run powerlottery
        */
        public void AddPoints(int points)
        {
            cp.score += points;
            PowerupLottery();
            Console.WriteLine(cp.name + " " + cp.score);
        }

        /*! \brief build the pause menu
        */
        private void BuildPauseMenu()
        {
            pmenu.Orientation = Orientation.Vertical;
            Canvas.SetZIndex(pmenu, 1);
            Canvas.SetLeft(pmenu, 500);
            pmenu.Visibility = Visibility.Collapsed;

            Button resumeb = new Button();
            Button SafeGameb = new Button();
            Button Quitb = new Button();

            resumeb.Width = 300;
            resumeb.Height = 200;
            SafeGameb.Width = 300;
            SafeGameb.Height = 200;
            Quitb.Width = 300;
            Quitb.Height = 200;

            resumeb.Content = "Resume Game";
            SafeGameb.Content = "Safe Game";
            Quitb.Content = "Quit Game";

            resumeb.Click += Resumeb_Click;
            SafeGameb.Click += SafeGameb_Click;
            Quitb.Click += Quitb_Click;

            pmenu.Children.Add(resumeb);
            pmenu.Children.Add(SafeGameb);
            pmenu.Children.Add(Quitb);
        }

        /*! \brief load the game from a save file
        */
        public void LoadGame(string s)
        {
            string playerstr = ""; // empty players string
            string cardstr = ""; // empty cards string
            string turn = ""; // empty turn string

            string[] stringmaster = s.Split(' '); // string array stringmaster fills 3 indexes with values from s 

            playerstr = stringmaster[0]; // playerstring equals stringmaster index 0
            cardstr = stringmaster[1]; // cardstring equals stringmaster index 1
            turn = stringmaster[2]; // turn equals stringmaster index 2

            //players loading
            string[] playersArray = playerstr.Split('|'); // split all players into array

            for(int i = 0; i < playersArray.Length - 1; i++) // loop through all players
            {
                string[] playerDetails = playersArray[i].Split(','); // split player up in its details
                Player p = new Player(); // make new player
                p.name = playerDetails[0]; // set name equal to playerdetails name
                p.score = Convert.ToInt32(playerDetails[1]); // set score equa to playerdetails score
                p.creditPU = playerDetails[2]; // give player credit for a powerup, needs to be added later due to dependency

                players.Add(p); // add player to the players list
            }

            //cards loading
            string[] cardsArray = cardstr.Split('|'); // splits all cards into array

            for(int i = 0; i < cardsArray.Length - 1; i++) // loops through cards
            { 
                string[] cardsDetails = cardsArray[i].Split(','); // gets details from cards string into other string array 
                int size = Convert.ToInt32(Math.Sqrt(cardsArray.Length)); // gets the appropriate size of the cards
                int type = Convert.ToInt32(cardsDetails[0]); // gets the type from the details string
                Card c = new Card(type, wp.Width, size, wp, pc, _mg); // makes new card with extracted arguments

                var brush = new ImageBrush(); // makes new image brush to set background
                brush.ImageSource = new BitmapImage(new Uri(cardsDetails[1], UriKind.Relative)); // give imagebrush a source
                c.backgroundimg = brush; // set card background to imagebrush

                wp.Children.Add(c); // add child to wrappanel
            }

            foreach(Player p in players) // loop through players
            {
                if(p.name == turn) // if player name equals turn
                {
                    cp = p; // set current player to player
                }

                switch(p.creditPU) // check value of creditPU
                {
                    case "Score Swap": // if value equals Score Swap
                        p.powerup = new ScoreSwap(players); // set player powerup to a new scoreswap
                        break;
                    case "Theme Swap": // if value equals Theme Swap
                        p.powerup = new ThemeSwap(this); // set player powerup to a new ThemeSwap
                        break;
                    case "Shuffle Cards": // if value equals Shuffle Cards
                        p.powerup = new ShuffleCards(this); // set player powerup to a new ShuffleCards
                        break;
                }
            }
        }

        /*! \brief save's the game to a .sav file
        */
        public void SaveGame() // touch this code and i will strangle you in your sleep :)
        {
            string OneStringToSaveThemAll = ""; // the motherload string that holds all the other strings

            foreach(Player p in players)
            {
                string ps = "";

                if (p.powerup != null)
                {
                    ps += p.name.ToString() + "," + p.score.ToString() + "," + p.powerup.name.ToString() + "|";
                }
                else
                {
                    ps += p.name.ToString() + "," + p.score.ToString() + "," + "np" + "|";
                }
                OneStringToSaveThemAll += ps;
            }

            OneStringToSaveThemAll += " "; // add space inbetween players and cards to sperate them when loading

            foreach(Card c in wp.Children)
            {
                ImageBrush IB = c.backgroundimg as ImageBrush;
                string source = IB.ImageSource.ToString();

                string cards = c.type.ToString() + "," + source + "|";
                OneStringToSaveThemAll += cards;
            }

            OneStringToSaveThemAll += " "; // add space inbetween cards and turn to sperate them when loading

            OneStringToSaveThemAll += cp.name.ToString();

            string rootPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            rootPath = System.IO.Path.GetDirectoryName(rootPath);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = rootPath + "../../../"; 
            saveFileDialog.Filter = "Save File (*.sav)|*.sav";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, OneStringToSaveThemAll);
            }
        }

        /*! \brief checks if the game is over and shows a textbox with the scores and a button to go back and save at the end
        */
        public void CheckGameOver()
        {
            bool finished = true;

            foreach(Card c in wp.Children)
            {
                if(c.IsEnabled) // if even one card is enabled finished will be false. when all cards are not enabled finished will be true
                {
                    finished = false;
                }
            }

            if(!finished)
            {
                return;
            }
            else if(finished)
            {
                TextBox scoreShow = new TextBox(); // new textbox that shows player scores
                scoreShow.Width = 600; // width of the textbox
                scoreShow.Height = 450; // height of the textbox
                DP.Children.Add(scoreShow); // add textbox to canvas
                Canvas.SetZIndex(scoreShow, 1); // set z index so its on the foreground
                Canvas.SetLeft(scoreShow, 400); // set it vaguely in the middle

                foreach(Player p in players) // loop through players
                {
                    scoreShow.Text += p.name + "'s Score: " + p.score + "\n"; // add player name plus score to the textbox
                }

                Button b = new Button();
                b.Width = 300;
                b.Height = 200;
                b.Content = "Save and Quit game";
                DP.Children.Add(b);
                Canvas.SetZIndex(b, 2); // set z index so its on the foreground
                Canvas.SetLeft(b, 400); // set it vaguely in the middle
                Canvas.SetTop(b, 300);
                b.Click += EndGame_Click;
            }
        }

        /*! \brief sends the highscores to be put into the highscores save file to the highscore class
        */
        private void SendHighScores()
        {
            List<string[,]> collective = new List<string[,]>(); // collection of 2 dimentional arrays of player names and scores

            foreach (Player p in players) // loop through players
            {
                string[,] set = new string[,] { {p.name, p.score.ToString()} }; // make 2 dimentional array with name and scores
                collective.Add(set); // add set to collective
            }

            Highscores hs = new Highscores(); // make new instance of highscores
            hs.InjectScores(collective); // inject collective into highscores so highscores can check it against excisting highscores
        }

        private static Random rng = new Random(); // initialize RNJezus
        /*! \brief powerup that is easier to have here that shuffles the cards
        */
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

        /*! \brief eventlistner for the resume button
        */
        private void Resumeb_Click(object sender, RoutedEventArgs e) // click event/function [works more like event]
        {
            pmenu.Visibility = Visibility.Collapsed;
            paused = false;
        }

        /*! \brief eventlistner for end of game button
        */
        private void EndGame_Click(object sender, RoutedEventArgs e)
        {
            SendHighScores();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location); // restarts the application bringing you back to the home screen
            Application.Current.Shutdown(); // shut the old running application down
        }

        /*! \brief eventlistner for the save game button(spelled wrong)
        */
        private void SafeGameb_Click(object sender, RoutedEventArgs e) // click event/function [works more like event]
        {
            SaveGame();
        }

        /*! \brief eventlistner for the quit button
        */
        private void Quitb_Click(object sender, RoutedEventArgs e) // click event/function [works more like event]
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location); // restarts the application bringing you back to the home screen
            Application.Current.Shutdown(); // shut the old running application down
        }

        /*! \brief eventlistner for the powerup button
        */
        private void PowerButton_Click(object sender, RoutedEventArgs e) // click event/function [works more like event]
        {
            if (cp.powerup != null)
            {
                cp.powerup.Use();
            }
        }

        /*! \brief eventlistner for when you press escape
        */
        private void Window_KeyDown(object sender, KeyEventArgs e) // key down function, is any key
        {
            if (e.Key == Key.Escape) // check key
            {
                e.Handled = true; // set handled to true
                if(pmenu.Visibility == Visibility.Visible)
                {
                    pmenu.Visibility = Visibility.Collapsed;
                    paused = false;
                }
                else
                {
                    pmenu.Visibility = Visibility.Visible;
                    paused = true;
                }
            }
        }

        /*! \brief eventlistner for the dispatchtimer
        */
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!paused)
            {
                curtime--;
                turnCountdown.Text = curtime.ToString();
                if (curtime <= turntime)
                {
                    CyclePlayers();
                }
            }
        }
    }
}
    

