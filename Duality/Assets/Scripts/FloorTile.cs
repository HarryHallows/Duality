using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/FloorTiles/Base")]
public class FloorTile : ScriptableObject
{
    public GameObject prefab;
    public bool spawned, placed;

}
