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
        protected override void OnClosed(EventArgs e) // shuts down program when main window is closed
        {
            base.OnClosed(e); // starts closing window event

            Application.Current.Shutdown(); // shuts down application
        }

        public MainWindow()
        {
            InitializeComponent(); // call the xaml file

            WrapPanel wp = new WrapPanel(); // create wrappanel
            wp.Height = this.Height - 50;
            wp.Width = this.Width - 50;
            wp.Orientation = Orientation.Vertical; // set orientation of wrappanel to vertical

            Button btn_start = new Button(); // create new start button
            btn_start.Content = "START"; // set content of btn
            btn_start.Width = 200; // set button width
            btn_start.Height = 100; // set button height
            btn_start.Click += Button_start_Click; // add button event to btn click
            wp.Children.Add(btn_start); // add btn to wrappanel as child

            Button btn_resume = new Button(); // create new resume button
            btn_resume.Content = "RESUME"; // set content of btn
            btn_resume.Width = 200; // set button width
            btn_resume.Height = 100; // set button height
            btn_resume.Click += Button_resume_Click; // add button event to btn click
            wp.Children.Add(btn_resume); // add btn to wrappanel as child

            Button btn_highscore = new Button(); // create new highscore button
            btn_highscore.Content = "HIGHSCORES"; // set content of btn
            btn_highscore.Width = 200; // set button width
            btn_highscore.Height = 100; // set button height
            btn_highscore.Click += Button_highscore_Click; // add button event to btn click
            wp.Children.Add(btn_highscore); // add btn to wrappanel as child

            Button btn_quit = new Button(); // create new quit button
            btn_quit.Content = "QUIT"; // set content of btn
            btn_quit.Width = 200; // set button width
            btn_quit.Height = 100; // set button height
            btn_quit.Click += Button_quit_Click; // add button event to btn click
            wp.Children.Add(btn_quit); // add btn to wrappanel as child



            this.Show(); // show content of window
            this.Content = wp; // add btn to content
        }

        private void Button_start_Click(object sender, RoutedEventArgs e) // click event/function [works more like event]
        {
            Memorygame mg = new Memorygame(); // instance of memorygame
            this.Content = mg; // show memorygame page in content
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
    }
}
