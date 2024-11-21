using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesignGenerator : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject portalsPrefab;
    [SerializeField] private GameObject[] environmentPrefabs;

    [SerializeField] private int initialGrounds = 5;
    [SerializeField] private float groundLength = 20f;

    private Queue<GameObject> activeGrounds = new();
    private Vector3 nextSpawnPosition;

    void Start()
    {
        nextSpawnPosition = transform.position;

        for (int i = 0; i < initialGrounds; i++)
        {
            SpawnSegment();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activeGrounds.Count == 0)
        {
            return;
        }

        if (player.transform.position.z > nextSpawnPosition.z - ((initialGrounds - 1) * groundLength))
        {
            SpawnSegment();
            RemoveOldSegment();
        }
    }

    void SpawnSegment()
    {
        GameObject newSegment = Instantiate(groundPrefab, nextSpawnPosition, Quaternion.identity);
        activeGrounds.Enqueue(newSegment);
        nextSpawnPosition += Vector3.forward * groundLength;
    }

    private void RemoveOldSegment()
    {
        GameObject oldSegment = activeGrounds.Dequeue();
        Destroy(oldSegment);
    }
}
