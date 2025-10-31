using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kolm_Rakendust
{
    public partial class picture_viewer : Form
    {
        PictureBox pb;
        RadioButton cb1, cb2, cb3;
        Button btn1, btn2, btn3, btn4;
        CheckBox cb;
        Label lbl;
        OpenFileDialog ofd;
        ColorDialog cd;
        Image originalImage;

        public picture_viewer()
        {
            this.Text = "Pildi Vaatamise";
            this.Width = 700;
            this.Height = 800;

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
            btn1.BackColor = Color.RoyalBlue;
            btn1.ForeColor = Color.White;
            btn1.Click += showButton_Click;
            this.Controls.Add(btn1);

            btn2 = new Button();
            btn2.Text = "Puhasta pilt";
            btn2.Location = new Point(100, 70);
            btn2.Size = new Size(80, 30);
            btn2.BackColor = Color.RoyalBlue;
            btn2.ForeColor = Color.White;
            btn2.Click += clearButton_Click;
            this.Controls.Add(btn2);
            btn2.Enabled = false;

            btn3 = new Button();
            btn3.Text = "Muuda taustavärvi";
            btn3.Location = new Point(190, 70);
            btn3.Size = new Size(120, 30);
            btn3.BackColor = Color.RoyalBlue;
            btn3.ForeColor = Color.White;
            btn3.Click += backgroundButton_Click;
            this.Controls.Add(btn3);

            btn4 = new Button();
            btn4.Text = "Sule";
            btn4.Location = new Point(320, 70);
            btn4.Size = new Size(80, 30);
            btn4.BackColor = Color.RoyalBlue;
            btn4.ForeColor = Color.White;
            btn4.Click += closeButton_Click;
            this.Controls.Add(btn4);

            cb = new CheckBox();
            cb.Text = "Venita pilt";
            cb.Location = new Point(430, 75);
            cb.Font = new Font("Arial", 10);
            cb.CheckedChanged += checkBox_CheckedChanged;

            cb1 = new RadioButton();
            cb1.Text = "Mustvalge";
            cb1.Location = new Point(430, 110);
            cb1.Font = new Font("Arial", 9);
            cb1.CheckedChanged += FilterChanged;

            cb2 = new RadioButton();
            cb2.Text = "Heledam pilt"; // <--- Яркость
            cb2.Location = new Point(430, 130);
            cb2.Font = new Font("Arial", 9);
            cb2.CheckedChanged += FilterChanged;

            cb3 = new RadioButton();
            cb3.Text = "Inversioon";
            cb3.Location = new Point(430, 150);
            cb3.Font = new Font("Arial", 9);
            cb3.CheckedChanged += FilterChanged;
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            if (pb != null)
            {
                this.Controls.Remove(pb);
                pb.Dispose();

                cb.Visible = false;
                cb1.Visible = false;
                cb2.Visible = false;
                cb3.Visible = false;
            }

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pb = new PictureBox();
                pb.Location = new Point(20, 40);
                pb.Size = new Size(350, 450);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Load(ofd.FileName);
                pb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                this.Controls.Add(pb);

                originalImage = (Image)pb.Image.Clone();

                this.Controls.Add(cb);
                this.Controls.Add(cb1);
                this.Controls.Add(cb2);
                this.Controls.Add(cb3);

                cb.Visible = true;
                cb1.Visible = true;
                cb2.Visible = true;
                cb3.Visible = true;

                btn2.Enabled = true;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            pb.Image = null; 
            btn2.Enabled = false; 
            cb.Visible = false;
            cb1.Visible = false;
            cb2.Visible = false;
            cb3.Visible = false;

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

        private void checkBox_CheckedChanged(object sender, EventArgs e) {
            if (cb.Checked) 
            { cb1.Checked = false; 
              cb2.Checked = false; 
              cb3.Checked = false; 
              pb.SizeMode = PictureBoxSizeMode.StretchImage; 
              pb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom; 
              pb.Location = new Point(10, 140); 
              cb1.Location = new Point(10, 480); 
              cb1.Size = new Size(100, 20); 
              cb2.Location = new Point(120, 480); 
              cb3.Location = new Point(240, 480); 
              pb.Size = new Size(630, 300); 
            } 
            else 
            { 
                cb1.Checked = false; 
                cb2.Checked = false; 
                cb3.Checked = false; 
                pb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom; 
                pb.Location = new Point(10, 40); 
                pb.Size = new Size(400, 500); 
                pb.SizeMode = PictureBoxSizeMode.Zoom; 
                cb1.Location = new Point(430, 110); 
                cb2.Location = new Point(430, 130); 
                cb3.Location = new Point(430, 150); 
            } 
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            if (pb == null || originalImage == null) return;

            pb.Image = (Image)originalImage.Clone();

            if (cb1.Checked)
                pb.Image = ApplyGrayscale(pb.Image);
            else if (cb2.Checked)
                pb.Image = ApplyBrightness(pb.Image, 1.3f); 
            else if (cb3.Checked)
                pb.Image = ApplyInvert(pb.Image);
        }
        private Image ApplyGrayscale(Image src)
        {
            Bitmap bmp = new Bitmap(src);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int gray = (c.R + c.G + c.B) / 3;
                    bmp.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }
            return bmp;
        }
        private Image ApplyBrightness(Image src, float factor)
        {
            Bitmap bmp = new Bitmap(src);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int r = (int)(c.R * factor);
                    int g = (int)(c.G * factor);
                    int b = (int)(c.B * factor);
                    r = Math.Min(255, r);
                    g = Math.Min(255, g);
                    b = Math.Min(255, b);
                    bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return bmp;
        }

        private Image ApplyInvert(Image src)
        {
            Bitmap bmp = new Bitmap(src);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    bmp.SetPixel(x, y, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
            return bmp;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}