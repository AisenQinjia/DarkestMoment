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
            return instance;
        }
    }



    private void Awake()
    {
        instance = this;
        this.playerTransform = null;
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


    public void AddRole()
    {

    }

    public void RemoveRole()
    {

    }
}
