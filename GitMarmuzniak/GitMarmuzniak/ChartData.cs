using System;
using System.Collections.Generic;
using System.Text;

namespace GitMarmuzniak
{
    public class ChartData
    {
        public string Name { get; set; }
        public double Value { get; set; }

        public ChartData(string name, double value)
        {
            Name = name;
            Value = value;
        }
        public ChartData() { }
    }
}
