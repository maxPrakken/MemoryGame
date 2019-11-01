using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Concept
{
    public partial class Card : Button // card class, card is a button
    {
        public int type = -1; // default type, -1 so you know when somethings wrong
        private List<Card> pc = new List<Card>();
        WrapPanel wp = new WrapPanel();
        Memorygame _mg;

        public ImageBrush backgroundimg = new ImageBrush();
        public ImageBrush defaultBG = new ImageBrush();

        public Card() // default constructor
        {
            type = 0; // set default type
            Content = "test"; // set default content
            Width = 50; // set default width
            Height = 50; // set default height
            this.Click += Button1_Click; // subscribes the click event to a function
        }
        public Card(int type, double width, int size, WrapPanel wp, List<Card> pc, Memorygame _mg) // override constructor [USE THIS ONE]
        {
            defaultBG.ImageSource = new BitmapImage(new Uri("BG.png", UriKind.Relative));
            this.type = type; // assign type given by main
            Width = width / size; // dynamic width according to amount of Cards given by size
            Height = width / size; // dynamic height according to amount of Cards given by size
            this.Click += Button1_Click; // subscribes the click event to a function
            this.pc = pc;
            this.wp = wp;
            this._mg = _mg;

            this.Background = defaultBG;
        }

        private void Button1_Click(object sender, RoutedEventArgs e) // click event/function [works more like event]
        {
            this.Background = backgroundimg;

            if (this.IsEnabled)
            {
                FlipManager();
                _mg.UpdateScores();
                _mg.UpdateAllScores();
            }
        }

        private void FlipManager()
        {
            if (pc.Count < 2)
            {
                pc.Add(this);
            }
            if (pc.Count == 2)
            {
                wp.IsHitTestVisible = false;

                if (pc[0].type == pc[1].type && pc[0] != pc[1])
                {
                    Task.Delay(2000).ContinueWith(_ =>
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            _mg.AddPoints(100);
                            pc[0].IsEnabled = false;
                            pc[1].IsEnabled = false;
                            pc.Clear();

                            wp.IsHitTestVisible = true;

                            _mg.curtime = 30;
                        });
                    }
                    );
                }
                else if (pc[0] == pc[1])
                {
                    pc[0].Background = defaultBG;
                    pc[1].Background = defaultBG;
                    pc.Clear();
                    wp.IsHitTestVisible = true;
                    return;
                }
                else
                {
                    Task.Delay(2000).ContinueWith(_ =>
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            pc[0].Background = defaultBG;
                            pc[1].Background = defaultBG;
                            pc.Clear();

                            wp.IsHitTestVisible = true;

                            _mg.CyclePlayers();
                        });
                    }
                    );
                }
            }
        }
    }
}
