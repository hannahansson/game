using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public partial class Game : Form
    {
        bool goLeft, goRight, jumping, isGameOver;
        int jumpSpeed;
        int force;
        int score = 0;
        int playerSpeed = 7;

        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemyOneSpeed = 3; //
        int enemyTwoSpeed = 3;

        public Game()
        {
            InitializeComponent();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;

            Player.Top += jumpSpeed;

            if (goLeft == true)
            {
                Player.Left -= playerSpeed;
            }

            if (goRight == true)
            {
                Player.Left += playerSpeed;
            }

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {


                    if ((string)x.Tag == "platform")
                    {
                        if (Player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            Player.Top = x.Top - Player.Height;


                            if ((string) x.Name == "horizontalPlatform" && goLeft == false ||
                                (string) x.Name == "horizontalPlatform" && goRight == false)
                            {
                                Player.Left -= horizontalSpeed;
                            }
                        }

                        x.BringToFront();

                    }
                    if ((string)x.Tag == "coin")
                    {
                        if (Player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }

                    if ((string) x.Tag == "enemy")
                    {
                        if (Player.Bounds.IntersectsWith(x.Bounds))
                        {
                            Console.Beep();
                            GameTimer.Stop();
                            isGameOver = true;
                            MessageBox.Show("Score: " + score + Environment.NewLine +
                                            "You were killed in your journey!!"
                                            + Environment.NewLine + "Click OK to play again!");
                            ResetGame();

                            //txtScore.Text = "Score: " + score + Environment.NewLine +
                            //     "You were killed in your journey!!";
                        }

                        horizontolplatform.Left -= horizontalSpeed;

                        if (horizontolplatform.Left < 0 ||
                            horizontolplatform.Left + horizontolplatform.Width > this.ClientSize.Width)
                        {
                            horizontalSpeed = -horizontalSpeed;
                        }

                        verticalplatform.Top += verticalSpeed;

                        if (verticalplatform.Top < 110 || verticalplatform.Top > 340)
                        {
                            verticalSpeed = -verticalSpeed;
                        }


                        enemyone.Left -= enemyOneSpeed;

                        if (enemyone.Left < pictureBox5.Left ||
                            enemyone.Left + enemyone.Width > pictureBox5.Left + pictureBox5.Width)
                        {
                            enemyOneSpeed = -enemyOneSpeed;
                        }

                        enemytwo.Left += enemyTwoSpeed;

                        if (enemytwo.Left < pictureBox2.Left ||
                            enemytwo.Left + enemytwo.Width > pictureBox2.Left + pictureBox2.Width)
                        {
                            enemyTwoSpeed = -enemyTwoSpeed;
                        }


                        if (Player.Top + Player.Height > this.ClientSize.Height + 20)
                        {
                            Console.Beep();
                            GameTimer.Stop();
                            isGameOver = true;
                            MessageBox.Show("Score: " + score + Environment.NewLine + "You died!" 
                                            + Environment.NewLine + "Click OK to play again!");
                            
                            ResetGame();
                            //txtScore.Text = "Score: " + score + Environment.NewLine + "You died!";
                        }

                        if (Player.Bounds.IntersectsWith(Door.Bounds) && score == 24) 
                        {
                            GameTimer.Stop();
                            isGameOver = true;
                            MessageBox.Show("Score: " + score + Environment.NewLine + "You won!" + Environment.NewLine +
                                            "Click OK to play again!");

                            ResetGame();
                            //txtScore.Text = "Score: " + score + Environment.NewLine + "You won!";
                        }
                        else
                        {
                            txtScore.Text = "Score: " + score + "\nCollect coins"; //all the 
                        }
                    }
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                ResetGame();
            }

        }

        private void ResetGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;

            txtScore.Text = "Score " + score;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            Player.Left = 49;
            Player.Top = 305; 

            enemyone.Left = 320;
            enemytwo.Left = 300;

            horizontolplatform.Left = 205;
            verticalplatform.Top = 290;

            GameTimer.Start();
            }
        }
    }