using UnityEngine;
using System.Collections.Generic;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour 
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _initialPoolSize = 20;
    [SerializeField] protected bool _expandable = true;

    protected readonly Queue<T> _pool = new Queue<T>();

    private void Awake()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            AddObjectToPool();
        }
    }

    protected T AddObjectToPool()
    {
        T instance = Instantiate(_prefab, transform);
        instance.gameObject.SetActive(false);
        _pool.Enqueue(instance);
        return instance;
    }

    public T GetObjectFromPool()
    {
        if (_pool.Count == 0)
        {
            if (_expandable)
            {
                AddObjectToPool();
            }
            else
            {
                Debug.LogWarning("Object pool is empty and not expandable.");
                return null;
            }
        }
        T instance = _pool.Dequeue();
        instance.gameObject.SetActive(true);
        return instance;
    }

    public void ReturnObjectToPool(T instance)
    {
        instance.gameObject.SetActive(false);
        instance.transform.SetParent(transform);
        _pool.Enqueue(instance);
    }

    public bool HasAvailableObjects()
    {
        return _pool.Count > 0 || _expandable;
    }
}
