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
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;

namespace Concept
{
    /*! \brief mainwindow class, first window you see and the menu
       */
    public partial class MainWindow : Window
    {
        List<string> playerList; /*!< list of players */

        /*! \brief eventlistner for if window closes
       */
        protected override void OnClosed(EventArgs e) // shuts down program when main window is closed
        {
            base.OnClosed(e); // starts closing window event

            Application.Current.Shutdown(); // shuts down application
        }

        /*! \brief default constructor
       */
        public MainWindow()
        {
            InitializeComponent(); // call the xaml file

            playerList = new List<string>();

            this.Show(); // show content of window
        }

        /*! \brief eventlistner for start button
       */
        private void Button_start_Click(object sender, RoutedEventArgs e) // click event/function [works more like event]
        {
            p1.Visibility = Visibility.Visible;
            p2.Visibility = Visibility.Visible;
            p3.Visibility = Visibility.Visible;
            p4.Visibility = Visibility.Visible;
            backButton.Visibility = Visibility.Visible;

            startbtn.Visibility = Visibility.Collapsed;
            resumebtn.Visibility = Visibility.Collapsed;
            highscorebtn.Visibility = Visibility.Collapsed;
            quitbtn.Visibility = Visibility.Collapsed;
        }

        /*! \brief eventlistner for resume button
       */
        private void Button_resume_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Memorygame mg = new Memorygame(File.ReadAllText(openFileDialog.FileName));
                this.Content = mg;
            }
        }

        /*! \brief eventlistner for highscore button
       */
        private void Button_highscore_Click(object sender, RoutedEventArgs e)
        {
            //go to highscore menu here
        }

        /*! \brief eventlistner for quit button
       */
        private void Button_quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // shuts down application
        }

        /*! \brief eventlistner for 1 spelers select
       */
        private void Button_1speler_Click(object sender, RoutedEventArgs e)
        {
            TextBoxPlace(1);
        }

        /*! \brief eventlistner for 2 spelers select
       */
        private void Button_2speler_Click(object sender, RoutedEventArgs e)
        {
            TextBoxPlace(2);
        }

        /*! \brief eventlistner for 3 spelers select
       */
        private void Button_3speler_Click(object sender, RoutedEventArgs e)
        {
            TextBoxPlace(3);
        }

        /*! \brief eventlistner for 4 spelers select
       */
        private void Button_4speler_Click(object sender, RoutedEventArgs e)
        {
            TextBoxPlace(4);
        }

        /*! \brief eventlistner for back button
       */
        private void BackButton_Click(object sender, RoutedEventArgs e)
        // Back Button 
        {
            p1.Visibility = Visibility.Collapsed;
            p2.Visibility = Visibility.Collapsed;
            p3.Visibility = Visibility.Collapsed;
            p4.Visibility = Visibility.Collapsed;
            startGamebtn.Visibility = Visibility.Collapsed;
            backButton.Visibility = Visibility.Collapsed;

            startbtn.Visibility = Visibility.Visible;
            resumebtn.Visibility = Visibility.Visible;
            highscorebtn.Visibility = Visibility.Visible;
            quitbtn.Visibility = Visibility.Visible;

            p1name.Visibility = Visibility.Collapsed;
            p2name.Visibility = Visibility.Collapsed;
            p3name.Visibility = Visibility.Collapsed;
            p4name.Visibility = Visibility.Collapsed;
        }


        /*! \brief eventlistner for final game start button
       */
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
            if (playerList.Count < 1 )
            {
                MessageBox.Show("Fill in your name");
            }
            else
            {

                IEnumerable<string> comparel = playerList.Distinct();
                if(playerList.Count == comparel.Count())
                {
                    Memorygame mg = new Memorygame(playerList);
                    this.Content = mg;
                    comparel = null;
                }
                else
                {
                    MessageBox.Show("names can't be the same, please change the duplicate name");
                    comparel = null;
                    playerList.Clear();
                }
            }
        }

        /*! \brief places the textboxes to input your name
       */
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

    }
}
