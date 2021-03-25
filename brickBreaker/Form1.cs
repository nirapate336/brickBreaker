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
        int platformWidth = 60;
        int platformSpeed = 15;

        int brickHeight = 20;
        int brickWidth = 60;

        int ballX = 290;
        int ballY = 0;
        int ballHeight = 15;
        int ballWidth = 15;
        int ballXSpeed = 0;
        int ballYSpeed = 10;

        int purplePowerUpX;
        int purplePowerUpY = -50;
        int purplePowerUpYSpeed = 0;

        int yellowPowerUpX;
        int yellowPowerUpY = -50;
        int yellowPowerUpYSpeed = 0;

        int greenPowerUpX;
        int greenPowerUpY = -50;
        int greenPowerUpYSpeed = 0;

        int powerUp1X = 250;
        int powerUp1Y = -50;
        int ball2XSpeed = 0;
        int ball2YSpeed = 0;

        int powerUp2X = 300;
        int powerUp2Y = -50;
        int ball3XSpeed = 0;
        int ball3YSpeed = 0;


        Random randGen = new Random();
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush purpleBrush = new SolidBrush(Color.MediumPurple);
        SolidBrush pinkBrush = new SolidBrush(Color.LightPink);
        SolidBrush yellowBrush = new SolidBrush(Color.LightGoldenrodYellow);
        SolidBrush greenBrush = new SolidBrush(Color.Green);


        bool leftDown = false;
        bool rightDown = false;

        int lives = 3;
        int score = 0;
        int counter = 0;
        int redCounter = 0;
        



        public Form1()
        {
            InitializeComponent();
            for (int i = 1; i < 5; i++)
            {
                brickYList.Add(brickY);
                brickXList.Add(brickX);
                brickX = brickX + 160;


            }

            brickY = brickY + 75;
            brickX = 30;

            for (int n = 1; n < 5; n++)
            {
                brickYList.Add(brickY);
                brickXList.Add(brickX);
                brickX = brickX + 160;

            }
            brickY = brickY + 75;
            brickX = 30;

            for (int n = 1; n < 5; n++)
            {
                brickYList.Add(brickY);
                brickXList.Add(brickX);
                brickX = brickX + 160;
            }
            brickY = brickY + 75;
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
            e.Graphics.FillEllipse(purpleBrush, purplePowerUpX, purplePowerUpY, ballHeight, ballWidth);
            e.Graphics.FillEllipse(pinkBrush, powerUp1X, powerUp1Y, ballWidth, ballHeight);
            e.Graphics.FillEllipse(pinkBrush, powerUp2X, powerUp2Y, ballWidth, ballHeight);
            e.Graphics.FillEllipse(yellowBrush, yellowPowerUpX, yellowPowerUpY, ballHeight, ballWidth);
            e.Graphics.FillEllipse(greenBrush, greenPowerUpX, greenPowerUpY, ballHeight, ballWidth);


            scoreLabel.Text = $"Score:{score}";
            lifeLabel.Text = $"Lives : {lives}";


        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //move the ball
            ballX += ballXSpeed;
            ballY += ballYSpeed;
            powerUp1X += ball2XSpeed;
            powerUp1Y += ball2YSpeed;
            powerUp2X += ball3XSpeed;
            powerUp2Y += ball3YSpeed;
            purplePowerUpY += purplePowerUpYSpeed;
            yellowPowerUpY += yellowPowerUpYSpeed;
            greenPowerUpY += greenPowerUpYSpeed;


            //move and control the platform
            if (leftDown == true && platformX > 0)
            {
                platformX -= platformSpeed;
            }
            if (rightDown == true && platformX + platformWidth < this.Width)
            {
                platformX += platformSpeed;
            }
            //check if ball collides with walls
            if (ballY < 0)
            {
                ballY += 1;
                ballYSpeed *= -1;

            }

            if (ballY > this.Height + ballHeight)
            {
                ballY = 0;
                ballX = 290;
                ballYSpeed = 10;
                ballXSpeed = 0;
                lives--;
               

            }
            if (ballX < 0)
            {
                ballX += 1;
                ballXSpeed *= -1;
            }


            if (ballX > this.Width - ballHeight)
            {
                ballX -= 1;
                ballXSpeed *= -1;
            }
            //check ball two if it collides with walls
            if (powerUp1Y < 0)

            {
                powerUp1Y += 1;
                ball2YSpeed *= -1;

            }
            if (powerUp1Y > this.Height + ballHeight)
            {
                powerUp1Y = -100;
                powerUp1X = 290;
                ball2YSpeed = 0;
                ball2XSpeed = 0;
            }
            if (powerUp1X < 0)

            {
                powerUp1X += 1;
                ball2XSpeed *= -1;

            }
            if (powerUp1X > this.Width - ballHeight)
            {
                powerUp1X -= 1;
                ball2XSpeed *= -1;
            }
            //check ballthreeif it collides with walls
            if (powerUp2Y < 0)

            {
                powerUp2Y += 1;
                ball3YSpeed *= -1;

            }
            if (powerUp2Y > this.Height + ballHeight)
            {
                powerUp2Y = -100;
                powerUp2X = 290;
                ball3YSpeed = 0;
                ball3XSpeed = 0;
            }
            if (powerUp2X < 0)

            {
                powerUp2X += 1;
                ball3XSpeed *= -1;

            }
            if (powerUp2X > this.Width - ballHeight)
            {
                powerUp2X -= 1;
                ball3XSpeed *= -1;
            }

            // check for collision with ball and paddle

            Rectangle platformRec = new Rectangle(platformX, platformY, platformWidth, platformHeight);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);
            Rectangle ball2Rec = new Rectangle(powerUp1X, powerUp1Y, ballWidth, ballHeight);
            Rectangle ball3Rec = new Rectangle(powerUp2X, powerUp2Y, ballWidth, ballHeight);

           

            int gen = randGen.Next(1, 3);
            if (ballRec.IntersectsWith(platformRec))
            {
                redCounter++;
                ballYSpeed *= -1;
                if (gen == 1)
                {
                    ballXSpeed = randGen.Next(-10, -3);
                }
                else if (gen == 2)
                {
                    ballXSpeed = randGen.Next(3, 10);
                }

                if (redCounter==2)
                {
                    platformWidth = 60;
                    platformSpeed = 15;
                    redCounter = 0;
                    
                }
                
            }
            // check if ball 2 intersects with the paddle

            if (ball2Rec.IntersectsWith(platformRec))
            {
                ball2YSpeed *= -1;
                if (gen == 1)
                {
                    ball2XSpeed = randGen.Next(-10, -3);
                }
                else if (gen == 2)
                {
                    ball2XSpeed = randGen.Next(3, 10);
                }

            }
            //check if ball 3 interescts with paddle
            if (ball3Rec.IntersectsWith(platformRec))
            {
                ball3YSpeed *= -1;
                if (gen == 1)
                {

                    ball3XSpeed = randGen.Next(-10, -3);
                }

                else if (gen == 2)
                {
                    ball3XSpeed = randGen.Next(3, 10);
                }

            }

            //check for ball and brick collisions and change direction
            for (int i = 0; i < brickYList.Count(); i++)
            {
                Rectangle brickRec = new Rectangle(brickXList[i], brickYList[i], brickWidth, brickHeight);
                Rectangle ballTopRec = new Rectangle(ballX, ballY, ballWidth, 1);
                Rectangle ballBottomRec = new Rectangle(ballX, ballY + ballHeight, ballWidth, 1);
                Rectangle ballLeftRec = new Rectangle(ballX, ballY, 1, ballHeight);
                Rectangle ballRightRec = new Rectangle(ballX + ballWidth, ballY, 1, ballHeight);

                if (ballTopRec.IntersectsWith(brickRec))
                {
                    ballY = brickHeight + brickYList[i];
                    ballYSpeed *= -1;
                    brickXList.RemoveAt(i);
                    brickYList.RemoveAt(i);
                    score++;

                    if (gen == 1)
                    {
                        ballXSpeed = randGen.Next(-10, -3);
                        break;
                    }
                    else if (gen == 2)
                    {
                        ballXSpeed = randGen.Next(3, 10);
                        break;

                    }

                    break;

                }
                if (ballBottomRec.IntersectsWith(brickRec))
                {
                    ballY = brickYList[i] - 10;
                    ballYSpeed *= -1;
                    brickXList.RemoveAt(i);
                    brickYList.RemoveAt(i);
                    score++;
                    if (gen == 1)
                    {
                        ballXSpeed = randGen.Next(-10, -3);
                        break;
                    }
                    else if (gen == 2)
                    {
                        ballXSpeed = randGen.Next(3, 10);
                    }
                    break;
                }

                if (ballLeftRec.IntersectsWith(brickRec))
                {
                    brickXList.RemoveAt(i);
                    brickYList.RemoveAt(i);
                    score++;
                    if (gen == 1)
                    {
                        ballYSpeed = randGen.Next(-10, -3);
                    }
                    else if (gen == 2)
                    {
                        ballYSpeed = randGen.Next(3, 10);
                    }

                    ballXSpeed *= -1;
                    ballX = ballX + 1;
                    break;
                }
                if (ballRightRec.IntersectsWith(brickRec))
                {
                    brickXList.RemoveAt(i);
                    brickYList.RemoveAt(i);
                    score++;
                    if (gen == 1)
                    {
                        ballY = randGen.Next(-10, -3);
                    }
                    else if (gen == 2)
                    {
                        ballY = randGen.Next(3, 10);
                    }

                    ballXSpeed *= -1;
                    ballX = ballX + ballWidth;
                    break;
                }
            }

            // check if ball 2 collided with the bricks
            for (int i = 0; i < brickYList.Count(); i++)
            {
                Rectangle brickRec = new Rectangle(brickXList[i], brickYList[i], brickWidth, brickHeight);
                Rectangle ball2TopRec = new Rectangle(powerUp1X, powerUp1Y, ballWidth, 1);
                Rectangle ball2BottomRec = new Rectangle(powerUp1X, powerUp1Y + ballHeight, ballWidth, 1);
                Rectangle ball2LeftRec = new Rectangle(powerUp1X, powerUp1Y, 1, ballHeight);
                Rectangle ball2RightRec = new Rectangle(powerUp1X + ballWidth, powerUp1Y, 1, ballHeight);

                if (ball2TopRec.IntersectsWith(brickRec))
                {
                    powerUp1Y = brickHeight + brickYList[i];
                    ball2YSpeed *= -1;
                    brickXList.RemoveAt(i);
                    brickYList.RemoveAt(i);
                    score++;

                    if (gen == 1)
                    {
                        ball2XSpeed = randGen.Next(-10, -3);
                        break;
                    }
                    else if (gen == 2)
                    {
                        ball2XSpeed = randGen.Next(3, 10);
                        break;

                    }
                }

                if (ball2BottomRec.IntersectsWith(brickRec))
                {
                    powerUp1Y = brickYList[i] - 10;
                    ball2YSpeed *= -1;
                    brickXList.RemoveAt(i);
                    brickYList.RemoveAt(i);
                    score++;
                    if (gen == 1)
                    {
                        ball2XSpeed = randGen.Next(-10, -3);
                        break;
                    }
                    else if (gen == 2)
                    {
                        ball2XSpeed = randGen.Next(3, 10);
                    }
                    break;
                }

                if (ball2LeftRec.IntersectsWith(brickRec))
                {
                    brickXList.RemoveAt(i);
                    brickYList.RemoveAt(i);
                    score++;
                    if (gen == 1)
                    {
                        ball2YSpeed = randGen.Next(-10, -3);
                    }
                    else if (gen == 2)
                    {
                        ball2YSpeed = randGen.Next(3, 10);
                    }

                    ball2XSpeed *= -1;
                    powerUp1X += 1;
                    break;
                }
                if (ball2RightRec.IntersectsWith(brickRec))
                {
                    brickXList.RemoveAt(i);
                    brickYList.RemoveAt(i);
                    score++;
                    if (gen == 1)
                    {
                        ball2YSpeed = randGen.Next(-10, -3);
                    }
                    else if (gen == 2)
                    {
                        ball2YSpeed = randGen.Next(3, 10);
                    }

                    ball2XSpeed *= -1;
                    powerUp1X += ballWidth;
                    break;
                }
            }

            //check if ball 3 collides with a brick
            for (int i = 0; i < brickYList.Count(); i++)
            {
                Rectangle brickRec = new Rectangle(brickXList[i], brickYList[i], brickWidth, brickHeight);
                Rectangle ball3TopRec = new Rectangle(powerUp2X, powerUp2Y, ballWidth, 1);
                Rectangle ball3BottomRec = new Rectangle(powerUp2X, powerUp2Y + ballHeight, ballWidth, 1);
                Rectangle ball3LeftRec = new Rectangle(powerUp2X, powerUp2Y, 1, ballHeight);
                Rectangle ball3RightRec = new Rectangle(powerUp2X + ballWidth, powerUp2Y, 1, ballHeight);

                if (ball3TopRec.IntersectsWith(brickRec))
                {
                    powerUp2Y = brickHeight + brickYList[i];
                    ball3YSpeed *= -1;
                    brickXList.RemoveAt(i);
                    brickYList.RemoveAt(i);
                    score++;

                    if (gen == 1)
                    {
                        ball3XSpeed = randGen.Next(-10, -3);
                        break;
                    }
                    else if (gen == 2)
                    {
                        ball3XSpeed = randGen.Next(3, 10);
                        break;

                    }
                }

                if (ball3BottomRec.IntersectsWith(brickRec))
                {
                    powerUp2Y = brickYList[i] - 10;
                    ball3YSpeed *= -1;
                    brickXList.RemoveAt(i);
                    brickYList.RemoveAt(i);
                    score++;
                    if (gen == 1)
                    {
                        ball3XSpeed = randGen.Next(-10, -3);
                        break;
                    }
                    else if (gen == 2)
                    {
                        ball3XSpeed = randGen.Next(3, 10);
                    }
                    break;
                }

                if (ball3LeftRec.IntersectsWith(brickRec))
                {
                    brickXList.RemoveAt(i);
                    brickYList.RemoveAt(i);
                    score++;
                    if (gen == 1)
                    {
                        ball3YSpeed = randGen.Next(-10, -3);
                    }
                    else if (gen == 2)
                    {
                        ball3YSpeed = randGen.Next(3, 10);
                    }

                    ball3XSpeed *= -1;
                    powerUp2X += 1;
                    break;
                }
                if (ball3RightRec.IntersectsWith(brickRec))
                {
                    brickXList.RemoveAt(i);
                    brickYList.RemoveAt(i);
                    score++;
                    if (gen == 1)
                    {
                        ball3YSpeed = randGen.Next(-10, -3);
                    }
                    else if (gen == 2)
                    {
                        ball3YSpeed = randGen.Next(3, 10);
                    }

                    ball3XSpeed *= -1;
                    powerUp2X += ballWidth;
                    break;
                }
            }

            // check for purple power up and platfrom collisions
            Rectangle purpleRec = new Rectangle(purplePowerUpX, purplePowerUpY, ballWidth, ballHeight);

            if (purpleRec.IntersectsWith(platformRec))
            {
                purplePowerUpY = 0-ballHeight;

                purplePowerUpYSpeed = 0;
                ball2YSpeed = 5;
                ball3YSpeed = 5;
            }

            // check for yellow power up and platform collision
            Rectangle yellowRec = new Rectangle(yellowPowerUpX, yellowPowerUpY, ballWidth, ballHeight);
          
            if (yellowRec.IntersectsWith(platformRec))
            {
                yellowPowerUpY = 0-ballHeight;
     
                yellowPowerUpYSpeed = 0;
                platformWidth = 150;
                redCounter = 0;
            }
            //check for green power up and platform collision
            Rectangle greenRec = new Rectangle(greenPowerUpX, greenPowerUpY, ballWidth, ballHeight);

            if (greenRec.IntersectsWith (platformRec))
            {
                redCounter = 0;
                greenPowerUpY = 0;
                greenPowerUpYSpeed = 0;
                platformSpeed = 30;
            }


            // when to drop powerups 
            counter++;
            if (counter == 20)
            {
                purplePowerUpX = randGen.Next(100, 550);
                purplePowerUpYSpeed = 5;

            }
            else if (counter == 200)
            {
                yellowPowerUpX = randGen.Next(100, 550);
                yellowPowerUpYSpeed = 5;

            }
            else if (counter == 380)
            {
                greenPowerUpX = randGen.Next(100, 550);
                greenPowerUpYSpeed = 5;

            }





































            //if (lives==0)
            //{
            //    gameTimer.Enabled = false;

            //}

            //if (score == 16)
            //{
            //    gameTimer.Enabled = false;


            //}

            Refresh();
        }


    }
}
