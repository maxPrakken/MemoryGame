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
            InitializeComponent();

            Button btn = new Button();

            btn.Click += Button1_Click;

            this.Show();
            this.Content = btn;
        }

        private void Button1_Click(object sender, RoutedEventArgs e) // click event/function [works more like event]
        {
            Memorygame mg = new Memorygame();
            this.Content = mg; // show type as 
        }
    }
}
