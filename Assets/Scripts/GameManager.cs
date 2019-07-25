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

    public static int level = 1;
    private void Awake()
    {
        instance = this;
    }


    public void ReloadScene(string sceneName)
    {
        AudioManager.Instance.ReleaseSource();
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        AudioManager.Instance.ReleaseSource();
        Time.timeScale = 1;
        if (GameManager.level == 1)
        {
            SceneManager.LoadScene(GameDefine.LevelOne);
        }
        else if (GameManager.level == 2)
        {
            SceneManager.LoadScene(GameDefine.LevelTwo);
        }
        else if (GameManager.level == 3)
        {
            SceneManager.LoadScene(GameDefine.BossLevel);
        }
        AudioManager.Instance.PlayClip(GameDefine.bgm, true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(GameDefine.LevelOne);
        Time.timeScale = 1;
        AudioManager.Instance.PlayClip(GameDefine.bgm, true);
    }

    public void GameOver()
    {
        UIManager.Instance.PopPanel(GameDefine.losePanel);
        Time.timeScale = 0;
        AudioManager.Instance.StopClip(GameDefine.bgm);
    }

    public void GameWin()
    {
        UIManager.Instance.PopPanel(GameDefine.winPanel);
        Time.timeScale = 0;
        AudioManager.Instance.StopClip(GameDefine.bgm);
    }



}
