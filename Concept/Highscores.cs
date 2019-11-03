using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Concept
{
    class Highscores
    {
        List<string[,]> HighScores = new List<string[,]>();

        public Highscores()
        {

        }

        public void InjectScores(List<string[,]> collective)
        {
            HighScores = collective;
            CheckHighScores();
        }

        private void CheckHighScores()
        {
            string rootPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase; // get root of directory
            rootPath = System.IO.Path.GetDirectoryName(rootPath); // get more root
            rootPath += "../../../HighScores.sav"; // go to top of directory

            rootPath = new Uri(rootPath).LocalPath;

            String line = "";
            string highscoresstr = "";

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(rootPath))
                {
                    // Read the stream to a string, and write the string to the console.
                    line = sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(rootPath))
                {
                    for (int x = 0; x < HighScores.Count; x++)
                    {
                        line += HighScores[x][0, 0] + "," + HighScores[x][0, 1] + "|";

                        sw.WriteLine(line);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be written:");
                Console.WriteLine(e.Message);
            }
        }

        public void ShowHighScores(Canvas parent)
        {
            string[,] top5hs = new string[5, 2] { {"nan", "0"}, {"nan", "0"}, { "", "0" }, { "nan", "0" }, { "nan", "0" },};

            TextBox hstb = new TextBox();
            hstb.Height = 600;
            hstb.Width = 500;
            Canvas.SetLeft(parent, 500);
            Canvas.SetTop(parent, 50);

            string rootPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase; // get root of directory
            rootPath = System.IO.Path.GetDirectoryName(rootPath); // get more root
            rootPath += "../../../HighScores.sav"; // go to top of directory

            rootPath = new Uri(rootPath).LocalPath;

            string line;

            using (StreamReader sr = new StreamReader(rootPath))
            {
                // Read the stream to a string, and write the string to the console.
                line = sr.ReadToEnd();
            }

            List<string[]> existingHS = new List<string[]>();
            string[] playersWhole = line.Split('|');

            for (int i = 0; i < playersWhole.Length; i++)
            {
                string[] playersDetailed = playersWhole[i].Split(',');
                existingHS.Add(playersDetailed);
            }

            for(int x = 0; x < existingHS.Count; x++)
            {
                for(int y = 0; y < top5hs.Length; y++)
                {
                    if(Convert.ToInt32(existingHS[x][1]) > Convert.ToInt32(top5hs[y, 2]))
                    {
                        //when trying to order these i realised i had gone the compleetly wrong direciton in how i was doing things
                        //and this was made on the last day so i didnt have time to finish it
                    }
                }
            }
        }
    }
}
