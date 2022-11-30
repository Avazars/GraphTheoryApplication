﻿using System.Collections.Generic;
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

        private enum ToolSet
        {
            None,
            Move,
            Vertex,
            Edge,
            Remove,
            Color
        }
        
        private List<Ellipse> _ellipses = new List<Ellipse>();
        private ToolSet currentToolSet;

        
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void topPanelMouseDown(object sender, MouseEventArgs e)
        {
            this.DragMove();
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
        private void MoveToolButton_OnUnChecked(object sender, RoutedEventArgs e)
        {
            currentToolSet = ToolSet.None;
            ResetButtons();
        }
        private void VertexToolButton_OnUnChecked(object sender, RoutedEventArgs e)
        {
            currentToolSet = ToolSet.None;
            ResetButtons();
        }
        private void EdgeToolButton_OnUnChecked(object sender, RoutedEventArgs e)
        {
            currentToolSet = ToolSet.None;
            ResetButtons();
        }
        private void ColorToolButton_OnUnChecked(object sender, RoutedEventArgs e)
        {
            currentToolSet = ToolSet.None;
            ResetButtons();
        }
        private void RemoveToolButton_OnUnChecked(object sender, RoutedEventArgs e)
        {
            currentToolSet = ToolSet.None;
            ResetButtons();
        }
        
        
    }
}