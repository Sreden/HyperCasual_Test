using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private float minimumLoadingTime = 1f;
    private void Start()
    {
        Application.targetFrameRate = 60;
        // Load the Menu Scene instead
        GameManager.Instance.Play();
    }

    public void LoadScene(string sceneName, Action onSceneLoaded = null)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, onSceneLoaded));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName, Action onSceneLoaded = null)
    {
        if (minimumLoadingTime > 0)
        {
            LoadingScreen.SetActive(true);
        }

        // Start a timer to track the minimum load time
        float startTime = Time.time;

        // Unload the current game or menu scene if any
        yield return UnloadCurrentScene();

        // Load the new scene additively
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return asyncLoad;

        // Set the loaded scene as the active scene to apply its lighting settings
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(loadedScene);
        Debug.LogWarning($"Loading Scene {sceneName} Finished");

        // Calculate the elapsed time and wait if the minimum time has not passed
        float elapsedTime = Time.time - startTime;
        if (elapsedTime < minimumLoadingTime)
        {
            yield return new WaitForSeconds(minimumLoadingTime - elapsedTime);
        }

        LoadingScreen.SetActive(false);
        onSceneLoaded?.Invoke();
    }

    private IEnumerator UnloadCurrentScene()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "PersistentScene")
            {
                yield return SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
}
