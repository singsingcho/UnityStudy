using UnityEngine;

public class Enemy : MonoBehaviour, PoolManagedObject
{
    public string key { get => Key;  set => Key = value; }
    public GameObject source { get; set; }
    public GameObject GameObject => this.gameObject;

    [SerializeField] private string Key;

    public void GetObject()
    {
        gameObject.SetActive(true);
    }

    public void ReturnObject()
    {
        gameObject.SetActive(false);
    }
}
