using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using battleship;
using System.Threading;

namespace battleship1
{
    public partial class Form1 : Form
    {
        public const int mapCount = 10;

        public int size = 50;
        public int shipsCount = 0;
        public string text = "ABCDEFGHIJ";
        public int[,] PlayerMap = new int[mapCount, mapCount];
        public int[,] BotMap = new int[mapCount, mapCount];
        public Button[,] PlayerButtons = new Button[mapCount, mapCount];
        public Button[,] BotButtons = new Button[mapCount, mapCount];
        public bool startedGame;
        

        public Bot bot;
        public Form1()
        {
            InitializeComponent();
            this.Text = "BattleShip";
            Init();
        }
        public void Init()
        {
            startedGame = false;
            CreateMap();
            shipsCount = 0;
            bot = new Bot(BotMap, PlayerMap, BotButtons, PlayerButtons);
            BotMap = bot.CreateShips();
        }

        public void CreateMap()
        {
            this.Width = mapCount * 2 * size + 160;
            this.Height = mapCount * size + 200;
            this.StartPosition = FormStartPosition.CenterScreen;
            for (int i = 0; i < mapCount; i++)
            {
                for (int j = 0; j < mapCount; j++)
                {
                    PlayerMap[i, j] = 0;
                    Button buttonP = new Button();
                    buttonP.Location = new Point(50 + j * size, 50 + i * size);
                    buttonP.Size = new Size(size, size);
                    buttonP.BackColor = Color.LightSteelBlue;
                    this.Controls.Add(buttonP);
                    PlayerButtons[i, j] = buttonP;
                    buttonP.Click += new EventHandler(CreateShips);


                    BotMap[i, j] = 0;
                    Button buttonB = new Button();
                    buttonB.Location = new Point(600 + j * size, 50 + i * size);
                    buttonB.Size = new Size(size, size);
                    buttonB.BackColor = Color.LightSteelBlue;
                    this.Controls.Add(buttonB);
                    BotButtons[i, j] = buttonB;
                    buttonB.Click += new EventHandler(PlayersMove);

                    Label labelText = new Label();
                    labelText.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Regular);
                    labelText.Location = new Point(50 + j * size, 0);
                    labelText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    labelText.Size = new Size(size, size);
                    labelText.Text = text[j].ToString();
                    this.Controls.Add(labelText);

                    Label labelNumber = new Label();
                    labelNumber.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Regular);
                    labelNumber.Location = new Point(0, 50 + i * size);
                    labelNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    labelNumber.Size = new Size(size, size);
                    labelNumber.Text = (i + 1).ToString();
                    this.Controls.Add(labelNumber);

                    Label labelText2 = new Label();
                    labelText2.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Regular);
                    labelText2.Location = new Point(550 + 50 + j * size, 0);
                    labelText2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    labelText2.Size = new Size(size, size);
                    labelText2.Text = text[j].ToString();
                    this.Controls.Add(labelText2);

                    Label labelNumber2 = new Label();
                    labelNumber2.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Regular);
                    labelNumber2.Location = new Point(550, 50 + i * size);
                    labelNumber2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    labelNumber2.Size = new Size(size, size);
                    labelNumber2.Text = (i + 1).ToString();
                    this.Controls.Add(labelNumber2);
                }
            }
            Label NameForPlayer = new Label();
            NameForPlayer.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Regular);
            NameForPlayer.Location = new Point(50, 550);
            NameForPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            NameForPlayer.Size = new Size(10 * size, size);
            NameForPlayer.Text = "P l a y e r     m a p";
            this.Controls.Add(NameForPlayer);

            Label Rules = new Label();
            Rules.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Regular);
            Rules.Location = new Point(0, 600);
            Rules.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Rules.Size = new Size(12 * size, size);
            Rules.Text = "1  s h i p  x  4     2  s h i p s  x  3     3  s h i p s  x  2     4  s h i p s  x  1";
            this.Controls.Add(Rules);

            Label NameForBot = new Label();
            NameForBot.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Regular);
            NameForBot.Location = new Point(600, 550);
            NameForBot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            NameForBot.Size = new Size(10 * size, size);
            NameForBot.Text = "B o t     m a p";
            this.Controls.Add(NameForBot);

            Button start = new Button();
            start.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Regular);
            start.Location = new Point(750, 600);
            start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            start.Size = new Size(4 * size, size);
            start.Text = "S t a r t";
            start.Click += new EventHandler(Start);
            this.Controls.Add(start);

        }

        public void Start(object sender, EventArgs eventArgs)
        {
            if (shipsCount==20) startedGame = true;
            else startedGame = false;
        }

        public void CreateShips(object sender, EventArgs eventArgs)
        {
            Button chosenButton = sender as Button;
            if (!startedGame)
            {
                if (PlayerMap[(chosenButton.Location.Y - 50) / size, (chosenButton.Location.X - 50) / size] == 0)
                {
                    chosenButton.BackColor = Color.CornflowerBlue;
                    PlayerMap[(chosenButton.Location.Y - 50) / size, (chosenButton.Location.X - 50) / size] = 1;
                    shipsCount++;
                }
                else
                {
                    chosenButton.BackColor = Color.LightSteelBlue;
                    PlayerMap[(chosenButton.Location.Y - 50) / size, (chosenButton.Location.X - 50) / size] = 0;
                    shipsCount--;
                }
            }
        }


        public void PlayersMove(object sender, EventArgs eventArgs)
        {
            Button chosenButton = sender as Button;
            bool turnPlayer = MakeMove(BotMap, chosenButton);
            if (!turnPlayer) bot.MakeMove();
            Win();

        }

        public bool CheckWinner()
        {
            bool WinPlayer = false;
            bool WinBot = true;
            int count = 0;
            for (int i = 0; i < mapCount; i++)
            {
                for (int j = 0; j < mapCount; j++)
                {
                    if (PlayerMap[i, j] == 1) WinBot = false;
                    if (BotMap[i, j]==1 && BotButtons[i, j].BackColor==Color.CornflowerBlue) count++;

                }
            }
            if (count == 20) WinPlayer = true;
            return (WinPlayer || WinBot);
        }

        public void Win()
        {
            if (CheckWinner())
            {
                for (int i = 0; i < mapCount; i++)
                {
                    for (int k = 0; k < mapCount; k++)
                    {
                        PlayerButtons[i, k].BackColor = Color.LightSteelBlue;
                        BotButtons[i, k].BackColor = Color.LightSteelBlue;
                    }
                }
                MessageBox.Show("End of the Game");
                Thread.Sleep(500);
                this.Controls.Clear();
                Init();
            }
        }

        public bool MakeMove(int[,] map, Button chosenButton)
        {
            bool hit = true;
            if (startedGame)
            {
                int check = 0;
                if (chosenButton.Location.X >= 600) check = 550;
                if (chosenButton.BackColor == Color.LightSteelBlue)
                {
                    if (map[(chosenButton.Location.Y - 50) / size, (chosenButton.Location.X - 50 - check) / size] == 1)
                    {
                        hit = true;
                        chosenButton.BackColor = Color.CornflowerBlue;
                        chosenButton.BackgroundImage = battleship.Properties.Resources.крестик;
                    }
                    else
                    {
                        hit = false;
                        chosenButton.BackColor = Color.AliceBlue;
                        chosenButton.BackgroundImage = battleship.Properties.Resources.нолик;
                    }
                }
            }
            return hit;
        }
    }
}