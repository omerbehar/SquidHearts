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
    [SerializeField] LineRenderer target;
    public int currentPosition = 0;
    public float speed = 20f;

    const int MaxPositions = 10000;
    private void Start()
    {
        EventManager.PartAddedToGrid.AddListener(OnPartAddedToGrid);
        trailRecorded = Grid.calculatedTrail;
        lastTargetPoint = trailRecorded.position;
    }

    private void OnPartAddedToGrid()
    {
        trailRecorded = Grid.calculatedTrail;
        Debug.Log(trailRecorded.children.Count);
        RunNode(trailRecorded);
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

    private void RunNode(Node node)
    {
        switch (node.children.Count)
        {
            case 0:
                return;
            case 1:
                DrawTrail(node.position);
                return;
            default:
                foreach (Node nodeChild in node.children)
                {
                    RunNode(nodeChild);
                }
                break;
        }
    }

    private void DrawTrail(Vector3Int position)
    {
        target.SetPosition(currentPosition, position / 2);
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
