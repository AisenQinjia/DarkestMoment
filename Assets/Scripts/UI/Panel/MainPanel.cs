using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPanel : BasePanel
{
    public float loadTime = 3f;

    private float timer = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= loadTime)
        {
            SceneManager.LoadScene(GameDefine.StoryOpenScene);
            return;
        }
    }
}
