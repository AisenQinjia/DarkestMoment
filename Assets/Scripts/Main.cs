using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{


    static bool hasCreated = false;

    void Awake()
    {
        if (hasCreated)
            return;

        gameObject.AddComponent<AudioManager>();
        gameObject.AddComponent<RoleManager>();

        hasCreated = true;
        DontDestroyOnLoad(this.gameObject);

    }

}
