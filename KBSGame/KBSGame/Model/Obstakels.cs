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
        public static List<Obstakel> obstakels = new List<Obstakel>();
        public static List<string> waardes = new List<string>();
        MovingObstacle mo;

        public Obstakels(int aantalBoom, int aantalBom, int aantalMoving, int aantalCoin, bool randomLevel = false)
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
                    switch (obs.ObstakelType) { 
                        case "Tree": //add the amount of trees to canvas
                            Tree t = new Tree(obs.ObstakelX, obs.ObstakelY);
                            obstakels.Add(t);
                            //canvas.Children.Add(t.image);
                            Thread.Sleep(25);
                            break;
                        case "Bomb": //generate amount of bombs but don't put them on the screen because it is a land mine
                            Bomb b = new Bomb(obs.ObstakelX, obs.ObstakelY);
                            obstakels.Add(b);
                            Thread.Sleep(25);
                            break;
                        case "Moving": //add the amount of moving obstacles to canvas
                            MovingObstacle mo = new MovingObstacle(true, obs.ObstakelX, obs.ObstakelY);
                            obstakels.Add(mo);
                            //canvas.Children.Add(mo.image);
                            Thread.Sleep(25);
                            break;
                        case "Coin": //add the amount of coins to canvas
                            Coin c = new Coin(obs.ObstakelX, obs.ObstakelY);
                            obstakels.Add(c);
                            //canvas.Children.Add(c.image);
                            Thread.Sleep(25);
                            break;
                    }
                }
            }
            else
            {
                //add the amount of trees to canvas
                for (int i = 0; i < aantalBoom; i++)
                {
                    Tree t = new Tree();
                    obstakels.Add(t);
                    //canvas.Children.Add(t.image);
                    Thread.Sleep(25);
                }
                //generate amount of bombs but don't put it on the screen because it is a land mine
                for (int i = 0; i < aantalBom; i++)
                {
                    Bomb b = new Bomb();
                    //obstakels.Add(b);
                    Thread.Sleep(25);
                }
                //add the amount of moving obstacles to canvas
                for (int i = 0; i < aantalMoving; i++)
                {
                    mo = new MovingObstacle();
                    obstakels.Add(mo);
                    //canvas.Children.Add(mo.image);
                    Thread.Sleep(25);
                }
                //add the amount of coins to canvas
                for (int i = 0; i < aantalCoin; i++)
                {
                    Coin c = new Coin();
                    obstakels.Add(c);
                    //canvas.Children.Add(c.image);
                    Thread.Sleep(25);
                }
            }

        }
        
    }
}
