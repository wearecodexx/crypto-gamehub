using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mainmenu: MonoBehaviour
{
    [SerializeField] private Text highscoreText;

    private void OnEnable()
    {
        DataManagement.DataLoaded += ScoreSystem.LoadData;
    }

    private void OnDisable()
    {
        DataManagement.DataLoaded -= ScoreSystem.LoadData;
    }

    private void Start()
    {
        DataManagement.Load();

        UpdateHighscoreText();
    }

    public void StartGame()
    {
        GameManager.ChangeState(GameState.ACTIVE);
        Utility.instance.LoadScene("SPAMPIG");
    }

    private void UpdateHighscoreText()
    {
        highscoreText.text = "Highscore: " + ScoreSystem.HighScore.ToString();
    }
}
