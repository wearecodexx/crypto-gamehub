using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    private static string QueuedLevel;

    private AsyncOperation result;

    public static void LoadLevel(string name)
    {
        QueuedLevel = name;
        SceneManager.LoadScene(1);
    }

    private void Awake()
    {
        StartCoroutine(LoadLevelAsync());
    }

    private IEnumerator LoadLevelAsync()
    {
        Debug.Log("Loading Level: " + QueuedLevel);

        result = SceneManager.LoadSceneAsync(QueuedLevel);

        result.allowSceneActivation = true;

        yield return null;
    }
}