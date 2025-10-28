using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        Random randomizer = new Random();

        public math_quiz()
        {
            InitializeComponent();

            this.Text = "Matemaatiline äraarvamismäng";
            this.Width = 500;
            this.Height = 500;

            lbl = CreateLabel("Matemaatiline äraarvamismäng", new Point(10, 20), new Size(500, 30), new Font("Arial", 16));

            start_btn = new Button();
            start_btn.Text = "Alusta testi";
            start_btn.Location = new Point(150, 200);
            start_btn.Size = new Size(150, 50);
            start_btn.Font = new Font("Arial", 13);
            start_btn.Click += start_btn_Click;
            this.Controls.Add(start_btn);

            btn = new Button();
            btn.Text = "Tagasi menüüsse";
            btn.Location = new Point(150, 270);
            btn.Size = new Size(150, 50);
            btn.Font = new Font("Arial", 13);
            btn.Click += btn_close;
            this.Controls.Add(btn);


            end_btn = new Button();
            end_btn.Text = "Lõpeta test";
            end_btn.Location = new Point(150, 350);
            end_btn.Size = new Size(150, 50);
            end_btn.Font = new Font("Arial", 13);
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
            if (CheckTheAnswer())
            {
                MessageBox.Show("Palju õnne! Sa vastasid kõigile küsimustele õigesti. Test on lõpetatud.");
            }
            else
            {
                MessageBox.Show("Vabandust, mõned vastused on valed. Proovi uuesti! Test on lõpetatud.");
            }
            nud1.Value = 0;
            nud2.Value = 0;
            nud3.Value = 0;
            nud4.Value = 0;
            time.Text = "30 seconds";
            lbl.Visible = false;
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
            start_btn.Visible = true;
            end_btn.Visible = false;
            btn.Visible = true;
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            lbl.Visible = true;
            end_btn.Visible = true;
            start_btn.Visible = false;
            btn.Visible = false;
            lblt = CreateLabel("Jäänud aeg", new Point(10, 60), new Size(150, 30), new Font("Arial", 15));
            t = 30;
            time = CreateLabel("30 seconds", new Point(170, 60), new Size(150, 30), new Font("Arial", 15));
            time.Text = "30 seconds";
            timer1.Start(); 

            // Esimene
            p1 = randomizer.Next(1, 51);
            p2 = randomizer.Next(1, 51);
            slbl = CreateLabel(p1.ToString(), new Point(50, 110), new Size(50, 50), new Font("Arial", 15));
            s = CreateLabel("+", new Point(110, 110), new Size(50, 50), new Font("Arial", 15));
            slbl1 = CreateLabel(p2.ToString(), new Point(170, 110), new Size(50, 50), new Font("Arial", 15));
            v1 = CreateLabel("=", new Point(230, 110), new Size(50, 50), new Font("Arial", 15));
            nud1 = CreateNumericUpDown(new Point(290, 110), new Size(100, 50), new Font("Arial", 15));

            //Teine
            m1 = randomizer.Next(1, 51);
            m2 = randomizer.Next(1, m1);
            rlbl = CreateLabel(m1.ToString(), new Point(50, 170), new Size(50, 50), new Font("Arial", 15));
            r = CreateLabel("-", new Point(110, 170), new Size(50, 50), new Font("Arial", 15));
            rlbl1 = CreateLabel(p2.ToString(), new Point(170, 170), new Size(50, 50), new Font("Arial", 15));
            v2 = CreateLabel("=", new Point(230, 170), new Size(50, 50), new Font("Arial", 15));
            nud2 = CreateNumericUpDown(new Point(290, 170), new Size(100, 50), new Font("Arial", 15));
            nud2.Minimum = -100;

            //Kolmas
            u1 = randomizer.Next(2, 13);
            u2 = randomizer.Next(2, 13);
            ulbl = CreateLabel(u1.ToString(), new Point(50, 230), new Size(50, 50), new Font("Arial", 15));
            u = CreateLabel("*", new Point(110, 230), new Size(50, 50), new Font("Arial", 15));
            ulbl1 = CreateLabel(u2.ToString(), new Point(170, 230), new Size(50, 50), new Font("Arial", 15));
            v3 = CreateLabel("=", new Point(230, 230), new Size(50, 50), new Font("Arial", 15));
            nud3 = CreateNumericUpDown(new Point(290, 230), new Size(100, 50), new Font("Arial", 15));
            nud3.Maximum = 144; 

            //Neljas
            d2 = randomizer.Next(2, 13);
            d1 = d2 * randomizer.Next(2, 13);
            dlbl = CreateLabel(d1.ToString(), new Point(50, 290), new Size(50, 50), new Font("Arial", 15));
            d = CreateLabel("/", new Point(110, 290), new Size(50, 50), new Font("Arial", 15));
            dlbl1 = CreateLabel(d2.ToString(), new Point(170, 290), new Size(50, 50), new Font("Arial", 15));
            v4 = CreateLabel("=", new Point(230, 290), new Size(50, 50), new Font("Arial", 15));
            nud4 = CreateNumericUpDown(new Point(290, 290), new Size(100, 50), new Font("Arial", 15));

            this.Controls.Add(end_btn);
        }

        private bool CheckTheAnswer()
        {
            return (p1 + p2 == nud1.Value) &&
                   (m1 - m2 == nud2.Value) &&
                   (u1 * u2 == nud3.Value) &&
                   (d1 / d2 == nud4.Value);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer1.Stop();
                MessageBox.Show("Sa vastasid kõikidele küsimustele õigesti! Palju õnne!");
                start_btn.Enabled = true;
            }
            else if (t > 0)
            {
                t--;
                time.Text = t + " seconds";
            }
            else
            {
                timer1.Stop();
                time.Text = "Aeg on otsas!";
                MessageBox.Show("Sa ei jõudnud õigeks ajaks valmis.„, “Vabandust!");
                nud1.Value = p1 + p2;
                nud2.Value = m1 - m2;
                nud3.Value = u1 * u2;
                nud4.Value = d1 / d2;
                start_btn.Enabled = true;
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


