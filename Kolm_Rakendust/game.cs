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
        public game()
        {
            InitializeComponent();
            // multiplicand = randomizer.Next(2, 11);
            //multiplier = randomizer.Next(2, 11);
            //timesLeftLabel.Text = multiplicand.ToString();
            //timesRightLabel.Text = multiplier.ToString();
            //product.Value = 0;

            //// Fill in the division problem.
            //divisor = randomizer.Next(2, 11);
            //int temporaryQuotient = randomizer.Next(2, 11);
            //dividend = divisor * temporaryQuotient;
            //dividedLeftLabel.Text = dividend.ToString();
            //dividedRightLabel.Text = divisor.ToString();
            //quotient.Value = 0;

            //// Start the timer.
            //timeLeft = 30;
            //timeLabel.Text = "30 seconds";
            //timer1.Start();
            //timer1.Tick += new EventHandler(timer1_Tick);
        }

        //private void startButton_Click(object sender, EventArgs e)
        //{
        //}
        //private bool CheckTheAnswer()
        //{
        //    if ((addend1 + addend2 == sum.Value)
        //        && (minuend - subtrahend == difference.Value)
        //        && (multiplicand * multiplier == product.Value)
        //        && (dividend / divisor == quotient.Value))
        //        return true;
        //    else
        //        return false;
        //}

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (CheckTheAnswer())
        //    {
        //        timer1.Stop();
        //        MessageBox.Show("You got all the answers right!",
        //                        "Congratulations!");
        //        startButton.Enabled = true;
        //    }
        //    else if (timeLeft > 0)
        //    {
        //        timeLeft = timeLeft - 1;
        //        timeLabel.Text = timeLeft + " seconds";
        //    }
        //    else
        //    {
        //        timer1.Stop();
        //        timeLabel.Text = "Time's up!";
        //        MessageBox.Show("You didn't finish in time.", "Sorry!");
        //        sum.Value = addend1 + addend2;
        //        difference.Value = minuend - subtrahend;
        //        product.Value = multiplicand * multiplier;
        //        quotient.Value = dividend / divisor;
        //        startButton.Enabled = true;
        //    }
        //}

        //}

        private void game_Load(object sender, EventArgs e)
        {

        }
    }
}
