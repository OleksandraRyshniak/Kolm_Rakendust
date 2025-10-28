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

namespace Kolm_Rakendust
{
    public partial class picture_viewer : Form
    {
        PictureBox pb;
        CheckBox cb, cb1, cb2, cb3;
        Button btn1, btn2, btn3, btn4;
        Label lbl;
        OpenFileDialog ofd;
        ColorDialog cd;
        Timer pulse;
        float scale = 1.0f;
        bool growing = true;
        int baseWidth, baseHeight;
        Point baseLocation;
        public picture_viewer()
        {
            //InitializeComponent();
            this.Text = "Pildi Vaatamise";
            this.Width = 1000;
            this.Height = 1000;

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
            cb.Font = new Font("Arial", 10);
            cb.CheckedChanged += checkBox_CheckedChanged;

            cb1 = new CheckBox();
            cb1.Text = "1.";
            cb1.Location = new Point(430, 110);
            cb1.Font = new Font("Arial", 10);
            cb1.CheckedChanged += AddReflection;

            cb2 = new CheckBox();
            cb2.Text = "2.";
            cb2.Location = new Point(430, 130);
            cb2.Font = new Font("Arial", 10);
            cb2.CheckedChanged += PulseAnimation ;

            cb3 = new CheckBox();
            cb3.Text = "3.";
            cb3.Location = new Point(430, 150);
            cb3.Font = new Font("Arial", 10);
            cb3.CheckedChanged += Border;

        }

        private void Border(object sender, EventArgs e)
        {
            if (pb == null || pb.Image == null) return;

            // чтобы не добавлять событие повторно
            pb.Paint -= DrawGradientBorder;

            if (cb3.Checked)
            {
                pb.Size = new Size(350, 200);
                pb.Location = new Point(10, 160);
                pb.Paint += DrawGradientBorder;
                pb.Invalidate(); // перерисовать
            }
            else
            {
                pb.Location = new Point(10, 40);
                pb.Size = new Size(350, 500);
                pb.Invalidate(); // обновить без рамки
            }
        }

        // Отдельный метод для отрисовки рамки
        private void DrawGradientBorder(object sender, PaintEventArgs e)
        {
            PictureBox box = sender as PictureBox;
            if (box == null) return;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                box.ClientRectangle,
                Color.MediumPurple,
                Color.DeepSkyBlue,
                45))
            {
                using (Pen pen = new Pen(brush, 6))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, box.Width - 1, box.Height - 1);
                }
            }
        }

        private void PulseAnimation(object sender, EventArgs e)
        {
            if (pb == null) return;

            if (cb2.Checked)
            {
                baseWidth = pb.Width;
                baseHeight = pb.Height;
                baseLocation = pb.Location;

                pulse = new Timer { Interval = 30 };

                pulse.Tick += (s, ev) =>
                {
                    scale += growing ? 0.01f : -0.01f;
                    if (scale >= 1.1f) growing = false;
                    if (scale <= 1.0f) growing = true;

                    int newWidth = (int)(baseWidth * scale);
                    int newHeight = (int)(baseHeight * scale);

                    pb.Location = new Point(
                        baseLocation.X - (newWidth - baseWidth) / 2,
                        baseLocation.Y - (newHeight - baseHeight) / 2
                    );

                    pb.Size = new Size(newWidth, newHeight);
                };

                pulse.Start();
            }
            else
            {
                if (pulse != null)
                {
                    pulse.Stop();
                    pulse.Dispose();
                    pulse = null;
                }

                pb.Size = new Size(baseWidth, baseHeight);
                pb.Location = baseLocation;
            }
        }

        private void AddReflection(object sender, EventArgs e)
        {
            if (pb == null || pb.Image == null) return;

            if (cb1.Checked)
            {
                Bitmap original = new Bitmap(pb.Image);
                Bitmap reflected = new Bitmap(original.Width, original.Height*2);
                using (Graphics g = Graphics.FromImage(reflected))
                {
                    g.DrawImage(original, 0, 0, original.Width, original.Height);
                    using (Bitmap flip = new Bitmap(original))
                    {
                        flip.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        Rectangle destRect = new Rectangle(0, original.Height, original.Width, original.Height);
                        g.DrawImage(flip, destRect);
                        using (LinearGradientBrush brush = new LinearGradientBrush(
                            destRect,
                            Color.FromArgb(200, Color.White),
                            Color.Transparent,
                            270))
                        {
                            g.FillRectangle(brush, destRect);
                        }
                    }
                }
                pb.Location = new Point(10, 100);
                pb.Image = reflected; 
            }
            else
            {
                pb.Location = new Point(10, 10);
                pb.Image = Image.FromFile(ofd.FileName);
            }
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
                pb.Location = new Point(10, 40);
                pb.Size = new Size(350, 500);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Load(ofd.FileName);
                pb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                this.Controls.Add(pb);

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