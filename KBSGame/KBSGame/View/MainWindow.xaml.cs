using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace KBSGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>    
    public partial class MainWindow : Window
    {
        public delegate void EsqKeyIsPressed(object source, EventArgs e);
        public event EsqKeyIsPressed escKeyIsPressed;
        public delegate void EnterKeyIsPressed(object source, EventArgs e);
        public event EnterKeyIsPressed enterKeyIsPressed;
        private bool IsPressed = false;

        Game game;

        public MainWindow(bool rl)
        {
            InitializeComponent();

            //hier wordt de game aangemaakt
            game = new Game(this, GameCanvas, 30, 10, 3, 5, 30, rl);
            
            //key eventhandler toevoegen
            this.KeyDown += new KeyEventHandler(OnKeyDown);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsPressed)
            {
                
                IsPressed = true;

                if (!game.FreezePlayer)
                {
                    switch (e.Key)
                    {
                        case Key.Right:
                        case Key.D:
                            game.Player.MoveRight();
                            break;
                        case Key.Left:
                        case Key.A:
                            game.Player.MoveLeft();
                            break;
                        case Key.Down:
                        case Key.S:
                            game.Player.MoveDown();
                            break;
                        case Key.Up:
                        case Key.W:
                            game.Player.MoveUp();
                            break;

                    }
                }
                if (e.Key == Key.Escape)
                {
                    OnEsqKeyIsPressed();
                }
                if (e.Key == Key.Enter)
                {
                    OnEnterKeyIsPressed();

                }
            }
                
        }        

        protected virtual void OnEsqKeyIsPressed()
        {
            escKeyIsPressed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnEnterKeyIsPressed()
        {
            enterKeyIsPressed?.Invoke(this, EventArgs.Empty);                
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            IsPressed = false;
        }
    }
}