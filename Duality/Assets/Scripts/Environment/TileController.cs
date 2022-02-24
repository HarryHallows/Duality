using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public FloorTile tile;

    [SerializeField] private bool placedDown;

    private void Start()
    {
        tile.spawned = true;
    }

    private void Update()
    {
        if (!placedDown)
        {

            StartCoroutine(SpawnPosition());
        }
    }

    private IEnumerator SpawnPosition()
    {
        Debug.Log("moving objects down from spawn");
        if (transform.position.y > 0)
        {
            Debug.Log("I should move down");
            transform.position -= new Vector3(0, Random.Range(80, 100) * Time.deltaTime, 0);
        }

        if (transform.position.y <= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            placedDown = true;
            yield return null;
        }
    }

    public IEnumerator LeavePosition()
    {
        if (transform.position.y > -10)
        {
            Debug.Log("I should move down");
            transform.position -= new Vector3(0, Random.Range(80, 100) * Time.deltaTime, 0);
            tile.placed = true;
        }
        else
        {
            Debug.Log("Still staying at spawned position");
            transform.position = new Vector3(transform.position.x, -10, transform.position.z);
            gameObject.SetActive(false);
            yield return null;
        }
    }
}
