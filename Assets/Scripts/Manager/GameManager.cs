using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int currentScore;
    public float timeLimit;
    public int scoreToWin;
    public int enemySpawnMultiplier;
    public int enemyDamageOverTime;

    public GameObject enemyPrefab;

    private float timeLimitTemp;
    private List<Transform> enemySpawnLocation = new List<Transform>() { };
    private List<bool> enemySpawn = new List<bool>() { false, false, false, false};
    private List<float> scorePercentToSpawnEnemy = new List<float>() { 0.0f, 0.25f, 0.50f, 0.75f};


    public void AddScore(int score, Transform lastGemSpawnerLocation)
    {
        currentScore = currentScore + score;
        UIManager.Instance.score.text = currentScore.ToString();

        GemSpawnerManager.Instance.SpawnGem(lastGemSpawnerLocation);

        CheckWin();
    }

    public void DeductScore()
    {
        currentScore = currentScore - enemyDamageOverTime;
        UIManager.Instance.score.text = currentScore.ToString();

        CheckLose();
    }

    public void CheckWin()
    {
        if (currentScore > scoreToWin)
        {
            //win
            UIManager.Instance.WinGame();
        }
    }

    public void CheckLose()
    {
        if(currentScore < 0)
        {
            //lose
            UIManager.Instance.LoseGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetTimeLimit(float timeLimit)
    {
        PlayerPrefs.SetFloat("timeLimit", timeLimit);
        UIManager.Instance.timeLimitPlaceHolder.text = timeLimit.ToString();
        this.timeLimit = timeLimit;
    }

    public void SetScoreToWin(int scoreToWin)
    {
        PlayerPrefs.SetInt("scoreToWin", scoreToWin);
        UIManager.Instance.ScoreToWinPlaceHolder.text = scoreToWin.ToString();
        this.scoreToWin = scoreToWin;
    }

    public void SetEnemySpawnMultiplier(int enemySpawnMultiplier)
    {
        PlayerPrefs.SetInt("enemySpawnMultiplier", enemySpawnMultiplier);
        UIManager.Instance.EnemySpawnMultiplierPlaceHolder.text = enemySpawnMultiplier.ToString();
        this.enemySpawnMultiplier = enemySpawnMultiplier;
    }

    public void SetEnemyDamageOverTime(int enemyDamageOverTime)
    {
        PlayerPrefs.SetInt("enemyDamageOverTime", enemyDamageOverTime);
        UIManager.Instance.EnemyDamageOverTimePlaceHolder.text = enemyDamageOverTime.ToString();
        this.enemyDamageOverTime = enemyDamageOverTime;
    }

    private void Awake()
    {
        Time.timeScale = 1.0f;
        SetTimeLimit(PlayerPrefs.GetFloat("timeLimit", 60.0f));
        SetScoreToWin(PlayerPrefs.GetInt("scoreToWin", 100));
        SetEnemySpawnMultiplier(PlayerPrefs.GetInt("enemySpawnMultiplier", 1));
        SetEnemyDamageOverTime(PlayerPrefs.GetInt("enemyDamageOverTime", 1));


    }

    private void Start()
    {
        timeLimitTemp = timeLimit;
        enemySpawnLocation.AddRange(GemSpawnerManager.Instance.gemLocations);
        enemySpawnLocation.AddRange(GemSpawnerManager.Instance.gemExtraLocations);
    }

    private void Update()
    {
        if(Time.timeScale == 1)
        {
            timeLimitTemp -= Time.deltaTime;
            int timeTemp = (int)timeLimitTemp;
            UIManager.Instance.timeLimit.text = timeTemp.ToString();
            if (timeLimitTemp < 0.0f)
            {
                UIManager.Instance.LoseGame();
            }

            if(!enemySpawn[0] && (scoreToWin * scorePercentToSpawnEnemy[0]) >= currentScore)
            {
                enemySpawn[0] = true;
                StartCoroutine(spawnEnemyDelay());
            }
            if (!enemySpawn[1] && (scoreToWin * scorePercentToSpawnEnemy[1]) <= currentScore)
            {
                enemySpawn[1] = true;
                StartCoroutine(spawnEnemyDelay());
            }
            if (!enemySpawn[2] && (scoreToWin * scorePercentToSpawnEnemy[2]) <= currentScore)
            {
                enemySpawn[2] = true;
                StartCoroutine(spawnEnemyDelay());
            }
            if (!enemySpawn[3] && (scoreToWin * scorePercentToSpawnEnemy[3]) <= currentScore)
            {
                enemySpawn[3] = true;
                StartCoroutine(spawnEnemyDelay());
            }

        }
        else
        {

        }
    }

    private IEnumerator spawnEnemyDelay()
    {
        yield return new WaitForSeconds(1.0f);
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        for (int i = 0; i < enemySpawnMultiplier; i++)
        {
            Vector3 randomPosition = enemySpawnLocation[Random.Range(0, enemySpawnLocation.Count)].position;
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }

}
