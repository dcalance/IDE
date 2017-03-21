using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2_midps
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        private void standardPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button12_Click(object sender, EventArgs e)
        {
            tablePanel1.Visible = !tablePanel1.Visible;
            tablePanel2.Visible = !tablePanel2.Visible;
        }

        private void operandClicked(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            richTextBox1.AppendText(control.Text);
        }

        private void dotPressed(object sender, EventArgs e)
        {

        }
        private void CEClicked(object sender, EventArgs e)
        {

        }
        private void plusMinusClick(object sender, EventArgs e)
        {

        }
        private void openBracketClick(object sender, EventArgs e)
        {

        }
        private void closeBracketClick(object sender, EventArgs e)
        {

        }
        private void cosMinus1Click(object sender, EventArgs e)
        {

        }

        private void MCClick(object sender, EventArgs e)
        {

        }
        private void MDownClick(object sender, EventArgs e)
        {

        }
        private void operatorClicked(object sender, EventArgs e)
        {

        }
        private void eraseClicked(object sender, EventArgs e)
        {

        }
        private void CClicked(object sender, EventArgs e)
        {

        }
        private void piClicked(object sender, EventArgs e)
        {

        }
        private void factorialClicked(object sender, EventArgs e)
        {

        }
        private void equalsClicked(object sender, EventArgs e)
        {

        }
        private void modClicked(object sender, EventArgs e)
        {

        }
        private void expClicked(object sender, EventArgs e)
        {

        }
        private void logClicked(object sender, EventArgs e)
        {

        }
        private void tenToPowerClicked(object sender, EventArgs e)
        {

        }
        private void sqrtClicked(object sender, EventArgs e)
        {

        }
        private void tanClicked(object sender, EventArgs e)
        {

        }
        private void sqrClicked(object sender, EventArgs e)
        {

        }
        private void cosClicked(object sender, EventArgs e)
        {

        }
        private void sinClicked(object sender, EventArgs e)
        {

        }
        private void exponentialClicked(object sender, EventArgs e)
        {

        }
        private void degClick(object sender, EventArgs e)
        {

        }
        private void cubeClick(object sender, EventArgs e)
        {

        }
        private void dmsClick(object sender, EventArgs e)
        {

        }
        private void baseSqrtClick(object sender, EventArgs e)
        {

        }
        private void lnClick(object sender, EventArgs e)
        {

        }
        private void expClick(object sender, EventArgs e)
        {

        }
        private void invertClick(object sender, EventArgs e)
        {

        }
        private void tanMinus1Click(object sender, EventArgs e)
        {

        }
        private void sinMinus1Click(object sender, EventArgs e)
        {

        }
        private void MRClick(object sender, EventArgs e)
        {

        }
        private void MPlusClick(object sender, EventArgs e)
        {

        }
        private void MMinusClick(object sender, EventArgs e)
        {

        }
        private void MSClick(object sender, EventArgs e)
        {

        }


    }
}
