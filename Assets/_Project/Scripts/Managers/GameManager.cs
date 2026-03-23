using System;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private float timeLimit = 60f;
    [SerializeField] private int maxHealth = 3;

    public float CurrentTime { get; private set; }
    public int CurrentHealth { get; private set; }
    public bool IsGameActive { get; private set; } = true;

    [Header("UI Settings")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI timeText; 
    [SerializeField] private TextMeshProUGUI gameOverText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        CurrentTime = timeLimit;
        CurrentHealth = maxHealth;
    }

    private void Update()
    {
        if (!IsGameActive) return;

        if (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
            UpdateUI();
        }
        else
        {
            GameOver("Süre Bitti!");
        }
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        UpdateUI();
        if (CurrentHealth <= 0) GameOver("Canın Bitti!");
    }

    private void GameOver(string reason)
    {
        IsGameActive = false;

        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = reason;
        }
        Time.timeScale = 0f;

        Debug.Log("Game Over: "+ reason);
    }

    private void UpdateUI()
    {
        if (healthText != null) healthText.text = CurrentHealth.ToString();
        if (timeText != null) timeText.text = Mathf.CeilToInt(CurrentTime).ToString();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelFinished()
    {
        IsGameActive = false;
        Time.timeScale = 0f;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "Kazandın!";
        }
    }
}