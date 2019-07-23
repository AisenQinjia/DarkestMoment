using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public int sourceCount = 3;

    private SourceManager sourceManager;
    private ClipManager clipManager;

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                instance = obj.AddComponent<AudioManager>();
                obj.name = "AudioManager";
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
        sourceManager = new SourceManager(this.gameObject, 3);
        clipManager = new ClipManager();
    }

    public void PlayClip(string name, bool isLoop = false)
    {
        AudioSource tmpSource = sourceManager.GetFreeSource();

        AudioClip clip = clipManager.GetClip(name);
        if (clip != null)
        {
            tmpSource.clip = clip;
            tmpSource.loop = isLoop;
            tmpSource.Play();
        }
    }

    public bool IsPlayAudio(string name)
    {
        AudioSource tmpSource = sourceManager.GetExistingSource(name);
        if (tmpSource != null && tmpSource.isPlaying)
        {
            return true;
        }
        return false;
    }

    public void StopClip(string name)
    {
        AudioSource tmpSource = sourceManager.GetExistingSource(name);
        if (tmpSource != null)
        {
            tmpSource.Stop();
        }
    }
}
