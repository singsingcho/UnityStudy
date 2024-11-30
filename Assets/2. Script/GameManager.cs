using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;
    public PlayManager playManager { get; private set; }
    public PoolManager poolManager { get; private set; }

    [field: SerializeField] public string userName { get; private set; }
    [field: SerializeField] public float playTime { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetUserName(string userName)
    {
        this.userName = userName;
    }

    public void UpdatePlayTime(float playTime)
    {
        this.playTime = playTime;
    }

    public GameObject GetObject(string key)
    {
        if (poolManager.GetSourceByKey(key, out PoolManagedObject source))
        {
            return poolManager.GetObject(source).gameObject;
        }

        return null;
    }

    public void ReturnObject(PoolManagedObject obj)
    {
        poolManager.ReturnObject(obj);
    }

    public void SetObjectPool(PoolManager manager)
    {
        this.poolManager = manager;
    }
}
