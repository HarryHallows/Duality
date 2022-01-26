using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private FloorTile floor;

    [SerializeField] private int gridX, gridZ;
    [SerializeField] private int tileOffset;

    public Vector3 gridOrigin = Vector3.zero;
    public bool floorPlaced, wallPlaced;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFloor());
    }

    public IEnumerator SpawnFloor()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                Debug.Log($"{z}: Z = {gridZ} : {x}: X = {gridX} ");
                floor.prefab = ObjectPool.Instance.GetPooledObject();

                if (floor.prefab != null)
                {
                    floor.prefab.transform.position = new Vector3(x * tileOffset, 50, z * tileOffset) + gridOrigin;
                    floor.prefab.SetActive(true);
                }

                if (x == gridX - 1 && z == gridZ - 1)
                {
                    floorPlaced = true;
                }
                yield return new WaitForSeconds(.25f);
            }
        }

    }

    private IEnumerator SpawnWalls()
    {
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
