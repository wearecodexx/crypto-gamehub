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

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private float idleThreshhold;
    private float lastActive;
    private Vector2 lastActivePosition;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        _currentState = currentState;

        CheckIdle();
    }

    private void CheckIdle()
    {
        if (Time.time >= lastActive)
        {
            if ((Vector2)Input.mousePosition == lastActivePosition)
            {
                if (currentState == GameState.ACTIVE)
                {
                    //Pause.
                    Debug.Log("Player Inactive! Pausing...");

                    Pause();
                }
            }


            lastActivePosition = Input.mousePosition;
            lastActive = Time.time + idleThreshhold;
        }
    }

    public void Pause()
    {
        ChangeState(GameState.INACTIVE);

        pausePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        ChangeState(GameState.ACTIVE);

        pausePanel.SetActive(false);
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

    public static void ChangeState(GameState state)
    {
        currentState = state;

        if (currentState == GameState.ACTIVE)
            Cursor.visible = false;
        else
            Cursor.visible = true;

        FireEvent(GameStateChanged);
    }
}

public enum GameState { INACTIVE, ACTIVE, AWAITING}