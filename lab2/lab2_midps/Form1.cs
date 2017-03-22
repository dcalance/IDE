using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lab2_midps_logic;
using OxyPlot;
using OxyPlot.Series;


namespace lab2_midps
{
    public partial class Form1 : Form
    {
        private string currentInputString = "0";
        private List<string> expressionList = new List<string>();
        int bracketsCount = 0;
        List<string> operatorList = new List<string>() {"+", "-", "/", "*", "^", "%" , "(", "Exp", "y√x" };
        bool inputChanged = false;
        public Form1()
        {
            InitializeComponent();
            classicPanel.Visible = true;
            scientificPanel.Visible = false;
            updateView();
            optionBox.Items.Add("Classic");
            optionBox.Items.Add("Scientific");
            optionBox.Items.Add("Plot");
            updateMemoryButtons();
            
        }
        private void updateMemoryButtons()
        {
            if (!System.IO.File.Exists(@".\sausage.txt"))
            {
                button46.Enabled = false;
                button47.Enabled = false;
                button48.Enabled = false;
            }
            else
            {
                button46.Enabled = true;
                button47.Enabled = true;
                button48.Enabled = true;
            }
        }
        private void updateView()
        {
            double nr;
            if (double.TryParse(currentInputString, out nr) && (Double.IsNaN(nr) || Double.IsInfinity(nr)))
            {
                currentInput.Text = "Goddamn it, you broke it";
                foreach (Control ctrl in scientificPanel.Controls)
                {
                    ctrl.Enabled = false;
                }

                foreach (Control ctrl in classicPanel.Controls)
                {
                    ctrl.Enabled = false;
                }
                tableLayoutPanel1.Enabled = true;
                foreach (Control ctrl in tableLayoutPanel1.Controls)
                {
                    if (ctrl.Text == "CE" || ctrl.Text == "C")
                    {
                        ctrl.Enabled = true;
                    }
                    else
                    {
                        ctrl.Enabled = false;
                    }
                }
                classicButtons.Enabled = true;
                foreach (Control ctrl in classicButtons.Controls)
                {
                    if (ctrl.Text == "CE" || ctrl.Text == "C")
                    {
                        ctrl.Enabled = true;
                    }
                    else
                    {
                        ctrl.Enabled = false;
                    }
                }
            }
            else
            {
                foreach (Control ctrl in scientificPanel.Controls)
                {
                    ctrl.Enabled = true;
                }
                foreach (Control ctrl in classicPanel.Controls)
                {
                    ctrl.Enabled = true;
                }
                foreach (Control ctrl in tableLayoutPanel1.Controls)
                {
                    ctrl.Enabled = true;
                }
                foreach (Control ctrl in classicButtons.Controls)
                {
                    ctrl.Enabled = true;
                }
                currentInput.Text = currentInputString;
            }
            expressionBox.Text = "";
            foreach (var item in expressionList)
            {
                expressionBox.Text += $"{item} ";
            }
            inputChanged = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            tablePanel1.Visible = !tablePanel1.Visible;
            tablePanel2.Visible = !tablePanel2.Visible;
        }

        private void operandClicked(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            if (currentInputString.Length < 16)
            {
                if (currentInputString.Length == 1 && currentInputString[0] == '0')
                {
                    currentInputString = control.Text;
                }
                else
                {
                    currentInputString += control.Text;
                }
                updateView();
            } 
        }

        private void dotPressed(object sender, EventArgs e)
        {
            if (!currentInputString.Contains("."))
            {
                currentInputString += ".";
                updateView();
            }
        }
        private void plusMinusClick(object sender, EventArgs e)
        {
            if (currentInputString != "0")
            {
                if (double.Parse(currentInputString) > 0)
                {
                    currentInputString = currentInputString.Insert(0, "-");
                }
                else
                {
                    currentInputString = currentInputString.Remove(0, 1);
                }    
            }
            updateView();
        }
        private void openBracketClick(object sender, EventArgs e)
        {
            if (expressionList.Count == 0 || operatorList.Contains(expressionList[expressionList.Count - 1]))
            {
                expressionList.Add("(");
                bracketsCount += 1;
                updateView();
            }
        }
        private void closeBracketClick(object sender, EventArgs e)
        {
            if (bracketsCount > 0)
            {
                expressionList.Add(currentInputString);
                expressionList.Add(")");
                bracketsCount -= 1;
                currentInputString = "0";
                updateView();
            }
        }
        
        private void operatorClicked(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            if (expressionList.Count > 0 && !inputChanged)
            {
                expressionList[expressionList.Count - 1] = control.Text;
            }
            else
            {
                if (expressionList.Count > 0 && expressionList[expressionList.Count - 1] == ")")
                {
                    expressionList.Add(control.Text);
                    //expressionList.Add(currentInputString);
                }
                else
                {
                    expressionList.Add(currentInputString);
                    expressionList.Add(control.Text);
                }
                currentInputString = "0";
            }
            updateView();
        }
        private void eraseClicked(object sender, EventArgs e)
        {
            if (currentInputString.Length > 1)
            {
                currentInputString = currentInputString.Substring(0, currentInputString.Length - 1);
            }
            else
            {
                currentInputString = "0";
            }
            updateView();
        }
        private void CClicked(object sender, EventArgs e)
        {
            currentInputString = "0";
            expressionList = new List<string>();
            updateView();
        }
        private void CEClicked(object sender, EventArgs e)
        {
            currentInputString = "0";
            updateView();
        }
        private void piClicked(object sender, EventArgs e)
        {
            currentInputString = "3.1415926535897";
            updateView();
        }
        private void factorialClicked(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"fact:{currentInputString}").ToString();
            updateView();
        }
        private void equalsClicked(object sender, EventArgs e)
        {
            if (expressionList.Count > 0 && operatorList.Contains(expressionList[expressionList.Count - 1]))
            {
                expressionList.Add(currentInputString);
            }
            while (bracketsCount > 0)
            {
                expressionList.Add(")");
                bracketsCount -= 1;
            }
            if (expressionList.Count > 0)
            {
                currentInputString = Calculator.eval(expressionList).ToString();
            }
            expressionList = new List<string>();
            updateView();
        }
        private void eToPowerXClick(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"exp:{currentInputString}").ToString();
            updateView();
        }
        private void logClicked(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"lg:{currentInputString}").ToString();
            updateView();
        }
        private void tenToPowerClicked(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"10to:{currentInputString}").ToString();
            updateView();
        }
        private void sqrtClicked(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"sqrt:{currentInputString}").ToString();
            updateView();
        }
        private void tanClicked(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"tan:{currentInputString}").ToString();
            updateView();
        }
        private void sqrClicked(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"sqr:{currentInputString}").ToString();
            updateView();
        }
        private void cosClicked(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"cos:{currentInputString}").ToString();
            updateView();
        }
        private void sinClicked(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"sin:{currentInputString}").ToString();
            updateView();
        }
       
        private void cubeClick(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"cube:{currentInputString}").ToString();
            updateView();
        }
        private void dmsClick(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"dms:{currentInputString}").ToString();
            updateView();
        }
        private void degClick(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"deg:{currentInputString}").ToString();
            updateView();
        }
        private void baseSqrtClick(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            operatorClicked(control, e);
            
        }
        private void lnClick(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"ln:{currentInputString}").ToString();
            updateView();
        }

        private void invertClick(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"invert:{currentInputString}").ToString();
            updateView();
        }
        private void aTanClick(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"atan:{currentInputString}").ToString();
            updateView();
        }
        private void aSinClick(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"asin:{currentInputString}").ToString();
            updateView();
        }
        private void acosClick(object sender, EventArgs e)
        {
            currentInputString = Calculator.unaryEval($"acos:{currentInputString}").ToString();
            updateView();
        }
        private void MRClick(object sender, EventArgs e)
        {
            currentInputString = System.IO.File.ReadAllText(@".\sausage.txt");
            updateView();
        }
        private void MPlusClick(object sender, EventArgs e)
        {
            string value = System.IO.File.ReadAllText(@".\sausage.txt");
            double number = double.Parse(value);
            string result = (number + double.Parse(currentInputString)).ToString();
            System.IO.File.WriteAllText(@".\sausage.txt", result);
        }
        private void MMinusClick(object sender, EventArgs e)
        {
            string value = System.IO.File.ReadAllText(@".\sausage.txt");
            double number = double.Parse(value);
            string result = (number - double.Parse(currentInputString)).ToString();
            System.IO.File.WriteAllText(@".\sausage.txt", result);
        }
        private void MSClick(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText(@".\sausage.txt", currentInputString);
            updateMemoryButtons();
        }
        private void MCClick(object sender, EventArgs e)
        {
            System.IO.File.Delete(@".\sausage.txt");
            updateMemoryButtons();
        }
        private void MDownClick(object sender, EventArgs e)
        {
            //hi there :)
        }

        private void menu_Click(object sender, EventArgs e)
        {
            optionBox.Visible = !optionBox.Visible;
            
        }

        private void optionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            optionBox.Visible = !optionBox.Visible;
            Control control = (Control)sender;
            switch (control.Text)
            {
                case "Classic":
                    scientificPanel.Visible = false;
                    classicPanel.Visible = true;
                    currentInputString = "0";
                    expressionList = new List<string>();
                    updateView();
                    break;
                case "Scientific":
                    classicPanel.Visible = false;
                    scientificPanel.Visible = true;
                    currentInputString = "0";
                    expressionList = new List<string>();
                    updateView();
                    break;
                case "Plot":
                    GraphWindow window = new GraphWindow();
                    window.Show();
                    break;
            }
        }

        private void scientificPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void backgroundPanel_paint(object sender, PaintEventArgs e)
        {

        }

    }
}
