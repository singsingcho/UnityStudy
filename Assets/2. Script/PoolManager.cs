using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public interface PoolManagedObject
{
    public string key { get; set; }
    public GameObject source { get; set; }
    public void GetObject();
    public void ReturnObject();
    public GameObject gameObject { get; }
}

public class PoolManager : MonoBehaviour
{
    public Dictionary<string, PoolManagedObject> prefabDictionary = new Dictionary<string, PoolManagedObject>();
    public Dictionary<GameObject, Queue<PoolManagedObject>> poolDictionary = new Dictionary<GameObject, Queue<PoolManagedObject>>();

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
                return obj.gameObject;
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
