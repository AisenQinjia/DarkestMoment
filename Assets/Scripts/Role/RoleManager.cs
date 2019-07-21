using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager : MonoBehaviour
{
    public Transform playerTransform;

    private static RoleManager instance;
    public static RoleManager Instance
    {

        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                go.name = "Rolemanager";
                instance = go.AddComponent<RoleManager>();

            }
            return instance;
        }
    }


    private List<Transform> roles = new List<Transform>();

    private void Awake()
    {
        instance = this;
        this.playerTransform = null;

        DontDestroyOnLoad(this.gameObject);
    }


    public void AddPlayer(Transform playerTransform)
    {
        if (this.playerTransform == null)
        {
            this.playerTransform = playerTransform;

        }
        else
        {
            RemovePlayer();
            this.playerTransform = playerTransform;
        }
    }

    public Transform GetPlayer()
    {
        return this.playerTransform;
    }


    public void RemovePlayer()
    {
        this.playerTransform = null;
    }


    public void AddRole(Transform t)
    {
        if (this.roles.Contains(t))
        {
            return;
        }
        this.roles.Add(t);
    }

    public void RemoveRole(Transform t)
    {
        if (this.roles.Contains(t))
        {
            this.roles.Remove(t);
            return;
        }
    }
}
