using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoad : MonoBehaviour
{
    public Text loadingText;
    public Slider loadingSlider;

    AsyncOperation async;
    float loadingProgress;

    // Use this for initialization
    void Start()
    {
        loadingProgress = 0;

        if (SceneManager.GetActiveScene().name == "LoadingScene")
        {
            async = SceneManager.LoadSceneAsync("Main");
            async.allowSceneActivation = false;
        }

    }

    void Update()
    {
        loadingProgress = Mathf.Lerp(loadingProgress, async.progress, Time.deltaTime);

        loadingText.text = ((int)(loadingProgress / 9 * 10 * 100)).ToString() + "%";
        loadingSlider.value = loadingProgress / 9 * 10;

        if (loadingProgress / 9 * 10 > 0.991)
        {
            loadingText.text = 100.ToString() + "%";
            loadingSlider.value = 1;
            async.allowSceneActivation = true;
        }
    }


}
