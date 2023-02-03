using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshJoiner : MonoBehaviour
{
    private Mesh mesh;
    private void Awake()
    {
        // Create a new mesh
        mesh = new Mesh();
        JoinMeshes();
    }
    // Get the vertices and triangles from the cubes
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    [SerializeField] private List<GameObject> cubes;

    private void JoinMeshes()
    {
        // Iterate through all the cubes
        foreach (GameObject cube in cubes)
        {
            // Get the mesh filter of the cube
            MeshFilter filter = cube.GetComponent<MeshFilter>();
            // Add the vertices and triangles to the list
            var prevCount = vertices.Count;
            vertices.AddRange(filter.sharedMesh.vertices.Select(cube.transform.TransformPoint));
            triangles.AddRange(filter.sharedMesh.triangles.Select(i => i + prevCount));
        }
        // Set the mesh's vertices and triangles
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        // Recalculate the mesh's normals
        mesh.RecalculateNormals();

        // Create a mesh filter and renderer for the mesh
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        //Instantiate(meshRenderer);
    }
}
