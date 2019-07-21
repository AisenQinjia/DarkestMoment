using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                instance = obj.AddComponent<GameManager>();
                obj.name = "GameManager";
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }


    public void ReloadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(GameDefine.LevelOne);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        UIManager.Instance.PopPanel(GameDefine.losePanel);
        Time.timeScale = 0;
    }

    public void GameWin()
    {
        UIManager.Instance.PopPanel(GameDefine.winPanel);
        Time.timeScale = 0;
    }



}
