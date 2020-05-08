using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace ZombeeShooter
{
    public partial class Form1 : Form
    {
        bool goup;
        bool godown;
        bool goleft;
        bool goright;
        string facing = "up";
        double playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int score = 0;
        int ZombeeSpd = 1;
        bool GameOver = false;
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        void DropAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo_Image;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = rnd.Next(10, 890);
            ammo.Top = rnd.Next(50, 600);
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);
            ammo.BringToFront();
            Player.BringToFront();
        }

        void Shoot(string direct)
        {
            bullet shoot = new bullet();
            shoot.direction = direct;
            shoot.bulletLeft = Player.Left + (Player.Width / 2);
            shoot.bulletTop = Player.Top + (Player.Height / 2);
            shoot.mkBullet(this);
        }

        void MakeZombee()
        {
            PictureBox zombie = new PictureBox();
            zombie.Tag = "Zombee";
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = rnd.Next(0, 900);
            zombie.Top = rnd.Next(0, 800);
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Controls.Add(zombie);
            Player.BringToFront();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (GameOver) return;
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
                facing = "left";
                Player.Image = Properties.Resources.left;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = true;
                facing = "right";
                Player.Image = Properties.Resources.right;
            }

            if (e.KeyCode == Keys.Up)
            {
                goup = true;
                facing = "up";
                Player.Image = Properties.Resources.up;
            }

            if (e.KeyCode == Keys.Down)
            {
                godown = true;
                facing = "down";
                Player.Image = Properties.Resources.down;
            }
            if (e.KeyCode == Keys.Space && ammo > 0)
            {
                ammo--;
                Shoot(facing);
                if (ammo < 1)
                    DropAmmo();
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (GameOver) return;
            if (e.KeyCode == Keys.Left)
                goleft = false;
            if (e.KeyCode == Keys.Right)
                goright = false;
            if (e.KeyCode == Keys.Up)
                goup = false;
            if (e.KeyCode == Keys.Down)
                godown = false;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

        }
        private void GameEngine(object sender, EventArgs e)
        {
            if (playerHealth > 1)
                progressBar1.Value = Convert.ToInt32(playerHealth);
            else
            {
                Player.Image = Properties.Resources.dead;
                timer1.Stop();
                GameOver = true;
                Form2 fr = new Form2();
                fr.Show();
                this.Close();
                MessageBox.Show("Ai murit","ok");
            }
            label1.Text = "Ammo: " + ammo;
            label2.Text = "Kills: " + score;
            if (goleft && Player.Left > 1)
                Player.Left -= speed;
            if (goright && Player.Left + Player.Width < 1280)
                Player.Left += speed;
            if (goup && Player.Top > 1)
                Player.Top -= speed;
            if (godown && Player.Bottom + Player.Height < 800)
                Player.Top += speed;
            foreach (Control x in this.Controls)
            {
                if(x is Label && x.Tag == "Level")
                    if(score >=20)
                    {
                        x.Text = "Level:2";
                        ZombeeSpd = 3;
                    }

                if (x is Label && x.Tag == "Level")
                    if (score >= 40)
                    {
                        x.Text = "Level:3";
                        ZombeeSpd = 5;
                    }

                if (x is PictureBox && x.Tag == "ammo")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(Player.Bounds))
                    {
                        this.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                        ammo += 5;
                    }
                }

                if (x is PictureBox && x.Tag == "bullet")
                {
                    if (((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 1280 || ((PictureBox)x).Top < 1 || ((PictureBox)x).Top > 720)
                    {
                        this.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                    }
                }
                if (x is PictureBox && x.Tag == "Zombee")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(Player.Bounds))
                    {
                        playerHealth -= 1;
                    }
                    if (((PictureBox)x).Left > Player.Left)
                    {
                        ((PictureBox)x).Left -= ZombeeSpd;
                        ((PictureBox)x).Image = Properties.Resources.zleft;
                    }
                    if (((PictureBox)x).Top > Player.Top)
                    {
                        ((PictureBox)x).Top -= ZombeeSpd;
                        ((PictureBox)x).Image = Properties.Resources.zup;
                    }
                    if (((PictureBox)x).Left < Player.Left)
                    {
                        ((PictureBox)x).Left += ZombeeSpd;
                        ((PictureBox)x).Image = Properties.Resources.zright;
                    }
                    if (((PictureBox)x).Top < Player.Top)
                    {
                        ((PictureBox)x).Top += ZombeeSpd;
                        ((PictureBox)x).Image = Properties.Resources.zdown;
                    }
                    foreach (Control j in this.Controls)
                    {                        
                        if ((j is PictureBox && j.Tag == "bullet") && (x is PictureBox && x.Tag == "Zombee"))
                        {
                            if (x.Bounds.IntersectsWith(j.Bounds))
                            {
                                score++;
                                this.Controls.Remove((PictureBox)j);
                                ((PictureBox)j).Dispose();
                                this.Controls.Remove((PictureBox)x);
                                ((PictureBox)x).Dispose();
                                MakeZombee();
                            }
                        }
                    }

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ProgressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
