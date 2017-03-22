using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;
using lab2_midps_logic;



using OxyPlot;
using OxyPlot.Series;

namespace lab2_midps
{
    public partial class GraphWindow : Form
    {
        PlotModel pm = new PlotModel();
        string title = "default title";
        string subtitle = "default subtitle";
        string function;
        double startingX;
        double endingX;
        double step;
        public GraphWindow()
        {
            InitializeComponent();
            pm.Series.Add(new FunctionSeries());
            updateGraph();
        }

        private void addToGraphClick(object sender, EventArgs e)
        {
            function = textBox5.Text;
            Double.TryParse(textBox6.Text, out startingX);
            Double.TryParse(textBox7.Text, out endingX);
            Double.TryParse(textBox8.Text, out step);
            var series1 = new LineSeries
            {
                Title = function.Replace(" ", string.Empty),
                RenderInLegend = true,
                MarkerStroke = OxyColors.White
            };
            for (double x = startingX; x < endingX; x += step)
            {
                var arr = function.Split(' ');
                for (int counter = 0; counter < arr.Length; counter++)
                {
                    var currentExp = arr[counter].Split('(', ')');
                    if (currentExp.Length > 1)
                    {
                        double nr;
                        if (!Double.TryParse(currentExp[1], out nr))
                        {
                            arr[counter] = Calculator.unaryEval($"{currentExp[0]}:{x}").ToString();
                        }
                        else
                        {
                            arr[counter] = Calculator.unaryEval($"{currentExp[0]}:{currentExp[1]}").ToString();
                        }
                    }
                    if (arr[counter].ToLower() == "x")
                    {
                        arr[counter] = x.ToString();
                    }
                }
                series1.Points.Add(new DataPoint(x, Calculator.eval(arr.ToList())));
            }
            pm.Series.Add(series1);
            updateGraph();
        }
        private void changeTitleClick(object sender, EventArgs e)
        {
            title = textBox11.Text;
            updateGraph();
        }
        private void changeSubtitleClick(object sender, EventArgs e)
        {
            subtitle = textBox12.Text;
            updateGraph();
        }
        private void clearGraphClick(object sender, EventArgs e)
        {
            while (pm.Series.Count > 0)
            {
                pm.Series.RemoveAt(0);
            }
            updateGraph();
        }

        private void updateGraph()
        {
            pm.Title = title;
            pm.Subtitle = subtitle;
            pm.PlotType = PlotType.Cartesian;
            pm.Background = OxyColors.White;
            pm.ResetAllAxes();
            plot1.Model = pm;
            plot1.OnModelChanged();
        }
    }
}
