using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.Reflection;
using System.IO;

namespace Auto_Load_Class_Prototype
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> Loaded = LoadClass();
            foreach (string value in Loaded)
            {
                Console.WriteLine(value);
            }
        }

        private List<string> LoadClass()
        {
            int[] times = { 515, 575, 630, 645, 660, 615, 730, 760, 790, 820, 875, 930, 990, 1440 };
            List<List<List<string>>> classData = new List<List<List<string>>>();
            using (StreamReader f = new StreamReader("classes.cls"))
            {
                foreach (string day in f.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    List<List<string>> tempDay = new List<List<string>>();
                    foreach (string period in day.Split('@'))
                    {
                        tempDay.Add(period.Split(',').ToList());
                    }
                    classData.Add(tempDay);
                }
            }

            int timeNum = int.Parse(DateTime.Now.ToString("HH")) * 60 + int.Parse(DateTime.Now.ToString("mm"));
            int ind = 0;

            foreach (int item in times)
            {
                if (timeNum < item)
                {
                    ind = Array.IndexOf(times, item);
                    break;
                }
            }
            try
            {
                if (ind != 0 & ind != 13 & (int)(DateTime.Now.DayOfWeek+6)%7 < 5)
                {
                    return classData[(int)(DateTime.Now.DayOfWeek+6)%7][ind-1];
                }
                else
                {
                    return new List<string>();
                }
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}
