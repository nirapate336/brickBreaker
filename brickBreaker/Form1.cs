using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;


namespace brickBreaker
{
    // NiravPatel Final Project - Brick Breaker - April 7 2021
    public partial class Form1 : Form
    {
        //global lists
        List<int> brickYList = new List<int>();
        List<int> brickXList = new List<int>();
        List<int> scoreList = new List<int>();

        //global variables
        int brickX = 30;
        int brickY = 30;

        int platformX = 260;
        int platformY = 450;
        int platformHeight = 45;
        int platformWidth = 100;
        int platformSpeed = 15;

        int brickHeight = 20;
        int brickWidth = 60;

        int ballX = 290;
        int ballY = 0;
        int ballHeight = 25;
        int ballWidth = 25;
        int ballXSpeed = 0;
        int ballYSpeed = 3;

        int purplePowerUpX;
        int purplePowerUpY = -50;
        int purplePowerUpYSpeed = 0;
        int yellowPowerUpX;
        int yellowPowerUpY = -50;
        int yellowPowerUpYSpeed = 0;
        int greenPowerUpX;
        int greenPowerUpY = -50;
        int greenPowerUpYSpeed = 0;

        int powerUp1X = 270;
        int powerUp1Y = -50;
        int ball2XSpeed = 0;
        int ball2YSpeed = 0;

        int powerUp2X = 320;
        int powerUp2Y = -50;
        int ball3XSpeed = 0;
        int ball3YSpeed = 0;
        string powerUpColour = "none";

        bool leftDown = false;
        bool rightDown = false;
        Random randGen = new Random();
        int lives = 3;
        int score = 0;
        int counter = 0;
        int redCounter = 0;
        string gameState = "waiting";

        SoundPlayer player = new SoundPlayer(Properties.Resources.hit);
        SoundPlayer powerUpPlayer = new SoundPlayer(Properties.Resources.powerUp);
        SoundPlayer lossPlayer = new SoundPlayer(Properties.Resources.ballLose);
        SoundPlayer winPlayer = new SoundPlayer(Properties.Resources.winner);
        SoundPlayer loserPlayer = new SoundPlayer(Properties.Resources.loser);
        Image brickImage = Properties.Resources.brick3;
        Image platformImage = Properties.Resources.platform;

        public Form1()
        {
            InitializeComponent();
        }
        public void GameInitialize()
        {
            //reset all variabels that were set in global and create starting bricks
            gameState = "running";
            lives = 3;
            score = 0;
            brickYList.Clear();
            brickXList.Clear();
            brickX = 30;
            brickY = 30;
            ballX = 290;
            ballY = 0;
            ballYSpeed = 10;
            ballXSpeed = 0;
            counter = 0;
            redCounter = 0;
            purplePowerUpX = 0;
            purplePowerUpY = -50;
            purplePowerUpYSpeed = 0;

            yellowPowerUpX = 0;
            yellowPowerUpY = -50;
            yellowPowerUpYSpeed = 0;
            greenPowerUpX = 0;
            greenPowerUpY = -50;
            greenPowerUpYSpeed = 0;

            powerUp1X = 270;
            powerUp1Y = -50;
            ball2XSpeed = 0;
            ball2YSpeed = 0;
            powerUp2X = 320;
            powerUp2Y = -50;
            ball3XSpeed = 0;
            ball3YSpeed = 0;
            powerUpColour = "none";

            platformHeight = 45;
            platformWidth = 100;
            platformSpeed = 15;
            ballX = 290;

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
            gameTimer.Enabled = true;
            titleLabel.Text = "";
            subTitleLabel.Text = "";
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

                case Keys.M:
                    if (gameState == "waiting")
                    {
                        gameState = "instructions";
                    }
                    break;

                case Keys.Space:
                    if (gameState == "waiting" || gameState == "win" || gameState == "scoreboard" || gameState == "lose" || gameState == "instructions")
                    {
                        GameInitialize();
                    }
                    break;

                case Keys.Escape:
                    if (gameState == "waiting" || gameState == "win" || gameState == "scorebord" || gameState == "lose" || gameState == "instructions")
                    {
                        Application.Exit();
                    }
                    break;

                case Keys.Enter:
                    if ( gameState == "win" || gameState == "lose")
                    {
                        titleLabel.Text = "";
                        gameState = "scoreboard";
                    }
                    break;

                case Keys.Back:
                    if (gameState == "scoreboard")
                    {
                        scoreList.Clear();
                        GameInitialize();
                    }
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
            //paint the screens

            if (gameState == "waiting")
            {
                titleLabel.Text = "BRICK BREAKER";
                subTitleLabel.Text = "Press SPACE BAR to Start, ESCAPE to Exit \n or M for Instructions";
                levelLabel.Text = "";
            }
            else if (gameState == "lose")
            {
                titleLabel.Text = "BRICK BREAKER";
                subTitleLabel.Text = "Press SPACE BAR to Start, ESC to Exit, or \n ENTER to view your last 5 scores";
                subTitleLabel.Text += "\n        Better luck next time!";
                levelLabel.Text = "";
                scoreLabel.Text = "";
                lifeLabel.Text = "";
            }
            else if (gameState == "win")
            {
                titleLabel.Text = "BRICK BREAKER";
                subTitleLabel.Text = "Press SPACE BAR to Start , ESC to Exit, or \n ENTER to view your last 5 scores";
                subTitleLabel.Text += "\n Congrats! ";
                levelLabel.Text = "";
                scoreLabel.Text = "";
                lifeLabel.Text = "";
            }
            else if (gameState == "instructions")
            {
                titleLabel.Text = "Power Up Guide";
                subTitleLabel.Text = " YOU ONLY LOSE A LIFE IF THE RED APPLE\n FALLS TO THE GROUND \n\nPurple flower = Extra balls \n Yellow tray = Size increase \n Green leaf = Speed increase\n\n Press SPACE to start game or ESC to exit";

                levelLabel.Text = "";
                scoreLabel.Text = "";
                lifeLabel.Text = "";
            }
            else if (gameState == "scoreboard")
            {
                titleLabel.Text = "Previous Scores";
                subTitleLabel.Text = "Press SPACE BAR to play again, ESC to exit or \n BACKSPACE to delete all previous scores\n and play again";
                scoreLabel.Text = "";
                lifeLabel.Text = "";

                if (scoreList.Count > 5)
                {
                    scoreList.RemoveAt(0);
                }

                for (int i = 0; i < scoreList.Count(); i++)
                {
                    subTitleLabel.Text += $"\n\n {scoreList[i]}";
                }
            }

            //draw screen objects
            else if (gameState == "running" || gameState == "level two")
            {
                for (int i = 0; i < brickYList.Count(); i++)
                {
                    
                    e.Graphics.DrawImage(brickImage, brickXList[i], brickYList[i], brickWidth, brickHeight);
                }

                e.Graphics.DrawImage(Properties.Resources.apple2, ballX, ballY, ballWidth, ballHeight);
                e.Graphics.DrawImage(Properties.Resources.smallpurple, purplePowerUpX, purplePowerUpY, ballHeight, ballWidth);
                e.Graphics.DrawImage(Properties.Resources.pinkapple, powerUp1X, powerUp1Y, ballWidth, ballHeight);
                e.Graphics.DrawImage(Properties.Resources.pinkapple, powerUp2X, powerUp2Y, ballWidth, ballHeight);
                e.Graphics.DrawImage(Properties.Resources.yellow4, yellowPowerUpX, yellowPowerUpY, ballHeight, ballWidth);
                e.Graphics.DrawImage(Properties.Resources.green2, greenPowerUpX, greenPowerUpY, ballHeight, ballWidth);

                //show the right level for the level label

                if (gameState == "running")
                {
                    levelLabel.Text = "First Level";
                }

                if (gameState == "level two")
                {
                    levelLabel.Text = "Last Level";
                }
                scoreLabel.Text = $"Score:{score}";
                lifeLabel.Text = $"Lives : {lives}";
                titleLabel.Text = "";
                subTitleLabel.Text = "";
            }

            //draw platform and change platform with powerups

            if (powerUpColour == "none" && gameState == "running")
            {
                platformWidth = 100;
                e.Graphics.DrawImage(platformImage, platformX, platformY, platformWidth, platformHeight);
            }

            if (powerUpColour == "none" && gameState == "level two")
            {
                platformWidth = 100;
                e.Graphics.DrawImage(platformImage, platformX, platformY, platformWidth, platformHeight);
            }

            if (powerUpColour == "yellow")
            {
                e.Graphics.DrawImage(Properties.Resources.yellow4, platformX, platformY, platformWidth, platformHeight);
            }

            if (powerUpColour == "green")
            {
                e.Graphics.DrawImage(Properties.Resources.green2, platformX, platformY, platformWidth, platformHeight);
            }
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
                lossPlayer.Play();
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

            if (powerUp1Y < 0 && powerUp1Y > -20)
            {
                powerUp1Y = 1;
                ball2YSpeed *= -1;
            }

            if (powerUp1Y > this.Height + ballHeight)
            {
                powerUp1Y = 0 - ballHeight * 2;
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

            //check ballthree if it collides with walls

            if (powerUp2Y < 0 && powerUp2Y > -20)
            {
                powerUp2Y = 1;
                ball3YSpeed *= -1;
            }

            if (powerUp2Y > this.Height + ballHeight)
            {
                powerUp2Y = 0 - ballHeight * 2;
                powerUp2X = 350;
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

            // check for collision with ball and paddle and make the ball jump off
            Rectangle platformRec = new Rectangle(platformX, platformY, platformWidth, platformHeight);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);
            Rectangle ball2Rec = new Rectangle(powerUp1X, powerUp1Y, ballWidth, ballHeight);
            Rectangle ball3Rec = new Rectangle(powerUp2X, powerUp2Y, ballWidth, ballHeight);

            int gen = randGen.Next(1, 3);

            if (ballRec.IntersectsWith(platformRec))
            {
                player.Play();
                redCounter++;
                ballYSpeed *= -1;

                if (gen == 1)
                {
                    ballXSpeed = randGen.Next(-6, -3);
                }
                else if (gen == 2)
                {
                    ballXSpeed = randGen.Next(3, 6);
                }

                //will get rid of yellow and green powerups after the ball hits paddle twice
                if (redCounter == 2)
                {
                    platformWidth = 60;
                    platformSpeed = 15;
                    redCounter = 0;
                    titleLabel.Text = "";
                    powerUpColour = "none";
                }
            }

            // check if ball 2 intersects with the paddle
            if (ball2Rec.IntersectsWith(platformRec))
            {
                player.Play();
                ball2YSpeed *= -1;

                if (gen == 1)
                {
                    ball2XSpeed = randGen.Next(-6, -3);
                }
                else if (gen == 2)
                {
                    ball2XSpeed = randGen.Next(3, 6);
                }

            }

            //check if ball 3 interescts with paddle
            if (ball3Rec.IntersectsWith(platformRec))
            {
                player.Play();
                ball3YSpeed *= -1;

                if (gen == 1)
                {
                    ball3XSpeed = randGen.Next(-6, -3);
                }
                else if (gen == 2)
                {
                    ball3XSpeed = randGen.Next(3, 6);
                }

            }

            //check for ball and brick collisions and change direction
            if (gameState == "running" || gameState == "level two")
            {
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
                        player.Play();

                        if (gen == 1)
                        {
                            ballXSpeed = randGen.Next(-6, -3);
                            break;
                        }
                        else if (gen == 2)
                        {
                            ballXSpeed = randGen.Next(3, 6);
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
                        player.Play();

                        if (gen == 1)
                        {
                            ballXSpeed = randGen.Next(-6, -3);
                            break;
                        }
                        else if (gen == 2)
                        {
                            ballXSpeed = randGen.Next(3, 6);
                        }
                        break;
                    }

                    if (ballLeftRec.IntersectsWith(brickRec))
                    {
                        player.Play();
                        brickXList.RemoveAt(i);
                        brickYList.RemoveAt(i);
                        score++;

                        if (gen == 1)
                        {
                            ballYSpeed = randGen.Next(-6, -3);
                        }
                        else if (gen == 2)
                        {
                            ballYSpeed = randGen.Next(3, 6);
                        }
                        ballXSpeed *= -1;
                        ballX = ballX + 1;
                        break;
                    }

                    if (ballRightRec.IntersectsWith(brickRec))
                    {
                        player.Play();
                        brickXList.RemoveAt(i);
                        brickYList.RemoveAt(i);
                        score++;
                        player.Play();

                        if (gen == 1)
                        {
                            ballY = randGen.Next(-6, -3);
                        }
                        else if (gen == 2)
                        {
                            ballY = randGen.Next(3, 6);
                        }
                        ballXSpeed *= -1;
                        ballX = ballX + ballWidth;
                        break;
                    }
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
                    player.Play();

                    if (gen == 1)
                    {
                        ball2XSpeed = randGen.Next(-6, -3);
                        break;
                    }
                    else if (gen == 2)
                    {
                        ball2XSpeed = randGen.Next(3, 6);
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
                    player.Play();

                    if (gen == 1)
                    {
                        ball2XSpeed = randGen.Next(-6, -3);
                        break;
                    }
                    else if (gen == 2)
                    {
                        ball2XSpeed = randGen.Next(3, 6);
                    }
                    break;
                }

                if (ball2LeftRec.IntersectsWith(brickRec))
                {
                    brickXList.RemoveAt(i);
                    brickYList.RemoveAt(i);
                    score++;
                    player.Play();

                    if (gen == 1)
                    {
                        ball2YSpeed = randGen.Next(-6, -3);
                    }
                    else if (gen == 2)
                    {
                        ball2YSpeed = randGen.Next(3, 6);
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
                    player.Play();

                    if (gen == 1)
                    {
                        ball2YSpeed = randGen.Next(-6, -3);
                    }
                    else if (gen == 2)
                    {
                        ball2YSpeed = randGen.Next(3, 6);
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
                    player.Play();

                    if (gen == 1)
                    {
                        ball3XSpeed = randGen.Next(-6, -3);
                        break;
                    }
                    else if (gen == 2)
                    {
                        ball3XSpeed = randGen.Next(3, 6);
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
                    player.Play();
                    if (gen == 1)
                    {
                        ball3XSpeed = randGen.Next(-6, -3);
                        break;
                    }
                    else if (gen == 2)
                    {
                        ball3XSpeed = randGen.Next(3, 6);
                    }
                    break;
                }

                if (ball3LeftRec.IntersectsWith(brickRec))
                {
                    brickXList.RemoveAt(i);
                    brickYList.RemoveAt(i);
                    score++;
                    player.Play();

                    if (gen == 1)
                    {
                        ball3YSpeed = randGen.Next(-6, -3);
                    }
                    else if (gen == 2)
                    {
                        ball3YSpeed = randGen.Next(3, 6);
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
                    player.Play();

                    if (gen == 1)
                    {
                        ball3YSpeed = randGen.Next(-6, -3);
                    }
                    else if (gen == 2)
                    {
                        ball3YSpeed = randGen.Next(3, 6);
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
                powerUpPlayer.Play();
                purplePowerUpY = 0 - ballHeight;
                purplePowerUpYSpeed = 0;
                ball2YSpeed = 5;
                ball3YSpeed = 5;
            }

            // check for yellow power up and platform collision
            Rectangle yellowRec = new Rectangle(yellowPowerUpX, yellowPowerUpY, ballWidth, ballHeight);

            if (yellowRec.IntersectsWith(platformRec))
            {
                powerUpPlayer.Play();
                yellowPowerUpY = 0 - ballHeight;

                yellowPowerUpYSpeed = 0;
                platformWidth = 250;
                redCounter = 0;
                powerUpColour = "yellow";
            }

            //check for green power up and platform collision
            Rectangle greenRec = new Rectangle(greenPowerUpX, greenPowerUpY, ballWidth, ballHeight);

            if (greenRec.IntersectsWith(platformRec))
            {
                platformWidth = 100;
                powerUpPlayer.Play();
                redCounter = 0;
                greenPowerUpY = 0 - ballHeight;
                greenPowerUpYSpeed = 0;
                platformSpeed = 30;
                powerUpColour = "green";
            }

            // check when to when to drop powerups 
            counter++;

            if (counter == 50)
            {
                purplePowerUpX = randGen.Next(100, 550);
                purplePowerUpYSpeed = 5;
            }
            else if (counter == 250)
            {
                yellowPowerUpX = randGen.Next(100, 550);
                yellowPowerUpYSpeed = 5;
            }
            else if (counter == 450)
            {
                greenPowerUpX = randGen.Next(100, 550);
                greenPowerUpYSpeed = 5;
            }

            if (counter == 1000)
            {
                counter = 0;
            }
            //check if you lose due to no lives being left

            if (lives == 0)
            {
                platformWidth = 0;
                platformHeight = 0;
                loserPlayer.Play();
                lossPlayer.Play();
                gameTimer.Enabled = false;
                gameState = "lose";
                scoreList.Add(score);
            }
            //check if player beat level one and should start level two and give them a bonus point

            if (score == 16)
            {
                lives = 3;
                
                gameState = "level two";
                brickX = 100;
                brickY = 50;

                for (int t = 0; t < 5; t++)
                {
                    brickYList.Add(brickY);
                    brickXList.Add(brickX);
                    brickX += 100;
                }
                brickY += 100;
                brickX -= 550;

                for (int l = 0; l < 5; l++)
                {
                    brickYList.Add(brickY);
                    brickXList.Add(brickX);
                    brickX += 100;
                }
                brickY += 100;
                brickX -= 450;

                for (int l = 0; l < 5; l++)
                {
                    brickYList.Add(brickY);
                    brickXList.Add(brickX);
                    brickX += 100;
                }

                score++;
                //check if player won the whole game
            }

            if (score == 32)
            {
                platformWidth = 0;
                platformHeight = 0;
                winPlayer.Play();
                gameTimer.Enabled = false;
                gameState = "win";
                scoreList.Add(score);
            }
            Refresh();
        }
    }
}
