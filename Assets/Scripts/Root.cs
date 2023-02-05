using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Root : MonoBehaviour
{

    private Node trailRecorded;

    [SerializeField] private Vector3 offset;

    private List<LineRenderer> lineRenderers = new();
    private void Awake()
    {
        EventManager.PartAddedToGrid.AddListener(OnPartAddedToGrid);
    }

    private LineRenderer CreateLineRendererChild()
    {
        GameObject go = new GameObject("LineRenderer");
        go.transform.SetParent(transform);
        LineRenderer lineRenderer = go.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;
        lineRenderers.Add(lineRenderer);
        return lineRenderer;
    }


    private void OnPartAddedToGrid()
    {
        foreach (LineRenderer lineRenderer in lineRenderers)
        {
            Destroy(lineRenderer.gameObject);
        }
        lineRenderers.Clear();
        CreateLineRendererChild();
        trailRecorded = Grid.calculatedTrail;
        if (trailRecorded != null)
        {
            RunNode(trailRecorded, lineRenderers[0]);
        }
    }
    
    private void RunNode(Node node, LineRenderer lineRenderer)
    {
        switch (node.children.Count)
        {
            case 0:
                DrawTrail(lineRenderer, node.position);
                return;
            case 1:
                DrawTrail(lineRenderer, node.position);
                RunNode(node.children[0], lineRenderer);
                return;
            default:
                DrawTrail(lineRenderer, node.position);
                for (int i = 0; i < node.children.Count; i++) 
                {
                    if (i != 0)
                    {
                        LineRenderer newLineRenderer = CreateLineRendererChild();
                        DrawTrail(newLineRenderer, node.position);
                        RunNode(node.children[i], newLineRenderer);
                    } 
                    RunNode(node.children[i], lineRenderer);
                }
                return;
        }
    }

    private void DrawTrail(LineRenderer lineRenderer, Vector3Int position)
    {
        Debug.Log(position);
        Vector3 realWorldPosition = new Vector3(position.x / 2f, position.y / 2f, position.z / 2f);
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, realWorldPosition);
        lineRenderer.positionCount++;
    }
    
}
