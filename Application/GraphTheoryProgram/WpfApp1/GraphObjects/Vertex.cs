
using System.Drawing;
using Point = System.Windows.Point;

namespace WpfApp1.GraphObjects;

public class Vertex
{
    private string label = "";
    
    private Color fillColor { get; set;}
    private Point center { get;}

    private bool isSelected;
    
    public static int radius = 25;

    public Vertex()
    {
        
    }

}