using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/WallTiles/Base")]
public class WallTile : ScriptableObject
{
    public GameObject prefab;
    public bool spawned, placed;
}
