using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject PausePanel;
    public GameObject PauseButton;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public TextMeshProUGUI score;
    public TextMeshProUGUI timeLimit;

    public TextMeshProUGUI timeLimitPlaceHolder;
    public TextMeshProUGUI ScoreToWinPlaceHolder;
    public TextMeshProUGUI EnemySpawnMultiplierPlaceHolder;
    public TextMeshProUGUI EnemyDamageOverTimePlaceHolder;

    public TMP_InputField timeLimitText;
    public TMP_InputField ScoreToWinText;
    public TMP_InputField EnemySpawnMultiplierText;
    public TMP_InputField EnemyDamageOverTimeText;

    public void PauseGame() {
        PausePanel.SetActive(true);
        PauseButton.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void WinGame()
    {
        Time.timeScale = 0.0f;
        WinPanel.SetActive(true);
    }

    public void LoseGame()
    {
        Time.timeScale = 0.0f;
        LosePanel.SetActive(true);
    }

    public void RestartGame()
    {
        if (timeLimitText.text != "")
        {
            GameManager.Instance.SetTimeLimit(int.Parse(timeLimitText.text.Trim()));
        }

        if (ScoreToWinText.text != "")
        {
            GameManager.Instance.SetScoreToWin(int.Parse(ScoreToWinText.text.Trim()));
        }

        if (EnemySpawnMultiplierText.text != "")
        {
            GameManager.Instance.SetEnemySpawnMultiplier(int.Parse(EnemySpawnMultiplierText.text.Trim()));
        }

        if (EnemyDamageOverTimeText.text != "")
        {
            GameManager.Instance.SetEnemyDamageOverTime(int.Parse(EnemyDamageOverTimeText.text.Trim()));
        }


        Time.timeScale = 1.0f;
        GameManager.Instance.RestartGame();
    }
}
