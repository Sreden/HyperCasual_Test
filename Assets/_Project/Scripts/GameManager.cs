using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject scoreUi;
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private TMP_Text bestScoreText;

    private float currentScore = 0f;
    private float bestScore = 0f;

    protected override void Awake()
    {
        base.Awake();
        LoadBestScore();
    }

    public void Play()
    {
        // Load the main game scene
        SceneController.Instance.LoadScene("MainScene");
        scoreUi.SetActive(true);
        currentScore = 0f;
        UpdateScoreUI();
    }

    public void SetScore(float distance)
    {
        currentScore = distance;
        currentScoreText.text = currentScore.ToString("F2") + "m";

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            bestScoreText.text = "Best: " + bestScore.ToString("F2") + "m";
            SaveBestScore();
        }
    }

    public void LoadBestScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetFloat("BestScore");
        }
        else
        {
            bestScore = 0f;
        }

        bestScoreText.text = "Best: " + bestScore.ToString("F2") + "m";
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetFloat("BestScore", bestScore);
        PlayerPrefs.Save();
    }

    private void UpdateScoreUI()
    {
        currentScoreText.text = currentScore.ToString("F2") + "m";
        bestScoreText.text = "Best: " + bestScore.ToString("F2") + "m";
    }
}