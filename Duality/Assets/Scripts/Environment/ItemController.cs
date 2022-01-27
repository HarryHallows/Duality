using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] public bool placedDown;

    [SerializeField] private float desiredDepth, removeDepth;

    [SerializeField] private float moveSpeed;

    public bool remove;

    private void Start()
    {
        moveSpeed = Random.Range(80, 100);
    }

    private void Update()
    {
        if (!placedDown)
        {
            StartCoroutine(SpawnPosition());
        }

        if (remove)
        {
            ReturnToPoolPosition();
        }
    }

    private IEnumerator SpawnPosition()
    {
        Debug.Log("moving objects down from spawn");
        if (transform.position.y > 0)
        {
            Debug.Log("I should move down");
            transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }

        if (transform.position.y <= 0)
        {
            transform.position = new Vector3(transform.position.x, desiredDepth, transform.position.z);
            placedDown = true;
            yield return null;
        }
    }

    private void ReturnToPoolPosition()
    {
        transform.position -= new Vector3(0, Random.Range(80, 100) * Time.deltaTime, 0);

        if (transform.position.y <= removeDepth)
        {
            PoolManager.Instance.PoolObject(gameObject, PoolObjectType.FLOOR);
            remove = false;
        }
    }
}
