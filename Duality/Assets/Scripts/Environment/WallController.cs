using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{

    [SerializeField] private GameManager gm;
    [SerializeField] private Animator cameraAnim;

    public WallTile tile;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        tile.spawned = true;
        transform.localScale = new Vector3(7.5f, 7f, 1f);
        cameraAnim = Camera.main.GetComponent<Animator>();
    }

    private void Update()
    {
        StartCoroutine(SpawnPosition());
    }

    private IEnumerator SpawnPosition()
    {
        if (transform.position.y > 20)
        {
            //Debug.Log(transform.position.y);
            transform.position -= new Vector3(0, 90 * Time.deltaTime, 0);
            tile.placed = true;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 20, transform.position.z);
            cameraAnim.SetTrigger("CameraShake");


            if (gm.wallPlacedCount >= 2)
            {
                gm.EnvrionmentRotate(true);
                yield return null;
            }
            else
            {
                gm.wallPlacedCount++;
                yield return null;
            }

        }
    }
}
