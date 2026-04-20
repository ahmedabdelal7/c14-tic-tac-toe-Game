using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_Game.Properties;

namespace Tic_Tac_Toe_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.FromArgb(255, 255, 255, 255);
            Pen whitePin = new Pen(white, 6);

            whitePin.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            whitePin.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(whitePin, 492, 104, 492, 500);
            e.Graphics.DrawLine(whitePin, 625, 104, 625, 500);

            e.Graphics.DrawLine(whitePin, 380, 230, 740, 230);
            e.Graphics.DrawLine(whitePin, 380, 360, 740, 360);


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

/*        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = $"X = {e.X}, Y = {e.Y}";
        }*/

        enum enPlayer
        {
            player1,
            player2
        }
        enPlayer PlayerTurn = enPlayer.player1;

        enum enWinner
        {
            Player1,
            Player2,
            Draw
        }
        struct stGameStatus
        {
            public short PlayCount;
            public bool GameOver;
            public enWinner Winner;
        }

        stGameStatus GameStatus = new stGameStatus();

        void EndGame()
        {
            lblWinner.Text = "Game Over";

            switch (GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player 1";
                    break;
                case enWinner.Player2:
                    lblWinner.Text = "Player 2";
                    break;
                default:
                    lblWinner.Text = "Draw";
                    break;
            }
            MessageBox.Show("Game Over");
        }
        bool CheckValues(Button btn1, Button btn2, Button btn3)
        {
            if(btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.Green;
                btn2.BackColor = Color.Green;
                btn3.BackColor = Color.Green;

                if(btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver  = true;
                    EndGame();
                    return true;
                }
                else
                {
                        GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;

                }

            }
            return false;
        }
        void CheckWinner(){

            //Check Raws
            //Check Raw1
            if (CheckValues(button1, button2, button3))
                return;
            //Check Raw2
            if (CheckValues(button4, button5, button6))
                return;
            //Check Raw3
            if(CheckValues(button7, button8, button9))
                return;

            //Check Columns
            //Check Column1
            if (CheckValues(button1, button4, button6))
                return;
            //Check Column2
            if (CheckValues(button2, button5, button8))
                return;
            //Check Column3
            if (CheckValues(button3, button6, button9))
                return;

            if (CheckValues(button1, button5, button9))
                return;
            if (CheckValues(button3, button5, button7))
                return;

            if(GameStatus.PlayCount == 9)
            {
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }
            

        }
        void ChangeImage(Button button)
        {
            if(button.Tag.ToString() == "?")
            {
                switch (PlayerTurn)
                {
                    case enPlayer.player1:
                        button.BackgroundImage = Resources.X;
                        PlayerTurn = enPlayer.player2;
                        GameStatus.PlayCount++;
                        lblTurn.Text = "Player 2";
                        button.Tag = "X";
                        CheckWinner();
                        break;
                    case enPlayer.player2:
                        button.BackgroundImage = Resources.O;
                        lblTurn.Text = "Player 1";
                        PlayerTurn = enPlayer.player1;
                        GameStatus.PlayCount++;
                        button.Tag = "Y";
                        CheckWinner();
                        break;
                }
            }
            else
            {

                MessageBox.Show("Make Another Choice!", "Wrong Choice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
        }
        void ReseteButton(Button button)
        {
            button.Tag = "?";
          
            button.BackgroundImage = Resources.Qustion;

            button.BackColor = Color.Transparent;

        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            GameStatus.GameOver = false;
            GameStatus.PlayCount=0;
            PlayerTurn = enPlayer.player1; 

            lblTurn.Text = "Player 1";
            lblWinner.Text = "In Progress";

            ReseteButton(button1);
            ReseteButton(button2);
            ReseteButton(button3);
            ReseteButton(button4);
            ReseteButton(button5);
            ReseteButton(button6);
            ReseteButton(button7);
            ReseteButton(button8);
            ReseteButton(button9);
        }
    }
}
