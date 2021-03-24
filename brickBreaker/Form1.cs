using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace brickBreaker
{
    public partial class Form1 : Form
    {
        //global lists
        List<int> brickYList = new List<int>();
        List<int> brickXList = new List<int>();



        //global variables

        int brickX = 30;
        int brickY = 30;

        int platformX = 245;
        int platformY = 450;
        int platformHeight = 20;
        int platformWidth = 100;
        int platformSpeed = 15;

        int brickHeight = 20;
        int brickWidth = 60;

        int ballX = 290;
        int ballY = 0;
        int ballHeight = 20;
        int ballWidth = 20;
        int ballXSpeed = 0;
        int ballYSpeed = 10;

        int powerUp1X =200;
        int powerUp1Y = 0;
        int powerUp2X= 400;
        int powerUp2Y = 0;

        Random randGen = new Random();
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);

        bool leftDown = false;
        bool rightDown = false;

        int lives = 2;
        int score = 0;


        public Form1()
        {
            InitializeComponent();
            for (int i = 1; i < 5; i++)
            {
                brickYList.Add(brickY);
                brickXList.Add(brickX);
                brickX = brickX + 160;


            }

            brickY = brickY + 100;
            brickX = 30;

            for (int n = 1; n < 5; n++)
            {
                brickYList.Add(brickY);
                brickXList.Add(brickX);
                brickX = brickX + 160;

            }
            brickY = brickY + 100;
            brickX = 30;

            for (int n = 1; n < 5; n++)
            {
                brickYList.Add(brickY);
                brickXList.Add(brickX);
                brickX = brickX + 160;


            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    rightDown = true;
                    break;

                case Keys.Left:
                    leftDown = true;
                    break;
            }
            }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    rightDown = false;
                    break;

                case Keys.Left:
                    leftDown = false;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < brickYList.Count(); i++)
            {
                e.Graphics.FillRectangle(whiteBrush, brickXList[i], brickYList[i], brickWidth, brickHeight);
            }
            e.Graphics.FillEllipse(redBrush, ballX, ballY, ballWidth, ballHeight);
            e.Graphics.FillRectangle(whiteBrush, platformX, platformY, platformWidth, platformHeight);





        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //move the ball
            ballX += ballXSpeed;
            ballY += ballYSpeed;

            //move and control the platform
            if (leftDown == true && platformX>0)
            {
                platformX -= platformSpeed;
            }
            if (rightDown == true&& platformX +platformWidth<this.Width)
            {
                platformX += platformSpeed;
            }
            //check if ball collides with walls
            if (ballY < 0 )
            {
                ballYSpeed *= -1;

            }

            if (ballY>this.Height +ballHeight)
            {
                ballY = 0;
            }
            if (ballX<0|| ballX > this.Height - ballHeight)
            {
                ballXSpeed *= -1;
            }

            // check for collision with ball and paddle

            Rectangle platform = new Rectangle(platformX, platformY, platformWidth, platformHeight);
            Rectangle ball = new Rectangle(ballX, ballY, ballWidth, ballHeight);
































            Refresh();
        }

      
    }
}
