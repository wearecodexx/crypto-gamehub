using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;

    #region Static Delegates & Events.
    public delegate void ScoreEventHandler();
    public static event ScoreEventHandler ScoreChanged;
    public static void FireEvent(ScoreEventHandler _event)
    {
        if (_event != null)
            _event();
    }

    public static void LoadData(Data data)
    {
        highScore = data.highscore;
    }
    #endregion;

    public int score { get; private set; }

    public static int HighScore { get { return highScore; } }
    private static int highScore;

    public void IncrementScore(int increment)
    {
        score += increment;

        if (score > HighScore)
            highScore = score;

        FireEvent(ScoreChanged);
    }

    public void DecerementScore(int decrement)
    {
        if (score - decrement >= 0)
            score -= decrement;
        else
            score = 0;

        FireEvent(ScoreChanged);
    }

    [SerializeField] private Text scoreText;

    private void OnEnable()
    {
        DataManagement.DataLoaded += LoadData;
        ScoreChanged += UpdateScoreUI;
    }
    private void OnDisable()
    {
        DataManagement.DataLoaded -= LoadData;
        ScoreChanged -= UpdateScoreUI;
    }

    private void Awake()
    {
        instance = this;

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }
}
