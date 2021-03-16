using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        int pipeSpeed = 8;            
        int gravity = 8;
        int score = 0;
        int lives = 3;
        int totalScore = 0;
        private int tableOf = 5; //preset for the table of 5
        


    /*Random rndHeight = new Random(1,30);
    int lngthTop= rndHeight.Next()*/




    public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            label2.Text = lives.ToString();
            label2.Text = "lives" + lives;
            label1.Text = score.ToString();
            label1.Text = "score" + score;
            


            if (pipeBottom.Left < -30)
            {
                pipeBottom.Left= 500;
                score++;
            }
            if (pipeTop.Left < -30)
            {
                pipeTop.Left = 520;
                score++;

            }
            
            if (score == tableOf)
            {
                   pipeSpeed = pipeSpeed + 1;
                tableOf = tableOf + 5;
                
            }

            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) || flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) || flappyBird.Bounds.IntersectsWith(ground.Bounds))
            {
                endGame();
                panel1.Visible = true;
                button1.Enabled=true;
                
                label3.Visible = true;
                label3.Text = totalScore.ToString();
                

                
                if (lives== 0)
                {
                    lives = 0;

                    label1.Text = "game Over";

                }

            }
            
           

            
            
        }

        
        private void endGame()
        {
            gameTimer.Stop();
            label1.Text = "lost a life";
            totalScore = totalScore + score;
            lives--;
        }

        private void ground_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            
           

            
        }
        private void gameKeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -18;
            }
        }

        private void gameKeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 8;
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            gameTimer.Start();
            pipeBottom.Location = new Point(649, 373);
            pipeTop.Location = new Point(649, -3);
            panel1.Hide();
            button1.Enabled = false;
            flappyBird.Location = new Point(160, 260);
            score = 0;
        }
    }
}
