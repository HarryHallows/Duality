using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameManager gm;

    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float horizontal;
    [SerializeField] private float vertical;
    [SerializeField] private float rotationSmoothSpeed;

    private float rayLength;


    [SerializeField] private GameObject playerGraphics;

    Vector3 movement;

    private Camera cam;
    private Plane groundPlane;

    Quaternion desiredRotation;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Inputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 inputVector = new Vector3(horizontal, 0, vertical);
        inputVector = Vector3.ClampMagnitude(inputVector, 1);

        Rotations();
    }

    private void Rotations()
    {
        if (gm.combatState)
        {
            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
            groundPlane = new Plane(Vector3.up, Vector3.zero);

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 _pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, _pointToLook, Color.red);
                playerGraphics.transform.LookAt(new Vector3(_pointToLook.x, playerGraphics.transform.position.y, _pointToLook.z));
            }
        }
        else
        {
            //Rotate towards Cardinal move directions
            Debug.Log(playerGraphics.transform.eulerAngles.y);

            #region Cardinal Rotations
            if (horizontal == 0 && vertical > 0)
            {
                //North
                //Debug.Log("NORTH");
                desiredRotation = Quaternion.Euler(0, 45, 0);
            }
            else if (horizontal > 0 && vertical > 0)
            {
                //North East
                //Debug.Log("NORTH EAST");
                desiredRotation = Quaternion.Euler(0, 90f, 0);
            }
            else if (horizontal > 0 && vertical == 0)
            {   //East
                //Debug.Log("EAST");
                desiredRotation = Quaternion.Euler(0, 135f, 0);
            }
            else if (horizontal > 0 && vertical < 0)
            {   //South East
                //Debug.Log("SOUTH EAST");
                desiredRotation = Quaternion.Euler(0, 180f, 0);
            }
            else if (horizontal == 0 && vertical < 0)
            {   //South
                //Debug.Log("SOUTH");
                desiredRotation = Quaternion.Euler(0, 225f, 0);
            }
            else if (horizontal < 0 && vertical < 0)
            {
                //South West
                //Debug.Log("SOUTH WEST");
                desiredRotation = Quaternion.Euler(0, 270f, 0);
            }
            else if (horizontal < 0 && vertical == 0)
            {
                //West
                //Debug.Log("WEST");
                desiredRotation = Quaternion.Euler(0, 315f, 0);
            }
            else if (horizontal < 0 && vertical > 0)
            {
                //North West
                //Debug.Log("NORTH WEST");
                desiredRotation = Quaternion.Euler(0, 360f, 0);
            }
            #endregion

            playerGraphics.transform.rotation = Quaternion.Slerp(playerGraphics.transform.rotation, desiredRotation, rotationSmoothSpeed);
        }

    }

    private void Move()
    {
        movement = transform.forward * vertical + transform.right * horizontal;
        controller.Move(movement * moveSpeed * Time.fixedDeltaTime);
    }
}
