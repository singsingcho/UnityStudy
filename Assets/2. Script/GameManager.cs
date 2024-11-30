using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ��� ������ ���� ����
    public static GameManager Instance { get; private set; }
    [SerializeField] PoolManager poolManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
}
