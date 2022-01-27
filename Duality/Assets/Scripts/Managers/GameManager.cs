using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject floorTilePrefab;
    [SerializeField] private GameObject wallTilePrefab;

    private Spawner spawner;

    public bool combatState;

    public bool rotateEnvironment;
    public int wallPlacedCount;


    [SerializeField] private bool wallPooled;

    public bool outsideScene;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        spawner = FindObjectOfType<Spawner>();
        SpawnPooledFloor();
    }

    public bool Combat(bool _combatState)
    {
        return combatState = _combatState;
    }

    public bool EnvrionmentRotate(bool _envrionmentReady)
    {
        return rotateEnvironment = _envrionmentReady;
    }

    private void SpawnPooledFloor()
    {
        //ObjectPool.Instance.objectToPool = floorTilePrefab;
        //ObjectPool.Instance.amountToPool = 64;
    }

    private void Update()
    {
        if (spawner.floorPlaced && !spawner.wallPlaced && !wallPooled)
        {
            SpawnPooledWalls();
        }
    }

    private void SpawnPooledWalls()
    {
        //ObjectPool.Instance.pooledObjects.Clear();
        //ObjectPool.Instance.objectToPool = wallTilePrefab;
        //ObjectPool.Instance.amountToPool = 2;

        //ObjectPool.Instance.AddObjectsToScene();
        wallPooled = true;
        return;
    }
}
