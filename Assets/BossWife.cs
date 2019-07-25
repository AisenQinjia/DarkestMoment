using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWife : MonoBehaviour
{
    string[] strs;
    bool preparePlayMovie = false;
    public float intervel = 1;
    float timer = 0;
    void Start()
    {
        strs = new string[2];
        strs[0] = "非礼啊";
        strs[1] = "老公救我";
    }

    void Update()
    {
        if (preparePlayMovie)
        {
            timer += Time.deltaTime;
            if(timer > intervel)
            {
                PlayMovie();
                preparePlayMovie = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameDefine.PlayerTag))
        {
            other.gameObject.GetComponent<BaseRoleController>().enabled = false;
            preparePlayMovie = true;
            UIManager.Instance.PopTypewriterSentences(strs, 0.1f, 0.5f, transform);
        }
    }

    void PlayMovie()
    {
        Camera cam = Camera.main;
        if(cam != null)
        {

        }
    }
}
