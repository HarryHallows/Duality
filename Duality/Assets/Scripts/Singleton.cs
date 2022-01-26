using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool dontDestroy = false;

    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject _singleton = new GameObject(typeof(T).Name);
                    instance = _singleton.AddComponent<T>();
                }
            }
            return instance;
        }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;

            if (dontDestroy)
            {
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
