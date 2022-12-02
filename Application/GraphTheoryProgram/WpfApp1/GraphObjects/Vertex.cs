using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1.GraphObjects;

public class Vertex
{
    private const int vertexSize = 50;
    private Canvas displayCanvas { get; }

    public Vertex()
    {
        displayCanvas = MakeNewDisplay("");
    }
    
    public Vertex(string label)
    {
        displayCanvas = MakeNewDisplay(label);
    }

    private Canvas MakeNewDisplay(string label)
    {

        Canvas returnCanvas = new Canvas()
        {
            
        };
        
        Ellipse ellipse = new Ellipse()
        {
            Stroke = Brushes.White,
            Fill = Brushes.White,
            StrokeThickness = 2,
            Width = vertexSize,
            Height = vertexSize,
            RenderTransform = new TranslateTransform(-(vertexSize/2),-(vertexSize/2))
        };
        
        TextBlock textBlock = new TextBlock()
        {
                TextAlignment = TextAlignment.Center,
                Text = label
        };

        returnCanvas.Children.Add(ellipse);
        returnCanvas.Children.Add(textBlock);
        
        return returnCanvas;
    }
    
    public void UpdateLabel(string newLabel)
    {
        
    }
    
    
}