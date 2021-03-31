using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace FlappyBird
{
    public partial class Form1 : Form
    {
        
       

        int pipeSpeed = 8;
        int groundspeed;
        int gravity = 8;
        int score = 0;
        int lives = 3;
        int totalScore = 0;
        private int tableOf = 5; //preset for the table of 5
        int lastleaderboardscore;


        Random rndHeight = new Random();



        /*string server = "localhost";
        string database = "uitleen_systeem";
        string dbUsername = "root";
        string dbPassword = "";
        string connectionString = "SERVER=" + server + ";" + "DATABASE=" +

            database + ";" + "UID=" + dbUsername + ";" + "PASSWORD=" + dbPassword + ";";

        mysqlcon = new MySqlConnection(connectionString);

        i = 0;
            mysqlcon.Open();
            MySqlCommand cmd = mysqlcon.CreateCommand();
        cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from category";
            cmd.ExecuteNonQuery();
            DataTable dtbl = new DataTable();
        MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sda.Fill(ds);
            i = Convert.ToInt32(dtbl.Rows.Count.ToString());
            dataGridView1.DataSource = ds.Tables[0];
        
        */



    public Form1()
        {

            InitializeComponent();

            string server = "localhost";
            string database = "flappyLeader";
            string dbUsername = "root";
            string dbPassword = "";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +

                database + ";" + "UID=" + dbUsername + ";" + "PASSWORD=" + dbPassword + ";";

            MySqlConnection mysqlcon = new MySqlConnection(connectionString);


            mysqlcon.Open();
            MySqlCommand cmd = mysqlcon.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from leaderboard order by Score";
            cmd.ExecuteNonQuery();
            mysqlcon.Close();
            
            
            
            KeyPreview = true;
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            ground.Left -= pipeSpeed;
            flappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            label2.Text = lives.ToString();
            label2.Text = "lives" + lives;
            label1.Text = score.ToString();
            label1.Text = "score" + score;

            if (ground.Left<-50)
            {
                ground.Left = 0;
            }

            if (pipeBottom.Left < -30)
            {
                pipeBottom.Left= 677;
                
                
                score++;
            }
            if (pipeTop.Left < -30)
            {
                pipeTop.Left = 677;
                pipeTop.Height = rndHeight.Next(200, 450);
                pipeBottom.Top = pipeTop.Bottom + 175;
               

            }
           
            
            if (score == tableOf)
            {
                   pipeSpeed = pipeSpeed + 1;
                tableOf = tableOf + 5;
                
            }

            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) || flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) || flappyBird.Bounds.IntersectsWith(ground.Bounds) || flappyBird.Top < 0 )
            {
                endGame();
                panel1.Visible = true;
                button1.Enabled=true;
                
                label3.Visible = true;
                label3.Text = totalScore.ToString();
                endLives();
                if (lives==3)
                {
                    label3.Visible = false;
                    
                }
                pipeSpeed = 8;

                
                

            }
            
           

            
            
        }

        
        private void endGame()
        {
            gameTimer.Stop();
            lives--;
            label2.Text = lives.ToString();
            label1.Text = "Lost a life";
            totalScore = totalScore + score;
            endLives();


        }
        private void endLives()
        {
            if (lives == 0)
            {
                if (totalScore >  lastleaderboardscore)
                {
                    /* TextBox1.Show();*/

                }
                label4.Show();
                label4.Text = "your total score is" + totalScore.ToString();
                lives = 3;
                
                pipeSpeed = 8;
                label1.Text = "game Over";
                totalScore = 0;
                label3.Visible = false;

                /*if (totalScore > )
                {
                   
                }*/




            }
           
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
                e.SuppressKeyPress = true;
                gravity = -18;
            }
        }

        private void gameKeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.SuppressKeyPress = true;
                gravity = 8;
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            gameTimer.Start();
            label4.Hide();
            
            pipeBottom.Location = new Point(677, 361);
            pipeTop.Location = new Point(677, -122);
            pipeTop.Height = rndHeight.Next(200, 450);
            pipeBottom.Top = pipeTop.Bottom + 155;
            pipeTop.Visible = true;
            pipeBottom.Visible = true;


            panel1.Hide();
            button1.Enabled = false;
            flappyBird.Location = new Point(160, 260);
            score = 0;
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
