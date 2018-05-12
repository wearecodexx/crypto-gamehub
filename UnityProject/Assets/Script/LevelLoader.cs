using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    private static string QueuedLevel = "Mainmenu";

    private AsyncOperation result;

    [SerializeField] private Slider loadingBar;

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

        while (!result.isDone)
        {
            if (loadingBar != null)
                loadingBar.value = result.progress;

            yield return null;
        }
    }
}