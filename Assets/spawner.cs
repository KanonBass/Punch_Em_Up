using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class spawner : MonoBehaviour
{
    private int maxEnemy;
    private int numEnemy;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject spawn1;
    [SerializeField] GameObject spawn2;
    [SerializeField] GameObject spawn3;
    [SerializeField] GameObject spawn4;

    int numEnemies;

    private int score = 0;

    UnityEvent IncreaseScore;
    public TMP_Text text;

    private void Awake()
    {
        SpawnEnemies();
    }

    private void UpdateCount()
    {
        numEnemy++;
        if (numEnemy >= maxEnemy)
        {
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        Instantiate(enemy, spawn1.transform.position, quaternion.identity);
        Instantiate(enemy, spawn2.transform.position, quaternion.identity);
        Instantiate(enemy, spawn3.transform.position, quaternion.identity);
        Instantiate(enemy, spawn4.transform.position, quaternion.identity);

        numEnemies = 4;
    }

    public void OnDeath()
    {
        if(--numEnemies <= 0)
        {
            SpawnEnemies();
        }

        score++;
        text.text = "Score: " + score;
    }
}
