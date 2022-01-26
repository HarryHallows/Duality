using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum PoolObjectType
{
    FLOOR,
    WALL
}


[System.Serializable]
public class PoolInfo
{
    public PoolObjectType type;
    public int amount = 0;
    public GameObject prefab;
    public GameObject container;

    [HideInInspector]
    public List<GameObject> pool = new List<GameObject>();
}

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] List<PoolInfo> listOfPool;
    private Vector3 defaultPosition = Vector3.zero;

    private void Start()
    {
        for (int i = 0; i < listOfPool.Count; i++)
        {
            AddObjectsToPool(listOfPool[i]);
        }
    }

    private void AddObjectsToPool(PoolInfo _info)
    {
        for (int i = 0; i < _info.amount; i++)
        {
            GameObject _objectInstance = null;
            _objectInstance = Instantiate(_info.prefab, _info.container.transform);
            _objectInstance.gameObject.SetActive(false);
            _objectInstance.transform.position = defaultPosition;
            _info.pool.Add(_objectInstance);
        }
    }

    public GameObject GetPooledObject(PoolObjectType _type)
    {
        PoolInfo selected = GetPoolByType(_type);
        List<GameObject> _pool = selected.pool;

        GameObject _objectInstance = null;

        if (_pool.Count > 0)
        {
            _objectInstance = _pool[_pool.Count - 1];
            _pool.Remove(_objectInstance);
        }
        else
        {
            _objectInstance = Instantiate(selected.prefab, selected.container.transform);
        }

        return _objectInstance;
    }

    public void PoolObject(GameObject _object, PoolObjectType _type)
    {
        _object.SetActive(false);
        _object.transform.position = defaultPosition;

        PoolInfo _selected = GetPoolByType(_type);
        List<GameObject> _pool = _selected.pool;

        if (!_pool.Contains(_object))
        {
            _pool.Add(_object);
        }
    }

    private PoolInfo GetPoolByType(PoolObjectType _type)
    {
        for (int i = 0; i < listOfPool.Count; i++)
        {
            if (_type == listOfPool[i].type)
            {
                return listOfPool[i];
            }
        }
        return null;
    }
}
