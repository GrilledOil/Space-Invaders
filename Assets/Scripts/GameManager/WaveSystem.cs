using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public int currentWave = 0; // The current wave
    public float waveDelay = 5f; // Seconds the wait after end of round to start next

    public GameObject spawnLocationEntity; // Location of leftmost first enemy spawn
    private Vector2 spawnLocation; // Location of leftmost first enemy spawn
    public Vector2 currSpawnLocation; //Location of current enemy spawn

    public int rows = 5;
    public int enemyPerRow = 5; // Amount of enemies per row of enemies

    public GameObject enemyPrefab; // Assign in the inspector
    public Sprite[] possibleSprites; // Possible enemy sprites

    public IEnumerator SpawnWave()
    {
        currentWave++; // Increases wave counter

        yield return new WaitForSeconds(waveDelay);

        spawnLocation = spawnLocationEntity.transform.position;
        currSpawnLocation = spawnLocation; // Resets first enemy spawn location

        for (int row = 0; row < rows; row++)
        {
            for (int enemyCount = 0; enemyCount < enemyPerRow; enemyCount++) // Spawns enemies
                {
                    GameObject enemy = Instantiate(enemyPrefab, new Vector2(spawnLocation.x + 1f * enemyCount, spawnLocation.y + 1f * row), Quaternion.identity); // Spawns enemy

                    SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();
                    sr.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)]; // Picks random sprite for enemy
                }
        }
        enemyPerRow += 3;

        gameObject.GetComponent<GameManagement>().canWave = true; // Allows next wave to spawn
    }
}
