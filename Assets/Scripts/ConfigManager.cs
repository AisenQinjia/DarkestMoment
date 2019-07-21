using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Reflection;
using System;
public enum configType
{
    ROLE,
}
public class ConfigManager
{


    private Dictionary<int, RoleData> roleDict = new Dictionary<int, RoleData>();
    private Dictionary<int, ItemData> itemDict = new Dictionary<int, ItemData>();
    private static ConfigManager instance;
    public static ConfigManager Instance
    {
        get
        {
            if (instance == null)
            {
                return new ConfigManager();
            }
            return instance;
        }
    }

    public static void LoadJson<T>(Dictionary<int, T> dict, string jsonPath) where T : IcfgId
    {

        if (dict.Count > 0)
            return;

        TextAsset textAsset = Resources.Load<TextAsset>("Json/" + jsonPath);

        T[] types = JsonConvert.DeserializeObject<T[]>(textAsset.text);


        for (int i = 0; i < types.Length; i++)
        {
            dict.Add(types[i].cfgId, types[i]);
        }

        Array.Clear(types, 0, types.Length);
        types = null;
    }

    public RoleData GetRoleData(int cfgId)
    {
        if (!this.roleDict.ContainsKey(cfgId))
        {
            return null;
        }
        return this.roleDict[cfgId];
    }

    public ItemData GetItemData(int cfgId)
    {
        if (!this.itemDict.ContainsKey(cfgId))
        {
            return null;
        }
        return this.itemDict[cfgId];
    }



    public ConfigManager()
    {
        instance = this;
        LoadJson<RoleData>(roleDict, "Role");
        LoadJson<ItemData>(itemDict, "Item");

    }
}
