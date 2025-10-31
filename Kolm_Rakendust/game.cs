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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Kolm_Rakendust
{

    public partial class game : Form
    {
        int user1_score = 0;
        int user2_score = 0;
        int timeLeft;
        TableLayoutPanel tlp;
        Label lbl, clickedlbl, timelbl, label, lbl_user1,lbl_user2 ;
        Label fClicked = null;
        Label sClicked = null;
        Timer hidetimer, gametimer;
        Button start_btn, close_btn, pluss_btn;
        RadioButton rb1, rb2, rb3;

        private bool isTimeMode = false;


        Random randomizer = new Random();
        int seconds = 0;

        List<string> icons = new List<string>()
    {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
    };
        public game()
        {
            InitializeComponent();
            this.Text = "Sarnaste piltide leidmise mäng";
            this.Width = 420;
            this.Height = 200;

            label = new Label();
            label.Text = "Vali mängurežiim ja vajuta 'Alusta Mängu'";
            label.Location = new Point(20, 20);
            label.Size = new Size(400, 30);
            label.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            this.Controls.Add(label);

            rb1 = new RadioButton
            {
                Text = "Tavaline režiim",
                Location = new Point(20, 70),
                Size = new Size(130, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            this.Controls.Add(rb1);

            rb2 = new RadioButton
            {
                Text = "Ajamäng",
                Location = new Point(150, 70),
                Size = new Size(100, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            this.Controls.Add(rb2);

            rb3 = new RadioButton
            {
                Text = "Mäng kahele",
                Location = new Point(250, 70),
                Size = new Size(130, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            this.Controls.Add(rb3);

            start_btn = new Button
            {
                Text = "Alusta Mängu",
                Location = new Point(40, 110),
                Size = new Size(150, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                BackColor = Color.RoyalBlue,
                ForeColor = Color.White
            };
            start_btn.Click += start_btn_Click;
            this.Controls.Add(start_btn);

            close_btn = new Button
            {
                Text = "Sulge Mäng",
                Location = new Point(200, 110),
                Size = new Size(150, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                BackColor = Color.RoyalBlue,
                ForeColor = Color.White
            };
            close_btn.Click += close_btn_Click;
            this.Controls.Add(close_btn);
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.Width = 500;
            this.Height = 600;

            lbl_user1 = new Label
            {
                Text = "Mängija 1 \nPuntkid: " + user1_score,
                Location = new Point(40, 10),
                Size = new Size(200, 70),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.BlanchedAlmond
            };
            this.Controls.Add(lbl_user1);
            lbl_user2 = new Label
            {
                Text = "Mängija 2 \nPuntkid: " + user2_score,
                Location = new Point(250, 10),
                Size = new Size(200, 70),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.LightGray
            };
            this.Controls.Add(lbl_user2);
            tlp = new TableLayoutPanel
            {
                RowCount = 4,
                ColumnCount = 4,
                Location = new Point(45, 110),
                Size = new Size(400, 400),
                BackColor = Color.CornflowerBlue,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
            };
            for (int i = 0; i < 4; i++)
            {
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    lbl = new Label
                    {
                        BackColor = Color.BlanchedAlmond,
                        Dock = DockStyle.Fill,
                        Margin = new Padding(1),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Webdings", 48, FontStyle.Bold)
                    };
                    lbl.Click += label2_Click;
                    tlp.Controls.Add(lbl, col, row);
                }
            }
            this.Controls.Add(tlp);
            AssignIconsToSquares();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.Width = 550;
            this.Height = 600;
            timeLeft = 45; 
            isTimeMode = true;

            hidetimer = new Timer();
            hidetimer.Interval = 300;
            hidetimer.Tick += hidetimer_Tick;

            gametimer = new Timer();
            gametimer.Interval = 1000;
            gametimer.Tick += gametimer_Tick1;
            gametimer.Start();

            timelbl = new Label
            {
                Text = "Aeg: 00:45 s",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(180, 10),
                Size = new Size(180, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.LightSteelBlue
            };
            this.Controls.Add(timelbl);

            tlp = new TableLayoutPanel
            {
                RowCount = 4,
                ColumnCount = 4,
                Location = new Point(40, 50),
                Size = new Size(450, 450),
                BackColor = Color.CornflowerBlue,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
            };

            for (int i = 0; i < 4; i++)
            {
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }

            pluss_btn = new Button
            {
                Text = "+10 Sekundit",
                Location = new Point(210, 510),
                Size = new Size(110, 40),
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                BackColor = Color.RoyalBlue,
                ForeColor = Color.White,
            };
            pluss_btn.Click += pluss_btn_Click;
            this.Controls.Add(pluss_btn);

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    lbl = new Label
                    {
                        BackColor = Color.BlanchedAlmond,
                        Dock = DockStyle.Fill,
                        Margin = new Padding(1),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Webdings", 48, FontStyle.Bold)
                    };

                    lbl.Click += label1_Click;
                    tlp.Controls.Add(lbl, col, row);
                }
            }

            this.Controls.Add(tlp);
            AssignIconsToSquares();
        }
        private void pluss_btn_Click(object sender, EventArgs e)
        {
            timeLeft += 10;
            pluss_btn.Visible = false;
        }
        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.Width = 550;
            this.Height = 600;
            seconds = 0;

            hidetimer = new Timer();
            hidetimer.Interval = 500;
            hidetimer.Tick += hidetimer_Tick;

            gametimer = new Timer();
            gametimer.Interval = 1000;
            gametimer.Tick += gametimer_Tick;
            gametimer.Start();

            timelbl = new Label
            {
                Text = "Aeg: 00:00 s",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(180, 10),
                Size = new Size(180, 30),
                Height = 30,

                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.LightSteelBlue
            };
            this.Controls.Add(timelbl);

            tlp = new TableLayoutPanel
            {
                RowCount = 4,
                ColumnCount = 4,
                Location = new Point(40, 50),
                Size = new Size(450, 450),
                BackColor = Color.CornflowerBlue,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
            };

            for (int i = 0; i < 4; i++)
            {
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    lbl = new Label
                    {
                        BackColor = Color.BlanchedAlmond,
                        Dock = DockStyle.Fill,
                        Margin = new Padding(1),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Webdings", 48, FontStyle.Bold)
                    };

                    lbl.Click += label1_Click;
                    tlp.Controls.Add(lbl, col, row);
                }
            }
            this.Controls.Add(tlp);
            AssignIconsToSquares();
        }
        private void start_btn_Click(object sender, EventArgs e)
        {
            if (rb1.Checked)
            {
                start_btn.Visible = false;
                close_btn.Visible = false;
                rb1.Visible = false;
                rb2.Visible = false;
                rb3.Visible = false;
                label.Visible = false;
                radioButton1_CheckedChanged(sender, e);
            }
            else if (rb2.Checked) 
            {
                start_btn.Visible = false;
                close_btn.Visible = false;
                rb1.Visible = false;
                rb2.Visible = false;
                rb3.Visible = false;
                label.Visible = false;
                radioButton2_CheckedChanged(sender, e);
            }
            else if (rb3.Checked) 
            {
                start_btn.Visible = false;
                close_btn.Visible = false;
                rb1.Visible = false;
                rb2.Visible = false;
                rb3.Visible = false;
                label.Visible = false;
                radioButton3_CheckedChanged(sender, e);
            }
            else
            {
                MessageBox.Show("Palun vali raskusaste!");
            }
        }
        private void gametimer_Tick(object sender, EventArgs e)
        {
            seconds++;
            int minutes = seconds / 60;
            int sec = seconds % 60;

            timelbl.SuspendLayout();
            timelbl.Text = $"Aeg: {minutes:D2}:{sec:D2} s";
            timelbl.ResumeLayout();
        }
        private void AssignIconsToSquares()
        {
            foreach (Control control in tlp.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = randomizer.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            clickedlbl = sender as Label;

            if (clickedlbl != null)
            {
                if (clickedlbl.ForeColor == Color.Black)
                    return;

                if (fClicked == null)
                {
                    fClicked = clickedlbl;
                    fClicked.ForeColor = Color.Black;

                    return;
                }
                sClicked = clickedlbl;
                sClicked.ForeColor = Color.Black;

                CheckForWinner();


                if (fClicked != null && sClicked != null)
                {
                    if (fClicked.Text == sClicked.Text)
                    {
                        fClicked = null;
                        sClicked = null;
                        return;
                    }

                    hidetimer.Start();
                }
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {
            clickedlbl = sender as Label;
            if (clickedlbl == null) return;
            if (clickedlbl.ForeColor == Color.Black) return;

            if (fClicked == null)
            {
                fClicked = clickedlbl;
                fClicked.ForeColor = Color.Black;
                return;
            }

            sClicked = clickedlbl;
            sClicked.ForeColor = Color.Black;

            if (fClicked.Text == sClicked.Text)
            {
                if (isPlayer1Turn)
                {
                    user1_score++;
                    lbl_user1.Text = $"Mängija 1 \nPunktid: {user1_score}";
                }
                else
                {
                    user2_score++;
                    lbl_user2.Text = $"Mängija 2 \nPunktid: {user2_score}";
                }

                fClicked = null;
                sClicked = null;

                CheckForWinner1();
                return;
            }
            if (hidetimer == null)
            {
                hidetimer = new Timer();
                hidetimer.Interval = 300;
                hidetimer.Tick += hidetimer_Tick;
            }
            hidetimer.Start();
            isPlayer1Turn = !isPlayer1Turn;

            if (isPlayer1Turn)
            {
                lbl_user1.BackColor = Color.BlanchedAlmond;
                lbl_user2.BackColor = Color.LightGray;
            }
            else
            {
                lbl_user1.BackColor = Color.LightGray;
                lbl_user2.BackColor = Color.CornflowerBlue;
            }

        }
        private void hidetimer_Tick(object sender, EventArgs e)
        {

            hidetimer.Stop();

            fClicked.ForeColor = fClicked.BackColor;
            sClicked.ForeColor = sClicked.BackColor;

            fClicked = null;
            sClicked = null;
        }
        private void CheckForWinner()
        {
            foreach (Control control in tlp.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null && iconLabel.ForeColor == iconLabel.BackColor)
                    return;
            }

            gametimer.Stop();
            if (hidetimer != null) hidetimer.Stop();

            MessageBox.Show("Kõik paarid on leitud! Tubli töö!");

            ResetToMenu(); 
        }
        private void CheckForWinner1()
        {
            foreach (Control control in tlp.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null && iconLabel.ForeColor == iconLabel.BackColor)
                    return;
            }

            if (user1_score > user2_score)
            {
                MessageBox.Show("Võitis mängija 1!\nTulemus — Mängija 1: " + user1_score + " punkti;\nMängija 2: " + user2_score + " punkti.");
            }
            else if (user2_score > user1_score)
            {
                MessageBox.Show("Võitis mängija 2!\nTulemus — Mängija 1: " + user1_score + " punkti;\nMängija 2: " + user2_score + " punkti.");
            }
            else
            {
                MessageBox.Show("Viik!\nTulemus — Mängija 1: " + user1_score + " punkti;\nMängija 2: " + user2_score + " punkti.");
            }

                ResetToMenu();
        }
        private void gametimer_Tick1(object sender, EventArgs e)
        {
            if (timeLeft <= 5)
            {
                timelbl.ForeColor = Color.Red;
            }
            if (timeLeft > 0)
            {
                timeLeft--;
                int minutes = timeLeft / 60;
                int sec = timeLeft % 60;

                timelbl.Text = $"Aeg: {minutes:D2}:{sec:D2} s";
            }
            else
            {
                gametimer.Stop();
                if (hidetimer != null) hidetimer.Stop();

                MessageBox.Show("Aeg sai otsa! Mäng läbi!", "Lõpp");

                ResetToMenu(); 
            }
        }
        private void ResetToMenu()
        {
            if (tlp != null)
            {
                this.Controls.Remove(tlp);
                tlp.Dispose();
                tlp = null;
            }

            if (timelbl != null)
            {
                this.Controls.Remove(timelbl);
                timelbl.Dispose();
                timelbl = null;
            }

            if (pluss_btn != null)
            {
                if (this.Controls.Contains(pluss_btn)) this.Controls.Remove(pluss_btn);
                pluss_btn.Dispose();
                pluss_btn = null;
            }

            if (lbl_user1 != null)
            {
                if (this.Controls.Contains(lbl_user1)) this.Controls.Remove(lbl_user1);
                lbl_user1.Dispose();
                lbl_user1 = null;
            }

            if (lbl_user2 != null)
            {
                if (this.Controls.Contains(lbl_user2)) this.Controls.Remove(lbl_user2);
                lbl_user2.Dispose();
                lbl_user2 = null;
            }

            if (gametimer != null)
            {
                gametimer.Stop();
                gametimer.Dispose();
                gametimer = null;
            }

            if (hidetimer != null)
            {
                hidetimer.Stop();
                hidetimer.Dispose();
                hidetimer = null;
            }

            this.Width = 420;
            this.Height = 200;

            label.Visible = true;
            rb1.Visible = true;
            rb2.Visible = true;
            rb3.Visible = true;
            start_btn.Visible = true;
            close_btn.Visible = true;

            rb1.Checked = false;
            rb2.Checked = false;
            rb3.Checked = false;

            icons = new List<string>()
    {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
    };

            fClicked = null;
            sClicked = null;

            user1_score = 0;
            user2_score = 0;
        }
        private bool isPlayer1Turn = true;
        private void game_Load(object sender, EventArgs e)
        {

        }
    }
}
