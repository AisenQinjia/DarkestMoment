using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{

    public Dictionary<string, List<GameObject>> pool;

    public Dictionary<string, GameObject> prefabs;

    private static ObjectPool instance;

    public static ObjectPool Instance
    {
        get
        {
            return instance;
        }
    }

    private static bool hasCreated = false;

    private void Awake()
    {

        instance = this;

        DontDestroyOnLoad(this.gameObject);

        hasCreated = true;
        pool = new Dictionary<string, List<GameObject>>();
        prefabs = new Dictionary<string, GameObject>();

    }

    public void CreatePool(string poolName, string path, int cnt)
    {
        if (pool.ContainsKey(poolName))
        {
            Debug.Log("已存在该对象池");
            return;
        }

        List<GameObject> newPool = new List<GameObject>();

        GameObject prefab = Resources.Load<GameObject>(path);

        prefabs.Add(poolName, prefab);

        for (int i = 0; i < cnt; i++)
        {
            GameObject temp = Instantiate(prefab);
            // Debug.Log(instance);
            temp.transform.SetParent(instance.transform);
            temp.name = poolName;
            temp.SetActive(false);
            newPool.Add(temp);
        }

        pool.Add(poolName, newPool);
    }

    public GameObject SpawnVfx(string poolName)
    {
        if (!pool.ContainsKey(poolName))
        {
            CreatePool(poolName, GameDefine.VfxPath + poolName, 10);

        }

        List<GameObject> tempPool = pool[poolName];

        GameObject res = null;
        if (tempPool.Count > 0)
        {
            res = tempPool[tempPool.Count - 1];
            res.SetActive(true);
            tempPool.Remove(res);
        }
        else
        {
            res = Instantiate(prefabs[poolName]);
            res.name = poolName;
            res.SetActive(true);
        }
        return res;
    }

    public GameObject SpawnVfx(string poolName, Vector3 pos, Quaternion rotation)
    {
        GameObject tempVfx = SpawnVfx(poolName);
        tempVfx.transform.position = pos;
        tempVfx.transform.rotation = rotation;
        return tempVfx;
    }

    public GameObject SpawnObj(string poolName)
    {
        if (!pool.ContainsKey(poolName))
        {
            Debug.Log("对象池不存在!");
            return null;
        }
        List<GameObject> tempPool = pool[poolName];

        GameObject res = null;
        if (tempPool.Count > 0)
        {
            res = tempPool[tempPool.Count - 1];
            res.SetActive(true);
            tempPool.Remove(res);
        }
        else
        {
            res = Instantiate(prefabs[poolName]);
            res.name = poolName;
            res.SetActive(true);
        }
        return res;
    }

    public GameObject SpawnObj(string poolName, Vector3 pos, Quaternion quaternion)
    {
        GameObject temp = SpawnObj(poolName);
        temp.transform.position = pos;
        temp.transform.rotation = quaternion;
        return temp;
    }

    public void RecycleObject(string poolName, GameObject go)
    {
        if (!pool.ContainsKey(poolName))
        {
            Debug.Log("对象池不存在！请先创建");
            return;
        }
        go.SetActive(false);
        pool[poolName].Add(go);
    }

    private void OnDestroy()
    {
        Debug.Log("destroy ojbect pool");
    }
}
