using Unity.Mathematics;
using UnityEngine;

public class spawner : MonoBehaviour
{
    private int maxEnemy;
    private int numEnemy;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject spawn1;
    [SerializeField] GameObject spawn2;
    [SerializeField] GameObject spawn3;
    [SerializeField] GameObject spawn4;

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
    }
}
