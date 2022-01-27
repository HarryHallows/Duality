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
    [SerializeField] private NavigationDirections directions;
    [SerializeField] private UIManager um;
    Button button;

    private void Awake()
    {
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

        switch (directions)
        {
            case NavigationDirections.NORTH:
                um.NorthDirection();
                break;
            case NavigationDirections.EAST:
                um.EastDirection();
                break;
            case NavigationDirections.SOUTH:
                um.SouthDirection();
                break;
            case NavigationDirections.WEST:
                um.WestDirection();
                break;
        }
    }
}
