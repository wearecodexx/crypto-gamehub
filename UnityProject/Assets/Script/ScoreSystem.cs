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
    #endregion;

    public float score { get; private set; }

    public static float HighScore { get { return highScore; } }
    private static float highScore;

    public void IncrementScore(float increment)
    {
        score += increment;

        if (score > HighScore)
            highScore = score;

        FireEvent(ScoreChanged);
    }

    public void DecerementScore(float decrement)
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
        ScoreChanged += UpdateScoreUI;
    }
    private void OnDisable()
    {
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
