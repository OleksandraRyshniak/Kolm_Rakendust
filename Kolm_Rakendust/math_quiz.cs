using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Kolm_Rakendust
{
    public partial class math_quiz : Form
    {
        int p1;
        int p2;
        int m1;
        int m2;
        int u1;
        int u2;
        int d1;
        int d2;
        int t;

        Label lbl, lblt, slbl, slbl1, rlbl, rlbl1, ulbl,
            ulbl1, dlbl, dlbl1, s, r, u, d, v1, v2, v3, v4;
        Button start_btn, end_btn, btn;
        Label time;
        Timer timer1;
        NumericUpDown nud1, nud2, nud3, nud4;
        RadioButton cb1, cb2, cb3;

        Random randomizer = new Random();

        int score = 0;

        public math_quiz()
        {
            InitializeComponent();

            this.Text = "Matemaatiline äraarvamismäng";
            this.Width = 450;
            this.Height = 300;

            lbl = CreateLabel("Matemaatiline äraarvamismäng", new Point(60, 20), new Size(500, 30), new Font("Arial", 16));

            cb1 = new RadioButton();
            cb1.Text = "Lihtne tase";
            cb1.Location = new Point(10, 60);
            cb1.Font = new Font("Arial", 11);
            this.Controls.Add(cb1);

            cb2 = new RadioButton();
            cb2.Text = "Keskmine tase";
            cb2.Location = new Point(130, 60);
            cb2.Font = new Font("Arial", 11);
            cb2.Size = new Size(130, 30);
            this.Controls.Add(cb2);

            cb3 = new RadioButton();
            cb3.Text = "Raske tase";
            cb3.Location = new Point(270, 60);
            cb3.Font = new Font("Arial", 11);
            this.Controls.Add(cb3);


            start_btn = new Button();
            start_btn.Text = "Alusta testi";
            start_btn.Location = new Point(10, 120);
            start_btn.Size = new Size(200, 70);
            start_btn.Font = new Font("Arial", 13);
            start_btn.BackColor = Color.RoyalBlue;
            start_btn.ForeColor = Color.White;
            start_btn.Click += start_btn_Click;
            this.Controls.Add(start_btn);

            btn = new Button();
            btn.Text = "Sule";
            btn.Location = new Point(220, 120);
            btn.Size = new Size(200, 70);
            btn.BackColor = Color.RoyalBlue;   
            btn.ForeColor = Color.White;
            btn.Font = new Font("Arial", 13);
            btn.Click += btn_close;
            this.Controls.Add(btn);


            end_btn = new Button();
            end_btn.Text = "Lõpeta test";
            end_btn.Location = new Point(150, 350);
            end_btn.Size = new Size(150, 50);
            end_btn.Font = new Font("Arial", 13);
            end_btn.BackColor = Color.RoyalBlue;
            end_btn.ForeColor = Color.White;
            end_btn.Click += end_btn_Click;

            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
        }

        private void btn_close(object sender, EventArgs e)
        {
            this.Close();
        }
        private void end_btn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            ScoreAnswers();
            ColorAnswers();
            if (CheckTheAnswer())
            {
                MessageBox.Show("Palju õnne! Sa vastasid kõigile küsimustele õigesti. Test on lõpetatud.\n Sa said: " + score + " punktid");
            }
            else
            {
                MessageBox.Show("Vabandust, mõned vastused on valed. Proovi uuesti! Test on lõpetatud.\n Sa said: " + score + " punktid");
            }
            this.Width = 450;
            this.Height = 300;
            nud1.Value = 0;
            nud2.Value = 0;
            nud3.Value = 0;
            nud4.Value = 0;
            score = 0;
            time.Text = "30 seconds";

            lblt.Visible = false;
            time.Visible = false;
            slbl.Visible = false;
            slbl1.Visible = false;
            s.Visible = false;
            v1.Visible = false;
            nud1.Visible = false;
            rlbl.Visible = false;
            rlbl1.Visible = false;
            r.Visible = false;
            v2.Visible = false;
            nud2.Visible = false;
            ulbl.Visible = false;
            ulbl1.Visible = false;
            u.Visible = false;
            v3.Visible = false;
            nud3.Visible = false;
            dlbl.Visible = false;
            dlbl1.Visible = false;
            d.Visible = false;
            v4.Visible = false;
            nud4.Visible = false;
            end_btn.Visible = false;

            cb1.Visible = true;
            cb1.Checked = false;
            cb2.Checked = false;
            cb2.Visible = true;
            cb3.Checked = false;
            cb3.Visible = true;
            start_btn.Visible = true;
            start_btn.Enabled = true;
            btn.Visible = true;
        }
        private void start_btn_Click(object sender, EventArgs e)
        {   
            this.Width = 500;
            this.Height = 500;
            lbl.Visible = true;
            end_btn.Visible = true;
            start_btn.Visible = false;
            cb1.Visible = false;
            cb2.Visible = false;
            cb3.Visible = false;    
            btn.Visible = false;

            if (cb1.Checked)
            {
                p1 = randomizer.Next(1, 11);
                p2 = randomizer.Next(1, 11);
                m1 = randomizer.Next(1, 11);
                m2 = randomizer.Next(1, m1);
                u1 = randomizer.Next(1, 11);
                u2 = randomizer.Next(1, 11);
                d2 = randomizer.Next(1, 11);
                d1 = d2 * randomizer.Next(1, 5);

            }
            else if (cb2.Checked)
            {
                p1 = randomizer.Next(1, 21);
                p2 = randomizer.Next(1, 21);
                m1 = randomizer.Next(1, 21);
                m2 = randomizer.Next(1, m1);
                u1 = randomizer.Next(1, 21);
                u2 = randomizer.Next(1, 21);
                d2 = randomizer.Next(1, 21);
                d1 = d2 * randomizer.Next(1, 11);
            }
            else if (cb3.Checked)
            {

                p1 = randomizer.Next(1, 51);
                p2 = randomizer.Next(1, 51);
                m1 = randomizer.Next(1, 51);
                m2 = randomizer.Next(1, m1);
                u1 = randomizer.Next(1, 31);
                u2 = randomizer.Next(1, 31);
                d2 = randomizer.Next(1, 51);
                d1 = d2 * randomizer.Next(1, 31);
            }
            else
            {
                MessageBox.Show("Palun vali raskusaste enne testi alustamist.");
                return;
            }
            lbl.Location = new Point(10, 20);  
            lblt = CreateLabel("Jäänud aeg", new Point(10, 60), new Size(150, 30), new Font("Arial", 15));
            t = 30;
            time = CreateLabel("30 seconds", new Point(170, 60), new Size(150, 30), new Font("Arial", 15));
            time.Text = "30 seconds";
            timer1.Start(); 

            // Esimene
            slbl = CreateLabel(p1.ToString(), new Point(50, 110), new Size(50, 50), new Font("Arial", 15));
            s = CreateLabel("+", new Point(110, 110), new Size(50, 50), new Font("Arial", 15));
            slbl1 = CreateLabel(p2.ToString(), new Point(170, 110), new Size(50, 50), new Font("Arial", 15));
            v1 = CreateLabel("=", new Point(230, 110), new Size(50, 50), new Font("Arial", 15));
            nud1 = CreateNumericUpDown(new Point(290, 110), new Size(100, 50), new Font("Arial", 15));
            nud1.Minimum = 0;
            nud1.Maximum = 100000;

            //Teine

            rlbl = CreateLabel(m1.ToString(), new Point(50, 170), new Size(50, 50), new Font("Arial", 15));
            r = CreateLabel("-", new Point(110, 170), new Size(50, 50), new Font("Arial", 15));
            rlbl1 = CreateLabel(m2.ToString(), new Point(170, 170), new Size(50, 50), new Font("Arial", 15));
            v2 = CreateLabel("=", new Point(230, 170), new Size(50, 50), new Font("Arial", 15));
            nud2 = CreateNumericUpDown(new Point(290, 170), new Size(100, 50), new Font("Arial", 15));
            nud2.Minimum = -100000;
            nud2.Maximum = 100000;

            //Kolmas
            ulbl = CreateLabel(u1.ToString(), new Point(50, 230), new Size(50, 50), new Font("Arial", 15));
            u = CreateLabel("*", new Point(110, 230), new Size(50, 50), new Font("Arial", 15));
            ulbl1 = CreateLabel(u2.ToString(), new Point(170, 230), new Size(50, 50), new Font("Arial", 15));
            v3 = CreateLabel("=", new Point(230, 230), new Size(50, 50), new Font("Arial", 15));
            nud3 = CreateNumericUpDown(new Point(290, 230), new Size(100, 50), new Font("Arial", 15));
            nud3.Maximum = 100000; 
            nud3.Minimum = 0;

            //Neljas
            dlbl = CreateLabel(d1.ToString(), new Point(50, 290), new Size(50, 50), new Font("Arial", 15));
            d = CreateLabel("/", new Point(110, 290), new Size(50, 50), new Font("Arial", 15));
            dlbl1 = CreateLabel(d2.ToString(), new Point(170, 290), new Size(50, 50), new Font("Arial", 15));
            v4 = CreateLabel("=", new Point(230, 290), new Size(50, 50), new Font("Arial", 15));
            nud4 = CreateNumericUpDown(new Point(290, 290), new Size(100, 50), new Font("Arial", 15));
            nud4.Maximum = 100000;
            nud4.Minimum = 0;

            this.Controls.Add(end_btn);
        }
        private bool CheckTheAnswer()
        {
            return (nud1.Value == p1 + p2) &&
                   (nud2.Value == m1 - m2) &&
                   (nud3.Value == u1 * u2) &&
                   (nud4.Value == d1 / d2);
        }
        private void ColorAnswers()
        {
            NumericUpDown[] nuds = { nud1, nud2, nud3, nud4 };
            int[] correctAnswers =
            {
                p1 + p2,
                m1 - m2,
                u1 * u2,
                d1 / d2
            };

            for (int i = 0; i < nuds.Length; i++)
            {
                if (nuds[i].Value == correctAnswers[i])
                {
                    nuds[i].BackColor = Color.LightGreen;
                }
                else
                {
                    nuds[i].BackColor = Color.LightCoral;
                }
            }
        }
        private int ScoreAnswers()
        {
            if (nud1.Value == p1 + p2) score+=5;
            if (nud2.Value == m1 - m2) score+=5;
            if (nud3.Value == u1 * u2) score+=5;
            if (nud4.Value == d1 / d2) score+=5;
            return score;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (t > 0)
            {
                t--;
                time.Text = t + " seconds";
            }
            else
            {
                ScoreAnswers();
                ColorAnswers();
                timer1.Stop();
                time.Text = "Aeg on otsas!";
                MessageBox.Show("Sa ei jõudnud õigeks ajaks valmis. Vabandust! \n Sa said: " + score + " punktid");
                start_btn.Enabled = true;
                start_btn.Visible = true;
                end_btn.Visible = false;
                btn.Visible = true;
            }
            if (t <= 5)
            {
                time.ForeColor = Color.Red;
            }
            
        }
        private Label CreateLabel(string text, Point location, Size size, Font font)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = location;
            lbl.Size = size;
            lbl.Font = font;
            this.Controls.Add(lbl);
            return lbl;
        }
        private NumericUpDown CreateNumericUpDown(Point location, Size size, Font font)
        {
            NumericUpDown nud = new NumericUpDown();
            nud.Value = 0;
            nud.Location = location;
            nud.Size = size;
            nud.Font = font;
            this.Controls.Add(nud);
            return nud;
        }
        private void math_quiz_Load(object sender, EventArgs e)
        {

        }
    }
}


