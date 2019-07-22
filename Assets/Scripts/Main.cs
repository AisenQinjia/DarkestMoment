﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{


    static bool hasCreated = false;

    void Awake()
    {
        if (hasCreated)
            return;

        ConfigManager configMgr = new ConfigManager();

        gameObject.AddComponent<GameManager>();
        gameObject.AddComponent<AudioManager>();
        gameObject.AddComponent<RoleManager>();
        gameObject.AddComponent<ObjectPool>();

        hasCreated = true;
        DontDestroyOnLoad(this.gameObject);

    }

}
