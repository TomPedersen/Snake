using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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

            //Set settings to default
            new GameSettings();

            //create a new snake object
            Snake.Clear();
            SnakeFood head = new SnakeFood {XAxis = 10, YAxis = 5};
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
            food = new SnakeFood { XAxis = random.Next(0, maxXPos), YAxis = random.Next(0, maxYPos)};
        }


        private void UpdateScreen(object sender, EventArgs e)
        {
            //Check for Game Over
            if (GameSettings.GameOver)
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
            if (!GameSettings.GameOver)
            {
                //Set colour of snake

                //Draw snake
                for (int i = 0; i < Snake.Count; i++)
                {
                    Brush snakeColour;
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
                                      Snake[i].YAxis * GameSettings.Height, 
                                      GameSettings.Width, GameSettings.Height));

                    //Draw food

                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.XAxis * GameSettings.Width,
                                      food.YAxis * GameSettings.Height,
                                      GameSettings.Width, GameSettings.Height));
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

        private void MovePlayer()
        {
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                //Move head
                if (i == 0)
                {
                    switch (GameSettings.direction)
                    {
                        case Direction.Right:
                            Snake[i].XAxis++;
                            break;
                        case Direction.Left:
                            Snake[i].XAxis--;
                            break;
                        case Direction.Up:
                            Snake[i].YAxis--;
                            break;
                        case Direction.Down:
                            Snake[i].YAxis++;
                            break;
                    }
   
                    //Get max X and Y pos (world boundries)
                    int maxXPos = GameWorld.Size.Width/GameSettings.Width;
                    int maxYPos = GameWorld.Size.Height / GameSettings.Height;

                    //Detect collision with boundries
                    if (Snake[i].XAxis < 0 || Snake[i].YAxis < 0
                        || Snake[i].XAxis >= maxXPos || Snake[i].YAxis >= maxYPos)
                    {
                        Die();
                    }


                    //Detect collision with body
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].XAxis == Snake[j].XAxis &&
                            Snake[i].YAxis == Snake[j].YAxis)
                        {
                            Die();
                        }
                    }

                    //Detect collision with food
                    if (Snake[0].XAxis == food.XAxis && Snake[0].YAxis == food.YAxis)
                    {
                        Eat();
                    }

                }
                else
                {
                    //Move body
                    Snake[i].XAxis = Snake[i - 1].XAxis;
                    Snake[i].YAxis = Snake[i - 1].YAxis;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ControlsInput.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            ControlsInput.ChangeState(e.KeyCode, false);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Eat()
        {
            //Add circle to body
            SnakeFood food = new SnakeFood
            {
                XAxis = Snake[Snake.Count - 1].XAxis,
                YAxis = Snake[Snake.Count - 1].YAxis
            };
            Snake.Add(food);

            //Update Score
            GameSettings.Score += GameSettings.Points;
            labelScore.Text = GameSettings.Score.ToString();

            GenerateFood();
        }

        private void Die()
        {
            GameSettings.GameOver = true;
        }

        private void pbCanvas_Click(object sender, EventArgs e)
        {

        }
    }
}
