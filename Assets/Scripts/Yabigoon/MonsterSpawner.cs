using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject monsterPrefab;
    public float spawnInterval = 10f;
    public Transform spawnPoint;

    void Start()
    {
        if (monsterPrefab == null)
        {
            Debug.LogError("Monster Prefab is not assigned in the MonsterSpawner script!");
            return;
        }

        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }

        StartCoroutine(SpawnMonsterRoutine());
    }

    IEnumerator SpawnMonsterRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnMonster();
        }
    }

    void SpawnMonster()
    {
        Instantiate(monsterPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("A monster has spawned! Interval: " + spawnInterval);
    }

    // Call this to make spawning faster
    public void DecreaseInterval(float amount)
    {
        spawnInterval -= amount;
        if (spawnInterval < 1f) // Set a minimum spawn time of 1 second
        {
            spawnInterval = 1f;
        }
    }

    // Call this to make spawning slower
    public void IncreaseInterval(float amount)
    {
        spawnInterval += amount;
    }
}
