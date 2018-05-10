using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region Delegates & Events
    public delegate void GameEventHandler();
    public static event GameEventHandler GameStateChanged;
    private static void FireEvent(GameEventHandler _event)
    {
        if (_event != null)
            _event();
    }
    #endregion

    public static GameState currentState { get; set; }
    public GameState _currentState;

    [SerializeField] private GameObject panel;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        _currentState = currentState;
    }

    public void StartGame()
    {
        ChangeState(GameState.ACTIVE);
        Utility.instance.LoadScene("SPAMPIG");
    }

    public void Gameover()
    {
        panel.SetActive(true);

        ChangeState(GameState.AWAITING);
    }

    public void Retry()
    {
        ChangeState(GameState.ACTIVE);

        InteractableObject.ResetCount();

        Utility.instance.LoadScene("SPAMPIG");
    }

    private void ChangeState(GameState state)
    {
        currentState = state;

        if (currentState != GameState.ACTIVE)
            Cursor.visible = true;
        else
            Cursor.visible = false;

        FireEvent(GameStateChanged);
    }
}

public enum GameState { INACTIVE, ACTIVE, AWAITING}