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
    public partial class game : Form
    {
        TableLayoutPanel tlp;
        Label lbl, clickedlbl, timelbl;
        Label fClicked = null;
        Label sClicked = null;
        Timer hidetimer, gametimer;

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
            this.Width = 550;
            this.Height = 550;

            hidetimer = new Timer();
            hidetimer.Interval = 500;
            hidetimer.Tick += hidetimer_Tick;

            gametimer = new Timer();
            gametimer.Interval = 1000; // 1 секунда
            gametimer.Tick += gametimer_Tick;
            gametimer.Start();

            timelbl = new Label
            {
                Text = "Aeg: 00:00 s",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.LightSteelBlue
            };
            this.Controls.Add(timelbl);

            tlp = new TableLayoutPanel
            {
                RowCount = 4,
                ColumnCount = 4,
                Dock = DockStyle.Fill,
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

        private void gametimer_Tick(object sender, EventArgs e)
        {
            seconds++;
            int minutes = seconds / 60;
            int sec = seconds % 60;
            timelbl.Text = $"Aeg: {minutes:D2}:{sec:D2} s";
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


                if (fClicked.Text == sClicked.Text)
                {
                    fClicked = null;
                    sClicked = null;
                    return;
                }
                hidetimer.Start();
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
            // Go through all of the labels in the TableLayoutPanel, 
            // checking each one to see if its icon is matched
            foreach (Control control in tlp.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // If the loop didn’t return, it didn't find
            // any unmatched icons
            // That means the user won. Show a message and close the form
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }

        private void game_Load(object sender, EventArgs e)
        {

        }
    }
}
