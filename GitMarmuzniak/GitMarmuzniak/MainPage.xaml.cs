using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace GitMarmuzniak
{
    public partial class MainPage : TabbedPage
    {
        public static List<ChartData> ChartData {  get; set; }
        public static string Title = "Przykadowy wykres";
        private Color[] Colors = { Color.Red, Color.Blue, Color.Orange, Color.Green, Color.Purple, Color.Gray};
        private Brush[] Brushes = { Brush.Red, Brush.Blue, Brush.Orange, Brush.Green, Brush.Purple, Brush.Gray};
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
            wykresSlupkowy.Children.Clear();
            wykresSlupkowy.ColumnDefinitions.Clear();
            wykresSlupkowy.RowDefinitions.Clear();
            for (int i = 0; i < ChartData.Count; i++)
                wykresSlupkowy.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star)});
            wykresSlupkowy.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            wykresSlupkowy.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Absolute) });
            wykresSlupkowy.RowDefinitions.Add(new RowDefinition { Height= new GridLength(1, GridUnitType.Auto) });
            Line horizontalLine = new Line
            {
                HeightRequest = 1,
                BackgroundColor = Color.Black
            };
            wykresSlupkowy.Children.Add(horizontalLine, 0, 1);
            if(ChartData.Count > 0)
            {
                Grid.SetColumnSpan(horizontalLine, ChartData.Count);
                double max = ChartData.Max(x => x.Value);
                for(int i = 0; i < ChartData.Count; i++)
                {
                    StackLayout stackLayout = new StackLayout
                    {
                        BackgroundColor = Colors[i],
                        Margin = new Thickness(10),
                        HeightRequest = ChartData[i].Value / max * 700,
                        VerticalOptions = LayoutOptions.End,
                        ScaleY = 0,
                        AnchorY = 1
                    };
                    Label label = new Label
                    {
                        TextColor = Color.White,
                        HorizontalOptions = LayoutOptions.Center,
                        Text = ChartData[i].Value.ToString()
                    };
                    stackLayout.Children.Add(label);
                    Label title = new Label
                    {
                        FontSize = 17,
                        HorizontalOptions = LayoutOptions.Center,
                        Text = ChartData[i].Name
                    };
                    wykresSlupkowy.Children.Add(stackLayout, i, 0);
                    wykresSlupkowy.Children.Add(title, i, 2);
                    stackLayout.ScaleYTo(1, 2000, Easing.SinInOut);
                }
            }
            slupkowyLabel.Text = Title;
        }
        private Point CenterPoint = new Point(100, 100);
        private int Radius = 100;
        private double CurrentAngle = 0;
        private Xamarin.Forms.Shapes.Path DrawArc(double degreeAngle, Brush color)
        {
            double startPointX = Math.Cos(CurrentAngle) * Radius + CenterPoint.X;
            double startPointY = Math.Sin(CurrentAngle) * Radius + CenterPoint.Y;
            CurrentAngle += degreeAngle * (Math.PI / 180);
            double endPointX = Math.Cos(CurrentAngle) * Radius + CenterPoint.X;
            double endPointY = Math.Sin(CurrentAngle) * Radius + CenterPoint.Y;
            Point startPoint = new Point(startPointX, startPointY);
            Point endPoint = new Point(endPointX, endPointY);
            LineSegment line1 = new LineSegment(startPoint);
            LineSegment line2 = new LineSegment(CenterPoint);
            ArcSegment arcSegment = new ArcSegment
            {
                SweepDirection = SweepDirection.Clockwise,
                RotationAngle = degreeAngle,
                Size = new Size(Radius, Radius),
                Point = endPoint,
                IsLargeArc = degreeAngle > 180
            };
            PathFigure pathFigure = new PathFigure
            {
                StartPoint = CenterPoint
            };
            pathFigure.Segments.Add(line1);
            pathFigure.Segments.Add(arcSegment);
            pathFigure.Segments.Add(line2);
            PathFigureCollection pathFigures = new PathFigureCollection
            {
                pathFigure
            };
            PathGeometry pathGeometry = new PathGeometry
            {
                Figures = pathFigures,
            };
            Xamarin.Forms.Shapes.Path path = new Xamarin.Forms.Shapes.Path
            {
                Data = pathGeometry,
                Fill = color
            };
            return path;
        }

        private void WykresKolowy_Appearing(object sender, EventArgs e)
        {
            wykresKolowy.Children.Clear();
            wykresKolowyStackLayout.Children.Clear();
            wykresKolowy.Rotation = -90;
            wykresKolowy.Scale = 0;
            double sum = 0;
            foreach (var item in ChartData)
                sum += item.Value;
            for(int i = 0; i < ChartData.Count; i++)
            {
                wykresKolowy.Children.Add(DrawArc(ChartData[i].Value / sum * 360, Brushes[i]));
                StackLayout stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal
                };
                BoxView boxView = new BoxView
                {
                    WidthRequest = 20,
                    HeightRequest = 20,
                    BackgroundColor = Colors[i],
                    Margin = new Thickness(5)
                };
                Label label = new Label
                {
                    FontSize = 17,
                    Text = ChartData[i].Name + " - " + ChartData[i].Value + " (" + (ChartData[i].Value / sum * 100).ToString("0.00") + "%)"
                };
                stackLayout.Children.Add(label);
                stackLayout.Children.Add(boxView);
                wykresKolowyStackLayout.Children.Add(stackLayout);
            }
            wykresKolowy.RotateTo(0, 2000, Easing.SinInOut);
            wykresKolowy.ScaleTo(1, 2000, Easing.SinInOut);
            kolowyLabel.Text = Title;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditPage());
        }
    }
}
