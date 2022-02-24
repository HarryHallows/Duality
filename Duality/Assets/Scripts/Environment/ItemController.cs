using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] public bool placedDown;

    public float desiredDepth, removeDepth;

    [SerializeField] private float moveSpeed;

    [Tooltip("Difference conditions to activate certain properties on the object")]
    public bool remove, pool, heavy, scale, area;
    public bool outside;
    [SerializeField] private Animator camAnim;

    public PoolObjectType poolObj;

    [SerializeField] private Vector3 localScalePrefence;
    private void Awake()
    {
        if (heavy)
        {
            camAnim = Camera.main.GetComponent<Animator>();
        }

        if (scale)
        {
            transform.localScale = localScalePrefence;
        }
    }

    private void Start()
    {
        moveSpeed = Random.Range(80, 100);

        if (!pool)
        {
            gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        placedDown = false;

        if (!pool)
        {
            gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (!placedDown)
        {
            StartCoroutine(SpawnPosition());
        }

        if (remove)
        {
            ReturnToPoolPosition(poolObj);
        }
    }

    private IEnumerator SpawnPosition()
    {
        Debug.Log("moving objects down from spawn");
        if (transform.position.y > desiredDepth)
        {
            Debug.Log("I should move down");
            transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }

        if (transform.position.y <= desiredDepth)
        {
            transform.position = new Vector3(transform.position.x, desiredDepth, transform.position.z);

            if (heavy)
            {
                camAnim.SetTrigger("CameraShake");
            }


            if (area)
            {
                GameManager.Instance.EnvrionmentRotate(true);
            }

            placedDown = true;
        }
        yield return new WaitForSeconds(.1f);
    }

    private void ReturnToPoolPosition(PoolObjectType _type)
    {
        transform.position -= new Vector3(0, Random.Range(80, 100) * Time.deltaTime, 0);

        if (transform.position.y <= removeDepth && pool)
        {
            PoolManager.Instance.PoolObject(gameObject, _type);
            remove = false;
        }
        else if (transform.position.y <= removeDepth && !pool)
        {
            gameObject.SetActive(false);
        }
    }
}
