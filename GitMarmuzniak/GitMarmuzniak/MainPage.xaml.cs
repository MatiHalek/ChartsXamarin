using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GitMarmuzniak
{
    public partial class MainPage : TabbedPage
    {
        public static List<ChartData> ChartData {  get; set; }
        public MainPage()
        {
            InitializeComponent();
            ChartData = new List<ChartData>
            {
                new ChartData("Seria 1", 15),
                new ChartData("Seria 2", 35),
                new ChartData("Seria 3", 5),
                new ChartData("Seria 4", 75),
            };
        }

        private void WykresSlupkowy_Appearing(object sender, EventArgs e)
        {

        }

        private void WykresKolowy_Appearing(object sender, EventArgs e)
        {

        }
    }
}
