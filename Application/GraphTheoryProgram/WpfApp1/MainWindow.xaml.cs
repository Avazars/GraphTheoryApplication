using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ColorPicker.Models;
using WpfApp1.GraphObjects;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private enum ToolSet
        {
            None,
            Move,
            Vertex,
            Edge,
            Remove,
            Color
        }

        private List<Vertex> vertices = new List<Vertex>();
        private List<Ellipse> _ellipses = new List<Ellipse>();
        private ToolSet currentToolSet;
        private Point? DragStart = null;
        
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void topPanelMouseDown(object sender, MouseEventArgs e)
        {
            this.DragMove();
        }
        
        
        
        private Ellipse MakeEllipse()
        {
            const int size = 50;
            
            Ellipse ellipse = new Ellipse
            {
                Stroke = Brushes.White,
                Fill = Brushes.White,
                StrokeThickness = 2,
                Width = size,
                Height = size,
                RenderTransform = new TranslateTransform(-(size/2),-(size/2))
            };
            ellipse.MouseLeftButtonDown += VertexClicked;
            ellipse.MouseMove += VertexDragged;
            ellipse.MouseLeftButtonUp += VertexReleased;
            
            return ellipse;
        }

        private void VertexDragged(object sender, MouseEventArgs e)
        {
            if (DragStart != null && e.LeftButton == MouseButtonState.Pressed)
            {
                var element = (UIElement) sender;
                var p2 = e.GetPosition(DrawingGrid);
                Canvas.SetLeft(element, p2.X + 25 - DragStart.Value.X);
                Canvas.SetTop(element, p2.Y + 25 - DragStart.Value.Y);
            }
        }

        private void VertexReleased(object sender, MouseButtonEventArgs e)
        {
            var element = sender as Ellipse;
            DragStart = null;
            element.ReleaseMouseCapture();
        }

        private void VertexClicked(object sender, MouseButtonEventArgs e)
        {
            var selectedVertex = sender as Ellipse;
            switch (currentToolSet)
            {
                case ToolSet.Remove:
                    DeleteEllipse(selectedVertex);
                    break;
                case ToolSet.Move:
                    DragStart = e.GetPosition(selectedVertex);
                    selectedVertex.CaptureMouse();
                    break;
                case ToolSet.Edge:
                    
                    break;
                case ToolSet.Color:
                    
                    break;
            }
        }
        
        
        private void AddVertex(double x, double y)
        {
            Ellipse temp = MakeEllipse();
            temp.Fill = new SolidColorBrush(colPic.SelectedColor);
            Canvas.SetLeft(temp, x);
            Canvas.SetTop(temp, y);
            _ellipses.Add(temp);
            DrawEllipses();
        }

        private void DrawEllipses()
        {
            foreach (var node in _ellipses.Where(node => !DrawingGrid.Children.Contains(node)))
            {
                DrawingGrid.Children.Add(node);
            }
            RecalculateInfoPanel();
        }
        
        private void DeleteEllipse(Ellipse oop)
        {
            if (DrawingGrid.Children.Contains(oop))
            {
                _ellipses.Remove(oop);
                DrawingGrid.Children.Remove(oop);
            }
            RecalculateInfoPanel();
        }

        private void RecalculateInfoPanel()
        {
            int numVertices = _ellipses.Count;
            int numEdges = 0;
            int connectivity = 0;
            NumVerticesTextBlock.Text = "n = " + numVertices;
            NumEdgesTextBlock.Text = "m = " + numEdges;
            NumConnectivityTextBlock.Text = "k = ";
        }
        
        private void DrawingGrid_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool mouseOver = false;
            for (int i = 0; i < DrawingGrid.Children.Count; i++)
            {
                if (DrawingGrid.Children[i].IsMouseOver)
                {
                    mouseOver = true;
                }
            }

            if (mouseOver == false && currentToolSet == ToolSet.Vertex)
            {
                AddVertex(e.GetPosition(DrawingGrid).X, e.GetPosition(DrawingGrid).Y);
            }
        }

        private void CloseButtonOnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ResetButtons()
        {
            switch (currentToolSet)
            {
                case ToolSet.Move:
                    VertexToolButton.IsChecked = false;
                    EdgeToolButton.IsChecked = false;
                    ColorToolButton.IsChecked = false;
                    RemoveToolButton.IsChecked = false;
                    break;
                case ToolSet.Edge:
                    VertexToolButton.IsChecked = false;
                    MoveToolButton.IsChecked = false;
                    ColorToolButton.IsChecked = false;
                    RemoveToolButton.IsChecked = false;
                    break;
                case ToolSet.Color:
                    MoveToolButton.IsChecked = false;
                    VertexToolButton.IsChecked = false;
                    EdgeToolButton.IsChecked = false;
                    RemoveToolButton.IsChecked = false;
                    break;
                case ToolSet.Vertex:
                    MoveToolButton.IsChecked = false;
                    EdgeToolButton.IsChecked = false;
                    ColorToolButton.IsChecked = false;
                    RemoveToolButton.IsChecked = false;
                    break;
                case ToolSet.Remove:
                    MoveToolButton.IsChecked = false;
                    VertexToolButton.IsChecked = false;
                    EdgeToolButton.IsChecked = false;
                    ColorToolButton.IsChecked = false;
                    break;
            }
        }
        private void MoveToolButton_OnChecked(object sender, RoutedEventArgs e)
        {
            currentToolSet = ToolSet.Move;
            ResetButtons();
        }
        private void VertexToolButton_OnChecked(object sender, RoutedEventArgs e)
        {
            currentToolSet = ToolSet.Vertex;
            ResetButtons();
        }
        private void EdgeToolButton_OnChecked(object sender,RoutedEventArgs e)
        {
            currentToolSet = ToolSet.Edge;
            ResetButtons();
        }
        private void ColorToolButton_OnChecked(object sender,RoutedEventArgs e)
        {
            currentToolSet = ToolSet.Color;
            ResetButtons();
        }
        private void RemoveToolButton_OnChecked(object sender,RoutedEventArgs e)
        {
            currentToolSet = ToolSet.Remove;
            ResetButtons();
        }
    }
}