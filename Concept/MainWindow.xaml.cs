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
        List<string> playerList;
        

        protected override void OnClosed(EventArgs e) // shuts down program when main window is closed
        {
            base.OnClosed(e); // starts closing window event

            Application.Current.Shutdown(); // shuts down application
        }

        public MainWindow()
        {
            InitializeComponent(); // call the xaml file

            playerList = new List<string>();

            this.Show(); // show content of window
        }

        private void Button_start_Click(object sender, RoutedEventArgs e) // click event/function [works more like event]
        {
            p1.Visibility = Visibility.Visible;
            p2.Visibility = Visibility.Visible;
            p3.Visibility = Visibility.Visible;
            p4.Visibility = Visibility.Visible;

            startbtn.Visibility = Visibility.Collapsed;
            resumebtn.Visibility = Visibility.Collapsed;
            highscorebtn.Visibility = Visibility.Collapsed;
            quitbtn.Visibility = Visibility.Collapsed;
        }

        private void Button_resume_Click(object sender, RoutedEventArgs e)
        {
            //do something here to resume the save(d) game
        }

        private void Button_highscore_Click(object sender, RoutedEventArgs e)
        {
            //go to highscore menu here
        }

        private void Button_quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // shuts down application
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

        private void ButtonGameStart_Click(object sender, RoutedEventArgs e)
        {
            if(p1name.Text != "" && p1name.Text != "name. . ." && p1name.Text != null)
            {
                playerList.Add(p1name.Text);
            }
            if (p2name.Text != "" && p2name.Text != "name. . ." && p2name.Text != null)
            {
                playerList.Add(p2name.Text);
            }
            if (p3name.Text != "" && p3name.Text != "name. . ." && p3name.Text != null)
            {
                playerList.Add(p3name.Text);
            }
            if (p4name.Text != "" && p4name.Text != "name. . ." && p4name.Text != null)
            {
                playerList.Add(p4name.Text);
            }
            if (playerList.Count <1 )
            {
                string text = "no players found";
                MessageBox.Show(text);
            }
            
                        
            //start the game here and send the list to the game
        }
        
        
        
        
        public void TextBoxPlace(int count)
        {
            switch(count) {
                case 1:
                    {
                        p1name.Visibility = Visibility.Collapsed;
                        p2name.Visibility = Visibility.Collapsed;
                        p3name.Visibility = Visibility.Collapsed;
                        p4name.Visibility = Visibility.Collapsed;

                        p1name.Visibility = Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        p1name.Visibility = Visibility.Collapsed;
                        p2name.Visibility = Visibility.Collapsed;
                        p3name.Visibility = Visibility.Collapsed;
                        p4name.Visibility = Visibility.Collapsed;

                        p1name.Visibility = Visibility.Visible;
                        p2name.Visibility = Visibility.Visible;
                        break;
                    }
                case 3:
                    {
                        p1name.Visibility = Visibility.Collapsed;
                        p2name.Visibility = Visibility.Collapsed;
                        p3name.Visibility = Visibility.Collapsed;
                        p4name.Visibility = Visibility.Collapsed;

                        p1name.Visibility = Visibility.Visible;
                        p2name.Visibility = Visibility.Visible;
                        p3name.Visibility = Visibility.Visible;
                        break;
                    }
                case 4:
                    {
                        p1name.Visibility = Visibility.Collapsed;
                        p2name.Visibility = Visibility.Collapsed;
                        p3name.Visibility = Visibility.Collapsed;
                        p4name.Visibility = Visibility.Collapsed;

                        p1name.Visibility = Visibility.Visible;
                        p2name.Visibility = Visibility.Visible;
                        p3name.Visibility = Visibility.Visible;
                        p4name.Visibility = Visibility.Visible;
                        break;
                    }
            }
            startGamebtn.Visibility = Visibility.Visible;
        }

        private void p3name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
