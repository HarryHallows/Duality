using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public FloorTile tile;

    private void Start()
    {
        tile.spawned = true;
    }

    private void Update()
    {
        StartCoroutine(SpawnPosition());
    }

    private IEnumerator SpawnPosition()
    {
        if (transform.position.y > 0)
        {
            Debug.Log("I should move down");
            transform.position -= new Vector3(0, Random.Range(80, 100) * Time.deltaTime, 0);
            tile.placed = true;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            yield return new WaitForSeconds(1);
        }
    }
}
