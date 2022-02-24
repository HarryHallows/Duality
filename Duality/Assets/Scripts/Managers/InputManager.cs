using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    private RaycastHit hit;
    [SerializeField] private GameObject currentHit;
    private Ray ray;
    [SerializeField] private Camera cam;

    [SerializeField] private LayerMask interactableLayers;

    [SerializeField] private GameObject environment;

    private void Awake()
    {
        cam = Camera.main;
        gm = GetComponent<GameManager>();
    }

    private void Start()
    {
        environment = gm.environment;
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
    }

    private void CastRay()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        CheckRotation();
        Interactables(_ray);
    }

    private void CheckRotation()
    {
        if (hit.collider != null)
        {
            if (Input.GetMouseButton(0) && gm.rotateEnvironment && hit.collider.tag != "Navigation")
            {
                gm.RotateEnvironment(-Input.mousePosition.x);
            }
        }

    }

    private void Interactables(Ray _ray)
    {
        if (Physics.Raycast(_ray, out hit, Mathf.Infinity, interactableLayers))
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.tag != "Navigation")
            {
                Debug.Log($"You have selected {hit.transform.name}");
                //Bring forward object into interaction position
                currentHit.GetComponent<InteractableObject>().SelectedObject(true);
            }

            if (hit.collider != null && hit.collider.tag != "Navigation")
            {
                Debug.Log($"You have hovered {hit.transform.name}");
                //Call Coroutine
                //Highlight object (increase localscale)

                //assigns the current collider data to current hit 
                currentHit = hit.collider.gameObject;

                currentHit.GetComponent<InteractableObject>().hovering = true;

                Debug.Log($"Current Hit: {currentHit.name} + Hit: {hit.collider.name}");
            }
        }
        else
        {
            if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
            {
                if (currentHit != null)
                {
                    if (hit.collider.gameObject != currentHit)
                    {
                        currentHit.GetComponent<InteractableObject>().hovering = false;
                    }
                }
            }
        }
    }
}
