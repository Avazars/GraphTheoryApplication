using System;
using System.Windows;
using System.Windows.Shapes;

namespace WpfApp1.GraphObjects;

public class Edge
{
    private bool isLoop;
    private bool isDirected { get;}
    private Vertex fromVertex { get;}
    private Vertex toVertex { get;}

    private Point startPoint { get; set; }
    private Point endPoint { get; set; }
    public Point getStartPoint()
    {
        return startPoint;
    }
    public Point getEndPoint()
    {
        return endPoint;
    }

    private Path edgePath;

    public Path GetPath()
    {
        return edgePath;
    }
    public void UpdatePath(Path Replacement)
    {
        edgePath = Replacement;
    }

    public bool GetIsLoop()
    {
        return isLoop;
    }
    
    public Edge(bool isLoop, bool isDirected, Vertex fromVertex, Vertex toVertex, ref Path edgePath)
    {
        this.isLoop = isLoop;
        this.isDirected = isDirected;
        this.fromVertex = fromVertex;
        this.toVertex = toVertex;
        this.edgePath = edgePath;
        this.startPoint = fromVertex.getPosition();
        this.endPoint = toVertex.getPosition();
        fromVertex.AddConnectedEdge(this);
        toVertex.AddConnectedEdge(this);
        fromVertex.PositionChanged += FromVertexMoved;
        toVertex.PositionChanged += ToVertexMoved;
    }

    private void FromVertexMoved(object? sender, EventArgs e)
    {
        var temp = sender as Vertex;
        startPoint = temp.getPosition();
        InvokeStartPositionChanged();
    }

    private void ToVertexMoved(object? sender, EventArgs e)
    {
        var temp = sender as Vertex;
        endPoint = temp.getPosition();
        InvokeEndPositionChanged();
    }
    
    protected virtual void InvokeStartPositionChanged()
    {
        //Console.WriteLine("X: "+ posX +"Y: "+ posY);
        StartPointUpdated?.Invoke(this, EventArgs.Empty);
    }
    protected virtual void InvokeEndPositionChanged()
    {
        //Console.WriteLine("X: "+ posX +"Y: "+ posY);
        EndPointUpdated?.Invoke(this, EventArgs.Empty);
    }
    
    public event EventHandler StartPointUpdated;
    public event EventHandler EndPointUpdated;

    public void Deconstruct()
    {
        fromVertex.RemoveConnectedEdge(this);
        toVertex.RemoveConnectedEdge(this);
    }
}