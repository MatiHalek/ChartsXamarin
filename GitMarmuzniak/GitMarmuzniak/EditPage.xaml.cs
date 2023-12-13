﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GitMarmuzniak
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    { 
        public EditPage()
        {
            InitializeComponent();
        
            for(int i = 0; i < MainPage.ChartData.Count; i++) 
            {
                (grid.Children[i] as Entry).Text = MainPage.ChartData[i].Name;
                (grid.Children[i + 7] as Entry).Text = MainPage.ChartData[i].Value.ToString();
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            MainPage.ChartData = new List<ChartData>();

            for(int i = 0; i < 7; i++) 
            {
                if ((grid.Children[i] as Entry).Text != null && (grid.Children[i + 7] as Entry).Text != null)
                    MainPage.ChartData.Add(new ChartData((grid.Children[i] as Entry).Text, double.Parse((grid.Children[i + 7] as Entry).Text)));
            }
            Navigation.PopAsync();
        }
    }
}