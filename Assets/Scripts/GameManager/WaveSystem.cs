using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    private float waveDelay = 5f; // Seconds the wait after end of round to start next

    public GameObject spawnLocationEntity; // Location of leftmost first enemy spawn
    private Vector2 spawnLocation; // Location of leftmost first enemy spawn

    private int rows = 3;
    private int enemyPerRow = 10; // Amount of enemies per row of enemies

    public List<GameObject> possibleEnemies; // Possible enemy sprites

    public List<List<GameObject>> enemies = new List<List<GameObject>>(); // Nested list of all enemies

    void Awake()
    {
        spawnLocation = spawnLocationEntity.transform.position;
    }

    public IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(waveDelay);

        enemies.Clear();
        
        for (int row = 0; row < rows; row++)
        {
            List<GameObject> enemiesInRow = new List<GameObject>();

            for (int enemyCount = 0; enemyCount < enemyPerRow; enemyCount++) // Spawns enemies
            {
                GameObject enemy = Instantiate(possibleEnemies[Random.Range(0, possibleEnemies.Count - 1)], new Vector2(spawnLocation.x + 1f * enemyCount, spawnLocation.y + 1f * row), Quaternion.identity); // Spawns enemy

                enemiesInRow.Add(enemy); // Add to current row
            }

            enemies.Add(enemiesInRow);
        }
        rows++;
        gameObject.GetComponent<GameManagement>().canWave = true; // Allows next wave to spawn

        gameObject.GetComponent<GameManagement>().enemies = enemies; // Allows next wave to spawn
    }
}
