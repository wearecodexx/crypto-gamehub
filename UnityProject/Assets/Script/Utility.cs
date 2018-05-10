using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utility : MonoBehaviour
{
    public static Utility instance;

    public static float ScreenWidth { get { return instance.screenWidth; } }
    public static float ScreenHeight { get { return instance.screenHeight; } }

    public static Vector2 MousePos { get { return instance.mousePos; } }

    private float screenWidth;
    private float screenHeight;

    private Vector2 mousePos;

    private void Awake()
    {
        instance = this;

        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Screen.width / Screen.height;
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void LoadScene(string name)
    {
        LevelLoader.LoadLevel(name);
    }
}
