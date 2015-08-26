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
