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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WrapPanel wp = new WrapPanel();

        public MainWindow()
        {
            InitializeComponent();

            wp.Width = 500;
            wp.Height = 500;

            CreateGrid(4);
        }

        public void CreateGrid(int size) // give even number
        {
            if (size % 2 != 0) // failsafe for idiots <3
            {
                throw new System.ArgumentException("give a even number for the grid");
            }

            int lastEven = 0;
            for (int i = 0; i < size * size; i++)
            {
                int type = i / 2; // set type
                
                Card btn = new Card(type, wp.Width, size);
                wp.Children.Add(btn);
            }

            RandomOrder();

            this.Content = wp;
            this.Show();
        }

        public void RandomOrder()
        {
            List<Card> cl = new List<Card>();

            foreach(Card c in wp.Children)
            {
                cl.Add(c);
            }
            ShuffleCards(cl);

            wp.Children.Clear();

            foreach(Card ca in cl)
            {
                wp.Children.Add(ca);
            }
        }

        private static Random rng = new Random();
        public static void ShuffleCards<T>(IList<T> list)
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

    public partial class Card : Button
    {
        public int type = -1;

        public Card()
        {
            type = 0;
            Content = "test";
            Width = 50;
            Height = 50;
            this.Click += button1_Click; // subscribes the click event to a function
        }
        public Card(int type, double width, int size)
        {
            this.type = type;
            Content = "test";
            Width = width / size;
            Height = width / size;
            this.Click += button1_Click; // subscribes the click event to a function
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Content = type;
        }
    }
}
