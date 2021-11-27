using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [Header("Time parameters")]
    [SerializeField] float timeToLoadLoading;
    [SerializeField] float timeToLoadMenu;
    int currentSceneIndex;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            StartCoroutine(LoadLoadingScene());
        }

        if (currentSceneIndex == 1)
        {
            StartCoroutine(LoadMenuScene());
        }
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void LoadPlay()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadLoadingScene()
    {
        yield return new WaitForSeconds(timeToLoadLoading);
    }
    IEnumerator LoadMenuScene()
    {
        yield return new WaitForSeconds(timeToLoadMenu);
    }
}
