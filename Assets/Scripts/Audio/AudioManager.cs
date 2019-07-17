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
        get { return instance; }
    }



    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
        sourceManager = new SourceManager(this.gameObject, 3);
        clipManager = new ClipManager();
    }

    public void PlayClip(string name)
    {
        AudioSource tmpSource = sourceManager.GetFreeSource();

        AudioClip clip = clipManager.GetClip(name);
        if (clip != null)
        {
            tmpSource.clip = clip;
            tmpSource.Play();
        }
    }

    public void StopClip(string name)
    {

    }


}
