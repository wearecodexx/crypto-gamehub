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

        SceneManager.LoadScene(1);
    }

    public void Gameover()
    {
        ChangeState(GameState.AWAITING);

        panel.SetActive(true);
    }

    public void Retry()
    {
        ChangeState(GameState.ACTIVE);

        InteractableObject.ResetCount();

        SceneManager.LoadScene(1);
    }

    private void ChangeState(GameState state)
    {
        currentState = state;

        FireEvent(GameStateChanged);
    }
}

public enum GameState { INACTIVE, ACTIVE, AWAITING}