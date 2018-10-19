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
    public class Obstacles
    {
        public List<Obstacle> obstacles = new List<Obstacle>();
        public static List<string> values = new List<string>();
        Canvas Canvas;
        MovingObstacle mo;

        public Obstacles(int amountOfTrees, int amountOfBombs, int amountOfMovingObstacles, int amountOfCons, Canvas canvas, Game game, bool randomLevel = false)
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
                    switch (obs.ObstacleType) { 
                        case "Tree": //add the amount of trees to canvas
                            Tree t = new Tree(obs.ObstacleX, obs.ObstacleY);
                            obstacles.Add(t);
                            canvas.Children.Add(t.image);
                            Thread.Sleep(25);
                            break;
                        case "Bomb": //generate amount of bombs but don't put it on the screen because it is a land mine
                            Bomb b = new Bomb(obs.ObstacleX, obs.ObstacleY);
                            obstacles.Add(b);
                            Thread.Sleep(25);
                            break;
                        case "Moving": //add the amount of moving obstacles to canvas
                            MovingObstacle mo = new MovingObstacle(game, true, obs.ObstacleX, obs.ObstacleY);
                            obstacles.Add(mo);
                            canvas.Children.Add(mo.image);
                            Thread.Sleep(25);
                            break;
                        case "Coin": //add the amount of coins to canvas
                            Coin c = new Coin(obs.ObstacleX, obs.ObstacleY);
                            obstacles.Add(c);
                            canvas.Children.Add(c.image);
                            Thread.Sleep(25);
                            break;
                    }
                }
            }
            else
            {
                //add the amount of trees to the canvas
                for (int i = 0; i < amountOfTrees; i++)
                {
                    Tree t = new Tree();
                    obstacles.Add(t);
                    canvas.Children.Add(t.image);
                    Thread.Sleep(25);
                }
                //generate amount of bombs but don't put them on the screen because it is a land mine
                for (int i = 0; i < amountOfBombs; i++)
                {
                    Bomb b = new Bomb();
                    obstacles.Add(b);
                    Thread.Sleep(25);
                }
                //add the amount of moving obstacles to the canvas
                for (int i = 0; i < amountOfMovingObstacles; i++)
                {
                    mo = new MovingObstacle(game);
                    obstacles.Add(mo);
                    canvas.Children.Add(mo.image);
                    Thread.Sleep(25);
                }
                //add the amount of coins to the canvas
                for (int i = 0; i < amountOfCons; i++)
                {
                    Coin c = new Coin();
                    obstacles.Add(c);
                    canvas.Children.Add(c.image);
                    Thread.Sleep(25);
                }
            }

            Canvas = canvas;
        }

        //reset all the obstacles that are placed on the canvas
        public void Reset()
        {
            for (int i = 0; i < obstacles.Count; i++)
            {
                //remove obstacles from the canvas
                Canvas.Children.Remove(obstacles[i].image);
            }

            //remove eventhandler from ghosts
            foreach (Obstacle o in obstacles)
            {
                if(o.GetType().ToString() == "KBSGame.Model.MovingObstacle")
                {
                    MovingObstacle mo = (MovingObstacle)o;
                    mo.timer.Tick -= mo.MoveObstakelRandom;
                }
            }
            //remove all of the data in values and obstacles so it is empty
            obstacles.Clear();
            values.Clear();
        }
    }
}
