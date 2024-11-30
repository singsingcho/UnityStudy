using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public const string EnemyKey = "Enemy";

    [SerializeField] private Button stopGame;
    [SerializeField] private Button spawnEnemy;
    [SerializeField] private Button killEnemy;
    [SerializeField] private Vector2 spawnRange;

    private void Start()
    {
        stopGame.onClick.AddListener(EndGame);
        spawnEnemy.onClick.AddListener(SpawnEnemy);
        killEnemy.onClick.AddListener(KillEnemy);

        if (playTimeCor == null)
        {
            playTimeCor = StartCoroutine(PlayTimeCheck());
        }
    }

    Coroutine playTimeCor = null;
    float playTime = 0;
    IEnumerator PlayTimeCheck()
    {
        playTime = 0;

        GameManager.Instance.UpdatePlayTime(playTime);

        while (true)
        {
            yield return null;

            playTime += Time.deltaTime;

            GameManager.Instance.UpdatePlayTime(playTime);
        }
    }

    void EndGame()
    {
        if (playTimeCor != null)
        {
            StopCoroutine(playTimeCor);

            GameManager.Instance.UpdatePlayTime(playTime);
        }

        SceneManager.LoadScene(2);
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = GameManager.Instance.GetObject(EnemyKey);
        newEnemy.transform.position = new Vector3(
                        Random.Range(-spawnRange.x, spawnRange.x),
                        0.5f,
                        Random.Range(-spawnRange.y, spawnRange.y));
    }

    void KillEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            GameManager.Instance.ReturnObject(enemies[i].GetComponent<PoolManagedObject>());
        }
    }
}
