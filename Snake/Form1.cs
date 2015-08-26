using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Snake.Properties;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<SnakeFood>Snake = new List<SnakeFood>();
        private SnakeFood food = new SnakeFood();

        public Form1()
        {
            InitializeComponent();

            //Set settings to default
            new GameSettings();

            //Set game speed and start timer
            gameTimer.Interval = 1000/GameSettings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            //Start new game
            StartGame();
        }

        private void StartGame()
        {
            labelGameOver.Visible = false;
            new GameSettings();

            //create a new snake object
            Snake.Clear();
            SnakeFood head = new SnakeFood();
            head.XAxis = 10;
            head.YAxis = 5;
            Snake.Add(head);


            labelScore.Text = GameSettings.Score.ToString();
            GenerateFood();
        }

        //Create randomly-placed food item in game world
        private void GenerateFood()
        {
            int maxXPos = GameWorld.Size.Width/GameSettings.Width;
            int maxYPos = GameWorld.Size.Height/GameSettings.Height;

            Random random = new Random();
            food = new SnakeFood();
            food.XAxis = random.Next(0, maxXPos);
            food.YAxis = random.Next(0, maxYPos);
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            //Check for Game Over
            if (GameSettings.GameOver == true)
            {
                //Check if Enter key is pressed
                if (ControlsInput.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                if(ControlsInput.KeyPressed(Keys.Right) && GameSettings.direction != Direction.Left)
                    GameSettings.direction = Direction.Right;
                else if (ControlsInput.KeyPressed(Keys.Left) && GameSettings.direction != Direction.Right)
                    GameSettings.direction = Direction.Left;
                else if (ControlsInput.KeyPressed(Keys.Up) && GameSettings.direction != Direction.Down)
                    GameSettings.direction = Direction.Up;
                else if (ControlsInput.KeyPressed(Keys.Down) && GameSettings.direction != Direction.Up)
                    GameSettings.direction = Direction.Down;

                MovePlayer();
            }

            GameWorld.Invalidate();
        }

        private void GameWorld_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if (GameSettings.GameOver != false)
            {
                //Set colour of snake
                Brush snakeColour;

                //Draw snake
                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                    {
                        snakeColour = Brushes.Black; //Draw head
                    }
                    else
                    {
                        snakeColour = Brushes.Green; //Draw body
                    }

                    //Draw snake
                    canvas.FillEllipse(snakeColour,
                        new Rectangle(Snake[i].XAxis * GameSettings.Width,
                            Snake[i].YAxis*GameSettings.Height, GameSettings.Width, GameSettings.Height));

                    //Draw food

                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.YAxis * GameSettings.Width,
                            GameSettings.Height, GameSettings.Width, GameSettings.Height));
                }
            }
            else
            {
                string gameOver = "Game over \nYour final score is: " + GameSettings.Score +
                                  "\nPress Enter to try again";
                labelGameOver.Text = gameOver;
                labelGameOver.Visible = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
