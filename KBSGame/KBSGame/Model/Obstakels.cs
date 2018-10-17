using KBSGame.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace KBSGame.GameObjects
{
    public class Obstakels
    {
        public List<Obstakel> obstakels = new List<Obstakel>();
        List<string> type = new List<string>();
        public static List<string> waardes = new List<string>();
        Canvas Canvas;

        public Obstakels(int aantalBoom, int aantalBom, int aantalMoving, int aantalCoin, Canvas canvas, Game game, bool randomLevel = false)
        {
            if (!randomLevel) // is XML level
            {
                Serializer ser = new Serializer();
                string path = string.Empty;
                string xmlInputData = string.Empty;
                string xmlOutputData = string.Empty;

                // Load Data from XML
                path = Directory.GetCurrentDirectory() + @"..\..\..\Resources\level.xml";
                xmlInputData = File.ReadAllText(path);

                // Deserialize nodes
                XMLItem obj = ser.Deserialize<XMLItem>(xmlInputData);

                //Loop through nodes and match type
                foreach(XMLObstakel obs in obj.XMLItems)
                {
                    Console.WriteLine(obs.ObstakelType);
                    switch (obs.ObstakelType) { 
                        case "Tree":
                            Tree t = new Tree(obs.ObstakelX, obs.ObstakelY);
                            obstakels.Add(t);
                            canvas.Children.Add(t.image);
                            Thread.Sleep(25);
                            break;
                        case "Bomb":
                            Bomb b = new Bomb(obs.ObstakelX, obs.ObstakelY);
                            obstakels.Add(b);
                            canvas.Children.Add(b.image);
                            Thread.Sleep(25);
                            break;
                        case "Moving":
                            MovingObstacle mo = new MovingObstacle(game, true, obs.ObstakelX, obs.ObstakelY);
                            obstakels.Add(mo);

                            canvas.Children.Add(mo.image);
                            Thread.Sleep(25);
                            break;
                        case "Coin":
                            Coin c = new Coin(obs.ObstakelX, obs.ObstakelY);
                            obstakels.Add(c);
                            canvas.Children.Add(c.image);
                            Thread.Sleep(25);
                            break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < aantalBoom; i++)
                {
                    Tree t = new Tree();
                    obstakels.Add(t);
                    canvas.Children.Add(t.image);
                    Thread.Sleep(25);
                }

                for (int i = 0; i < aantalBom; i++)
                {
                    Bomb b = new Bomb();
                    obstakels.Add(b);
                    //canvas.Children.Add(b.image);
                    Thread.Sleep(25);
                }
                for (int i = 0; i < aantalMoving; i++)
                {
                    MovingObstacle mo = new MovingObstacle(game);
                    obstakels.Add(mo);
                    canvas.Children.Add(mo.image);
                    Thread.Sleep(25);
                }
                for (int i = 0; i < aantalCoin; i++)
                {
                    Coin c = new Coin();
                    obstakels.Add(c);
                    canvas.Children.Add(c.image);
                    Thread.Sleep(25);
                }
            }

            Canvas = canvas;

        }

        public void Reset()
        {
            for (int i = 0; i < obstakels.Count; i++)
            {
                Canvas.Children.Remove(obstakels[i].image);
            }

            Console.WriteLine(waardes.Count());

            waardes.Clear();
            Console.WriteLine(waardes.Count());
        }
    }
}
