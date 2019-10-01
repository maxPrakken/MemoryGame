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
                int type = -1;
                if(lastEven % 2 != 0)
                {
                    type = lastEven - 1;
                }
                else { type = lastEven; }
                lastEven++;
                Card btn = new Card(lastEven, wp.Width, size);
                wp.Children.Add(btn);
            }
            this.Content = wp;
            this.Show();
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
        }
        public Card(int type, double width, int size)
        {
            this.type = type;
            Content = "test";
            Width = width / size;
            Height = width / size;
        }
    }
}
