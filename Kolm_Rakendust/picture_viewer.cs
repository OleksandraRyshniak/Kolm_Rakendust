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
        Button btn1, btn2, btn3, btn4, btn5;
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
            btn1.Size = new Size(100, 30);
            btn1.Click += showButton_Click;
            this.Controls.Add(btn1);

            cb = new CheckBox();

        }

        private void showButton_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pb.Load(ofd.FileName);
            }
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            pb.Image = null;
        }
        private void backgroundButton_Click(object sender, EventArgs e)
        {
            // Show the color dialog box. If the user clicks OK, change the
            // PictureBox control's background to the color the user chose.
            if (cd.ShowDialog() == DialogResult.OK)
                pb.BackColor = cd.Color;
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            // If the user selects the Stretch check box, 
            // change the PictureBox's
            // SizeMode property to "Stretch". If the user clears 
            // the check box, change it to "Normal".
            if (cb.Checked)
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pb.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
