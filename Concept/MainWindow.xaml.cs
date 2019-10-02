using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Concept
{

    public partial class MainWindow : Window
    {
        WrapPanel wp = new WrapPanel(); // initialise wrap panel

        public MainWindow() // main function
        {
            InitializeComponent(); // no clue what it does but dont remove it

            wp.Width = 500; // set wrappanel width
            wp.Height = 500; // set wrappanel height

            CreateGrid(4); // create grid with set size, give it a even number or it'll throw an exception
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
                
                Card btn = new Card(type, wp.Width, size); // initialize class Card, Card derives from Button
                wp.Children.Add(btn); // add the card to the  wrappanel
            }

            RandomOrder(); // give the wrappanel a random order

            this.Content = wp; // give wrappanel to the content
            this.Show(); // show the content
        }

        public void RandomOrder() // function that empties and fills the wrappanel for randomisation
        {
            List<Card> cl = new List<Card>(); // list to hold the cards in temporary

            foreach(Card c in wp.Children) // loop through wrappanel children
            {
                cl.Add(c); // add child to temporary list
            }
            ShuffleCards(cl); // call shufflecards function

            wp.Children.Clear(); // empty all children from wrappanel

            foreach(Card ca in cl) // loop through cards in list
            {
                wp.Children.Add(ca); // add children back to wrappanel but now shuffled
            }
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
    }

    public partial class Card : Button // card class, card is a button
    {
        public int type = -1; // default type, -1 so you know when somethings wrong

        public Card() // default constructor
        {
            type = 0; // set default type
            Content = "test"; // set default content
            Width = 50; // set default width
            Height = 50; // set default height
            this.Click += button1_Click; // subscribes the click event to a function
        }
        public Card(int type, double width, int size) // override constructor [USE THIS ONE]
        {
            this.type = type; // assign type given by main
            Content = "test"; // temporary content
            Width = width / size; // dynamic width according to amount of Cards given by size
            Height = width / size; // dynamic height according to amount of Cards given by size
            this.Click += button1_Click; // subscribes the click event to a function
        }

        private void button1_Click(object sender, RoutedEventArgs e) // click event/function [works more like event]
        {
            this.Content = type; // show type as content
        }
    }
}
