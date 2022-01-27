using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    private RaycastHit hit;
    private Ray ray;
    [SerializeField] private Camera cam;

    [SerializeField] private LayerMask interactableLayers;

    [SerializeField] private GameObject environment;

    private void Awake()
    {
        cam = Camera.main;
        environment = GameObject.FindGameObjectWithTag("EnvironmentRoot");
        gm = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();
        CastRay();
    }

    private void InputCheck()
    {

    }

    private void CastRay()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity, interactableLayers))
        {
            Debug.Log("Raycasting");
            Debug.Log(hit.collider.name);

            if (Input.GetMouseButtonDown(0) && hit.collider.tag != "Navigation")
            {
                Debug.Log($"You have selected {hit.transform.name}");
                //Bring forward object into interaction position
            }
            else
            {
                Debug.Log($"You have hovered {hit.transform.name}");
                //Call Coroutine
                //Highlight object (increase localscale)
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && gm.rotateEnvironment /**/)
            {
                //Debug.Log(Input.mousePosition.x);
                RotateEnvironment(-Input.mousePosition.x);
            }
            else
            {
                Quaternion _resetRotation = Quaternion.Euler(environment.transform.eulerAngles.x, 0, environment.transform.eulerAngles.z);
                environment.transform.rotation = Quaternion.Slerp(environment.transform.rotation, _resetRotation, 0.9f * Time.deltaTime);
            }
        }
    }

    private void RotateEnvironment(float _mousePosition)
    {
        Quaternion _desiredRotation = Quaternion.Euler(environment.transform.eulerAngles.x, _mousePosition, environment.transform.eulerAngles.z);
        environment.transform.rotation = Quaternion.Slerp(environment.transform.rotation, _desiredRotation, 0.03f);
    }
}
