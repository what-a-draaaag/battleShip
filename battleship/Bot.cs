using battleship1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battleship
{
    public class Bot
    {
        public int[,] BotMap = new int[Form1.mapCount, Form1.mapCount];
        public int[,] PlayerMap = new int[Form1.mapCount, Form1.mapCount];
        public Button[,] BotButtons = new Button[Form1.mapCount, Form1.mapCount];
        public Button[,] PlayerButtons = new Button[Form1.mapCount, Form1.mapCount];

        public Bot(int[,] PlayerMap, int[,] BotMap, Button[,] PlayerButtons, Button[,] BotButtons)
        {
            this.BotMap = PlayerMap;
            this.PlayerMap = BotMap;
            this.BotButtons = PlayerButtons;
            this.PlayerButtons = BotButtons;

        }

        public bool Validation(int i, int j)
        {
            if (i < 0 || j < 0 || i >= Form1.mapCount || j >= Form1.mapCount) return false;
            return true;
        }

        public bool NoShip(int i, int j, int length, int direction)
        {
            bool noShip = true;
            if (i == 0 && j == 0)
            {
                if (direction == 0)
                {
                    for (int k = 0; k <= length; k++)
                        if (BotMap[i, k] == 1 || BotMap[i + 1, k] == 1) noShip = false;
                }
                else
                {
                    for (int k = 0; k <= length; k++)
                        if (BotMap[k, j] == 1 || BotMap[k, j + 1] == 1) noShip = false;
                }
            }
            else if (i == 9 && j == 9)
            {
                if (direction == 0)
                {
                    for (int k = j; k >= j + length; k++)
                        if (BotMap[i, k] == 1 || BotMap[i - 1, k] == 1) noShip = false;
                }
                else
                {
                    for (int k = i; k >= i + length; k++)
                        if (BotMap[k, j] == 1 || BotMap[k, j - 1] == 1) noShip = false;
                }
            }
            else if (i == 0 && j != 0 && j != 9)
            {
                if (direction == 0)
                {
                    for (int k = j - 1; k <= j + length; k++)
                        if (BotMap[i, k] == 1 || BotMap[i + 1, k] == 1) noShip = false;
                }
                else
                {
                    for (int k = i; k <= i + length; k++)
                        if (BotMap[k, j] == 1 || BotMap[k, j - 1] == 1 || BotMap[k, j + 1] == 1) noShip = false;
                }
            }
            else if (i != 0 && i != 9 && j == 0)
            {
                if (direction == 0)
                {
                    for (int k = j; k <= length; k++)
                        if (BotMap[i, k] == 1 || BotMap[i + 1, k] == 1 || BotMap[i - 1, k] == 1) noShip = false;
                }
                else
                {
                    for (int k = i - 1; k < i + length; k++)
                        if (BotMap[k, j] == 1 || BotMap[k, j + 1] == 1) noShip = false;
                }
            }
            else if (j == 9 && i != 0 && i != 9)
            {
                if (direction == 0)
                {
                    for (int k = j - 1; k < j + length; k++)
                        if (BotMap[i, k] == 1 || BotMap[i - 1, k] == 1 || BotMap[i + 1, k] == 1) noShip = false;
                }
                else
                {
                    for (int k = i - 1; k < i + length; k++)
                        if (BotMap[k, j] == 1 || BotMap[k, j - 1] == 1) noShip = false;
                }
            }
            else if (i == 9 && j != 0 && j != 9)
            {
                if (direction == 0)
                {
                    for (int k = j - 1; k < j + length; k++)
                        if (BotMap[i, k] == 1 || BotMap[i - 1, k] == 1) noShip = false;
                }
                else
                {
                    for (int k = i - 1; k < i + length; k++)
                        if (BotMap[k, j] == 1 || BotMap[k, j + 1] == 1 || BotMap[k, j - 1] == 1) noShip = false;
                }
            }
            else if (i == 9 && j == 0)
            {
                if (direction == 0)
                {
                    for (int k = j - 1; k < j + length; k++)
                        if (BotMap[i, k] == 1 || BotMap[i - 1, k] == 1) noShip = false;
                }
                else
                {
                    for (int k = i - 1; k < i + length; k++)
                        if (BotMap[k, j] == 1 || BotMap[k, j + 1] == 1) noShip = false;
                }
            }
            else if (i == 0 && j == 9)
            {
                if (direction == 0)
                {
                    for (int k = j - 1; k < j + length; k++)
                        if (BotMap[i, k] == 1 || BotMap[i + 1, k] == 1) noShip = false;
                }
                else
                {
                    for (int k = i; k <= i + length; k++)
                        if (BotMap[k, j] == 1 || BotMap[k, j - 1] == 1) noShip = false;
                }
            }
            else
            {
                if (direction == 0)
                {
                    for (int k = j - 1; k <= j + length; k++)
                        if (BotMap[i, k] == 1 || BotMap[i - 1, k] == 1 || BotMap[i + 1, k] == 1) noShip = false;
                }
                else
                {
                    for (int k = i - 1; k <= i + length; k++)
                        if (BotMap[k, j] == 1 || BotMap[k, j - 1] == 1 || BotMap[k, j + 1] == 1) noShip = false;
                }
            }
            return noShip;
        }

        public int[,] CreateShips()
        {
            int ships = 10;
            int positionX, positionY = 0;
            int direction;
            int count = 0;
            Random rand = new Random();
            for (int i = 4; i > 0; i--)
            {
                count = 4 - i + 1;
                for (int k = 0; k < count; k++)
                {
                    positionX = rand.Next(0, Form1.mapCount - i);
                    positionY = rand.Next(0, Form1.mapCount - i);
                    direction = rand.Next(0, 2);
                    while (!NoShip(positionX, positionY, i, direction))
                    {
                        positionX = rand.Next(0, Form1.mapCount - i);
                        positionY = rand.Next(0, Form1.mapCount - i);
                        direction = rand.Next(0, 2);
                    }
                    if (direction == 0) for (int j = 0; j < i; j++) BotMap[positionX, positionY + j] = 1;
                    else for (int j = 0; j < i; j++) BotMap[positionX + j, positionY] = 1;
                    ships--;
                }
                if (ships <= 0) break;
            }
            return BotMap;
        }

        public bool MakeMove()
        {
            bool hit = false;
            Random rand = new Random();
            int positionX = rand.Next(0, Form1.mapCount);
            int positionY = rand.Next(0, Form1.mapCount);
            if (PlayerMap[positionX, positionY] == 5)
            {
                while (PlayerMap[positionX, positionY] == 5)
                {
                    positionX = rand.Next(0, Form1.mapCount);
                    positionY = rand.Next(0, Form1.mapCount);
                }
            }
            if (PlayerMap[positionX, positionY] == 1)
            {
                hit = true;
                PlayerMap[positionX, positionY] = 5;
                PlayerButtons[positionX, positionY].BackColor = Color.CornflowerBlue;
                PlayerButtons[positionX, positionY].BackgroundImage = battleship.Properties.Resources.крестик;
            }
            else
            {
                hit = false;
                PlayerMap[positionX, positionY] = 5;
                PlayerButtons[positionX, positionY].BackColor = Color.AliceBlue;
                PlayerButtons[positionX, positionY].BackgroundImage = battleship.Properties.Resources.нолик;
            }
            if (hit) MakeMove();
            return hit;
        }
    }
}
