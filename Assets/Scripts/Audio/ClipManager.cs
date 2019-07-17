using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipManager
{

    private List<AudioClip> audioClips;

    private string[] clipNames = {
        "hit_npc1",
        "hit_npc2",
        "hit_npc3",
        "attack",
    };

    public ClipManager()
    {
        audioClips = new List<AudioClip>();

        LoadClip();
    }

    public void LoadClip()
    {
        for (int i = 0; i < clipNames.Length; i++)
        {
            AudioClip clip = Resources.Load<AudioClip>(GameDefine.audioPath + clipNames[i]);
            audioClips.Add(clip);

        }
    }

    public AudioClip GetClip(string name)
    {
        for (int i = 0; i < clipNames.Length; i++)
        {
            if (clipNames[i].Equals(name))
            {
    
                return audioClips[i];
            }
        }

        return null;
    }

}
