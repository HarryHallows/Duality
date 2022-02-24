using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NavigationDirections
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}

public class NavigationButtons : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private NavigationDirections directions;

    [SerializeField] private PoolObjectType currentArea;
    [SerializeField] private GameAreas newArea;

    [SerializeField] private UIManager um;

    [SerializeField] private bool outside, currentOutside;

    private Button button;

    private void Awake()
    {
        gm = GameManager.Instance;
        button = GetComponent<Button>();
        um = FindObjectOfType<UIManager>();

        button.onClick.AddListener(TaskOnClick);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void TaskOnClick()
    {
        Debug.Log($"You have clicked {gameObject.name}!");

        um.SpawnerToRemoveFloor(currentArea);
        GameManager.Instance.EnvrionmentRotate(false);

        /* if (!currentOutside)
         {
             um.SpawnerToRemoveWalls(currentArea);
         }
        */
        switch (directions)
        {
            case NavigationDirections.NORTH:
                //LOAD NEXT AREA
                gm.areas = newArea;
                break;
            case NavigationDirections.EAST:
                //LOAD NEXT AREA
                gm.areas = newArea;
                break;
            case NavigationDirections.SOUTH:
                //LOAD NEXT AREA
                gm.areas = newArea;
                break;
            case NavigationDirections.WEST:
                //LOAD NEXT AREA
                gm.areas = newArea;
                break;
        }

        if (outside)
        {
            gm.outsideScene = true;
        }
        else
        {
            gm.outsideScene = false;
        }
    }
}
