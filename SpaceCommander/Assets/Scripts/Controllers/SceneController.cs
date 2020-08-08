using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public KeyCode ResetKey = KeyCode.R;
    public static void NextSceneStatic()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public static void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public static void QuitGame()
    {
        Application.Quit();
    }
    public static void ResetGame()
    {
        SceneManager.LoadScene(1);
    }
    public static void GoToScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    public static void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(ResetKey))
        {
            ResetLevel();
        }


    }
}
