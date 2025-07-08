using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagement : MonoBehaviour
{
    private WaveSystem waveSystem;

    public int score = 0;
    public int wave = 1;

    public bool canWave = false;

    public TMP_Text waveText;

    public int moveDivide;
    public float moveTime;

    public void Start()
    {
        waveSystem = FindObjectOfType<WaveSystem>();
        NewWave();

    }

    public void Update()
    {
        if (GameObject.FindWithTag("Enemy") == null && canWave == true)
        {
            canWave = false;
            NewWave();
        }
    }

    public void NewWave()
    {
        StartCoroutine(waveSystem.SpawnWave()); // Spawns next wave
        wave = waveSystem.currentWave; // Updates wave count

        waveText.text = $"Wave {wave}";
    }

    public void PermaDeath()
    {
        GameObject.Find("Player").GetComponent<PlayerHealth>().lives = 0; // Sets lives to 0
        //UnityEditor.EditorApplication.isPlaying = false; // placeholder death
    }
}