
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Connectable : MonoBehaviour, IGridElement
{
    public GridElement ElementType { get; protected set; }
    private List<Link> links = new List<Link>();

    public int GetHopCount()
    {
        return links.First(l => l.linkState == LinkState.RootPort).hopCount;
    }
    public void AddLink(Connectable element, LinkState state)
    {
        links.Add(new Link() { connected = element, linkState = state, hopCount = -1 });
    }
    protected void ConnectToElement(IGridElement element)
    {
        if (typeof(Connectable).IsAssignableFrom(element.GetType()))
        {
            Connectable connected = (Connectable)element;
            links.Add(new Link() { connected = connected, hopCount = connected.GetHopCount() + 1 });
        }
    }
    protected void UpdateNewConnections()
    {
        int shortestPath = int.MaxValue;
        int rootIndex = -1;
        for (int i = 0; i < links.Count; i++)
        {
            if (links[i].hopCount < shortestPath)
            {
                shortestPath = links[i].hopCount;
                rootIndex = i;
            }
        }
        if (rootIndex == -1) return;
        links[rootIndex].linkState = LinkState.RootPort;
        foreach (Link link in links)
        {
            link.connected.AddLink(this, link.linkState == LinkState.Blocking ? LinkState.Blocking : LinkState.DesignatedPort);
        }
    }
}
public class Link
{
    public Connectable connected;
    public LinkState linkState { get; set; }
    public int hopCount;
}