using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
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
            Color,
            Info
        }


        private Dictionary<Ellipse, Vertex> _verticesDictionary = new();
        private Dictionary<Path, Edge> _edgesDictionary = new();

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
        private void TopPanelMouseDown(object sender, MouseEventArgs e)
        {
            this.DragMove();
        }
        
        private void VertexDragged(object sender, MouseEventArgs e)
        {
            if (DragStart != null && e.LeftButton == MouseButtonState.Pressed)
            {
                Ellipse element = sender as Ellipse;
                var p2 = e.GetPosition(DrawingGrid);
                _verticesDictionary[element].updatePosition(p2.X, p2.Y);
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
                    DeleteVertex(ClickedVertex);
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
                        AddEdge(ClickedVertex);
                        SelectedVertex.Stroke = new SolidColorBrush(Colors.White);
                        SelectedVertex = null;
                    }
                    break;
                case ToolSet.Color:
                    ClickedVertex.Fill = new SolidColorBrush(colPic.SelectedColor);
                    break;
                case ToolSet.Info:
                    break;
            }
        }
        private void EdgeClicked(object sender, MouseButtonEventArgs e)
        {
            var ClickedLine = sender as Path;
            switch (currentToolSet)
            {
                case ToolSet.Move:
                    //DragStart = e.GetPosition(ClickedLine);
                    //ClickedLine.CaptureMouse();
                    break;
                case ToolSet.Vertex:
                    break;
                case ToolSet.Edge:
                    break;
                case ToolSet.Remove:
                    DeleteEdge(ClickedLine);
                    break;
                case ToolSet.Color:
                    //ClickedLine.Stroke = new SolidColorBrush(colPic.SelectedColor);
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
            Vertex vert = new Vertex(x, y);
            
            Ellipse temp = MakeEllipse();
            temp.Fill = new SolidColorBrush(colPic.SelectedColor);
            Canvas.SetLeft(temp, x);
            Canvas.SetTop(temp, y);
            
            _verticesDictionary.Add(temp,vert);
            DrawingGrid.Children.Add(temp);
            Canvas.SetZIndex(temp, 10);
            RecalculateInfoPanel();
        }
        private void AddEdge(Ellipse ClickedVertex)
        {
            bool isLoop = false;
            Point startPoint = new Point(Canvas.GetLeft(SelectedVertex), Canvas.GetTop(SelectedVertex));
            Point endPoint = new Point(Canvas.GetLeft(ClickedVertex), Canvas.GetTop(ClickedVertex));
            
            
            if (SelectedVertex == ClickedVertex)
            {
                isLoop = true; 
            }
            Path temp = MakePath(startPoint, endPoint, isLoop);
            Edge edge = new Edge(isLoop, false,_verticesDictionary[SelectedVertex],_verticesDictionary[ClickedVertex], ref temp);
            edge.EndPointUpdated += updateEdgeTo;
            edge.StartPointUpdated += updateEdgeFrom;
            _edgesDictionary.Add(temp, edge);
            temp.MouseLeftButtonDown += EdgeClicked;
            temp.MouseMove += EdgeDragged;
            temp.MouseLeftButtonUp += EdgeReleased;
            DrawingGrid.Children.Add(temp);
            Canvas.SetZIndex(temp, 0);
            RecalculateInfoPanel();
        }
        private void updateEdgeFrom(object? sender, EventArgs e)
        {
            var temp = sender as Edge;
            DrawingGrid.Children.Remove(temp.GetPath());
            temp.UpdatePath(MakePath(temp.getStartPoint(), temp.getEndPoint(), temp.GetIsLoop()));
            DrawingGrid.Children.Add(temp.GetPath());
        }
        private void updateEdgeTo(object? sender, EventArgs e)
        {
            var temp = sender as Edge;
            DrawingGrid.Children.Remove(temp.GetPath());
            temp.UpdatePath(MakePath(temp.getStartPoint(), temp.getEndPoint(), temp.GetIsLoop()));
            DrawingGrid.Children.Add(temp.GetPath());
        }
        private Path MakePath(Point start, Point end, bool isLoop)
        {
            Path temp = new Path()
            {
                Stroke = new SolidColorBrush(colPic.SelectedColor), StrokeThickness = 6
            };

            if (isLoop)
            {
                Point origin = new Point(ellipseSize / 1.5 + start.X, ellipseSize / 1.5 + start.Y);
                temp.Data = new EllipseGeometry(origin,ellipseSize,ellipseSize);
            }
            else
            {
                var v1 = new Vector(start.X, start.Y);
                var v2 = new Vector(end.X, end.Y);
                var d = v2 - v1;
                var angle = Math.Atan2(d.Y, d.X) / Math.PI * 180.0;
                var length = d.Length / 2.0;
                            
                PathFigure pf = new PathFigure()
                {
                    StartPoint = start,
                    Segments = new PathSegmentCollection()
                    {
                        new ArcSegment()
                        {
                            RotationAngle = angle,
                            Size = new Size(length,30), 
                            Point = end,
                        }
                    }
                };
                PathGeometry pg = new PathGeometry
                {
                    Figures = new PathFigureCollection() {pf},
                };
                temp.Data = pg;
                
            }
            return temp;
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
        private void DeleteVertex(Ellipse oop)
        {
            if (DrawingGrid.Children.Contains(oop))
            {
                foreach (var edge in _verticesDictionary[oop].GetConnectedEdges())
                {
                    if (DrawingGrid.Children.Contains(edge.GetPath()))
                    {
                        _edgesDictionary.Remove(edge.GetPath());
                        DrawingGrid.Children.Remove(edge.GetPath());
                    }
                }
                _verticesDictionary[oop].GetConnectedEdges().Clear();
                _verticesDictionary.Remove(oop);
                DrawingGrid.Children.Remove(oop);
            }
            RecalculateInfoPanel();
        }
        
        private void DeleteEdge(Path pt)
        {
            if (DrawingGrid.Children.Contains(pt))
            {
                _edgesDictionary[pt].Deconstruct();
                _edgesDictionary.Remove(pt);
                DrawingGrid.Children.Remove(pt);
            }
            RecalculateInfoPanel();
        }
        private void RecalculateInfoPanel()
        {
            int numVertices = _verticesDictionary.Count;
            int numEdges = _edgesDictionary.Count;
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
        private void InfoToolButton_OnChecked(object sender, RoutedEventArgs e)
        {
            currentToolSet = ToolSet.Info;
            ResetButtons();
        }
    }
}