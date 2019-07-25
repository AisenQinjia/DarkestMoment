using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipManager
{

    private List<AudioClip> audioClips;

    private string[] clipNames = {
        "playerWalk",
        "playerEat",
        "bgm",
        "playerJump",
        "changeToPower",
        "changeToNormal",
        "changeToFlow",
        "open_door",
        "close_door",
        "stone_roll",
        "wall_break",
        "ceiling_lamp_break",
        "EnemyChase",
        "pipe_delivery",
        "likethelove",
        "endbgm",
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
           // Debug.Log(clipNames[i]);
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
