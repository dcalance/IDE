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
            initStandardPanel();
        }

        void initStandardPanel()
        {
            List<string> buttonsList = new List<string>()
            {
                "%" , "sqrt" , "x^2", "1/x",
                "ce", "c" , "erase", "/",
                "7", "8", "9", "*",
                "4", "5", "6", "-",
                "1", "2", "3", "+",
                "+/-", "0", ".", "="
            };
            this.Controls.Add(standardPanel);
            this.Size = new Size(304, 540);
            int top = this.Top + 185;
            int left = this.Left;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string currentButton = buttonsList[0];
                    buttonsList.RemoveAt(0);
                    Button button = new Button();
                    button.Left = left;
                    button.Top = top;
                    button.Size = new Size(70, 50);
                    button.Click += buttonClickStandard;
                    button.Name = currentButton;
                    button.Text = currentButton;
                    standardPanel.Controls.Add(button);
                    left += button.Width + 2;
                }
                top += 50 + 2;
                left = this.Left;
            }
        }

        void buttonClickStandard(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            List<string> binaryOp = new List<string>
            {
                "/", "*", "+", "-"
            };
            List<string> unaryOp = new List<string>
            {
                "%", "sqrt", "x^2", "1/x"
            };
        }

        string evalExpress(string input)
        {
            string result;
            List<string> stack = new List<string>();
            while (input.Length > 0)
            {
                if (Char.IsDigit(input[0]))
                {
                    string number = input[0].ToString();
                    input = input.Substring(1);
                    while (Char.IsDigit(input[0]))
                    {
                        number += input[0];
                        input = input.Substring(1);
                    }
                    result += number;
                }
            }
        }
    }
}
