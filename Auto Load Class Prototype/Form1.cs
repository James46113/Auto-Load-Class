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
            int[] times = { 515, 575, 630, 645, 660, 615, 730, 760, 790, 820, 875, 930, 990, 1440 }; // A list of the times when different periods start, calculated by 60*hours + minuites
            int timeNum = int.Parse(DateTime.Now.ToString("HH")) * 60 + int.Parse(DateTime.Now.ToString("mm")); // A calculation of the current time num, calculated by 60*hours + minuites
            int ind = 0;  // Index of the lesson in the array
            List<List<List<string>>> classData = new List<List<List<string>>>(); // A 3D list, the first list inside is the days, the lists inside that are the periods
            using (StreamReader f = new StreamReader("classes.cls"))  // Opens the file where the data is stored
            {
                foreach (string day in f.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None)) // Splits the data at the newline - each day
                {
                    List<List<string>> tempDay = new List<List<string>>(); // Creates temporary 2D list to store one day at a time, with lists of the students in the periods inside it. This will get writen to every iteration through the days
                    foreach (string period in day.Split('@')) // Splits the day up into periods
                    {
                        tempDay.Add(period.Split(',').ToList()); // Splits the periods up into the students in the period
                    }
                    classData.Add(tempDay); // Adds the day to the main list of days
                }
            }

            foreach (int item in times) // Iterating through the times when lessons start
            {
                if (timeNum < item) // If the current time value is less than the one in the time list, then it must be in that lesson
                {
                    ind = Array.IndexOf(times, item); // Gets the index of the number that the current time value is smaller than
                    break;
                }
            }
            try
            {
                if (ind != 0 & ind != 13 & (int)(DateTime.Now.DayOfWeek+6)%7 < 5) // Checks if it is not before school hours (ind != 0), if it is not after school hours (ind != 13), and if it isn't the weekend ((int)(DateTime.Now.DayOfWeek+6)%7 <5)
                {
                    return classData[(int)(DateTime.Now.DayOfWeek+6)%7][ind-1]; // Returns the current class list, takes one away from the index because index 0 in the times list is before school, index 0 in the students list is 1st period, therefore if you take one away, it will get the current period
                }
                else
                {
                    return new List<string>(); // If it is the weekend, before, or after school it will return an empty list
                }
            }
            catch
            {
                return new List<string>(); // If an error occurs, such as no file saved at that time, it will return an empty list
            }
        }
    }
}
