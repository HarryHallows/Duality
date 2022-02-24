using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private FloorTile floor;
    [SerializeField] private WallTile wall;
    [SerializeField] private List<GameObject> areaPrefabs;
    [SerializeField] private Transform[] wallPositions;
    [SerializeField] private int gridX, gridZ;
    [SerializeField] private int tileOffset;

    [SerializeField] private List<GameObject> floorObjects, wallObjects;
    [SerializeField] private GameObject area;

    public int tileCount = 0;

    public Vector3 gridOrigin = Vector3.zero;
    public bool floorPlaced, wallPlaced;

    public bool floorRespawn = false;

    [SerializeField] private float floorRespawnTimer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        floorObjects = new List<GameObject>();
        wallObjects = new List<GameObject>();
        areaPrefabs = new List<GameObject>();
        StartCoroutine(SpawnFloor(PoolObjectType.FLOOR));
    }

    private void Update()
    {
        if (floorRespawn)
        {
            if (floorObjects.Count >= 65)
            {
                floorObjects.RemoveAt(64);
            }

            floorRespawnTimer -= Time.deltaTime;

            if (floorRespawnTimer <= 0)
            {
                StartCoroutine(SpawnFloor(PoolObjectType.FLOOR));
                tileCount = 0;
                floorRespawnTimer = 2f;
                area = null;
                floorRespawn = false;
                return;
            }
        }
    }

    public IEnumerator SpawnFloor(PoolObjectType _type)
    {
        gm.CurrentArea(gm.areas);

        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                floor.prefab = PoolManager.Instance.GetPooledObject(_type);

                if (floor.prefab != null)
                {
                    floor.prefab.transform.position = new Vector3(x * tileOffset, 50, z * tileOffset) + gridOrigin;
                    floor.prefab.SetActive(true);

                    if (floorObjects.Count <= 64)
                    {
                        floorObjects.Add(floor.prefab);
                    }
                }

                if (x == gridX - 1 && z == gridZ - 1)
                {
                    if (PoolManager.Instance.listOfPool[0].amount > 0)
                    {
                        PoolManager.Instance.listOfPool[0].pool.Clear();
                    }

                    floorPlaced = true;

                    if (!gm.outsideScene && floorPlaced)
                    {
                        StartCoroutine(SpawnWalls(PoolObjectType.WALL));
                    }

                    switch (gm.areas)
                    {
                        case GameAreas.GUIDEPOST:
                            //Spawn GuidePost
                            StartCoroutine(SpawnGuidePost(PoolObjectType.GUIDEPOST));
                            break;
                        case GameAreas.HOUSE_ENTRACE:
                            //Spawn House Game Areas
                            StartCoroutine(SpawnHouseEntrance(PoolObjectType.HOUSE_ENTRANCE));
                            break;
                        case GameAreas.PARK:
                            //Spawn Park Game Area
                            break;
                        case GameAreas.HOSPITAL:
                            //Spawn Hospital Game Area
                            break;
                    }

                }
                yield return new WaitForSeconds(.15f);
            }
        }

    }

    private IEnumerator SpawnGuidePost(PoolObjectType _type)
    {
        area = PoolManager.Instance.GetPooledObject(_type);

        if (!areaPrefabs.Contains(area))
        {
            areaPrefabs.Add(area);
        }

        if (areaPrefabs[0] != null)
        {
            areaPrefabs[0].transform.position = new Vector3(0, 50, 0);
            areaPrefabs[0].SetActive(true);
            yield return null;
        }
    }

    private IEnumerator SpawnHouseEntrance(PoolObjectType _type)
    {

        //temporary gameobject that looks for the designated reference type to decide which gameobject it is assigned
        area = PoolManager.Instance.GetPooledObject(_type);

        if (!areaPrefabs.Contains(area))
        {
            //Takes the current pooled object type and adds it to the spawner area prefab list for referencing
            areaPrefabs.Add(area);
        }

        if (areaPrefabs[1] != null)
        {
            areaPrefabs[1].transform.position = new Vector3(0, 50, 0);
            areaPrefabs[1].SetActive(true);
            yield return null;
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

                if (wallObjects.Count < 2)
                {
                    wallObjects.Add(wall.prefab);
                }
            }

            yield return new WaitForSeconds(.25f);

            //IF I wanted to directly pass back the pooled object back to the specific pool
            //PoolManager.Instance.PoolObject(wall.prefab, _type);
        }

    }

    #region ResetScene
    public void RemoveFloorActivate(PoolObjectType _type)
    {
        StartCoroutine(RemoveFloor(_type));
    }

    public IEnumerator RemoveFloor(PoolObjectType _type)
    {
        bool _checkIteration = true;

        for (int i = 0; i < floorObjects.Count; i++)
        {
            if (_checkIteration)
            {
                tileCount = i;
            }
            floorPlaced = false;
            floorObjects[i].GetComponent<ItemController>().poolObj = PoolObjectType.FLOOR;
            floorObjects[i].GetComponent<ItemController>().remove = true;
            yield return new WaitForSeconds(.15f);

            if (tileCount == floorObjects.Count - 1)
            {
                //TODO Need a check for it the walls are currently active
                _checkIteration = false;
                if (!gm.outsideScene)
                {
                    StartCoroutine(RemoveArea(_type));
                    yield return null;
                }
                else
                {
                    StartCoroutine(RemoveWalls(_type));
                    yield return null;
                }
                /* StartCoroutine(RemoveArea(_type)); */
            }
        }
    }

    public IEnumerator RemoveWalls(PoolObjectType _type)
    {
        for (int i = 0; i < wallObjects.Count; i++)
        {
            wallObjects[i].GetComponent<ItemController>().poolObj = PoolObjectType.WALL;
            wallObjects[i].GetComponent<ItemController>().remove = true;

            yield return new WaitForSeconds(.1f);
        }

        StartCoroutine(RemoveArea(_type));
        yield return new WaitForSeconds(1f);
        wallPlaced = false;

    }

    private IEnumerator RemoveArea(PoolObjectType _type)
    {
        area.GetComponent<ItemController>().poolObj = _type;
        area.GetComponent<ItemController>().remove = true;
        yield return new WaitForSeconds(.5f);
        floorRespawn = true;
        yield return null;
    }
    #endregion

}
