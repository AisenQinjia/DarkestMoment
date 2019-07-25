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
        strs = new string[4];
        strs[0] = "非礼啊";
        strs[1] = "老公救我";
        strs[2] = "注意，敌人即将从右侧冲来";
        strs[3] = "吃掉它的心脏";
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
            UIManager.Instance.PopTypewriterSentences(strs, 0.1f, 0.5f, transform);
            gameObject.GetComponent<Collider2D>().enabled = false;
            var boss = GameObject.Find("Boss");
            if (boss == null) Debug.Log("Boss not exist1");
            else boss.GetComponent<BaseRoleController>().Statemanager.PerformTransition(Transition.LostPlayer, other.gameObject, boss);
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
