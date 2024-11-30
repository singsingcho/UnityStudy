using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public interface PoolManagedObject
{
    public string key { get; set; }
    public GameObject source { get; set; }
    public void GetObject();
    public void ReturnObject();
    public GameObject GameObject { get; }
}

public class PoolManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    public Dictionary<string, PoolManagedObject> prefabDictionary = new Dictionary<string, PoolManagedObject>();
    public Dictionary<GameObject, Queue<PoolManagedObject>> poolDictionary = new Dictionary<GameObject, Queue<PoolManagedObject>>();

    private void Awake()
    {
        GameManager.Instance.SetObjectPool(this);
    }

    private void Start()
    {
        enemyPrefab.source = enemyPrefab.gameObject;
        prefabDictionary.Add(PlayManager.EnemyKey, enemyPrefab);
    }

    public bool GetSourceByKey(string key, out PoolManagedObject source)
    {
        if (prefabDictionary.ContainsKey(key))
        {
            source = prefabDictionary[key];

            return true;
        }

        source = null;

        return false;
    }

    public GameObject GetObject(PoolManagedObject source)
    {
        if (poolDictionary.ContainsKey(source.source))
        {
            if (poolDictionary[source.source].Count > 0)
            {
                var obj = poolDictionary[source.source].Dequeue();
                obj.GetObject();
                return obj.GameObject;
            }
        }
        else
        {
            poolDictionary.Add(source.source, new Queue<PoolManagedObject>());
        }

        return Instantiate(source.source);
    }

    public void ReturnObject(PoolManagedObject obj)
    {
        if (!poolDictionary.ContainsKey(obj.source))
        {
            poolDictionary.Add(obj.source, new Queue<PoolManagedObject>());
        }

        obj.ReturnObject();
        poolDictionary[obj.source].Enqueue(obj);
    }
}
