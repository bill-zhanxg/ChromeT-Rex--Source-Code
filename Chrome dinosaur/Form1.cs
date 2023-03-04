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
using Chrome_dinosaur.Properties;

namespace Chrome_dinosaur
{
    public partial class Form1 : Form
    {
        bool isjumping = false;
        int jumpspeed;
        double force = 12;
        int startscore = 0;
        int hurtspeed = 5;
        Random rand = new Random();
        int position;
        bool isgameover = false;
        bool ispaused = false;
        bool isdown = false;
        bool countscore = false;
        bool isstarted = false;
        bool isgoingdown;
        bool soundplayed = false;
        int highestscoretext;
        SoundPlayer levelup = new SoundPlayer(@".\Harder.wav");
        SoundPlayer jumpsound = new SoundPlayer(@".\Jump.wav");

        public Form1()
        {
            InitializeComponent();
            label1.Hide();
            highestscoretext = Convert.ToInt32(Settings.Default["Highscoresave"]);
            highscore.Text = "Highest Score: " + highestscoretext.ToString();
        }

        private void end()
        {
            timer1.Stop();
            Player.Image = Properties.Resources.dinosaur;
            countscore = false;
            isgameover = true;
            isstarted = false;
            label1.Hide();
            pictureBox1.Show();
            label5.Show();
            label7.Show();
            label4.Show();
            SoundPlayer deadsound = new SoundPlayer();
            deadsound.SoundLocation = @".\Dead.wav";
            deadsound.Play();
            if (startscore > highestscoretext)
            {
                highestscoretext = startscore;
                Settings.Default["Highscoresave"] = highestscoretext;
                Settings.Default.Save();
                highscore.Text = "Highest Score: " + highestscoretext;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            start();
            label2.Hide();
            label1.Show();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            Player.Top += jumpspeed;
            score.Text = "Score: " + startscore;
            if (force > 0 && force < 1)
            {
                isgoingdown = true;
            }
            if (isjumping == true && force > 1)
            {
                isgoingdown = true;
            }
            if (force == 12)
            {
                isgoingdown = false;
            }
            if (isjumping == true && force < 0)
            {
                isjumping = false;
            }
            if (isjumping == true)
            {
                jumpspeed = -10;
                force -= 0.653;
            }
            else
            {
                jumpspeed = 12;
            }

            if (Player.Top > 236 && isjumping == false)
            {
                force = 12;
                Player.Top = 237;
                jumpspeed = 0;
            }

            if (isdown == true)
            {
                Player.Size = new Size(76, 30);
                Player.Location = new Point(39, 300);
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "hurt")
                {
                    x.Left -= hurtspeed;
                    if (x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + rand.Next(10, 50) + (x.Width * 15);
                    }

                    if (Player.Bounds.IntersectsWith(x.Bounds))
                    {
                        end();
                    }
                }
            }

            foreach (Control fire in this.Controls)
            {
                if (fire is PictureBox && fire == pictureBox4)
                {
                    fire.Left -= hurtspeed;
                    if (fire.Left < -100)
                    {
                        fire.Left = this.ClientSize.Width + rand.Next(1000, 1500) + (fire.Width * 15);
                        fire.Top = rand.Next(200, 271);
                    }

                    if (Player.Bounds.IntersectsWith(fire.Bounds))
                    {
                        end();
                    }
                }
            }
            levelup.Load();

            //levels
            if (startscore == 20 && soundplayed == false)
            {
                jumpsound.Stop();
                levelup.Play();
                soundplayed = true;
                hurtspeed = 6;
            }
            if (startscore == 21)
            {
                soundplayed = false;
            }

            if (startscore == 50 && soundplayed == false)
            {
                jumpsound.Stop();
                levelup.Play();
                soundplayed = true;
                hurtspeed = 7;
            }
            if (startscore == 51)
            {
                soundplayed = false;
            }

            if (startscore == 100 && soundplayed == false)
            {
                jumpsound.Stop();
                levelup.Play();
                soundplayed = true;
                hurtspeed = 8;
            }
            if (startscore == 101)
            {
                soundplayed = false;
            }

            if (startscore == 500 && soundplayed == false)
            {
                jumpsound.Stop();
                levelup.Play();
                soundplayed = true;
                hurtspeed = 9;
            }
            if (startscore == 501)
            {
                soundplayed = false;
            }

            if (startscore == 1000 && soundplayed == false)
            {
                jumpsound.Stop();
                levelup.Play();
                soundplayed = true;
                hurtspeed = 10;
            }

            if (startscore >= 100000000)
            {
                timer1.Stop();
                Player.Image = Properties.Resources.dinosaur;
                isgameover = true;
                isstarted = false;
                countscore = false;
                label1.Hide();
                label3.Show();
                label7.Show();
                label4.Show();
                pictureBox1.Show();
                SoundPlayer jumpsound = new SoundPlayer();
                jumpsound.SoundLocation = @".\Winning.wav";
                jumpsound.Play();
            }

            //background picture
            var y = hill;
            y.Left -= hurtspeed;
            if (y.Left < -820)
            {
                y.Left = this.ClientSize.Width;
            }

            var c = clone;
            c.Left -= hurtspeed;
            if (c.Left < -820)
            {
                c.Left = this.ClientSize.Width;
            }

            //Handle picturebox generate too close
            Double x1 = pictureBox3.Location.X;
            Double x2 = pictureBox2.Location.X;
            Double y1 = pictureBox3.Location.Y;
            Double y2 = pictureBox2.Location.Y;
            Double distance = Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));

            if (distance < 150)
            {
                pictureBox3.Left = this.ClientSize.Width + rand.Next(1, 10);
            }

            Double f1 = pictureBox4.Location.X;
            Double f2 = pictureBox4.Location.Y;
            Double distance2 = Math.Sqrt((Math.Pow(x1 - f1, 2) + Math.Pow(y1 - f2, 2)));
            Double distance3 = Math.Sqrt((Math.Pow(x2 - f1, 2) + Math.Pow(y2 - f2, 2)));
            if (distance2 < 300)
            {
                pictureBox4.Left = this.ClientSize.Width + rand.Next(1, 5);
            }
            if (distance3 < 300)
            {
                pictureBox4.Left = this.ClientSize.Width + rand.Next(1, 5);
            }
        }

        private async void conuntscore()
        {
            while (startscore <= 100000000 && countscore == true)
            {
                await Task.Delay(500);
                startscore++;
            }
        }


        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && Player.Top >= 237 && isstarted == true)
            {
                jumpsound.Play();
            }

            if (e.KeyCode == Keys.Up && Player.Top >= 237 && isstarted == true)
            {
                jumpsound.Play();
            }

            if (e.KeyCode == Keys.Space && isjumping == false && isstarted == true)
            {

                isjumping = true;
            }

            if (e.KeyCode == Keys.Up && isjumping == false && isstarted == true)
            {
                isjumping = true;
            }

            if (e.KeyCode == Keys.Down && isgoingdown == true && isstarted == true)
            {
                force -= 20;
            }

            if (e.KeyCode == Keys.Down && isgoingdown == false && isstarted == true)
            {
                isdown = true;
            }

            if (e.KeyCode == Keys.P && ispaused == false && isstarted == true)
            {
                Player.Image = Properties.Resources.dinosaur;
                timer1.Enabled = false;
                label1.Text = "Press R to resume";
                await Task.Delay(10);
                ispaused = true;
            }

            if (e.KeyCode == Keys.R && ispaused == true && isstarted == true)
            {
                timer1.Enabled = true;
                Player.Image = Properties.Resources.running;
                label1.Text = "Press P to pause";
                await Task.Delay(10);
                ispaused = false;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && isstarted == true)
            {
                isdown = false;
                Player.Top = 237;
                Player.Height = 93;
            }
        }

        private void start()
        {
            force = 12;
            jumpspeed = 0;
            isjumping = false;
            isdown = false;
            startscore = 0;
            hurtspeed = 5;
            score.Text = "Score: " + startscore;
            Player.Image = Properties.Resources.running;
            isgameover = false;
            Player.Top = 237;
            Player.Height = 93;
            ispaused = false;
            isstarted = true;
            soundplayed = false;
            label1.Show();
            pictureBox1.Hide();
            label5.Hide();
            label7.Hide();
            label4.Hide();
            label3.Hide();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string) x.Tag == "hurt")
                {
                    position = this.ClientSize.Width + rand.Next(10, 50) + x.Width * 10;

                    x.Left = position;
                }
            }

            foreach (Control fireball in this.Controls)
            {
                if (fireball is PictureBox && fireball == pictureBox4)
                {
                    position = this.ClientSize.Width + rand.Next(50, 100) + fireball.Width * 10;

                    fireball.Left = position;
                }
            }

            //background
            hill.Left = -124;
            clone.Left = 670;

            timer1.Start();
            countscore = true;
            conuntscore();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            start();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Settings.Default["Highscoresave"] = 0;
            Settings.Default.Save();
            highestscoretext = Convert.ToInt32(Settings.Default["Highscoresave"]);
            highscore.Text = "Highest Score: " + highestscoretext;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            highestscoretext = Convert.ToInt32(Settings.Default["Highscoresave"]);
            if (startscore > highestscoretext)
            {
                Settings.Default["Highscoresave"] = startscore;
                Settings.Default.Save();
            }
        }
    }
}
