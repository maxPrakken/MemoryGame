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
    /// Interaction logic for Selectplayers.xaml
    /// </summary>
    public partial class Selectplayers : Page
    {
        Canvas cv1 = new Canvas();

        Canvas sp = new Canvas();

        public Selectplayers()
        {
            cv1.Width = 200;
            cv1.Height = 300;
            sp.Children.Add(cv1);
            Canvas.SetTop(cv1, 175);
            Canvas.SetLeft(cv1, 300);

            InitializeComponent(); // call the xaml file

            
            sp.Height = this.Height - 50;
            sp.Width = this.Width - 50;
            //sp.Orientation = Orientation.Vertical; // set orientation of wrappanel to vertical

            Button btn_1speler = new Button(); // create new start button
            btn_1speler.Content = "1 speler"; // set content of btn
            btn_1speler.Width = 200; // set button width
            btn_1speler.Height = 100; // set button height
            btn_1speler.Click += Button_1speler_Click; // add button event to btn click
            Canvas.SetTop(btn_1speler, 0);
            sp.Children.Add(btn_1speler); // add btn to wrappanel as child

            Button btn_2speler = new Button(); // create new resume button
            btn_2speler.Content = "2 spelers"; // set content of btn
            btn_2speler.Width = 200; // set button width
            btn_2speler.Height = 100; // set button height
            btn_2speler.Click += Button_2speler_Click; // add button event to btn click
            Canvas.SetTop(btn_2speler, 100);
            sp.Children.Add(btn_2speler); // add btn to wrappanel as child

            Button btn_3speler = new Button(); // create new highscore button
            btn_3speler.Content = "3 spelers"; // set content of btn
            btn_3speler.Width = 200; // set button width
            btn_3speler.Height = 100; // set button height
            btn_3speler.Click += Button_3speler_Click; // add button event to btn click
            Canvas.SetTop(btn_3speler, 200);
            sp.Children.Add(btn_3speler); // add btn to wrappanel as child

            Button btn_4speler = new Button(); // create new quit button
            btn_4speler.Content = "4 spelers"; // set content of btn
            btn_4speler.Width = 200; // set button width
            btn_4speler.Height = 100; // set button height
            btn_4speler.Click += Button_4speler_Click; // add button event to btn click
            Canvas.SetTop(btn_4speler, 300);
            sp.Children.Add(btn_4speler); // add btn to wrappanel as child



            
            this.Content = sp; // add btn to content
        }
        private void Button_1speler_Click(object sender, RoutedEventArgs e)
        {
            TextBoxPlace(1);
        }
        private void Button_2speler_Click(object sender, RoutedEventArgs e)
        {
            TextBoxPlace(2);
        }
        private void Button_3speler_Click(object sender, RoutedEventArgs e)
        {
            TextBoxPlace(3);
        }
        private void Button_4speler_Click(object sender, RoutedEventArgs e)
        {
            TextBoxPlace(4);
        }

        public void TextBoxPlace(int count)
        {
            cv1.Children.Clear();
            for (int i = 0; i < count; i++)
            {
                TextBox tb1 = new TextBox();
                tb1.Width = 100;
                tb1.Height = 20;
                cv1.Children.Add(tb1);
                Canvas.SetTop(tb1, 30 * i);
            }
        }
    }
}
