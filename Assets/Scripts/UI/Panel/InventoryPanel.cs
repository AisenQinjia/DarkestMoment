using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : BasePanel
{

    public GameObject ItemUI;

    private GameObject layout;

    private Button quitBtn;

    public override void OnPop()
    {
        Time.timeScale = 0;
        ShowItem();
    }


    public override void Awake()
    {
        this.layout = this.transform.Find("layout").gameObject;
        this.quitBtn = this.transform.Find("quitBtn").GetComponent<Button>();

        this.quitBtn.onClick.AddListener(OnQuitBtnClick);
    }

    private void Start()
    {
        ShowItem();
    }

    private void ShowItem()
    {
        if (RoleManager.Instance.playerTransform == null)
            return;
        List<ItemData> items = RoleManager.Instance.playerTransform.GetComponent<PlayerController>().InventoryData.Items;

        foreach (Transform child in this.layout.transform)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("items count " + items.Count);
        for (int i = 0; i < items.Count; i++)
        {
            for (int j = 0; j < items[i].count; j++)
            {
                GameObject temp = Instantiate(this.ItemUI);
                temp.transform.SetParent(this.layout.transform, false);
                temp.GetComponent<ItemUI>().Init(items[i]);
            }
        }

    }

    public override void OnClose()
    {
        Time.timeScale = 1;
    }


    private void OnQuitBtnClick()
    {
        UIManager.Instance.ClosePanel(GameDefine.inventoryPanel);
    }


}
