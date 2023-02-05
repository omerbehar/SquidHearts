using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Root : MonoBehaviour
{

    private Node trailRecorded;
    //public Transform targetP;
    public Vector3 lastTargetPoint;
    //public Vector3 target_Offset;
    [SerializeField] private Vector3 offset;
    const int MaxPositions = 10000;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();
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

    private void Start()
    {
        // trailRecorded = Grid.calculatedTrail;
        // lastTargetPoint = trailRecorded.position;
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

    void Update()
    {
        // int numberOfPositions = target.GetPositions(new NativeArray<Vector3>());
        //
        // foreach (Node child in trailRecorded.children)
        // {
        //     
        // }
        // speed += 0.25f * Time.deltaTime;
        // if (currentPosition < this.trailRecorded.Length)
        // {
        //     if (lastTargetPoint == null)
        //         lastTargetPoint = trailRecorded[currentPosition];
        //     follow();
        // }
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

    // void follow()
    // {
    //     transform.forward = Vector3.RotateTowards(transform.forward, lastTargetPoint - transform.position, speed * Time.deltaTime, 4.0f);
    //  
    //     transform.position = Vector3.MoveTowards(transform.position, lastTargetPoint, speed * Time.deltaTime);
    //     if (transform.position == lastTargetPoint)
    //     {
    //         currentPosition++;
    //         lastTargetPoint = trailRecorded[currentPosition];
    //     }
    // }
    
}
