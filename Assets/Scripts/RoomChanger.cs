using UnityEngine;
using Pathfinding;

public class RoomChanger : MonoBehaviour
{
    public AudioSource door;
    public GameObject[] rooms;
    public GameObject[] enemyPrefabs;
    public GameObject spawnPoint; // A point where the player spawns in the new room

    private AStarAreaScanner areaScanner;

    void Start() {
        areaScanner = GetComponent<AStarAreaScanner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter");
        if (other.CompareTag("Exit1"))
        {
            door.Play();
            ChangeRoom(1);
        } if (other.CompareTag("Exit2")) 
        {
            door.Play();
            ChangeRoom(2);
        } if (other.CompareTag("Exit3"))
        {
            door.Play();
            ChangeRoom(3);
        }
    }

    private void ChangeRoom(int difficulty)
    {
        int randomValue = Random.Range(1*difficulty, 3*difficulty);
        GameObject currentRoom = GameObject.FindGameObjectWithTag("Room");
        Debug.Log("Changing rooms");

        if (rooms.Length == 0)
        {
            Debug.LogWarning("No rooms assigned to the script!");
            return;
        }

        // Select a random room
        int randomIndex = Random.Range(0, rooms.Length);
        Debug.Log($"Room [#{randomIndex}]");
        GameObject selectedRoom = rooms[randomIndex];
        //if (randomIndex >= 10 && randomIndex <= 12) {
        //    randomValue = 0;
        //}
        DeleteAllEnemies();

        currentRoom.SetActive(false);
        Destroy(currentRoom);

        Instantiate(selectedRoom);

        selectedRoom.SetActive(true);

        // Call ScanArea using the singleton instance
        if (AStarAreaScanner.Instance != null)
        {
            AStarAreaScanner.Instance.ScanArea();
        }

        // Call ScanArea using the singleton instance
        if (AStarAreaScanner.Instance != null)
        {
            AStarAreaScanner.Instance.FloodFillEnclosedArea(Vector3.zero);
        }

        GenerateEnemies(randomValue); 

        // Move the player to the spawn point of the new room
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
    }

    public void DeleteAllEnemies()
    {
        // Find all game objects with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Loop through each enemy and destroy it
        if(enemies.Length > 0) {
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }

        
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        // Loop through each enemy and destroy it
        if(bullets.Length > 0) {
            foreach (GameObject bullet in bullets)
            {
                Destroy(bullet);
            }
        }

        Debug.Log("All enemies have been destroyed.");
    }

    public void GenerateEnemies(int x)
    {
        GetComponent<PlayerInventory>().AddGold(x);
        Debug.Log("Instantiating Enemies");
        
        // Get the player's Transform
        Transform playerTransform = GetPlayerTransform();
        if (playerTransform == null)
        {
            Debug.LogError("Cannot generate enemies: Player transform is null.");
            return;
        }

        // Ensure we have a valid x
        if (x <= 0)
        {
            Debug.LogError("x must be greater than 0.");
            return;
        }

        // Continue until x reaches 1
        while (x > 1)
            {
                int randomCostIndex = Random.Range(0, 3); // 0 = cost 1, 1 = cost 2, 2 = cost 3
                int cost = randomCostIndex + 1;

                if (x < cost)
                {
                    continue;
                }

                Vector3 spawnPosition = Vector3.zero;
                if (AStarAreaScanner.Instance != null)
                {
                    spawnPosition = AStarAreaScanner.Instance.GetRandomWalkableTile();
                }

                // Instantiate the prefab
                GameObject enemyInstance = Instantiate(enemyPrefabs[randomCostIndex], spawnPosition, Quaternion.identity);

                if (randomCostIndex == 2)
                {
                    EnemyShooter2D enemyShooter = enemyInstance.GetComponent<EnemyShooter2D>();
                    if (enemyShooter != null)
                    {
                        enemyShooter.AssignTarget(GetPlayerTransform());
                    }
                }


                // Set the AIDestinationSetter target to the player's transform
                AIDestinationSetter destinationSetter = enemyInstance.GetComponent<AIDestinationSetter>();
                if (destinationSetter != null)
                {
                    destinationSetter.target = GetPlayerTransform(); // Assuming you already have GetPlayerTransform defined
                }

                // Reduce x by the cost
                x -= cost;

                Debug.Log($"Spawned prefab with cost {cost}. Remaining x: {x}");
            }

        if (x == 1)
        {
            Vector3 spawnPosition = Vector3.zero;

            if (AStarAreaScanner.Instance != null)
            {
                spawnPosition = AStarAreaScanner.Instance.GetRandomWalkableTile();
            }

            GameObject enemy = Instantiate(enemyPrefabs[0], spawnPosition, Quaternion.identity);

            AIDestinationSetter aiDestinationSetter = enemy.GetComponent<AIDestinationSetter>();
            if (aiDestinationSetter != null)
            {
                aiDestinationSetter.target = playerTransform;
            }

            Debug.Log("Remaining x reached 1. Spawned prefab with cost 1.");
        }
    }


    public Transform GetPlayerTransform()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            return player.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene! Ensure the player has the 'Player' tag.");
            return null;
        }
    }
}