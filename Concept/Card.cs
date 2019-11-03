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
    /*! \brief Card class derived from button
       */
    public partial class Card : Button 
    {
        public int type = -1; /*!< default type, -1 so you know when somethings wrong */
        private List<Card> pc = new List<Card>(); /*!< private list of all cards */
        WrapPanel wp = new WrapPanel(); /*!< instance of wrappanel from memorygame */
        Memorygame _mg; /*!< instance of memorygame */

        public ImageBrush backgroundimg = new ImageBrush(); /*!< front facing image of card */
        public ImageBrush defaultBG = new ImageBrush(); /*!< rear facing image of card */

        /*! \brief default constructor
       */
        public Card() 
        {
            type = 0; // set default type
            Content = "test"; // set default content
            Width = 50; // set default width
            Height = 50; // set default height
            this.Click += Button1_Click; // subscribes the click event to a function
        }

        /*! \brief override constructor that takes arguements: type, width, size, wrappanel, list of cards, and memorygame instance
       */
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

        /*! \brief eventlistener for when card is clicked
       */
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

        /*! \brief makes sure the card flips and waits with flipping back
       */
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
                    _mg.AddPoints(100);

                    Task.Delay(2000).ContinueWith(_ =>
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            pc[0].IsEnabled = false;
                            pc[1].IsEnabled = false;
                            pc.Clear();

                            wp.IsHitTestVisible = true;

                            _mg.curtime = 30;
                            _mg.CheckGameOver();
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
