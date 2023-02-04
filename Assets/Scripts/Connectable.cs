
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Connectable : MonoBehaviour, IGridElement
{
    [field: SerializeField] public GridElementType ElementType { get; protected set; }
    private List<Link> links = new List<Link>();

    public int GetHopCount()
    {
        Link newLink = links.FirstOrDefault(l => l.linkState == LinkState.RootPort);
        return newLink?.hopCount ?? 1000;
    }
    
    protected void OnConnected(IGridElement element)
    {
        if (element == null) return;
        if (typeof(Connectable).IsAssignableFrom(element.GetType()))
        {
            Connectable connected = (Connectable)element;
            links.Add(new Link() { neighbor = connected, hopCount = connected.GetHopCount() + 1 });
        }
    }
    protected void SetNewConnectionsStatus()
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
        for (int i = 0; i < links.Count; i++)
        {
            if (i == rootIndex)
                links[i].linkState = LinkState.RootPort;
            else links[i].linkState = LinkState.Blocking;
        }
        foreach (Link link in links)
        {
            link.neighbor.AddToLinkList(this, link.linkState == LinkState.Blocking ? LinkState.Blocking : LinkState.DesignatedPort);
        }
    }
    protected void OnDestroyCollectable()
    {
        foreach(Link link in links)
        {
            link.reconfigureFlag = true;
        }
        foreach (Link link in links)
        {
            link.neighbor.RemoveFromLinkList(this);
        }
    }
    private void AddToLinkList(Connectable element, LinkState state)
    {
        links.Add(new Link() { neighbor = element, linkState = state, hopCount = -1 });
    }
    private void RemoveFromLinkList(Connectable neighbor)
    {
        Link removedLink = links.FirstOrDefault(l => l.neighbor == neighbor);
        if (removedLink == null) return;
        links.Remove(removedLink);
        if (removedLink.linkState != LinkState.RootPort) return;
        bool isPing = false;
        foreach (Link link in links)
        {
            link.reconfigureFlag = true;
        }
        foreach (Link link in links)
        {
            isPing = link.neighbor.PingRoot(this);
            if (isPing) break;
        }
        if (!isPing)
        {
            foreach (Link link in links)
            {
                link.neighbor.RemoveFromLinkList(this);
            }
                DestroyConnectable();
            return;
        }
            foreach (Link link in links)
        {
            link.reconfigureFlag = false;
        }
    }
    private bool PingRoot(Connectable neighbor)
    {
        if(this.GetType() == typeof(ImmovableConnectable))
        {
            return true;
        }
        bool isPing = false;
        Link origin = links.FirstOrDefault(l => l.neighbor = neighbor);
        if (origin != null) origin.reconfigureFlag = true;
        foreach (Link link in links)
        {
            isPing = link.neighbor.PingRoot(this);
            if (isPing) break;
        }
        if (origin != null) origin.reconfigureFlag = false;
        return isPing;
    }
    private void DestroyConnectable()
    {
        //TODO
    }
}
public class Link
{
    public Connectable neighbor;
    public LinkState linkState { get; set; }
    public int hopCount;
    public bool reconfigureFlag = false;
}