using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    private WaveSystem waveSystem;

    public AudioClip waveWinSoundClip;
    private GameObject camera;

    public int score = 0;
    public int hiScore = 0;

    public bool canWave = false;
    public bool canSwitch = false;
    private bool canCheck = true;

    public TMP_Text scoreText;
    public TMP_Text hiScoreText;
    public TMP_Text lifeText;
    public GameObject GameUI;

    public GameObject DeathUI;
    public GameObject NewHighScoreIMG;
    public GameObject HighScoreIMG;
    public GameObject ScoreIMG;
    public TMP_Text DeathScoreText;
    public TMP_Text HighScoreText;
    public GameObject DeathScoreTexts;

    public float moveTime;
    private float startMoveTime;

    public bool paused = false;

    public float dropChance = 5f;

    public List<List<GameObject>> enemies = new List<List<GameObject>>(); // Nested list of all enemies

    private void Awake()
    {
        camera = GameObject.Find("cams");
        SaveSystem.Init();

        Load(); // Load high score from file if it exists
    }

    public void Start()
    {
        waveSystem = FindObjectOfType<WaveSystem>();

        startMoveTime = moveTime;
        NewWave();

    }

    public void Update()
    {
        moveTime = Mathf.Clamp(moveTime, 0.25f, startMoveTime);
        if (GameObject.FindWithTag("Enemy") == null && canWave == true)
        {
            canWave = false;
            NewWave();
            AudioSource.PlayClipAtPoint(waveWinSoundClip, camera.transform.position, 1f);
        }

        if (canCheck)
        {
            StartCoroutine(MovementCheck(moveTime));
        }
        scoreText.text = $"{score}";
        hiScoreText.text = $"{hiScore}";
        lifeText.text = $"{GameObject.Find("Player").GetComponent<PlayerHealth>().lives}";

        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
            {
                Pause();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Unpause();
            }

        // Restarts scene after death
        if (paused && DeathUI.activeSelf && Input.anyKeyDown)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void NewWave()
    {
        moveTime = startMoveTime;
        StartCoroutine(waveSystem.SpawnWave()); // Spawns next wave
    }

    public void PermaDeath()
    {
        GameObject.Find("Player").GetComponent<PlayerHealth>().lives = 0;
        GameUI.SetActive(false);
        DeathUI.SetActive(true);

        if (score > hiScore)
        {
            NewHighScoreIMG.SetActive(true);
            HighScoreText.text = $"{score}";

        }
        else
        {
            HighScoreIMG.SetActive(true);
            HighScoreText.text = $"{hiScore}";
            ScoreIMG.SetActive(true);
            DeathScoreTexts.SetActive(true);
            DeathScoreText.text = $"{score}";
        }

        GameObject.Find("Audio Source").SetActive(false);

        Save(); 
        Load();

        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        paused = true;
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        paused = false;
    }

    private void Save()
    {
        SaveObject saveObject = new SaveObject
        {
            highScore = score > hiScore ? score : hiScore
        };
        string json = JsonUtility.ToJson(saveObject);
        string path = Application.persistentDataPath + "/Saves/" + "/save.txt";

        SaveSystem.Save(json, path);
    }
    private void Load()
    {
        string path = Application.persistentDataPath + "/Saves/" + "/save.txt";

        string saveString = SaveSystem.Load(path);
        if (saveString != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            hiScore = saveObject.highScore;
        }

        else
        {
            hiScore = 0;
        }
    }

    // Checks if enemy should switch direction
    IEnumerator MovementCheck(float waitTime)
    {
        canCheck = false;

        bool shouldSwitch = false;

        foreach (var row in enemies)
        {

            // Remove nulls (destroyed enemies)
            row.RemoveAll(e => e == null);
            // Skip empty rows
            if (row == null || row.Count == 0)
                continue;

            GameObject leftmost = row[0];
            GameObject rightmost = row[row.Count - 1];

            if (leftmost != null && leftmost.transform.position.x <= -5.2f + 0.05f)
            {
                shouldSwitch = true;
                break;
            }
            if (rightmost != null && rightmost.transform.position.x >= 5.2f - 0.05f)
            {
                shouldSwitch = true;
                break;
            }
        }

        if (shouldSwitch)
        {
            canSwitch = true;
            yield return new WaitForSeconds(waitTime);
            canSwitch = false;

            // Wait until all enemies are away from the edge before allowing another switch
            bool atEdge;
            do
            {
                atEdge = false;
                foreach (var row in enemies)
                {
                    row.RemoveAll(e => e == null);
                    if (row == null || row.Count == 0)
                        continue;

                    GameObject leftmost = row[0];
                    GameObject rightmost = row[row.Count - 1];

                    if ((leftmost != null && leftmost.transform.position.x <= -5.2f + 0.05f) || (rightmost != null && rightmost.transform.position.x >= 5.2f - 0.05f))
                    {
                        atEdge = true;
                        break;
                    }
                }
                if (atEdge)
                    yield return null; // Wait a frame and check again
            } while (atEdge);
        }

        canCheck = true;
    }

    private class SaveObject
    {
        public int highScore;
    }
}