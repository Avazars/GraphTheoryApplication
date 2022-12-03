using System;
using System.Collections;
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


        private Dictionary<Ellipse, Vertex> _verticesDictionary = new Dictionary<Ellipse, Vertex>();
        
        private List<Vertex> vertices = new List<Vertex>();
        private List<Ellipse> _ellipses = new List<Ellipse>();
        private List<Path> _paths = new List<Path>();
        
        private ToolSet currentToolSet;
        private Point? DragStart = null;
        private Color selectionColor = Colors.DodgerBlue;
        private List<Tuple<Ellipse, Ellipse>> listOfEdges;
        private Ellipse SelectedVertex = null;
        
        private const int ellipseSize = 50;

        public MainWindow()
        {
            InitializeComponent();
            colPic.SelectedColor = Colors.Azure;
            colPic.SecondaryColor = Colors.Black;
        }
        private void topPanelMouseDown(object sender, MouseEventArgs e)
        {
            this.DragMove();
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
            
            var ClickedVertex = sender as Ellipse;
            switch (currentToolSet)
            {
                case ToolSet.Remove:
                    DeleteEllipse(ClickedVertex);
                    break;
                case ToolSet.Move:
                    DragStart = e.GetPosition(ClickedVertex);
                    ClickedVertex.CaptureMouse();
                    break;
                case ToolSet.Edge:
                    if (SelectedVertex == null)
                    {
                        SelectedVertex = ClickedVertex;
                        ClickedVertex.Stroke = new SolidColorBrush(selectionColor);
                    }
                    else
                    {
                        double posX1 =  Canvas.GetLeft(SelectedVertex);
                        double posY1 = Canvas.GetTop(SelectedVertex);
                        
                        double posX2 = Canvas.GetLeft(ClickedVertex);
                        double posY2 = Canvas.GetTop(ClickedVertex);

                        Path temp = new Path() { Stroke = new SolidColorBrush(colPic.SelectedColor), StrokeThickness = 6};
                        
                        if (SelectedVertex == ClickedVertex)
                        {
                            temp.Data = new EllipseGeometry(new Point(ellipseSize/1.5+posX1,ellipseSize/1.5+posY1),ellipseSize,ellipseSize);
                            _paths.Add(temp);
                        }
                        else
                        {
                            PathFigure pf = new PathFigure()
                            {
                                StartPoint = new Point(posX1, posY1),
                                Segments = new PathSegmentCollection()
                                {
                                    new ArcSegment()
                                    {
                                        Size = new Size(50,50), 
                                        Point = new Point(posX2,posY2),
                                    }
                                }
                            };
                            PathGeometry pg = new PathGeometry
                            {
                                Transform = null,
                                Figures = new PathFigureCollection() {pf},
                                FillRule = FillRule.EvenOdd
                            };
                            temp.Data = pg;
                            //temp.Data = new LineGeometry(new Point(posX1,posY1),new Point(posX2,posY2));
                            _paths.Add(temp);
                        }
                        temp.MouseLeftButtonDown += EdgeClicked;
                        temp.MouseMove += EdgeDragged;
                        temp.MouseLeftButtonUp += EdgeReleased;
                        DrawingGrid.Children.Add(temp);
                        Canvas.SetZIndex(temp, 0);
                        RecalculateInfoPanel();
                        
                        SelectedVertex.Stroke = new SolidColorBrush(Colors.Black);
                        SelectedVertex = null;
                    }
                    break;
                case ToolSet.Color:
                    ClickedVertex.Fill = new SolidColorBrush(colPic.SelectedColor);
                    break;
            }
        }
        
        private void RemoveLine(Path pt)
        {
            if (DrawingGrid.Children.Contains(pt))
            {
                _paths.Remove(pt);
                DrawingGrid.Children.Remove(pt);
            }
            RecalculateInfoPanel();
        }
        
        private void EdgeClicked(object sender, MouseButtonEventArgs e)
        {
            var ClickedLine = sender as Path;
            switch (currentToolSet)
            {
                case ToolSet.Move:
                    DragStart = e.GetPosition(ClickedLine);
                    ClickedLine.CaptureMouse();
                    break;
                case ToolSet.Vertex:
                    break;
                case ToolSet.Edge:
                    break;
                case ToolSet.Remove:
                    RemoveLine(ClickedLine);
                    break;
                case ToolSet.Color:
                    ClickedLine.Stroke = new SolidColorBrush(colPic.SelectedColor);
                    break;
            }
        }
        
        private void EdgeDragged(object sender, MouseEventArgs e)
        {
            if (DragStart != null && e.LeftButton == MouseButtonState.Pressed)
            {
                var element = (UIElement) sender;
                var p2 = e.GetPosition(DrawingGrid);
                Canvas.SetLeft(element, p2.X - DragStart.Value.X);
                Canvas.SetTop(element, p2.Y - DragStart.Value.Y);
            }
        }
        
        private void EdgeReleased(object sender, MouseButtonEventArgs e)
        {
            var element = sender as Path;
            DragStart = null;
            element.ReleaseMouseCapture();
        }
        
        private void AddVertex(double x, double y)
        {
            Ellipse temp = MakeEllipse();
            temp.Fill = new SolidColorBrush(colPic.SelectedColor);
            Canvas.SetLeft(temp, x);
            Canvas.SetTop(temp, y);
            _ellipses.Add(temp);
            DrawingGrid.Children.Add(temp);
            Canvas.SetZIndex(temp, 10);
            RecalculateInfoPanel();
        }
        
        private Ellipse MakeEllipse()
        {
            Ellipse ellipse = new Ellipse
            {
                Stroke = Brushes.White,
                Fill = Brushes.White,
                StrokeThickness = 2,
                Width = ellipseSize,
                Height = ellipseSize,
                RenderTransform = new TranslateTransform(-(ellipseSize/2),-(ellipseSize/2))
            };
            ellipse.MouseLeftButtonDown += VertexClicked;
            ellipse.MouseMove += VertexDragged;
            ellipse.MouseLeftButtonUp += VertexReleased;
            
            return ellipse;
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
            int numEdges = _paths.Count;
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