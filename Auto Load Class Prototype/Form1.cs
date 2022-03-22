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
            List<List<string>> classData = new List<List<String>>();
            foreach (string item in Properties.Resources.ResourceManager.GetString(DateTime.Now.DayOfWeek.ToString()).Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                classData.Add(item.Split(',').ToList());
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
                if (ind != 0 & ind != 13)
                {
                    return classData[ind];
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
