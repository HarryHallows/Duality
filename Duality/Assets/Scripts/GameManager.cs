using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool combatState;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public bool Combat(bool _combatState)
    {
        return combatState = _combatState;
    }
}
