using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Ellipse> _ellipses = new List<Ellipse>();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawEllipses()
        {
            foreach (var node in _ellipses)
            {
                if (!DrawingGrid.Children.Contains(node))
                    DrawingGrid.Children.Add(node);
            }
        }
        
        private Ellipse MakeEllipse()
        {
            int size = 50;
            
            Ellipse ellip = new Ellipse
            {
                Stroke = Brushes.White,
                Fill = Brushes.White,
                StrokeThickness = 2,
                Width = size,
                Height = size,
                RenderTransform = new TranslateTransform(-(size/2),-(size/2))
            };
            ellip.MouseRightButtonDown += DeleteEllipse;
            return ellip;
        }

        private void AddEllipse(double x, double y)
        {
            Ellipse temp = MakeEllipse();
            Canvas.SetLeft(temp, x);
            Canvas.SetTop(temp, y);
            _ellipses.Add(temp);
            DrawEllipses();
        }

        
        private void DeleteEllipse(object sender, MouseEventArgs e)
        {
            var oop = sender as Ellipse;

            if (DrawingGrid.Children.Contains(oop) && oop != null)
            {
                _ellipses.Remove(oop);
                DrawingGrid.Children.Remove(oop);
            }
        }
        private void DrawingGrid_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddEllipse(e.GetPosition(DrawingGrid).X, e.GetPosition(DrawingGrid).Y);
        }
    }
}