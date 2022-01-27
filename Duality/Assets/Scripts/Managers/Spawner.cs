using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private FloorTile floor;
    [SerializeField] private WallTile wall;
    [SerializeField] private Transform[] wallPositions;
    [SerializeField] private int gridX, gridZ;
    [SerializeField] private int tileOffset;

    [SerializeField] private List<GameObject> floorObjects;
    public int tileCount = 0;

    public Vector3 gridOrigin = Vector3.zero;
    public bool floorPlaced, wallPlaced;

    public bool debugFloorSpawning;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        floorObjects = new List<GameObject>();
        StartCoroutine(SpawnFloor(PoolObjectType.FLOOR));
    }

    private void Update()
    {
        if (debugFloorSpawning)
        {
            StartCoroutine(RemoveFloor());
            debugFloorSpawning = false;
            return;
        }
    }

    public IEnumerator SpawnFloor(PoolObjectType _type)
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                //Debug.Log(floor.prefab = ObjectPool.Instance.GetPooledObjects());
                floor.prefab = PoolManager.Instance.GetPooledObject(_type);

                if (floorObjects.Count <= 0)
                {
                    floor.prefab = PoolManager.Instance.GetPooledObject(_type);
                }

                if (floor.prefab != null)
                {
                    floor.prefab.transform.position = new Vector3(x * tileOffset, 50, z * tileOffset) + gridOrigin;
                    floor.prefab.SetActive(true);

                    if (PoolManager.Instance.listOfPool[0].amount >= floorObjects.Count)
                    {
                        floorObjects.Add(floor.prefab);
                    }
                }

                if (x == gridX - 1 && z == gridZ - 1)
                {
                    floorPlaced = true;

                    if (gm.outsideScene && floorPlaced)
                    {
                        gm.EnvrionmentRotate(true);
                    }
                    else
                    {
                        StartCoroutine(SpawnWalls(PoolObjectType.WALL));
                    }

                }
                yield return new WaitForSeconds(.15f);

                //IF I wanted to directly pass back the pooled object back to the specific pool
                //PoolManager.Instance.PoolObject(floor.prefab, _type);
            }
        }

    }

    public void RemoveFloorActivate()
    {
        StartCoroutine(RemoveFloor());
    }

    public IEnumerator RemoveFloor()
    {
        bool _checkIteration = true;

        for (int i = 0; i < floorObjects.Count; i++)
        {
            if (_checkIteration)
            {
                tileCount = i;
            }

            gm.EnvrionmentRotate(true);
            floorPlaced = false;

            floorObjects[i].GetComponent<ItemController>().remove = true;

            if (floorObjects[i].transform.position.y > -10)
            {

            }
            else
            {
                Debug.Log("returning items to pool");
                #region Legacy Code
                //floorObjects[i].SetActive(false);
                //PoolManager.Instance.PoolObject(floor.prefab, _type);
                //floorObjects.Remove(floorObjects[i]);
                #endregion
            }
            yield return new WaitForSeconds(.15f);

            if (tileCount == floorObjects.Count - 1)
            {
                StartCoroutine(SpawnFloor(PoolObjectType.FLOOR));
                tileCount = 0;
                debugFloorSpawning = false;
                _checkIteration = false;
                yield return null;
            }
        }
    }

    private IEnumerator SpawnWalls(PoolObjectType _type)
    {
        for (int i = 0; i < wallPositions.Length; i++)
        {
            //Debug.Log(wall = ObjectPool.Instance.GetPooledObject());
            wall.prefab = PoolManager.Instance.GetPooledObject(_type);


            if (wall.prefab != null)
            {
                wall.prefab.transform.position = new Vector3(wallPositions[i].transform.position.x, 80, wallPositions[i].transform.position.z);
                wall.prefab.transform.rotation = wallPositions[i].transform.rotation;
                wall.prefab.SetActive(true);
            }

            yield return new WaitForSeconds(.25f);

            //IF I wanted to directly pass back the pooled object back to the specific pool
            //PoolManager.Instance.PoolObject(wall.prefab, _type);
        }

    }
}
