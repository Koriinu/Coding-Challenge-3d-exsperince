using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI messageText;
    public float timeLimit = 60f;

    int coins;
    int totalCoins;
    float timeLeft;
    bool gameOver;

    void Awake() => Instance = this;

    void Start()
    {
        totalCoins = FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;
        timeLeft = timeLimit;
        messageText.gameObject.SetActive(false);
        UpdateUI();
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            EndGame("TIME'S UP!\nPress R to restart");
        }
        UpdateUI();
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        UpdateUI();
        if (coins >= totalCoins)
            EndGame("YOU WIN!\nPress R to restart");
    }

    void UpdateUI()
    {
        coinText.text = $"Coins: {coins} / {totalCoins}";
        timerText.text = $"Time: {Mathf.CeilToInt(timeLeft)}";
    }

    void EndGame(string msg)
    {
        gameOver = true;
        messageText.text = msg;
        messageText.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}