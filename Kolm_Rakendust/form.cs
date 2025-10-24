using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kolm_Rakendust
{
    public partial class form : Form
    {
        Button btn1, btn2, btn3, btn4;
        Label lbl1, lbl2, lbl3, lbl4;

        public form()
        {
            //InitializeComponent();
            this.Text = "Menu";
            this.Width = 400;
            this.Height = 600;

            //Pealkiri
            lbl1 = new Label();
            lbl1.Text = "Vali rakendus";
            lbl1.Location = new Point(100, 20);
            lbl1.Size = new Size(210, 50);
            lbl1.Font = new Font("Arial", 20);
            this.Controls.Add(lbl1);

            //Pealkiri 1. rakendusele
            lbl2 = new Label();
            lbl2.Text = "1. Pildi vaatamise programm";
            lbl2.ForeColor = Color.Black;
            lbl2.Location = new Point(50, 90);
            lbl2.Size = new Size(400, 40);
            lbl2.Font = new Font("Arial", 15);
            this.Controls.Add(lbl2);

            //Nupp 1. rakendusele
            btn1 = new Button();
            btn1.Text = "Ava";
            btn1.Location = new Point(100, 130);
            btn1.Size = new Size(200, 40);
            btn1.Click += Btn1_Click;
            btn1.BackColor = Color.DarkBlue;
            btn1.ForeColor = Color.White;
            this.Controls.Add(btn1);

            //Pealkiri 2. rakendusele
            lbl3 = new Label();
            lbl3.Text = "2. Matemaatiline äraarvamismäng";
            lbl3.ForeColor = Color.Black;
            lbl3.Location = new Point(50, 180);
            lbl3.Size = new Size(400, 40);
            lbl3.Font = new Font("Arial", 15);
            this.Controls.Add(lbl3);

            //Nupp 2. rakendusele
            btn2 = new Button();
            btn2.Text = "Ava";
            btn2.Location = new Point(100, 220);
            btn2.Size = new Size(200, 40);
            btn2.Click += Btn2_Click;
            btn2.BackColor = Color.DarkBlue;
            btn2.ForeColor = Color.White;
            this.Controls.Add(btn2);

            //Pealkiri 3. rakendusele
            lbl4 = new Label();
            lbl4.Text = "3. Sarnaste piltide leidmise mäng";
            lbl4.ForeColor = Color.Black;
            lbl4.Location = new Point(50, 270);
            lbl4.Size = new Size(400, 40);
            lbl4.Font = new Font("Arial", 15);
            this.Controls.Add(lbl4);

            //Nupp 3. rakendusele
            btn3 = new Button();
            btn3.Text = "Ava";
            btn3.Location = new Point(100, 310);
            btn3.Size = new Size(200, 40);
            btn3.Click += Btn3_Click;
            btn3.BackColor = Color.DarkBlue;
            btn3.ForeColor = Color.White;
            this.Controls.Add(btn3);

            //Väljumise nupp
            btn4 = new Button();
            btn4.Text = "Väljapääs";
            btn4.Location = new Point(50, 400);
            btn4.Size = new Size(300, 50);
            btn4.Click += Btn4_Click;
            this.Controls.Add(btn4);
        }
        private void Btn1_Click(object sender, EventArgs e)
        {
            picture_viewer picView = new picture_viewer();
            picView.Show();
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            math_quiz mathGame = new math_quiz();
            mathGame.Show();
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            game Game = new game();
            Game.Show();
        }

        private void Btn4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void form_Load(object sender, EventArgs e)
        {

        }
    }
}
