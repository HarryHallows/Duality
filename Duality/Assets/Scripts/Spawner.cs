using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private FloorTile floor;
    [SerializeField] private WallTile wall;
    [SerializeField] private Transform[] wallPositions;
    [SerializeField] private int gridX, gridZ;
    [SerializeField] private int tileOffset;

    public Vector3 gridOrigin = Vector3.zero;
    public bool floorPlaced, wallPlaced;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFloor(PoolObjectType.FLOOR));
    }

    public IEnumerator SpawnFloor(PoolObjectType _type)
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                //Debug.Log(floor.prefab = ObjectPool.Instance.GetPooledObjects());
                //floor.prefab = ObjectPool.Instance.GetPooledObject();
                floor.prefab = PoolManager.Instance.GetPooledObject(_type);

                if (floor.prefab != null)
                {
                    floor.prefab.transform.position = new Vector3(x * tileOffset, 50, z * tileOffset) + gridOrigin;
                    floor.prefab.SetActive(true);
                }

                if (x == gridX - 1 && z == gridZ - 1)
                {
                    floorPlaced = true;
                    StartCoroutine(SpawnWalls(PoolObjectType.WALL));
                }
                yield return new WaitForSeconds(.15f);

                //IF I wanted to directly pass back the pooled object back to the specific pool
                //PoolManager.Instance.PoolObject(floor.prefab, _type);
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
