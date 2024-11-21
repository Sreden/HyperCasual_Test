using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelDesignGenerator : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject portalPrefab;

    [SerializeField] private int initialGrounds = 5;
    [SerializeField] private float groundLength = 20f;

    private Queue<GameObject> portals = new();
    private Queue<GameObject> grounds = new();

    private Vector3 groundSpawnPosition;
    private Vector3 portalSpawnPosition;

    public enum LD_Type
    {
        GROUND,
        PORTAL
    }

    void Start()
    {
        groundSpawnPosition = transform.position;
        for (int i = 0; i < initialGrounds; i++)
        {
            SpawnLD(LD_Type.GROUND, ref groundSpawnPosition, groundLength);
        }

        portalSpawnPosition = transform.position + (player.transform.position.z + (initialGrounds - 1) * groundLength) * Vector3.forward;

        SpawnLD(LD_Type.PORTAL, ref portalSpawnPosition, 0);
    }

    void Update()
    {
        portalSpawnPosition = transform.position + (player.transform.position.z + (initialGrounds - 1) * groundLength) * Vector3.forward;

        if (player.transform.position.z > groundSpawnPosition.z - ((initialGrounds - 1) * groundLength))
        {
            SpawnLD(LD_Type.GROUND, RemoveOldLD(LD_Type.GROUND), ref groundSpawnPosition, groundLength); // simple pooling
        }
    }

    void SpawnLD(LD_Type type, ref Vector3 spawnPosition, float nextForwardOffset)
    {
        GameObject newSegment = Instantiate(GetPrefabWithType(type), spawnPosition, Quaternion.identity);
        GetPrefabQueueWithType(type).Enqueue(newSegment);
        spawnPosition += Vector3.forward * nextForwardOffset;
    }

    void SpawnLD(LD_Type type, GameObject alreadySpawnedLD, ref Vector3 spawnPosition, float nextForwardOffset)
    {
        alreadySpawnedLD.transform.position = spawnPosition;
        GetPrefabQueueWithType(type).Enqueue(alreadySpawnedLD);
        spawnPosition += Vector3.forward * nextForwardOffset;
    }

    private GameObject RemoveOldLD(LD_Type type)
    {
        return GetPrefabQueueWithType(type).Dequeue();
    }

    private Queue<GameObject> GetPrefabQueueWithType(LD_Type Type)
    {
        return Type switch
        {
            LD_Type.GROUND => grounds,
            LD_Type.PORTAL => portals,
            _ => grounds,
        };
    }

    private GameObject GetPrefabWithType(LD_Type Type)
    {
        return Type switch
        {
            LD_Type.GROUND => groundPrefab,
            LD_Type.PORTAL => portalPrefab,
            _ => groundPrefab,
        };
    }
}