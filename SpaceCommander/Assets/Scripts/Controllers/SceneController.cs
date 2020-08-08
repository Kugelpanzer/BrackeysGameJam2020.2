using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public KeyCode ResetKey = KeyCode.R;
    public  void NextSceneStatic()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public  void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public  void QuitGame()
    {
        Application.Quit();
    }
    public  void ResetGame()
    {
        SceneManager.LoadScene(1);
    }
    public  void GoToScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    public  void ResetLevel()
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
