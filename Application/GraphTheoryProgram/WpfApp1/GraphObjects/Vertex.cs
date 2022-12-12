using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApp1.GraphObjects;

public class Vertex
{
    public event EventHandler PositionChanged;

    private double posX { get; set; }
    private double posY { get; set; }

    private List<Edge> connectedEdges = new();

    public void updatePosition(double newX, double newY)
    {
        posX = newX;
        posY = newY;
        InvokePositionChanged();
    }
    protected virtual void InvokePositionChanged()
    {
        PositionChanged?.Invoke(this, EventArgs.Empty);
    }
    public Point getPosition()
    {
        return new Point(posX, posY);
    }
    public List<Edge> GetConnectedEdges()
    {
        return connectedEdges;
    }
    public void AddConnectedEdge(Edge e)
    {
        connectedEdges.Add(e);
    }
    public void RemoveConnectedEdge(Edge e)
    {
        if (connectedEdges.Contains(e))
            connectedEdges.Remove(e);
    }
    public Vertex(double posX, double posY)
    {
        this.posX = posX;
        this.posY = posY;
    }

    public String GetVertexInfo()
    {
        return "";
    }
}