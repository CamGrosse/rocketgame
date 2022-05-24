using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace rocketgame
{
    public partial class CamTheMan : Form
    {
        bool wDown = false;
        bool sDown = false;
        SoundPlayer rightHook = new SoundPlayer(Properties.Resources.Right_Cross_SoundBible_com_1721311663);
        SoundPlayer upperCut = new SoundPlayer(Properties.Resources.Upper_Cut_SoundBible_com_1272257235);
        SoundPlayer rocket = new SoundPlayer(Properties.Resources.Chamber_Decompressing_SoundBible_com_1075404493);


        bool upArrowDown = false;
        bool downArrowDown = false;
        int playerSpeed = 5;
        int player1Score = 0;
        int player2Score = 0;
        int randValue = 0;
        Random randGen = new Random();
        int metor = 10;
        List<int> metorSpeeds = new List<int>();
        List<Rectangle> metors = new List<Rectangle>();

        Rectangle player1 = new Rectangle(200, 360, 20, 40);
        Rectangle player2 = new Rectangle(600, 360, 20, 40);
        Rectangle ground = new Rectangle(1, 400, 3000, 60);
        Rectangle top = new Rectangle(0, 0, 3000, 10);
        SolidBrush blueBrush = new SolidBrush(Color.White);
        SolidBrush groundColour = new SolidBrush(Color.DimGray);
        Rectangle border = new Rectangle(400, 0, 10, 500);
        Rectangle meator = new Rectangle(0, 0, 10, 10);
        public CamTheMan()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;


                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    break;
            }

        }


        private void Form1_KeyUp_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;



                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
            }

        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(groundColour, ground);
            e.Graphics.FillRectangle(groundColour, border);
            e.Graphics.FillRectangle(groundColour, top);
            e.Graphics.FillRectangle(blueBrush, meator);


            for (int i = 0; i < metors.Count(); i++)
            {

                e.Graphics.FillEllipse(blueBrush, metors[i]);
            }

        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            //move player1
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
                rocket.Play();
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }

            //move player 2
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
                rocket.Play();
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }

            //when player 1 reaches the top
            if (player1.IntersectsWith(top))
            {
                player1.Y = 340;
                player1.X = 200;
                player1Score++;
                player1ScoreLabel.Text = $"{player1Score}";
            }

            //when player 2 reaches the top
            if (player2.IntersectsWith(top))
            {
                player2.Y = 340;
                player2.X = 600;
                player2Score++;
                player2ScoreLabel.Text = $"{player2Score}";
            }

            //to not let player 1 go past the bottom
            if (player1.IntersectsWith(ground))
            {
                player1.Y = 340;
                player1.X = 200;
            }

            //to not let player 2 go past the bottom
            if (player2.IntersectsWith(ground))
            {
                player2.Y = 340;
                player2.X = 600;


            }

            if (player1Score == 3)
            {
                gameLoop.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!!";
            }
            else if (player2Score == 3)
            {
                gameLoop.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!!";

            }

            randValue = randGen.Next(0, 101);
            //meators
            if (randValue < 10)
            {
                
                int y = randGen.Next(10, this.Height - 150);
                metors.Add(new Rectangle(0, y, metor, metor));
                metorSpeeds.Add(randGen.Next(2, 10));

                y = randGen.Next(10, this.Height - 150);
                metors.Add(new Rectangle(1000, y, metor, metor));
                metorSpeeds.Add(randGen.Next(-10, -2));

            }

            for (int i = 0; i < metors.Count(); i++)
            {
                //find the new postion of y based on speed
                int x = metors[i].X + metorSpeeds[i];

                //replace the rectangle in the list with updated one using new y
                metors[i] = new Rectangle(x, metors[i].Y, metor, metor);
            }



            //to see if rockets crash
            for (int i = 0; i < metors.Count(); i++)
            {
                if (player1.IntersectsWith(metors[i]))
                {
                    player1.Y = 340;
                    player1.X = 200;
                    upperCut.Play();
                }
            }
            for (int i = 0; i < metors.Count(); i++)
            {
                if (player2.IntersectsWith(metors[i]))
                {
                    player2.Y = 340;
                    player2.X = 600;
                    rightHook.Play();
                }
            }
            
            //if (randValue < 10)
            //{



            //}

            //for (int i = 10; i < metors.Count(); i--)
            //{
            //    //find the new postion of y based on speed
            //    int x = metors[i].X - metorSpeeds[i];

            //    //replace the rectangle in the list with updated one using new y
            //    metors[i] = new Rectangle(x, metors[i].Y, metor, metor);
            //}




            Refresh();
        }

        private void CamTheMan_Load(object sender, EventArgs e)
        {

        }
    }
}

