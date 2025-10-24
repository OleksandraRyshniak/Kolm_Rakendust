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
    public partial class picture_viewer : Form
    {
        PictureBox pb;
        CheckBox cb;
        Button btn1, btn2, btn3, btn4;
        Label lbl;
        OpenFileDialog ofd;
        ColorDialog cd;
        public picture_viewer()
        {
            //InitializeComponent();
            this.Text = "Pildi Vaatamise";
            this.Width = 500;
            this.Height = 500;

            ofd = new OpenFileDialog();
            ofd.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*";
            cd = new ColorDialog();

            lbl = new Label();
            lbl.Text = "Pildi vaatamise programm";
            lbl.Location = new Point(10, 20);
            lbl.Size = new Size(500, 50);
            lbl.Font = new Font("Arial", 16);
            this.Controls.Add(lbl);

            btn1 = new Button();
            btn1.Text = "Näita pilti";
            btn1.Location = new Point(10, 70);
            btn1.Size = new Size(80, 30);
            btn1.Click += showButton_Click;
            this.Controls.Add(btn1);

            btn2 = new Button();
            btn2.Text = "Puhasta pilt";
            btn2.Location = new Point(100, 70);
            btn2.Size = new Size(80, 30);
            btn2.Click += clearButton_Click;
            this.Controls.Add(btn2);
            btn2.Enabled = false;

            btn3 = new Button();
            btn3.Text = "Muuda taustavärvi";
            btn3.Location = new Point(190, 70);
            btn3.Size = new Size(120, 30);
            btn3.Click += backgroundButton_Click;
            this.Controls.Add(btn3);

            btn4 = new Button();
            btn4.Text = "Sule";
            btn4.Location = new Point(320, 70);
            btn4.Size = new Size(80, 30);
            btn4.Click += closeButton_Click;
            this.Controls.Add(btn4);

            cb = new CheckBox();
            cb.Text = "Venita pilt";
            cb.Location = new Point(410, 75);
            cb.CheckedChanged += checkBox_CheckedChanged;
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            if (pb != null)
            {
                this.Controls.Remove(pb);
                pb.Dispose();
            }

            if (ofd.ShowDialog() == DialogResult.OK )
            {
                pb = new PictureBox();
                pb.Location = new Point(10, 110);
                pb.Size = new Size(400, 300);
                pb.SizeMode = PictureBoxSizeMode.Zoom; 
                pb.Load(ofd.FileName);
                this.Controls.Add(pb);

                this.Controls.Add(cb);
                cb.Visible= true;

                btn2.Enabled = true;
            }
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            pb.Image = null;
            btn2.Enabled = false;
            cb.Visible = false;
        }
        private void backgroundButton_Click(object sender, EventArgs e)
        {
            if (cd.ShowDialog() == DialogResult.OK)
                BackColor = cd.Color;
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cb.Checked)
            {
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Size = new Size(700, 300);
                this.Width = 750;

            }
            else
            {
                pb.Size = new Size(400, 300);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                this.Width = 500;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
