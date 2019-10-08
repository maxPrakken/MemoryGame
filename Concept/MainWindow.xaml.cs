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
        public MainWindow()
        {
            InitializeComponent(); // call the xaml file

            Button btn = new Button(); // create new button

            btn.Click += Button1_Click; // add button event to btn click

            this.Show(); // show content of window
            this.Content = btn; // add btn to content
        }

        private void Button1_Click(object sender, RoutedEventArgs e) // click event/function [works more like event]
        {
            Memorygame mg = new Memorygame(); // instance of memorygame
            this.Content = mg; // show memorygame page in content
        }
    }
}
