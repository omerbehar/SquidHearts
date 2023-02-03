using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlobSpawner : MonoBehaviour
{
    [SerializeField] private List<Blob> blobPrefabs = new List<Blob>();
    
    [SerializeField] private List<Blob> levelDesignBlobSequence = new List<Blob>();

    private int levelDesignBlobIndex;

    [SerializeField] private bool isRandom;
    
    private Blob newBlobPrefab;
    
    void Awake()
    {
        InitListeners();
    }

    private void Start()
    {
        InstantiateBlob();
    }

    private void InitListeners()
    {
        RemoveListeners();
        EventManager.NextBlobRequested.AddListener(InstantiateBlob);
    }

    private void RemoveListeners()
    {
        EventManager.NextBlobRequested.RemoveListener(InstantiateBlob);
    }

    private void InstantiateBlob()
    {
        newBlobPrefab = isRandom ? GetRandomBlobPrefab() : GetLevelDesignBlobPrefab();
        Blob newBlob = Instantiate(newBlobPrefab, transform, false);
        EventManager.BlobCreated.Invoke(newBlob);
    }

    private Blob GetRandomBlobPrefab() //random, can be changed to level design
    {
        return blobPrefabs[Random.Range(0, blobPrefabs.Count)];
    }

    private Blob GetLevelDesignBlobPrefab()
    {
        Blob blob = levelDesignBlobSequence[levelDesignBlobIndex];
        levelDesignBlobIndex++;
        return blob;
    }
}
