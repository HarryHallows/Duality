using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Spawner spawner;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    #region Dialogue System

    #endregion

    #region Buttons

    public void PowerButton()
    {
        //If pressed objects that can become transparant activate and the lighting changes e.g. the BG colour becomes darker

    }

    #region Cardinal Buttons
    //These will be turned on and off depending which room is currently active

    public void NorthDirection()
    {
        SpawnerToRemoveFloor();
    }

    public void EastDirection()
    {
        SpawnerToRemoveFloor();
    }

    public void SouthDirection()
    {
        //spawner.StartCoroutine(spawner.RemoveFloor(PoolObjectType.FLOOR));
        //SpawnerToRemoveFloor();
        spawner.RemoveFloorActivate();
    }

    public void WestDirection()
    {
        SpawnerToRemoveFloor();
    }

    private void SpawnerToRemoveFloor()
    {
        spawner.tileCount = 0;
        StartCoroutine(spawner.RemoveFloor());
    }
    #endregion
    #endregion

}
