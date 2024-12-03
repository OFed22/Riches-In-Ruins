using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Difficulty DiffScript;
    private EnemyData EnemyData;
    private Camera mainCamera;
    

    void Start(){
        SpawnEnemies();
    }

    private void Awake()
    {
        DiffScript.GetComponent<Difficulty>();
        EnemyData = GetComponent<EnemyData>();

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found! Ensure a camera is tagged as 'MainCamera'.");
        }
    }

    void SpawnEnemies()
    {
        Debug.Log($"EnemyData contains {EnemyData.EnemyV.Count} entries.");
        foreach (EnemyValues ev in EnemyData.EnemyV)
        {
            Debug.Log($"Enemy ID: {ev.ID}, Cost: {ev.EnemyCost}, Prefab: {ev.EnemyPrefab}");
        }
        int EnemyCost = DiffScript.GetDifficultyPoints();
        while(EnemyCost != 0)
        {
            int EnemyID = Random.Range(1, 4); 
            foreach(EnemyValues EV in EnemyData.EnemyV)
            {
                if(EnemyID == EV.ID && EnemyCost >= EV.EnemyCost)
                {
                    Spawn(GetSpawnLocation(), EV.EnemyPrefab);
                    EnemyCost -= EV.EnemyCost;
                }
            }
        }
    }

    Transform GetSpawnLocation()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not set.");
            return null;
        }

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        const int maxAttempts = 10;
        float checkRadius = 2.5f;

        for (int i = 0; i < maxAttempts; i++)
        {
            float randomX = Random.Range(bottomLeft.x, topRight.x);
            float randomY = Random.Range(bottomLeft.y, topRight.y);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            if (!Physics2D.OverlapCircle(spawnPosition, checkRadius))
            {
                GameObject tempSpawn = new GameObject("TempSpawnPoint");
                tempSpawn.transform.position = spawnPosition;
                return tempSpawn.transform;
            }
        }

        Debug.LogWarning("No valid spawn location found after retries. Using default position.");
        Vector3 defaultPosition = new Vector3(0, 0, 0);
        GameObject fallbackSpawn = new GameObject("FallbackSpawnPoint");
        fallbackSpawn.transform.position = defaultPosition;
        return fallbackSpawn.transform;
    }

    void Spawn(Transform TargetSpawn, GameObject EnemyType)
    {
        if (TargetSpawn != null)
        {
            Instantiate(EnemyType, TargetSpawn.position, Quaternion.identity);
            Destroy(TargetSpawn.gameObject); // Clean up temporary spawn point
        }
    }

}
