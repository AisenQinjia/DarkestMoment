using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceManager
{
    private GameObject gameObject;
    private List<AudioSource> audioSources;

    private int maxSourceCount;

    public SourceManager(GameObject go, int count)
    {
        gameObject = go;
        audioSources = new List<AudioSource>();
        maxSourceCount = count;
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < maxSourceCount; i++)
        {
            AudioSource tmpSource = gameObject.AddComponent<AudioSource>();
            audioSources.Add(tmpSource);
        }
    }



    public AudioSource GetFreeSource()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (!audioSources[i].isPlaying)
                return audioSources[i];
        }

        AudioSource tmpSource = gameObject.AddComponent<AudioSource>();
        audioSources.Add(tmpSource);
        return tmpSource;
    }

    public void RealseSource()
    {
        int count = 0;

        List<AudioSource> tmpSources = new List<AudioSource>();

        for (int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i].isPlaying)
            {
                count++;

                if (count >= maxSourceCount)
                {
                    tmpSources.Add(audioSources[i]);
                }
            }
        }

        for (int i = 0; i < tmpSources.Count; i++)
        {
            audioSources.Remove(tmpSources[i]);

            GameObject.Destroy(tmpSources[i]);
        }

        tmpSources.Clear();

        tmpSources = null;
    }
}
