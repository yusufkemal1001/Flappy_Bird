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
        int gravity = 8;
        int score = 0;
        int lives = 3;
        int totalScore = 0;
        private int tableOf = 5; //preset for the table of 5
        int lastleaderboardscore;
        int i;


        Random rndHeight = new Random();



        



        public Form1()
        {
            /*gets last value in database */
            InitializeComponent();

            string server = "localhost";
            string database = "flappyLeader";
            string dbUsername = "root";
            string dbPassword = "";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +

                database + ";" + "UID=" + dbUsername + ";" + "PASSWORD=" + dbPassword + ";";

            MySqlConnection mysqlcon = new MySqlConnection(connectionString);

            i = 0;
            mysqlcon.Open();
            MySqlCommand cmd = mysqlcon.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select score from leaderboard order by Score  desc limit 1 offset 9";
            
            DataTable dtbl = new DataTable("dtbl");
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            i = Convert.ToInt32(dtbl.Rows.Count.ToString());

            lastleaderboardscore = (int)ds.Tables[0].Rows[0]["score"];

            
            
            mysqlcon.Close();
            
            
            
            KeyPreview = true;
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            /*what happens when the timer is open*/
            ground.Left -= pipeSpeed;
            flappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            label2.Text = lives.ToString();
            label2.Text = "Lives : " + lives;
            label1.Text = score.ToString();
            label1.Text = "Score : " + score;

            if (ground.Left<-50)/*if ground is offscreen, adds the ground again from the fuar left side*/
            {
                ground.Left = 0;
            }

            if (pipeBottom.Left < -30) /*if pipe is offscreen, then add a pipe at the far right side of screen*/
            {
                pipeBottom.Left= 677;
                
                
                score++;
            }
            if (pipeTop.Left < -30)/*if pipe is offscreen, then add a pipe at the far right side of screen*/
            {
                pipeTop.Left = 677;
                pipeTop.Height = rndHeight.Next(200, 450);/*gives a random height to the pipes with a gap of 155 pixels*/
                pipeBottom.Top = pipeTop.Bottom + 155;
               

            }
           
            
            if (score == tableOf)/*makes speed faster after every 5 pionts*/
            {
                   pipeSpeed = pipeSpeed + 1;
                tableOf = tableOf + 5;
                
            }
            /*collision detection*/
            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) || flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) || flappyBird.Bounds.IntersectsWith(ground.Bounds) || flappyBird.Top < 0 )
            {
                endGame();
                panel1.Visible = true;
                button1.Enabled=true;
                
                label3.Visible = true;
                label3.Text ="Total Score : " + totalScore.ToString();
                endLives();
                if (lives==3)
                {
                    label3.Visible = false;
                    
                }
                pipeSpeed = 8;

                
                

            }
            
           

            
            
        }

        /*the function that is runned after a collision*/
        private void endGame()
        {
            label1.BringToFront();
            label2.BringToFront();
            label3.BringToFront();
            label5.Show();
            gameTimer.Stop();
            lives--;
            label2.Text = "Lives : " + lives.ToString();
            label5.Text = "Lost a life";
            totalScore = totalScore + score;
            endLives();


        }
        

        private void endLives() /*function that gets runned after you have no lives left*/
        {
            if (lives == 0)
            {
                
                label5.Text = "Game Over";
                if (totalScore >lastleaderboardscore) /*this code runs if the totalscore is higher than the lowest value in the database. This code shows button2 which makes a connection to the database*/
                {
                    label5.Location = new Point(65, 50);
                    button2.Show();
                    textBox2.Show();
                    textBox2.Visible = true;
                    label6.Text = "Please enter your name";
                    button1.Hide();
                    label6.Show();
                    label5.Text = "You have a highscore!";


                }
                else /*if you dont get a highscore, then the game just ends and you start again*/
                {
                    button1.Width = 120;
                    button1.Text = "Play Again!";
                    label4.Show();
                    label4.Text = "Your total score is " + totalScore.ToString();
                    lives = 3;

                    pipeSpeed = 8;


                    label3.Visible = false;
                    label1.Hide();
                    label2.Hide();
                    totalScore = 0;
                }

                
                
                





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
            if (e.KeyCode == Keys.Space)/*this snippet of codes runs if you press the spacebar*/
            {
                e.SuppressKeyPress = true;
                gravity = -18;
                textBox2.Text =textBox2.Text+ " ";
                textBox2.Select(textBox2.Text.Length, 0);
                
            }
            if (e.KeyCode==Keys.Escape) /*this code runs if you press the escape button*/
            {
                dataGridView1.Hide();
                panel1.Location = new Point(-3, 0);
                panel1.Width = 788;
                panel1.Height = 629;
                panel1.BackgroundImage = Properties.Resources.Main;
                button1.Location = new Point(227, 525);
                button4.Location = new Point(446, 525);
                label5.Show();
                button1.Show();
                button4.Show();
                label1.Hide();
                label2.Hide();
                label3.Hide();
                label5.Hide();
            }
        }

        private void gameKeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)/* this code runs when you dont press the spacebar, or when you release the spacebar*/
            {
                e.SuppressKeyPress = true;
                gravity = 10;
                
            }
        }
        

        private void button1_Click(object sender, EventArgs e) /*button that starts the game again and arranges places of buttons, labels, textboxes etc. Resets the location of pipes and flappy*/
        {
            

            label1.SendToBack();
            label2.SendToBack();
            label3.SendToBack();
            panel1.Location = new Point(240, 128);
            panel1.BackgroundImage = null;
            panel1.BackColor = Color.LightBlue;
            panel1.Width = 310;
            panel1.Height = 367;
            button1.Show();
            button1.Text = "Go!";
            button1.Location = new Point(110, 250);
            button1.Width = 98;
            button1.Height = 44;
            label5.Location = new Point(110, 50);
            label4.Location = new Point(75, 100);
            button2.Location = new Point(110,250);
            label6.Location = new Point(65, 150);
            label1.Show();
            label2.Show();
            label6.Hide();
            


            endLives();
            if (lives == 0)
            {
                totalScore = 0;
            }

            textBox2.Hide();
            textBox2.Clear();

            gameTimer.Start();
            label4.Hide();
            
            pipeBottom.Location = new Point(677, 361);
            pipeTop.Location = new Point(677, -122);
            pipeTop.Height = rndHeight.Next(200, 450);/*makes the height of the first pipes random*/
            pipeBottom.Top = pipeTop.Bottom + 155;
            pipeTop.Visible = true;
            pipeBottom.Visible = true;


            panel1.Hide();
            button1.Enabled = false;
            flappyBird.Location = new Point(160, 260);
            score = 0;
            
            
        }

        private void Form1_Load(object sender, EventArgs e) /* makes connection to database to show leaderboard. the leaderboard is only shown if button4 is clicked*/
        {
            string server = "localhost";
            string database = "flappyLeader";
            string dbUsername = "root";
            string dbPassword = "";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +

                database + ";" + "UID=" + dbUsername + ";" + "PASSWORD=" + dbPassword + ";";
           MySqlConnection mysqlcon = new MySqlConnection(connectionString);

            i = 0;
            mysqlcon.Open();
            MySqlCommand cmd = mysqlcon.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select Name,Score from leaderboard order by Score  desc limit 10 ";
            cmd.ExecuteNonQuery();
            DataTable dtbl = new DataTable();
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            i = Convert.ToInt32(dtbl.Rows.Count.ToString());
            dataGridView1.DataSource = ds.Tables[0];
            
            DataGridViewColumn column1 = dataGridView1.Columns[0];
            column1.Width = 280;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            column2.Width = 280;
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            
        }

        private void button2_Click(object sender, EventArgs e)/* if the player gets a highscore, this button is showed. after the name is entered, the player clicks this button to add their highscore to the database*/
        { 
            
            label5.Show();
            button1.Show();
            button2.Hide();
            string Name = textBox2.Text;


            string server = "localhost";
            string database = "flappyLeader";
            string dbUsername = "root";
            string dbPassword = "";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +

                database + ";" + "UID=" + dbUsername + ";" + "PASSWORD=" + dbPassword + ";";

            MySqlConnection mysqlcon = new MySqlConnection(connectionString);

            
            mysqlcon.Open();




            using (mysqlcon)
            {
                using (MySqlCommand cmd3 = new MySqlCommand("delete from leaderboard Where ID not in (select * from (select ID from leaderboard order by score desc limit 9) as temp)", mysqlcon))
                {
                    cmd3.ExecuteNonQuery();


                }
                using (MySqlCommand cmd1 = new MySqlCommand("insert into leaderboard(Name,Score) values('" +Name+ "', " + totalScore + ")", mysqlcon))
                {
                    cmd1.ExecuteNonQuery();
                }
                
                
                
                



                mysqlcon.Close();


            }
            
            label5.Text = "You are now in the leaderboard!";
            textBox2.Hide();
            label4.Hide();
            label6.Hide();
            label5.Location = new Point(20,50);
            button1.Width = 120;
            totalScore = 0;
            label1.Show();
            label2.Show();
        }

        private void button4_Click(object sender, EventArgs e)/* shows leaderboard*/
        {
            dataGridView1.Show();
            button1.Hide();
            label5.Hide();
            button4.Hide();
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
