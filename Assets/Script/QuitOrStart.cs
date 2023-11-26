using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
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
    public void Unvisible()
    {
        GameObject clickBtn = EventSystem.current.currentSelectedGameObject;
        clickBtn.transform.parent.gameObject.SetActive(false);
        Destroy(clickBtn.transform.parent.gameObject);
    }
}
