using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitOrStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToMain()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void Level2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void Level3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
